using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreeManager : MonoBehaviour
{
	public Region[] regions;
	public TextMeshProUGUI fundsText;
	EconomyManager economyManager;
	public float incomeSpeed;

	void Start()
	{
		regions = FindObjectsOfType(typeof(Region)) as Region[];
		economyManager = gameObject.GetComponent<EconomyManager>();	
		fundsText.text = economyManager.funds + "$";
	}

	void Update()
	{
		foreach(Region region in regions) {
			if (Time.time - region.lastIncomeTime < incomeSpeed) {
				continue;
			}
			region.lastIncomeTime = Time.time;
			// Only player
			if (region.enemyUnits.Count == 0 && region.playerUnits.Count != 0) {
				int sum = 0;
				foreach (var unit in region.playerUnits) {
					sum+= unit.incomePower;
				}
				economyManager.funds += sum;
				fundsText.text = economyManager.funds + "$";
				region.treeCount -= sum;
				//Show popup with funds

			} else if (region.enemyUnits.Count != 0 && region.playerUnits.Count == 0) {
				//GenerateTrees
				int sum = 0;
				foreach (var unit in region.playerUnits) {
					sum+= unit.incomePower;
				}
				region.treeCount += sum;
			}
			region.UpdateForestLevel();
		}
	}
}
