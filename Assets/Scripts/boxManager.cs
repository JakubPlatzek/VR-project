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
    private void OnCollisionEnter(Collision collision)
    {
        float collisionForce = collision.relativeVelocity.magnitude;
        
        if (collisionForce > damageThreshold)
        {
            float damage = collisionForce * 2f; 
            
            health -= damage;

            if (health <= 0)
            {
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
