
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDisplay : MonoBehaviour
{
    public Upgrade upgrade;

    public TextMeshProUGUI name;
    public TextMeshProUGUI description;
    public Image icon;


    // Start is called before the first frame update
    void Start()
    {
        name.text = upgrade.upgradeName;
        description.text = upgrade.description;   
        icon.sprite = upgrade.icon;   
    }

   
}
