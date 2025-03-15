using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Enemy enemy;
    private Vector3 moveDir;
    private Vector3 towerPosition;
    private Tower towerPrefab;
    public static Arrow Create(Tower towerPrefab,Vector3 position, Enemy enemy)
    {
        Transform arrow = Resources.Load<Transform>("Arrow");
        Transform arrowTransform = Instantiate(arrow,position,Quaternion.identity);

        Arrow arrowObject = arrowTransform.GetComponent<Arrow>();
        arrowObject.SetTarget(enemy);
        arrowObject.towerPosition = position;
        arrowObject.towerPrefab = towerPrefab;
        
        return arrowObject;
    }
    private void Update()
    {
        if(enemy != null)
            moveDir = (enemy.transform.position - transform.position).normalized;

        Vector3 direction = (transform.position - towerPosition).normalized;
        transform.eulerAngles = new Vector3(0, 0, GetArrowAngle(direction));
        transform.position += moveDir * 20f * Time.deltaTime;
        StartCoroutine(DestroyArrow());
    }

    public void SetTarget(Enemy enemy)
    {
        this.enemy = enemy;
    }

    private float GetArrowAngle(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;

        return degrees;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.GetComponent<HealthSystem>().Damage(towerPrefab.gameObject.GetComponent<Building>().Damage);

            Destroy(gameObject);
        }
    }
    private IEnumerator DestroyArrow()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }
}
