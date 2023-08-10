using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Quest")]
public class Quest : ScriptableObject
{
    [SerializeField] public string questName;
    [SerializeField] List<Objective> objectives = new List<Objective>();
    [SerializeField] List<Reward> rewards = new List<Reward>();

    [System.Serializable]
    class Reward
    {
        public int number;
    }

    [System.Serializable]
    public class Objective
    {
        public string reference;
        public string description;
        public int itemToCollect;
        public int itemCollected;
    }

    public string GetTitle()
    {
        return questName;
    }

    public int GetObjectiveCount()
    {
        return objectives.Count;
    }

    public IEnumerable<Objective> GetOjectives()
    {
        return objectives;
    }

    public bool HasObjective(string objectiveRef)
    {
        foreach (var objective in objectives)
        {
            if (objective.reference == objectiveRef)
            {
                return true;
            }
        }
        return false;
    }

    public static Quest GetByName(string questName)
    {
        foreach (Quest quest in Resources.LoadAll<Quest>(""))
        {
            if (quest.name == questName)
            {
                return quest;
            }
        }
        return null;
    }
}
