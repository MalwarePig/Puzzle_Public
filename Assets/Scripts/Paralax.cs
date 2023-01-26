using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
      public float Nubes;

    void Start()
    {
          Nubes = GameObject.Find("Nubes").transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
          //this.transform.Translate(speed * Time.deltaTime * Vector3.forward);
      this.transform.Translate(1 * Time.deltaTime * Vector3.right);
    }
}
