using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinDataSO", menuName = "Create SkinDataSO")]
public class SkinDataSO : ScriptableObject
{
    public List<SkinData> skinDataList = new();
}
