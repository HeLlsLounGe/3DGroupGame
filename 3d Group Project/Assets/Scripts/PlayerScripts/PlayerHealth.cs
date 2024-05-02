using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    private bool bleed = false;
    private bool funky;
    [Header("Health Bar")]
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public float bleedRate;
    private float bleedTimer;
    public float healRate;
    private float healTimer;
    public Image frontHealthBar;
    public Image backHealthBar;

    [Header("Damage Overlay")]
    public Image overlay;
    public float duration = 3f;
    public float fadeSpeed = 3f;
    public float opacity = 0.7f;

    private float durationTimer;

    PlayerMovementPhysics playerMovement;
    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        bleedTimer = bleedRate;
        healTimer = healRate;
        playerMovement = GetComponent<PlayerMovementPhysics>();
    }


    void Update()
    {

        if (bleed == true && bleedTimer > 0)
        {
            bleedTimer = bleedTimer - Time.deltaTime;
        }
        if (bleed == true && bleedTimer <= 0)
        {
            bleedTimer = bleedRate;
            health--;
        }
        if (bleed == false && healTimer > 0 && health < maxHealth)
        {
            healTimer = healTimer - Time.deltaTime;
        }
        if (bleed == false && healTimer <= 0)
        {
            healTimer = healRate;
            health++;
        }

        health = Mathf.Clamp(health, 0, maxHealth);

        UpdateHealthUI();

        if (overlay.color.a > 0)
        {
            if (health < 30)
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, opacity * 2);

            if (health < 30)
                return;
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }
    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        duration = 0f;
        if (damage > 10)
        { bleed = true;}

        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, opacity);

        if (health <= 0)
        { playerMovement.isDead = true; }
    }
    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
        bleed = false;
    }
}

