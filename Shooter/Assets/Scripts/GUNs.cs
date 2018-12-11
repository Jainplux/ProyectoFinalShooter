using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "Guns/List", order = 1)]
public class GUNs : ScriptableObject {
    public List<Gunslist> gun;
   
}
[System.Serializable]                                                          
public class Gunslist
{
    public string Gunname = "New Item";
    public int Damage = 10;
    public Material material = null;
    public int Maxammo = 10;
    public GameObject partic;
    public int actualammo = 0;
}