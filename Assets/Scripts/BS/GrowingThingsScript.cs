using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingThingsScript : MonoBehaviour
{
    
    public Transform canvas;
    TextMesh texter;
    public bool IsRipe;

    // Start is called before the first frame update
    void Start()
    {
        texter = gameObject.GetComponentInChildren<TextMesh>();
        canvas = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
