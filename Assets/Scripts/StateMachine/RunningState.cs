using UnityEngine;

public class RunningState : IState
{
    public void EnterState(Enemy enemy, Animator animator)
    {
        animator.SetTrigger("Running");
    }
    public void UpdateState(Enemy enemy, Animator animator)
    {
        if (enemy.IsDead)
        {
            enemy.ChangeState(new DyingState());
            animator.SetTrigger("Dying");
        }
        if (enemy.IsSlashing)
        {
            enemy.ChangeState(new SlashingState());
            animator.SetTrigger("Slashing");
        }
    }
}
