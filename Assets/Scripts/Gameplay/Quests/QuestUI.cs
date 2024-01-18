using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] QuestItemUi questPrefab;
    QuestList questList;
    [SerializeField] TextMeshProUGUI questTitle;

    public void Start()
    {
        questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
        questList.onQuestUpdated += RedrawQuest;
        RedrawQuest();
    }

    private void RedrawQuest()
    {
        foreach (Transform uiElement in this.transform)
        {
            Destroy(uiElement.gameObject);
        }

        QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();

        foreach (QuestStates state in questList.GetQuests())
        {
            QuestItemUi uiInstance = Instantiate(questPrefab, transform);
            uiInstance.Setup(state);
        }
    }
}
