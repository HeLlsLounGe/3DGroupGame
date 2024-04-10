using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public bool deflectionState = false;
    private bool hitBoxActive = false;
    public KeyCode dashKey = KeyCode.Mouse1;
    public float  sliceDelay, sliceCd;
    public int damage, criticalDamage;
    private float sliceCdTimer;

    void Start()
    {
        sliceCdTimer = sliceCd;
    }

    // Update is called once per frame
    void Update()
    {
        if (sliceCdTimer > 0)
        {
            sliceCdTimer -= Time.deltaTime;
            return;
        }

        if (Input.GetKeyDown(dashKey) && !deflectionState)
        {
            Debug.Log("Sword Swing");
           Invoke(nameof(Slice), sliceDelay); 
        }

        else if (Input.GetKeyDown(dashKey) && deflectionState)
        { Invoke(nameof(Deflect), 0f); }
    }

    void Slice()
    {
        hitBoxActive = true;
    }
    void Deflect()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && hitBoxActive)
        {
            other.gameObject.GetComponent<EnemyHealthBar>().TakeDamage(damage);
        }
        hitBoxActive = false;
    }
}
