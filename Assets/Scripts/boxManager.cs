using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
public class boxManager : MonoBehaviour
{
    public float health = 20f;
    public float damageThreshold = 4f;
    public GameObject smokeEffectPrefab;

    bool allowCollision = false;
    MoneyHandler moneyHandler;

    void Awake() {
        moneyHandler = GameObject.Find("MoneyCounter").GetComponent<MoneyHandler>();
        // have to do this, otherwise boxes get destroyed on spawn and you lose money
        Invoke("TriggerBoxCollider", 2.0f);
    }

    void TriggerBoxCollider() {
        allowCollision = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!allowCollision) return;

        float collisionForce = collision.relativeVelocity.magnitude;
        
        if (collisionForce > damageThreshold)
        {
            float damage = collisionForce * 2f; 
            
            health -= damage;

            if (health <= 0)
            {
                moneyHandler.SetMoney(-10);

                if (smokeEffectPrefab)
                {
                    GameObject smokeEffect = Instantiate(smokeEffectPrefab, transform.position, Quaternion.identity);
                    Destroy(smokeEffect, 1f);
                }
            
                Destroy(gameObject);
            }
        }
    }
}
