using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public  class Upgrade : ScriptableObject
{
    public string upgradeName;
    public string upgradeType;
    public string description;
    public int level;
    public Sprite icon;


}
