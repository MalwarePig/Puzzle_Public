using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxNube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         this.transform.Translate((-1 * Time.deltaTime * Vector3.right)/8);
    }
}
