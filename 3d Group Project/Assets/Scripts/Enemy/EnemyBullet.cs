using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsPlayer;

    [Header("Stats")]
    public float bounciness;
    public bool useGravity;

    [Header("Damage")]
    public int explosionDamage;
    public float explosionRange;

    [Header("Life time")]
    public int maxCollisions;
    public float maxLifeTime;
    public bool explodeOnTouch = true;

    int collisions;
    PhysicMaterial physics_mat;

    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        if (collisions > maxCollisions) Explode();

        maxLifeTime -= Time.deltaTime;
        if (maxLifeTime <= 0) Explode();
    }

    private void Delay()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisions++;
        if (collision.gameObject.tag == "Player")
        { 
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(explosionDamage);
            Destroy(gameObject);

        }

        if (collision.collider.CompareTag("Player") && explodeOnTouch) Explode();

    }
    private void Explode()
    {
        //if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        //Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemy);
        //for (int i = 0; 1 < enemies.Length; i++)
        //{
        // enemies[i].GetComponent<EnemyHealthBar>().TakeDamage(explosionDamage);
        // }

        Invoke("Delay", 0.05f);
    }
    void Setup()
    {
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;

        GetComponent<SphereCollider>().material = physics_mat;

        rb.useGravity = useGravity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
