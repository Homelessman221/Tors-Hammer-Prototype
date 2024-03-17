using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossStateController : MonoBehaviour
{
    private int _bossState;
    private int _bossPhase;

    [SerializeField] private Transform rightCorner;
    [SerializeField] private Transform leftCorner;

    [SerializeField] private MonoBehaviour[] attackScripts;

    private void Update()
    {
        switch(_bossState)
        {
            case 0:
                //Boss begins and selects first attack
                break;
                case 1:
                //Start up
                //Boss moves to attack start posision, if already there, boss start attack
                break;
                case 2:
                //The attack. Happens in a different script
                break;
                case 3:
                //select next attack in current phase pool
                break;
        }
    }
}
