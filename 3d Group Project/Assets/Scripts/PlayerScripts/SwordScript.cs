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
    public Animator animator;

    public Transform attackPoint;

    public GameObject target, katana;

    DashScript dashScript;
    public LayerMask enemyLayers;
    void Awake()
    {
        katana = GameObject.FindGameObjectWithTag("Katana");
        rechargeTimer = recharge;
        leniancyTimer = 0;
        sliceCdTimer = sliceCd;
        deflectsLeft = maxDeflects;
        animator = katana.GetComponent<Animator>();
        dashScript = GetComponentInParent<DashScript>();
        
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
    
        }

        if (Input.GetKeyDown(sliceKey) && !deflectionState && sliceCdTimer <= 0)
        {
           sliceCdTimer = sliceCd;
           Debug.Log("Sword Swing");
           animator.SetTrigger("Slash");
           Invoke(nameof(Slice), sliceDelay); 
        }

        else if (Input.GetKeyDown(sliceKey) && deflectionState && sliceCdTimer <= 0)
        { 
            Invoke(nameof(Deflect), 0f);
            Debug.Log("Deflect");
            animator.SetTrigger("Deflect");
            sliceCdTimer = sliceCd;
        }
    }

    void Slice()
    {
      Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        
       foreach(Collider enemy in hitEnemies) 
        {
            if (dashScript.isDashing == true)
            { 
                enemy.GetComponent<EnemyHealthBar>().TakeDamage(criticalDamage);
            }
            else
            {
                enemy.GetComponent<EnemyHealthBar>().TakeDamage(damage);
            }
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
        if (target.GetComponent<EnemyAttackScript>().bulletState == false)
        { return; }
        target.GetComponent<EnemyAttackScript>().Countered();
    }

    void DownSword()
    {
        deflectionState = false;
        animator.SetTrigger("DownSword");
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
