using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class OverseerMapAlingningRaycaster : MonoBehaviour //OMAR for short
{
    /*
    /// <summary>
    /// YOU NEED 3 TAGS FOR SCRIPT TO WORK "Terrain" "OMAR Object" and "SeedSlot"
    /// </summary>
    static Func<float, float> percent = (first) => first/100;
    static float Amount => percent(15);
    static int SEED = 5;


    //[MenuItem("OMAR/Tags")]
    static private void Tags() //Shit doesnt work at all
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        // First check if it is not already present
        bool found = false;
        bool found2 = false;
        bool found3 = false;

        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals("Terrain")) { found = true;}
            if (t.stringValue.Equals("OMAR Object")) { found2 = true;}
            if (t.stringValue.Equals("SeedSlot")) { found3 = true;}
        }

        // if not found, add it
        if (!found)
        {
            tagsProp.InsertArrayElementAtIndex(0);
            SerializedProperty n = tagsProp.GetArrayElementAtIndex(0);
            n.stringValue = "Terrain";
        }
        if (!found2)
        {
            tagsProp.InsertArrayElementAtIndex(0);
            SerializedProperty n = tagsProp.GetArrayElementAtIndex(0);
            n.stringValue = "OMAR Object";
        }
        if (!found3)
        {
            tagsProp.InsertArrayElementAtIndex(0);
            SerializedProperty n = tagsProp.GetArrayElementAtIndex(0);
            n.stringValue = "SeedSlot";
        }
    }
    [MenuItem("OMAR/Create seedlings")]
    static void CreateSeedlings() 
    {
        Func<Vector3, Vector2> toVector2 = vector => new Vector2(vector.x, vector.y);
        int numRand = SEED;
        var goj = GameObject.Find("SeedSlot");
        GameObject summoned =  goj != null ? goj : new GameObject("SeedSlot");

        summoned.tag = "SeedSlot";
        UnityEngine.Random.InitState(numRand);
        GameObject[] objects = GameObject.FindGameObjectsWithTag("OMAR Object");
        GameObject terrain = GameObject.FindWithTag("Terrain");
        if (objects.Length != 0)
        {
            if(terrain != null)
            {
                foreach(GameObject objec in objects)
                {
                    Vector3 scale = objec.transform.localScale;
                    float multipiled = 1 * scale.x * scale.z;
                    multipiled *= Amount;
                    Debug.Log(multipiled);
                    for(int i = 0; i < multipiled; i++)
                    {
                        
                        Vector3 pScale = scale * 5;
                        Vector2 Originpos;
                        Originpos.x = UnityEngine.Random.Range(-pScale.x, pScale.x);
                        //numRand++;
                        Originpos.y = UnityEngine.Random.Range(-pScale.z, pScale.z);
                        //numRand++;
                        //Originpos += new Vector2(objec.transform.position.x, objec.transform.position.z);
                        Vector3 position = new Vector3(Originpos.x,0,Originpos.y) + objec.transform.position;
                        //position += objec.transform.position;
                        RaycastHit hit;
                        if(Physics.Raycast(position, Vector3.down * 50, out hit))
                        {
                            Debug.DrawRay(position, Vector3.down * 100, Color.red, 3);
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



    [MenuItem("OMAR/Delete child")]
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



    [MenuItem("OMAR/Regen")]
    static public void gen()
    {
        DeleteSeedlings();
        CreateSeedlings();
    }
    
   */
}
