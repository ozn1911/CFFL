using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTheRock : MonoBehaviour
{
    private static readonly Vector3 zeroOne = new Vector3(0.1f,0.1f,0.1f);
    public Collider hitten;
    Transform hittenChild;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray,out hit, 50 , 0, QueryTriggerInteraction.Collide);

        Debug.Log(hit.point);
        hittenChild = hit.transform.GetChild(0);
        
        

    }

    private void FixedUpdate()
    {
        Vector3 scale = hittenChild.localScale;
        if (scale.x < Vector3.one.x)
        {
            scale += zeroOne;
        }
        hittenChild.localScale = scale;
    }
}
