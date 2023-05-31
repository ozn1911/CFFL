using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class Gate : MonoBehaviour
    {
        [SerializeField]
        GameObject gateDoor;
        public event EventHandler GateEntered;
        public static Gate instance;
        private void Awake()
        {
            instance = this;
        }

        public void OpenGate()
        {
            gateDoor.SetActive(false);
        }
        public void CloseGate()
        {
            gateDoor.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag.Equals("Player"))
            {
                GateEntered(this,System.EventArgs.Empty);
            }
        }
    }
}