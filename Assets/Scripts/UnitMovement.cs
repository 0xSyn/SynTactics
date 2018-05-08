using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Networking;
public class UnitMovement : MonoBehaviour {
    private Rigidbody rb;
    private Animator anim;
    private bool cancelAnim = false;
    private float cancelAnimTimer = 1;
    private int cancelAnimToState;
    public Sprite[] spr;
    public AnimatorController[] anims;
    //AnimationClip
    

    void Start () {
	    rb = GetComponent<Rigidbody>();
	    anim = GetComponentInChildren<Animator>();
        //GetComponentInChildren<SpriteRenderer>().sprite=spr[1];
        //GetComponentInChildren<AnimatorController>().name = anims[1].;
        //anim.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animations/"+ anims[1].name, typeof(RuntimeAnimatorController));
        //GetComponent<UnitData>().GetTeam()
        SetAnimCont(GetComponent<UnitData>().GetTeam());
    }

    public void SetAnimCont(int i) {
        anim.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animations/" + anims[i].name, typeof(RuntimeAnimatorController));
    }

    void Update() {
        CancelAnim();
    }

    void CancelAnim() {
        if (cancelAnim) {
            if ((cancelAnimTimer -= .02f) <= 0) {
                cancelAnim = false;
                cancelAnimTimer = 1;
                anim.SetInteger("State", cancelAnimToState);
            }
        }
    }

    public void Move(Vector3[] nodes) {
        StartCoroutine(SmoothMovement(nodes));
       
    }

    public void AnimAttack() {
        Debug.Log("PLAY ATTCK ANIM");
        anim.SetInteger("State", 4);
        cancelAnimToState = 3;
        cancelAnimTimer = 2;
        cancelAnim = true;
    }

    public void AnimDeath() {
        Debug.Log("PLAY DEATH ANIM");
        anim.SetInteger("State", 5);
        cancelAnimToState = 5;
        cancelAnim = true;
    }


    void PlayAnimation(Vector3 newPos) {
        if (newPos.x > transform.position.x) {//right
            anim.SetInteger("State", 3);
        }
        else if (newPos.x < transform.position.x) {//left
            anim.SetInteger("State", 1);
        }
        else if (newPos.z > transform.position.z) {//up
            anim.SetInteger("State", 2);
        }
        else if (newPos.z < transform.position.z) {//down
            anim.SetInteger("State", 0);
        }
    }

    protected IEnumerator SmoothMovement(Vector3[] nodes) {

        for (int i = 1; i < nodes.Length; i++) {
            PlayAnimation(nodes[i]);
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
