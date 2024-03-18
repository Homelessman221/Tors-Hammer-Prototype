using UnityEngine;

public class BossStateController : MonoBehaviour
{
    [SerializeField] private IntVariable _bossState;
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
    [SerializeField]private float _jumpTimeCurrent;

    [SerializeField] private GameObjectVariable Player;

    [SerializeField] private IntVariable facingRight;

    private MonoBehaviour currentAttackScript;
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
                currentAttack = currentAttackScrub.attackID;
                StartUpState();
                break;
                case 1:
                //Start up
                //Boss moves to attack start posision, if already there, boss start attack
                if(currentAttackScrub.StartUpType == 1 || currentAttackScrub.StartUpType == 2)
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
                attackScripts[currentAttackScrub.attackID].enabled = false;
                print("attack end");
                break;

                
        }
        if (facingRight.Value == 0)
        {
            gameObject.transform.localScale = new Vector3(1,1,1);
        }
        if (facingRight.Value == 1)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
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

            if(transform.position.x >= 0)
            {
                if(transform.position.x <= 3.5f)
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
            if(Player.Value.transform.position.x > 0)
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
    private void SelectAttack()
    {
        //script matching current attack iD starts
        print("current attack is" + currentAttack);
        currentAttackScript = attackScripts[currentAttack];
        currentAttackScript.enabled = true;
        print("attacking");
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

    private void NextAttack()
    {
        //print("next attack");
        currentAttackScript.enabled = false;
        if (randomAttackMode)
        {
            if (_bossPhase ==1)
            {
                int rand = Mathf.FloorToInt(Random.Range(0, phase1Attacks.Length));

                if(rand != currentAttack && phase1Attacks.Length > 1)
                {
                    currentAttack = phase1Attacks[rand].attackID;
                    currentAttackScrub = phase1Attacks[rand];
                    _bossState.Value = 1;
                    StartUpState();
                }
                else
                {
                    _bossState.Value = 1;
                    StartUpState();
                }
            }
            if (_bossPhase == 2)
            {
                int rand = Mathf.FloorToInt(Random.Range(0, phase2Attacks.Length));

                if (rand != currentAttack && phase2Attacks.Length > 1)
                {
                    currentAttack = phase2Attacks[rand].attackID;
                    currentAttackScrub = phase1Attacks[rand];
                }
            }
            if (_bossPhase == 3)
            {
                int rand = Mathf.FloorToInt(Random.Range(0, phase3Attacks.Length));

                if (rand != currentAttack && phase3Attacks.Length > 1)
                {
                    currentAttack = phase3Attacks[rand].attackID;
                    currentAttackScrub = phase1Attacks[rand];
                }
            }
        }
        else
        {
            if (_bossPhase == 1)
            {
                currentAttack += 1;
                print("current attack is" + currentAttack);
                if (currentAttack >= phase1Attacks.Length)
                {
                    
                    currentAttack = 0;
                    print("current attack is reset");
                }
                if(currentAttack < phase1Attacks.Length)
                {
                    currentAttackScrub = phase1Attacks[currentAttack];
                    currentAttackScript = attackScripts[currentAttackScrub.attackID];
                    _bossState.Value = 1;
                    StartUpState();
                    print("next attack");
                }
                
            }
            if (_bossPhase == 2)
            {
                currentAttack += 1;
                if (currentAttack > phase2Attacks.Length)
                {
                    currentAttack = 0;
                }
            }
            if (_bossPhase == 3)
            {
                if (currentAttack > phase3Attacks.Length)
                {
                    currentAttack = 0;
                }
            }
        }
    }
}
