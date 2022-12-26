using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GemDataSO", menuName = "Create GemDateSO")]
public class GemDataSO : ScriptableObject
{
    public List<GemData> gemDataList = new();
}
