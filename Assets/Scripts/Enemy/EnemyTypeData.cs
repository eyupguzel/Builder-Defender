using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/EnemyTypeData")]
public class EnemyTypeData : ScriptableObject
{
    public List<EnemyTypeLevel> EnemyTypeLevelList;
}
