using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ClassSO;

public class ClassTypeHolder : MonoBehaviour
{
    [SerializeField]
    private ClassSO _classType;

    public ClassSO GetClassType()
    {
        return _classType;
    }

    public List<ClassPerks> GetPerks()
    {
        return _classType.classPerks;
    }
}
