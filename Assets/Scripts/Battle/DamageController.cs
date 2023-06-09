using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// * Date edited:     7/6/2023 by Seph
 
public class DamageController : MonoBehaviour {

    [SerializeField]private bool destroyOnHit = true;
    public float damage;

	public ParticleSystem frostBeamEffect;
	public GameObject burnEffect;

    private bool canHit = true;

	void OnCollisionEnter2D(Collision2D c) {
        OnTriggerEnter2D(c.collider);
	}

	void OnTriggerEnter2D(Collider2D c) {

        Debug.Log(gameObject + " hit " + c.gameObject);

        if (canHit)
        {
            if (this.gameObject.tag == "FireBurnEffect") {

                if (c.gameObject.CompareTag("Enemy") || c.gameObject.CompareTag("Player")) {
                    if (burnEffect)
                    {
                        GameObject effect = Instantiate(burnEffect, c.transform.position, c.transform.rotation);
                        Destroy(effect, 1f);
                    }
                }
            }

            if (this.gameObject.tag == "FrostbiteBeam") {

                if (c.gameObject.CompareTag("Enemy")) {
                    ParticleSystem effect = Instantiate(frostBeamEffect, transform.position, transform.rotation);
                    Destroy(effect.gameObject, 2f);

                    c.gameObject.GetComponent<EnemyBattleController>().Froze();
                }
            }

            if (gameObject.tag == "Spores")
            {
                if (c.gameObject.CompareTag("Player")) {
                    c.gameObject.GetComponent<PlayerBattleController>().SporeHit();
                }
            }

            HitObject(c.gameObject);
            if (destroyOnHit)
                Destroy(gameObject);
            else if (c.gameObject.CompareTag("Enemy") || c.gameObject.CompareTag("Player")) // only stop further collisions for non-destroyed powers if this was a character hit
            {
                canHit = false;
            }
        }

	}

	void HitObject(GameObject g) {
		if (g.CompareTag("Shield")) {
			SoundSystemManager.instance.PlaySFXStandard("Shield Hit-001");
		}
		else {
            HealthController health = g.GetComponentInParent<HealthController>();

            if (health != null) {
                health.TakeDamage(damage);
			    SoundSystemManager.instance.PlaySFXStandard("Attack Hit");
            }
		}
	}
}
