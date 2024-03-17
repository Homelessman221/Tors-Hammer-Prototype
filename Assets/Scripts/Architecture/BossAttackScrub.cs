using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossAttack", menuName = "BossAttack")]
public class BossAttackScrub : ScriptableObject
{
    public string attackName;
    public int attackID;
    //0 = no startup, 1 = nearest corner, 2 = corner away from player, 3 = move towards player
    public int StartUpType;
}
