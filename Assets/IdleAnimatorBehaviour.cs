using UnityEngine;
using System.Collections;

public class IdleAnimatorBehaviour : StateMachineBehaviour {
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	    var mover = animator.GetComponent<BowMover>();
        mover.MoveOffScreen();
	}
}
