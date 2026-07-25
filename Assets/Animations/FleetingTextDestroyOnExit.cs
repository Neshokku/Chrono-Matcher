using UnityEngine;

public class DestroyFleetingTextOnStateExit : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject textParent = animator.gameObject.transform.parent.gameObject;
        animator.gameObject.transform.parent = null;
        Destroy(textParent);
        Destroy(animator.gameObject);
    }
}