using UnityEngine;

public interface IState
{
    void EnterState(Enemy enemy,Animator animator);
    void UpdateState(Enemy enemy, Animator animator);
}
