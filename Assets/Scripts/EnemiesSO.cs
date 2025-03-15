using UnityEngine;
public enum Level{ Level1,Level2,Level3,Level4,Level5}
[CreateAssetMenu(menuName ="ScriptableObjects/EnemyType")]
public class EnemiesSO : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public string nameString;
    public int healthMax;
    public int health;
    public int damage;
    public float speed;
    public Level level;
}
