using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.SceneData;


namespace Assets.Scripts.Main_Menu
{
    public class anim : MonoBehaviour
    {

        public void animEvent()
        {
            Debug.Log("aaaaaaaaaaa");
            //StartCoroutine(LoadScene());
            //SceneManager.LoadScene("Desert",LoadSceneMode.Single);
            SceneLoader.instance.StartGameButton();
        }

        IEnumerator LoadScene()
        {
            // The Application loads the Scene in the background as the current Scene runs.
            // This is particularly good for creating loading screens.
            // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
            // a sceneBuildIndex of 1 as shown in Build Settings.

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                Debug.Log(asyncLoad.progress);
                yield return null;
            }
        }
    }
}