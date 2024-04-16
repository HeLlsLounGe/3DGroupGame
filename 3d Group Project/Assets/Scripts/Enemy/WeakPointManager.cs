using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointManager : MonoBehaviour
{
    [SerializeField] int WeakPointValue;

    public EnemyHealthBar healthScript;
    public SpriteRenderer SR;

    [SerializeField] Collider collider1;
    [SerializeField] Collider collider2;


    private void Awake()
    {
        SR = GetComponentInChildren<SpriteRenderer>();
        collider1.enabled = false;
        collider2.enabled = false;
        SR.enabled = false;
        healthScript = GetComponentInParent<EnemyHealthBar>();
        WeakPointAssign();
       
    }
    public void WeakPointAssign()
    {
        if (WeakPointValue == healthScript.randomPoint)
        {
           SR.enabled = true;
           collider1.enabled = true;
           collider2.enabled = true;
        }
        else
        {
            collider1.enabled = false;
            collider2.enabled = false;
            SR.enabled = false;
        }
    }
    public void KillFunction()
    {
       Destroy(gameObject);
    }
}
