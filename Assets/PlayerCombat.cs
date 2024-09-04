using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    float AttackTimer;
    public float AttackCooldown = 0.3f;
    public float lightDamage = 15;
    public float heavyDamage = 40;

    enum CombatStates 
    {
        lightAtck, heavyAtck, Defend //Use the enums so that when u use one state u cant invoke the others IM DEAD
    }

    bool lightAtck = false;
    bool heavyAtck = false;
    bool Defend = false;

    public GameObject Enemy; //Enemy trqbva da ima funkciq TakeDmg(float damage)
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        AttackTimer = 0;
    }

    IEnumerator DealDamageHeavy(GameObject enemy, float damage) 
    {
        yield return new WaitForSeconds(2);//2 sec delay for the animation to play out
        DealDamage(Enemy, heavyDamage);
    }
    
    // Update is called once per frame
    private void FixedUpdate()
    {
    }

    private void Update()
    {
        if (AttackTimer > 0)
            AttackTimer -= Time.deltaTime;
    }

    private void MyInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            lightAtck = true;
        else if (Input.GetKeyUp(KeyCode.Mouse0))
            lightAtck = false;

        if (Input.GetKeyDown(KeyCode.Mouse1))
            heavyAtck = true;
        else if (Input.GetKeyUp(KeyCode.Mouse1))
            heavyAtck = false;

        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            Defend = true;
            this.gameObject.tag = "Invulnerable";
        }
        else if (Input.GetKeyUp(KeyCode.CapsLock))
        {
            Defend = false;
            this.gameObject.tag = "Player";
        }
    }
    private void LightAtck()
    {
        if (Enemy != null)
        {
            /*Check if weapon collider hits && AttackTimer <= 0*/
            /*if ()
            {
                // Deal damage to the enemy
                DealDamage(Enemy, lightDamage);
            }*/
            if (AttackTimer <= 0)
                DealDamage(Enemy, lightDamage);

        }
    }

    private void HeavyAtck()
    {
        if (Enemy != null)
        {
            /*Check if weapon collider hits && AttackTimer <= 0*/
            /*if ()
            {
                // Deal damage to the enemy
                DealDamage(Enemy, heavyDamage);
            }*/
            if (AttackTimer <= 0)
                StartCoroutine(DealDamageHeavy(Enemy, heavyDamage));

        }
    }

    void DealDamage(GameObject target, float damage)
    {
        if (Enemy != null)
        {
            target.GetComponent<EnemyHolder>().TakeDamage(damage);
            AttackTimer = AttackCooldown;
        }
    }
}
