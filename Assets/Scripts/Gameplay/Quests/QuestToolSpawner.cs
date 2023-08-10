using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestToolSpawner : ToolSpawner
{
    public override bool CanCreateTool()
    {
        return true;
    }

    public override void UpdateTool(GameObject tool)
    {
        QuestStates state = GetComponent<QuestItemUi>().GetQuestStates();
        tooltip.GetComponent<QuestTooltipUi>().Setup(state);
    }
}
