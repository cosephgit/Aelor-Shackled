using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

    public float damage = 1;

	void OnCollisionEnter2(Collision2D c) {
		HitObject(c.gameObject);
        Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D c) {
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
