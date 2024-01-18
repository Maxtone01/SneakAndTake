using Assets.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Resources.ClassSO;

public class ClassElementsProvider : MonoBehaviour
{
    [SerializeField]
    private ClassSO _classType;

    public string GetClassDescription()
    {
        return _classType.classDescription;
    }

    public string GetClassName()
    {
        return _classType.className;
    }

    public string GetClassNameEng()
    {
        return _classType.engClassName;
    }

    public string GetPerkName()
    {
        foreach (ClassPerks perkName in _classType.classPerks)
        {
            return perkName.perkName;
        }
        return null;
    }

    public Sprite GetPerkImage()
    {
        foreach (ClassPerks perkImage in _classType.classPerks)
        {
            return perkImage.perkImage;
        }
        return null;
    }

    public string GetPerkDescription()
    {
        foreach (ClassPerks perkDescription in _classType.classPerks)
        {
            return perkDescription.perkDescription;
        }
        return null;
    }
}
