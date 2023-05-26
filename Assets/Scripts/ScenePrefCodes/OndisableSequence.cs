using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ScenePrefCodes
{
    public class OndisableSequence : MonoBehaviour
    {

        private void OnDisable()
        {
            SendMessageUpwards("TaskCompleted", SendMessageOptions.DontRequireReceiver);
        }
    }
}