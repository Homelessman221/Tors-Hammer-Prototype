using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBossStateController : MonoBehaviour
{
    [SerializeField] private IntVariable _bossState;

    [SerializeField] private float[] testList;

    [SerializeField] private BossAttackScrub[] firstPhaseAttacks;
    [SerializeField] private MonoBehaviour[] attackScriptList;

    private int lastAttack;

    private int _bossPhase = 1;

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
    [SerializeField] private float _jumpTimeCurrent;

    [SerializeField] private GameObjectVariable Player;

    [SerializeField] private IntVariable facingRight;

    private MonoBehaviour currentAttackScript;
    private void Start()
    {
        _bossState.Value = 0;
    }
    private void Update()
    {
        switch (_bossState.Value)
        {
            case 0:
                NextAttackScrub();
                _bossState.Value = 1;
                break; 
                case 1:
                if (currentAttackScrub.StartUpType == 1 || currentAttackScrub.StartUpType == 2)
                {
                    JumpToCorner();
                }
                break;
        }

        if (facingRight.Value == 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        if (facingRight.Value == 1)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void NextAttackScrub()
    {
        if(currentAttackScript != null)
        {
            currentAttackScript.enabled = false;
        }        
        lastAttack++;
        if(lastAttack >= firstPhaseAttacks.Length)
        {
            lastAttack = 0;
        }
        _bossState.Value = 1;
        currentAttackScrub = firstPhaseAttacks[lastAttack];
        StartUpState();
        print("current attack is" + firstPhaseAttacks[lastAttack]);
    }

    private void StartUpState()
    {
        print("start up state");
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

            if (transform.position.x >= 0)
            {
                if (transform.position.x <= 3.5f)
                {
                    _jumpTarget = rightCorner.position;
                    facingRight.Value = 0;
                }
                else
                {
                    _bossState.Value = 2;
                    SelectAttack();
                    facingRight.Value = 0;
                }

            }
            if (transform.position.x < 0)
            {
                if (transform.position.x >= -3.5f)
                {
                    _jumpTarget = leftCorner.position;
                    facingRight.Value = 1;
                }
                else
                {
                    _bossState.Value = 2;
                    SelectAttack();
                    facingRight.Value = 1;
                }

            }
        }
        if (currentAttackScrub.StartUpType == 2)
        {
            _jumpOrigin = transform.position;

            _jumpTimeCurrent = _jumpTime;
            _bossState.Value = 1;
            if (Player.Value.transform.position.x > 0)
            {
                _jumpTarget = leftCorner.position;
                facingRight.Value = 1;
            }
            if (Player.Value.transform.position.x < 0)
            {
                _jumpTarget = rightCorner.position;
                facingRight.Value = 0;
            }
        }
    }
    private void JumpToCorner()
    {
        if (_jumpTimeCurrent <= 0f)
        {
            _bossState.Value = 2;
            SelectAttack();
        }
        else
        {
            _jumpTimeCurrent -= Time.deltaTime;
            float t = 1f - _jumpTimeCurrent / _jumpTime;

            transform.position = Vector3.Lerp(_jumpOrigin, _jumpTarget, t) + Vector3.up * (Mathf.Pow(t, 2) * -4 + t * 4) * _jumpHeight;
        }
    }

    private void SelectAttack()
    {
        currentAttackScript = attackScriptList[currentAttackScrub.attackID];
        currentAttackScript.enabled = true;
        _bossState.Value = 3;
    }

}
