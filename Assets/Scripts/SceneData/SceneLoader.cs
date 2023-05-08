using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Scripts.SceneData
{
    public class SceneLoader : MonoBehaviour
    {
#nullable enable
        public static SceneLoader? instance;

        SceneDataObject data => SceneDataObject.instance;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
        

        public void StartGameButton()
        {
            SceneManager.LoadScene(data.GetCurrentSceneBuild());
        }

        public void NextScene()
        {
            SceneManager.LoadScene(data.GetNextSceneBuild());
        }


    }
}