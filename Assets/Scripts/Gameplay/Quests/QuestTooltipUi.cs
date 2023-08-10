using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestTooltipUi : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] Transform objectiveContainer;
    [SerializeField] GameObject objectivePrefab;
    [SerializeField] GameObject objectiveIncompletedPrefab;
    [SerializeField] TextMeshProUGUI objectivesCounter;
    public void Setup(QuestStates state)
    {
        Quest quest = state.GetQuest();

        ClearPrefab();

        titleText.text = quest.GetTitle();

        foreach (Quest.Objective item in quest.GetOjectives())
        {
            GameObject prefab = objectiveIncompletedPrefab;

            if (state.IsCompletedQuest(item.reference))
            {
                prefab = objectivePrefab;
            }
            GameObject objInstance = Instantiate(prefab, objectiveContainer);
            TextMeshProUGUI [] objText = objInstance.GetComponentsInChildren<TextMeshProUGUI>();
            objText[0].text = item.description;
            if (item.itemToCollect > 0)
            {
                objText[1].text = item.itemCollected.ToString() + " / " + item.itemToCollect.ToString();
            }
            else
            {
                objText[1].text = null;
            }
        }
    }

    private void ClearPrefab()
    {
        foreach (Transform containerChild in objectiveContainer.transform)
        {
            Destroy(containerChild.gameObject);
        }
    }
}
