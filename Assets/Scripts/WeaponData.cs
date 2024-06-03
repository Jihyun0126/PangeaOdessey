using UnityEngine;
[CreateAssetMenu(fileName ="Weapon Info", menuName ="Scriptable Object Asset/MonsterStat")]
public class WeaponData : ScriptableObject
{
    public int id;
    /* category
        방패 1
        활 2
        도끼 3
        장막 4
        지팡이 5  */
    public int category;
    public new string name;
    public int price;
    public int damage;
    public int property;
    public Sprite image;
    
}
