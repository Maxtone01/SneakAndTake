using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteQuest : MonoBehaviour
{
    private QuestList questList;

    public void Start()
    {
        questList = GetComponent<QuestList>();
    }

    public void CollectItem()
    {
        Quest quest = null;

        foreach (QuestStates questState in questList.GetQuests())
        {
            quest = questState.GetQuest();
        }
        foreach (Quest.Objective qObjective in quest.GetOjectives())
        {
            if (qObjective.reference == "1")
            {
                if (qObjective.itemToCollect > qObjective.itemCollected)
                {
                    qObjective.itemCollected++;
                }
                if (qObjective.itemCollected >= qObjective.itemToCollect)
                {
                    CompleteObjective(quest, "1");
                }
            }
        }
    }

    public void StoreItem()
    {
        Quest quest = null;

        foreach (QuestStates questState in questList.GetQuests())
        {
            quest = questState.GetQuest();
        }
        foreach (Quest.Objective qObjective in quest.GetOjectives())
        {
            if (qObjective.reference == "2")
            {
                CompleteObjective(quest, "2");
            }
        }
    }

    public void CompleteObjective(Quest quest, string objective)
    {
        QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
        questList.CompleteObjective(quest, objective);
    }
}
