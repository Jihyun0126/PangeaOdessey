using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    public int id;
    public new string name;
    public int price;
    public int damage;
    public int property;
    public Sprite image;
    
    public WeaponData(int id, string name, int price, int damage, int property, Sprite image)
    {
        this.id = id;
        this.name = name;
        this.price = price;
        this.damage = damage;
        this.property = property;
        this.image = image;
    }
    
}
