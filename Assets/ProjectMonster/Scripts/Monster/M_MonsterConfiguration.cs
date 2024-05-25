using UnityEngine;

[CreateAssetMenu(fileName = "Monster Configuration", menuName = "Object Configuration/Monster", order = 1)]
public class M_MonsterConfiguration : ScriptableObject
{
    [SerializeField] public MonsterId id = MonsterId.NONE;

    // monster information such as name and cost
    [SerializeField] public M_MonsterInformation information = null;

    // for prefab to be initialize
    [SerializeField] public M_BaseMonster monsterModel = null;

}
