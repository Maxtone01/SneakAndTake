using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteQuestOnItemCollect : MonoBehaviour
{
    private QuestList questList;
    private QuestCompletion qCompl;

    public void Start()
    {
        qCompl = GetComponent<QuestCompletion>();
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
                    qCompl.CompleteObjective(quest, "1");
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
                qCompl.CompleteObjective(quest, "2");
            }
        }
    }
}
