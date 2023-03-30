using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GrowThings : MonoBehaviour
{
    [SerializeField]
    int seed;
    GameObject[] things;
    [SerializeField]
    float growRate;

    [SerializeField]
    GameObject Bush;
    [SerializeField]
    GameObject Rock;
    GameObject[] seedlings;
    // Start is called before the first frame update
    void Start()
    {
        
        things = GameObject.FindGameObjectsWithTag("SeedSlot");
        Random.InitState(seed);
        ResourceCreate();
        seedlings = GameObject.FindGameObjectsWithTag("Growable");
        ResourceArrange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ResourceCreate()
    {
        foreach (GameObject objec in things)
        {
            if(objec.transform.childCount == 0)
            {
                if(Random.value <0.5f)
                {
                    GameObject.Instantiate(Bush,objec.transform);
                }
                else
                {
                    GameObject.Instantiate(Rock,objec.transform);
                }
            }
            else
            {}
        }
    }

    void ResourceArrange()
    {
        foreach(GameObject obj in seedlings)
        {
            obj.transform.localPosition = Vector3.zero;
        }
    }

    void ResourceGrow()
    {
        Vector3 zeroOne = Vector3.one / 10;
        foreach(GameObject obj in seedlings)
        {
            var scale = obj.transform.localScale;
            if (scale.x < zeroOne.x)
            {
                scale += zeroOne / 10;
            }
            else
            {
                GrowingThingsScript script;
                if (obj.TryGetComponent<GrowingThingsScript>(out script))
                {
                    script.IsRipe = true;
                }
            }
        }
    }

    
}
