using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class OMAR : MonoBehaviour
{

    public OMAR()
    {
        _seed = 5;
        perc = 15;
    }
    public OMAR(int seed, float percentage)
    {
        _seed = seed;
        perc = percentage;
    }
    public OMAR(int seed)
    {
        _seed = seed;
    }
    float perc = 15;
    Func<float, float> percent = (first) => first / 100;
    float Amount => percent(perc);
    int _seed = 5;

    private void Awake()
    {
        gen();
        SeedSlots = GameObject.FindGameObjectsWithTag("SeedSlot");
    }
    [SerializeField]
    public GameObject[] SeedSlots;
    private void Start()
    {
        //gen();
        //SeedSlots = GameObject.FindGameObjectsWithTag("SeedSlot");
    }

    void CreateSeedlings(int seed)
    {
        Func<Vector3, Vector2> toVector2 = vector => new Vector2(vector.x, vector.y);
        int numRand = seed;
        var goj = GameObject.Find("SeedSlot");
        GameObject summoned = goj != null ? goj : new GameObject("SeedSlot");

        summoned.tag = "SeedSlot";
        UnityEngine.Random.InitState(numRand);
        GameObject[] objects = GameObject.FindGameObjectsWithTag("OMAR Object");
        GameObject terrain = GameObject.FindWithTag("Terrain");
        if (objects.Length != 0)
        {
            if (terrain != null)
            {
                foreach (GameObject objec in objects)
                {
                    Vector3 scale = objec.transform.localScale;
                    float multipiled = 1 * scale.x * scale.z;
                    multipiled *= Amount;
                    Debug.Log(multipiled);
                    for (int i = 0; i < multipiled; i++)
                    {

                        Vector3 pScale = scale * 5;
                        Vector2 Originpos;
                        Originpos.x = UnityEngine.Random.Range(-pScale.x, pScale.x);
                        //numRand++;
                        Originpos.y = UnityEngine.Random.Range(-pScale.z, pScale.z);
                        //numRand++;
                        //Originpos += new Vector2(objec.transform.position.x, objec.transform.position.z);
                        Vector3 position = new Vector3(Originpos.x, 0, Originpos.y) + objec.transform.position;
                        //position += objec.transform.position;
                        RaycastHit hit;
                        if (Physics.Raycast(position, Vector3.down * 50, out hit))
                        {
                            Debug.DrawRay(position, Vector3.down * 100, Color.red, 300);
                            Debug.DrawRay(position, hit.point, Color.white, 3);
                            GameObject.Instantiate<GameObject>(summoned, hit.point, Quaternion.Euler(0, 0, 0), objec.transform);
                            //Debug.Log(hit.point);
                        }
                        else
                        {
                            Debug.LogWarning("Nothing under " + position);
                        }
                        //Debug.Log( position + Vector3.down);
                        //Debug.Log(hit.point);

                        //GameObject.Instantiate<GameObject>(summoned, hit.point,Quaternion.Euler(0,0,0),objec.transform);
                    }
                }
            }
            else
            {
                Debug.LogError("ERROR: No object with tag (Terrain) found");
            }
        }
        else
        {
            Debug.LogError("ERROR: No object with tag (OMAR Object)(yes really, i thought that name would be ok) found");
        }
    }




    static void DeleteSeedlings()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("OMAR Object");
        foreach (GameObject objec in objects)
        {
            int count = objec.gameObject.transform.childCount;

            for (int i = 0; i < count; i++)
            {
                GameObject.DestroyImmediate(objec.transform.GetChild(0).gameObject);
            }
        }

    }

    public void gen()
    {
        DeleteSeedlings();
        CreateSeedlings(_seed);
    }


}
