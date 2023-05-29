using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

    public float damage;

	public ParticleSystem frostBeamEffect;

	void OnCollisionEnter2(Collision2D c) {
		HitObject(c.gameObject);
        Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D c) {
		if (this.gameObject.tag == "FrostbiteBeam") {
			ParticleSystem newfrostBeamEffect = Instantiate(frostBeamEffect, transform.position, transform.rotation);
			Destroy(newfrostBeamEffect, 5f);
		}
		HitObject(c.gameObject);
        Destroy(gameObject);
	}

	void HitObject(GameObject g) {
		HealthController health = g.GetComponent<HealthController>();
		if (health != null) {
			health.TakeDamage(damage);
		}
	}
}
