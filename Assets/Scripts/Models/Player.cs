using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public float MovementSpeed;
    public GameObject PlayerMovePoint;
    private Transform pmr;
    private bool moving;
    private bool triggeringPMR;
    private bool triggeringEnemy;
    private Animation anim;

    //attack
    private bool attacked;
    private GameObject enemyTarget;
    public float AttackTimer;
    private bool followingEnemy;
    private float currentAttackTimer;

    // Start is called before the first frame update
    void Start()
    {
        pmr = Instantiate(PlayerMovePoint.transform, transform.position, Quaternion.identity);
        pmr.GetComponent<BoxCollider>().enabled = false;
        anim = GetComponent<Animation>();
        currentAttackTimer = AttackTimer;
        _minDamage = 15;
        _maxDamage = 25;
    }

    // Update is called once per frame
    void Update()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        float hitDistance = 0.0f;

        if (playerPlane.Raycast(ray, out hitDistance))
        {
            Vector3 mousePosition = ray.GetPoint(hitDistance);

            if (Input.GetMouseButtonDown(0))
            {
                moving = true;
                triggeringPMR = false;
                pmr.transform.position = mousePosition;
                pmr.GetComponent<BoxCollider>().enabled = true;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Enemy")
                    {
                        enemyTarget = hit.collider.gameObject;
                        followingEnemy = true;
                    }
                    else
                    {
                        followingEnemy = false;
                        enemyTarget = null;
                        attacked = true;
                    }
                }
            }
        }
        if (moving)
            Move();
        else
        {
            Idle();
        }

        if (triggeringPMR)
        {
            moving = false;
        }

        if (triggeringEnemy && enemyTarget != null)
        {
            Attack();
        }

        if (attacked && enemyTarget != null)
        {
            currentAttackTimer -= 1 * Time.deltaTime;
        }
        else
        {
            currentAttackTimer = AttackTimer;
        }

        if (currentAttackTimer <= 0)
        {
            currentAttackTimer = AttackTimer;
            attacked = false;
        }
    }

    void Move()
    {
        if (followingEnemy)
        {
            if (!triggeringEnemy)
            {
                transform.position = Vector3.MoveTowards(transform.position, enemyTarget.transform.position, MovementSpeed);
                transform.LookAt(enemyTarget.transform);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pmr.transform.position, MovementSpeed);
            transform.LookAt(pmr.transform);
        }
        anim.CrossFade("walk");
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "PMR")
        {
            triggeringPMR = true;
        }

        if (collider.tag == "Enemy")
        {
            triggeringEnemy = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "PMR")
        {
            triggeringPMR = false;
        }

        if (collider.tag == "Enemy")
        {
            triggeringEnemy = false;
        }
    }

    void Idle()
    {
        anim.CrossFade("idle");
    }

    public void Attack()
    {
        if (!attacked)
        {
            _damage = Random.Range(_minDamage, _maxDamage);
            enemyTarget.GetComponent<Health>().ModifyHealth(-_damage);
            attacked = true;
        }

        if (enemyTarget)
        {
            transform.LookAt(enemyTarget.transform);
            enemyTarget.GetComponent<Enemy>().Aggro = true;
        }

        anim.CrossFade("attack");
    }
}
