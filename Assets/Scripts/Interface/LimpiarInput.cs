using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimpiarInput : MonoBehaviour
{
 
    [SerializeField] private float Campo; 
    [SerializeField] private Button LimpiarButton;

     void OnEnable()
    {
        LimpiarButton.onClick.AddListener(delegate { LimpiarCampo(); });
       
    }

    void LimpiarCampo(){
        GameObject.Find("url" + Campo).GetComponent<InputField>().text = ""; 
    }
}
