using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Volumen : MonoBehaviour
{
    public GameObject CamaraPrincipal;
    private float totalVolumen = 0f;
    public Slider mySlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        totalVolumen = mySlider.value;
        CamaraPrincipal.GetComponent<AudioSource>().volume = totalVolumen; 
    }
}
