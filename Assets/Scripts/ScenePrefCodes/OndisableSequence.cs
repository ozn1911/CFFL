using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ScenePrefCodes
{
    public class OndisableSequence : MonoBehaviour
    {
        GameObject gm;
        private void Awake()
        {
            gm = GameObject.FindGameObjectWithTag("System");
        }
        private void OnDisable()
        {
            gm.SendMessage("TaskCompleted_", SendMessageOptions.DontRequireReceiver);
        }
    }
}