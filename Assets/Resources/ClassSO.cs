using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Class Perk")]
public class ClassSO : ScriptableObject
{
    public string className;
    public string classDescription;
    public List<ClassPerks> classPerks = new();

    [System.Serializable]
    public class ClassPerks
    {
        public Sprite perkImage;
        public string perkName;
        public string perkDescription;
    }
}
