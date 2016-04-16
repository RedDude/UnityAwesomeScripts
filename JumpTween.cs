using UnityEngine;
using System.Collections;

public class JumpTween : MonoBehaviour {
	[SerializeField] public float jumpTimeFactor = 0.16f; // Jump time factor
	[SerializeField] public float jumpHeightFactor = 0.4f;

	string callback; // Callback method to execute if present
	
	Vector3 sizeHeight; // Used in height calculations so the player lands on feet
	
	// Must be nested inside a jump origin during jumps so the character has a relative point to tween upon
	public GameObject jumpOriginPrefab;
	GameObject jumpOrigin;
	
	void Awake () {
		sizeHeight = new Vector3(0, GetComponent<Collider2D>().bounds.size.y, 0);
		
		// Generate jump origin outside of our object and store it somewhere safe (dynamically attached)
		jumpOrigin = Instantiate(jumpOriginPrefab) as GameObject;
	}
	
	void ClearJumpOrigin () {
		// Remove the jump origin container
		transform.parent = null;
		if (callback != null) SendMessage(callback, null, SendMessageOptions.DontRequireReceiver);
		GetComponent<Rigidbody2D>().isKinematic = false;
	}

	/**
	 * @jumpTarget Position to land at
	 */
	public void JumpTo (Vector3 jumpTarget, string newCallback = null) {
		// Disable physics temporarily
		GetComponent<Rigidbody2D>().isKinematic = true;

		// Set the callback in a string so we can fire it via sendmessage
		callback = newCallback;

		// Move the jumpOrigin to our current location
		jumpOrigin.transform.position = transform.position;
		transform.parent = jumpOrigin.transform;
		
		// @TODO Time and height are relative to jump distance
		float jumpDistance = Vector3.Distance(transform.position, jumpTarget);
		float jumpTime = jumpDistance * jumpTimeFactor;
		float jumpHeight = jumpDistance * jumpHeightFactor;
		
		// @TODO Height should probably also be relative to jump distance
		// Run the up and down tween on our object
		iTween.MoveBy(gameObject, iTween.Hash(
			"y", jumpHeight,
			"time", jumpTime / 2,
			"easeType", iTween.EaseType.easeOutQuad
		));
		
		iTween.MoveBy(gameObject, iTween.Hash(
			"y", -jumpHeight,
			"time", jumpTime / 2,
			"delay", jumpTime / 2,
			"easeType", iTween.EaseType.easeInCubic,
			"onComplete", "ClearJumpOrigin"
		));
		
		// Horizontal move animation
		iTween.MoveTo(jumpOrigin, iTween.Hash(
			"position", jumpTarget + sizeHeight / 2, 
			"time", jumpTime, 
			"easeType", iTween.EaseType.linear
		));
	}
}
