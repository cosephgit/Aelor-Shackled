using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

    public float damage;

	public ParticleSystem frostBeamEffect;
	public GameObject burnEffect;

	void OnCollisionEnter2(Collision2D c) {
		HitObject(c.gameObject);
        Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D c) {
		if (this.gameObject.tag == "FireBurnEffect") {
			GameObject effect = Instantiate(burnEffect, this.transform.position + this.transform.up, this.transform.rotation);
			Destroy(effect.gameObject, 3f);
		}

		if (this.gameObject.tag == "FrostbiteBeam") {
			ParticleSystem effect = Instantiate(frostBeamEffect, transform.position, transform.rotation);
			Destroy(effect.gameObject, 5f);
		}

		HitObject(c.gameObject);
        Destroy(gameObject);
	}

	void HitObject(GameObject g) {
		HealthController health = g.GetComponentInParent<HealthController>();
		if (health != null) {
			health.TakeDamage(damage);
		}
	}
}
