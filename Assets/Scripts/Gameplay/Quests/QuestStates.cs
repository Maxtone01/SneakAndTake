using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStates
{
    Quest quest;
    List<string> completedObjectives = new List<string>();

    [System.Serializable]
    class QuestStatusRecord
    {
        public string questName;
        public List<string> completedObjectives;
    }
    public QuestStates(Quest quest)
    {
        this.quest = quest;
    }

    public QuestStates(object objectState)
    {
        QuestStatusRecord state = objectState as QuestStatusRecord;
        quest = Quest.GetByName(state.questName);
        completedObjectives = state.completedObjectives;
    }

    public Quest GetQuest()
    {
        return quest;
    }

    public int GetCompletedQuests()
    { 
        return completedObjectives.Count;
    }

    public bool IsCompletedQuest(string objective) 
    {
        return completedObjectives.Contains(objective);
    }

    public void CompleteObjective(string objective)
    {
        if (quest.HasObjective(objective))
        {
            if (completedObjectives.Contains(objective))
            {
                return;
            }
            else 
            {
                completedObjectives.Add(objective);
            }
        };
    }
}
