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
		HitObject(c.gameObject);
        Destroy(gameObject);
		Debug.Log(c);
	}

	void OnTriggerEnter2D(Collider2D c) {

        if (canHit)
        {
            if (this.gameObject.tag == "FireBurnEffect") {

                if (c.gameObject.CompareTag("Enemy") || c.gameObject.CompareTag("Player")) {
                    GameObject effect = Instantiate(burnEffect, this.transform.position + this.transform.up, this.transform.rotation);
                    Destroy(effect, 3f);
                }
            }

            if (this.gameObject.tag == "FrostbiteBeam") {

                if (c.gameObject.CompareTag("Enemy") || c.gameObject.CompareTag("Player")) {
                    ParticleSystem effect = Instantiate(frostBeamEffect, transform.position, transform.rotation);
                    Destroy(effect.gameObject, 5f);
                }
                
                if(c.gameObject.CompareTag("Enemy")) {
                    c.gameObject.GetComponent<EnemyBattleController>().Froze();
                }
            }

            if (c.gameObject.CompareTag("Shield")) {
                Debug.Log(c);
                // Seph: redundant
                //Destroy(this.gameObject);
            }

            HitObject(c.gameObject);
            if (destroyOnHit)
                Destroy(gameObject);
            else // stop further collisions
                canHit = false;
        }

	}

	void HitObject(GameObject g) {
		if (g.CompareTag("Shield")) {
			SoundSystemManager.instance.PlaySFXStandard("Shield Hit-001");
			Debug.Log(g);
		}
		else {
			SoundSystemManager.instance.PlaySFXStandard("Attack Hit");
		}
		HealthController health = g.GetComponentInParent<HealthController>();
		if (health != null) {
			health.TakeDamage(damage);
		}
	}

	void Update() {
		if (this.gameObject.tag == "Shield") {
			Destroy(this.gameObject, 5f);
		}
	}
}
