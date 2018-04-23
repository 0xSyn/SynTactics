using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class UnitMovement : MonoBehaviour {
    private Rigidbody rb;
	
	void Start () {
	    rb = GetComponent<Rigidbody>();
	}

    public void Move(Vector3[] nodes) {
        StartCoroutine(SmoothMovement(nodes));
       
    }
    


    protected IEnumerator SmoothMovement(Vector3[] nodes) {

        for (int i = 1; i < nodes.Length; i++) {
            Vector3 end = new Vector3(nodes[i].x, nodes[i].y + 2.5f, nodes[i].z);
            float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            while (sqrRemainingDistance > float.Epsilon) {
                Vector3 newPostion = Vector3.MoveTowards(rb.position, end, 1.3f * Time.deltaTime);
                rb.MovePosition(newPostion);
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;
                yield return null;
            }
            yield return null;
        }
    }
}
