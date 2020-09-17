using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShipBuildingsRenderer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    WarshipTypes _types;
    [SerializeField] Sprite _minerShip;
    [SerializeField] Sprite _default;
    [SerializeField] GameObject visionTime;
    private void Start()
    {
        _types = GameObject.FindGameObjectWithTag("WarshipTypes").GetComponent<WarshipTypes>();
        RenderShip(null, 0);
    }
    public void RenderShip(string type, int time)
    {
        Image typeImage = gameObject.GetComponent<Image>();
        if (type != null)
        {
            visionTime.SetActive(true);
            if (type != "Miner")
            {
                foreach (var item in _types.ShipTypes)
                {
                    if (type == item.WeaponType)
                    {
                        typeImage.sprite = item.Icon;
                    }
                }
            }
            else
            {
                typeImage.sprite = _minerShip;
            }
        }
        else
        {
            typeImage.sprite = _default;
            visionTime.SetActive(false);
        }
        timeText.text = time.ToString();
    }
}
