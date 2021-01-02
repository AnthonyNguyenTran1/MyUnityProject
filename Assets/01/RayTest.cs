using UnityEngine;
using System.Collections;

public class RayTest : MonoBehaviour {
	public float distance = 6.0f;
	bool cast = false;
	
	void Update () {
		if (cast) return;
		RaycastHit[] hitsForward = Physics.RaycastAll(transform.position, 
		                                              new Vector3(0,0,1), distance);
		Debug.Log ("Hits in forward direction: " + hitsForward.Length.ToString ());
		RaycastHit[] hitsBackward = Physics.RaycastAll(new Vector3 (transform.position.x, transform.position.y, transform.position.z + distance), 
		                                               new Vector3(0,0,-1), distance);
		Debug.Log ("Hits in backward direction: " + hitsBackward.Length.ToString ());
		foreach (RaycastHit hitf in hitsForward) {
			foreach (RaycastHit hitb in hitsBackward) {
				if (hitf.collider == hitb.collider) {
					Debug.Log ("Found Object : " + hitf.collider.gameObject.name
					           + ", Entrance point : " + hitf.point.ToString ()
					           + ", Exit point : " + hitb.point.ToString ()
					           + ", Object thickness: " + (hitb.point.z - hitf.point.z).ToString());
				}
			}
		}
		cast = true;
	}
}
