using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointManager : MonoBehaviour
{
    [SerializeField] int WeakPointValue;

    public EnemyHealthBar healthScript;
    public MeshRenderer MR;

    [SerializeField] Collider collider1;
    [SerializeField] Collider collider2;


    private void Awake()
    {
        MR= GetComponent<MeshRenderer>();
        collider1.enabled = false;
        collider2.enabled = false;
        MR.enabled = false;
        healthScript = GetComponentInParent<EnemyHealthBar>();
       
    }
    private void Update()
    {
        if (WeakPointValue == healthScript.randomPoint)
        {
           MR.enabled = true;
           collider1.enabled = true;
           collider2.enabled = true;
        }
    }
}
