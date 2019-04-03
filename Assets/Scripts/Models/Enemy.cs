using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private bool triggeringPlayer;
    public bool Aggro;
    private bool attacked;

    public float AttackTimer;
    private float _attackTimer;
    private GameObject player;
    public float MovementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        _attackTimer = AttackTimer;
        _minDamage = 5;
        _maxDamage = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Aggro)
        {
            FollowPlayer();
        }       
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            triggeringPlayer = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            triggeringPlayer = false;
        }
    }

    public void Attack()
    {
        if (!attacked)
        {
            _damage = Random.Range(_minDamage, _maxDamage);
            player.GetComponent<Health>().ModifyHealth(-_damage);
            attacked = true;
        }
    }

    public void FollowPlayer()
    {
        if (!triggeringPlayer)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, MovementSpeed);
        }

        if(_attackTimer <= 0)
        {
            attacked = false;
            _attackTimer = AttackTimer;
        }

        if (attacked)
        {
            _attackTimer -= 1 * Time.deltaTime;
        }

        Attack();
    }
}
