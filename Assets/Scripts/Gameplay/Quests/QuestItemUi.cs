using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestItemUi : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI progress;
    QuestStates state;

    public void Setup(QuestStates state)
    {
        this.state = state;
        title.text = state.GetQuest().GetTitle();
        Debug.Log(state.GetQuest().questName);
        //title.text = state.GetQuest().name;
        progress.text = state.GetCompletedQuests() + " / " + state.GetQuest().GetObjectiveCount();
    }

    public QuestStates GetQuestStates()
    {
        return state;
    }
}
