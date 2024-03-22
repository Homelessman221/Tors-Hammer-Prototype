using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetIntToUi : MonoBehaviour
{
    [SerializeField] private IntVariable intVar;
    [SerializeField] private TextMeshProUGUI textThing;

    private void Update()
    {
        textThing.text = intVar.Value.ToString();
    }
}
