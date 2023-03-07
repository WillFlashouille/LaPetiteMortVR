﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnceAppeared : StateMachineBehaviour {

	private bool done = false;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.SetFloat ("AnimationStarter", 0f);
		done = false;
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if( animator.GetFloat("AnimationStarter") > 0.1f && stateInfo.normalizedTime >= 0.15f && !done) {
			animator.GetComponent<Poussiere> ().FadeOut(0.6f);
			done = true;
		}
	}
	
	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
//	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
//		animator.SetFloat ("AnimationStarter", 0f);
//	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
