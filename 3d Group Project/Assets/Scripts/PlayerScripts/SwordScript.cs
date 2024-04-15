using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public bool deflectionState = false;
    bool flipper = false;
    public KeyCode sliceKey = KeyCode.Mouse1;
    public float  sliceDelay, sliceCd, attackRange, leniancy, leniancyTimer, recharge;
    public int damage, criticalDamage, maxDeflects;
    int deflectsLeft;
    private float sliceCdTimer, rechargeTimer;

    public Transform attackPoint;

    public GameObject target;

    public LayerMask enemyLayers;
    void Awake()
    {
        rechargeTimer = recharge;
        leniancyTimer = 0;
        sliceCdTimer = sliceCd;
        deflectsLeft = maxDeflects;
    }

    // Update is called once per frame
    void Update()
    {
        if (rechargeTimer > 0)
        {
            rechargeTimer -= Time.deltaTime;
        }
        else if (rechargeTimer <= 0 && deflectsLeft < maxDeflects)
        {
           deflectsLeft++;
        }
        if (leniancyTimer > 0)
        {
           leniancyTimer -= Time.deltaTime;
        }
        else if (leniancyTimer <= 0 && !flipper)
        {
           DownSword();
        }
        if (sliceCdTimer > 0)
        {
            sliceCdTimer -= Time.deltaTime;
            return;
        }

        if (Input.GetKeyDown(sliceKey) && !deflectionState)
        {
           sliceCdTimer = sliceCd;
           Debug.Log("Sword Swing");
           Invoke(nameof(Slice), sliceDelay); 
        }

        else if (Input.GetKeyDown(sliceKey) && deflectionState)
        { 
            Invoke(nameof(Deflect), 0f);
            Debug.Log("Deflect");
            sliceCdTimer = sliceCd;
        }
    }

    void Slice()
    {
      Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        
       foreach(Collider enemy in hitEnemies) 
        {
            enemy.GetComponent<EnemyHealthBar>().TakeDamage(damage);
        } 
    }
    void Deflect()
    {
        
        if (deflectsLeft<= 0)
        { return; }
        leniancyTimer = leniancy;
        flipper = false;
        rechargeTimer = recharge;
        deflectsLeft--;
        Debug.Log("Deflects left" + deflectsLeft);
        target.GetComponent<EnemyAttackScript>().Countered();
    }

    void DownSword()
    {
        deflectionState = false;
    }
    public void DeflectState()
    {
        flipper = true;
        deflectionState = true;
        
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) { return; }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
