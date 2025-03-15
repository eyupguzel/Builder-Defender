using UnityEngine;

public class SlashingState : IState
{
    public void EnterState(Enemy enemy, Animator animator)
    {
        animator.SetTrigger("Slashing");
    }
    public void UpdateState(Enemy enemy, Animator animator)
    {
        if (enemy.IsDead)
        {
            enemy.ChangeState(new DyingState());
            animator.SetTrigger("Dying");
        }
        else if (enemy.IsMoving)
        {
            enemy.ChangeState(new RunningState());
            animator.SetTrigger("Running");
        }
    }
}
