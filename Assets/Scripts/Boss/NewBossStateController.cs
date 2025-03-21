using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBossStateController : MonoBehaviour
{
    [SerializeField] private IntVariable _bossState;

    [SerializeField] private float[] testList;

    [SerializeField] private BossAttackScrub[] firstPhaseAttacks;
    [SerializeField] private BossAttackScrub[] secondPhaseAttacks;
    [SerializeField] private BossAttackScrub[] thirdPhaseAttacks;
    [SerializeField] private BossAttackScrub[] fourthPhaseAttacks;
    [SerializeField] private MonoBehaviour[] attackScriptList;

    [SerializeField] private int secondPhaseThreshold;
    [SerializeField] private int thirdPhaseThreshold;
    [SerializeField] private int fourthPhaseThreshold;

    


    private int lastAttack;

    [SerializeField]private IntVariable currentBossPhase;
    [SerializeField] private IntVariable bossHealth;
    [SerializeField] private int totalBossPhases;

    [SerializeField] private Transform rightCorner;
    [SerializeField] private Transform leftCorner;

    [SerializeField] private bool randomAttackMode;
    private int currentAttack;
    private BossAttackScrub currentAttackScrub;

    [SerializeField] private float _jumpTime, _jumpHeight;

    private Vector3 _jumpOrigin, _jumpTarget;
    [SerializeField] private float _jumpTimeCurrent;

    [SerializeField] private GameObjectVariable Player;

    [SerializeField] private IntVariable facingRight;

    private MonoBehaviour currentAttackScript;

    [SerializeField] private GameObject visualGameObject;
    private void Start()
    {
        _bossState.Value = 0;
        lastAttack = -1;
        currentBossPhase.Value = 1;
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
            visualGameObject.transform.localScale = new Vector3(0.2387f, 0.2387f, 0.2387f);
        }
        if (facingRight.Value == 1)
        {
            visualGameObject.transform.localScale = new Vector3(-0.2387f, 0.2387f, 0.2387f);
        }
        nextPhase();
    }

    private void NextAttackScrub()
    {
        if(currentAttackScript != null)
        {
            currentAttackScript.enabled = false;
        }        
        lastAttack++;

        if(currentBossPhase.Value == 1)
        {
            if (lastAttack >= firstPhaseAttacks.Length)
            {
                lastAttack = 0;
            }
            _bossState.Value = 1;
            currentAttackScrub = firstPhaseAttacks[lastAttack];
        }
        if (currentBossPhase.Value == 2)
        {
            if (lastAttack >= secondPhaseAttacks.Length)
            {
                lastAttack = 0;
            }
            _bossState.Value = 1;
            currentAttackScrub = secondPhaseAttacks[lastAttack];
        }
        if (currentBossPhase.Value == 3)
        {
            if (lastAttack >= thirdPhaseAttacks.Length)
            {
                lastAttack = 0;
            }
            _bossState.Value = 1;
            currentAttackScrub = thirdPhaseAttacks[lastAttack];
        }
        if (currentBossPhase.Value == 4)
        {
            if (lastAttack >= fourthPhaseAttacks.Length)
            {
                lastAttack = 0;
            }
            _bossState.Value = 1;
            currentAttackScrub = fourthPhaseAttacks[lastAttack];
        }

        StartUpState();
        //print("current attack is" + firstPhaseAttacks[lastAttack]);
    }

    private void StartUpState()
    {
        //print("start up state");
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
        if(_jumpTarget == leftCorner.position && transform.position.x <= -6.1f)
        {
            _bossState.Value = 2;
            SelectAttack();
        }
        else if(_jumpTarget == rightCorner.position && transform.position.x >= 6.1f)
        {
            _bossState.Value = 2;
            SelectAttack();
        }
        else
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
        /*
        if(currentAttackScrub.StartUpType == 1)
        {
            if(transform.position.x < 0)
            {
                if(transform.position.x <= 3.5f)
                {
                    _bossState.Value = 2;
                    SelectAttack();
                }
            }
            else if (transform.position.x > 0)

            {
                if(transform.position.x <= 3.5f)
                {
                    _bossState.Value = 2;
                    SelectAttack();
                }
            }
            else
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
        }
        */


    }

    private void SelectAttack()
    {
        currentAttackScript = attackScriptList[currentAttackScrub.attackID];
        currentAttackScript.enabled = true;
        _bossState.Value = 3;
    }

    private void nextPhase()
    {
        if(bossHealth.Value <= secondPhaseThreshold && secondPhaseThreshold != 0)
        {
            currentBossPhase.Value = 2;
        }
        if (bossHealth.Value <= thirdPhaseThreshold && thirdPhaseThreshold != 0)
        {
            currentBossPhase.Value = 3;
        }
        if (bossHealth.Value <= fourthPhaseThreshold && fourthPhaseThreshold != 0)
        {
            currentBossPhase.Value = 4;
        }
    }

}
