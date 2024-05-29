using UnityEngine;
[CreateAssetMenu(fileName ="Weapon Info", menuName ="Scriptable Object Asset/MonsterStat")]
public class WeaponData : ScriptableObject
{
    public int id;
    public new string name;
    public int price;
    public int damage;
    public int property;
    public Sprite image;
    
}
