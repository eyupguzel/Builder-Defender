using UnityEngine;

public class Tower : MonoBehaviour
{
    private float targetMaxRadius = 15;

    private float timer;
    private float maxTimer = 0.2f;

    private float timerShooting;
    private float maxTimerShooting = .25f;

    private Enemy enemy;
    private Enemy targetTransform;

    private Vector3 arrowSpawnPoint;

    private void Awake()
    {
        arrowSpawnPoint = transform.Find("arrowSpawnPoint").position;
    }

    private void Update()
    {
        HandleTarget();
        HandleShooting();
    }
    private void HandleShooting()
    {
        if (targetTransform != null)
        {
            timerShooting -= Time.deltaTime;
            if (timerShooting < 0)
            {
                Arrow.Create(this,arrowSpawnPoint, targetTransform);
                timerShooting += maxTimerShooting;
            }
        }
            
    }
    private void HandleTarget()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer += maxTimer;
            LookForTargets();
        }

    }
    private void LookForTargets()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider in collider2DArray)
        {
            enemy = collider.gameObject.GetComponent<Enemy>();
            
            if (enemy != null)
            {
                // Is a enemy!
                if (targetTransform == null)
                    targetTransform = enemy;
                else
                    if(Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position,enemy.transform.position))
                    targetTransform = enemy;
            }
        }
    }
}
