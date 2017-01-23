using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CarSurvialCouterUI : MonoBehaviour {
    public Text text;

    // Update is called once per frame
    void Update()
    {
        UpdateText(VehicleFactory.instance.VehiclesSurvived);

    }

    void UpdateText(int count)
    {
        text.text = string.Format("{0} Narrowly Escaped!", count);
    }
 }
