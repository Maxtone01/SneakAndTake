using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources
{
    [CreateAssetMenu(menuName = "New Class Perk")]
    public class ClassSO : ScriptableObject
    {
        public string engClassName;
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
}