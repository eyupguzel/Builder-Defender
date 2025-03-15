using UnityEngine;

public class DyingState : IState
{
    public void EnterState(Enemy enemy, Animator animator)
    {
        animator.SetTrigger("Dying");
    }
    public void UpdateState(Enemy enemy, Animator animator)
    {
        // :/
    }
}
