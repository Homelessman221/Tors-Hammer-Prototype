using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BossStateController : MonoBehaviour
{
    [SerializeField] private IntVariable _bossState;
    private int _bossPhase;

    [SerializeField] private Transform rightCorner;
    [SerializeField] private Transform leftCorner;

    [SerializeField] private MonoBehaviour[] attackScripts;

    [SerializeField] private BossAttackScrub[] openingAttacks;
    [SerializeField] private BossAttackScrub[] phase1Attacks;
    [SerializeField] private BossAttackScrub[] phase2Attacks;
    [SerializeField] private BossAttackScrub[] phase3Attacks;

    [SerializeField] private bool randomAttackMode;
    private int currentAttack;
    private BossAttackScrub currentAttackScrub;

    [SerializeField] private float _jumpTime, _jumpHeight;

    private Vector3 _jumpOrigin, _jumpTarget;
    [SerializeField]private float _jumpTimeCurrent;
    private void Start()
    {
        _bossState.Value = 0;
    }
    private void Update()
    {
        switch(_bossState.Value)
        {
            case 0:
                //Boss begins and selects first attack
                //currentAttack = attackID of randomly selected attack
                int rand = Mathf.FloorToInt(Random.Range(0,openingAttacks.Length));
                currentAttack = openingAttacks[rand].attackID;
                currentAttackScrub = openingAttacks[rand];
                StartUp();
                break;
                case 1:
                //Start up
                //Boss moves to attack start posision, if already there, boss start attack
                if(currentAttackScrub.StartUpType == 1)
                {
                   JumpToCorner();
                }
                break;
                case 2:
                //The attack. Happens in a different script

                break;
                case 3:
                //select next attack in current phase pool
                NextAttack();
                break;
        }
    } private void StartUp()
    {
        if (currentAttackScrub.StartUpType == 0)
        {
            _bossState.Value = 2;
            SelectAttack();
        }
        if (currentAttackScrub.StartUpType == 1)
        {
            _jumpOrigin = transform.position;

            _jumpTimeCurrent = _jumpTime;
            _bossState.Value = 1;

            if(transform.position.x >= 0)
            {
                _jumpTarget = rightCorner.position;
            }
            if (transform.position.x < 0)
            {
                _jumpTarget = leftCorner.position;
            }
        }
    }
    private void SelectAttack()
    {
        //script matching current attack iD starts
        attackScripts[currentAttack].enabled = true;
    }
    private void JumpToCorner()
    {
        if (_jumpTimeCurrent <= 0f)
            return;

        _jumpTimeCurrent -= Time.deltaTime;
        float t = 1f - _jumpTimeCurrent / _jumpTime;

        transform.position = Vector3.Lerp(_jumpOrigin, _jumpTarget, t) + Vector3.up * (Mathf.Pow(t, 2) * -4 + t * 4) * _jumpHeight;
    }

    private void NextAttack()
    {
        if (randomAttackMode)
        {
            if (_bossPhase ==1)
            {
                int rand = Mathf.FloorToInt(Random.Range(0, openingAttacks.Length));

                if(rand != currentAttack && phase1Attacks.Length > 1)
                {
                    currentAttack = phase1Attacks[rand].attackID;
                    currentAttackScrub = phase1Attacks[rand];
                }
            }
            if (_bossPhase == 2)
            {

            }
            if (_bossPhase == 3)
            {

            }
        }
        else
        {
            if (_bossPhase == 1)
            {
                currentAttack += 1;
                if(currentAttack > phase1Attacks.Length)
                {
                    currentAttack = 1;
                }
            }
            if (_bossPhase == 2)
            {
                currentAttack += 1;
                if (currentAttack > phase2Attacks.Length)
                {
                    currentAttack = 1;
                }
            }
            if (_bossPhase == 3)
            {
                if (currentAttack > phase3Attacks.Length)
                {
                    currentAttack = 1;
                }
            }
        }
    }
}
