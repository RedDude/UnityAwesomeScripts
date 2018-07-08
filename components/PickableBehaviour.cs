using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableBehaviour : MonoBehaviour
{
	[Header("References")]
	public GameObject itemVisuals;
	public GameObject collectedParticleSystem;
	public CircleCollider2D itemCollider2D;

	private float durationOfCollectedParticleSystem;


	void Start()
	{
		durationOfCollectedParticleSystem = collectedParticleSystem.GetComponent<ParticleSystem>().main.duration;
	}

	void OnTriggerEnter2D(Collider2D theCollider)
	{
		if (theCollider.CompareTag ("Player")) {
			ItemCollected ();
		}
	}

	void ItemCollected()
	{
		itemCollider2D.enabled = false;
		itemVisuals.SetActive (false);
		collectedParticleSystem.SetActive (true);
		Invoke ("DeactivateitemGameObject", durationOfCollectedParticleSystem);

	}

	void DeactivateItemGameObject()
	{
		gameObject.SetActive (false);
	}
}
