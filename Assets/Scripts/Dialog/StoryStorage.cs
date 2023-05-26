using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Ink.Runtime;

namespace Assets.Scripts.Dialog
{
    [CreateAssetMenu(fileName = "StoryStorage", menuName = "Dat?/StoryStorage")]
    public class StoryStorage : ScriptableObject
    {
        public List<TextAsset> DialogJsons;
        public List<Story> DialogStory = new List<Story>();

        private void OnEnable()
        {
            StoryListInitialize(DialogJsons, DialogStory);
        }

        public void StoryListInitialize(List<TextAsset> Jsons, List<Story> Storylist)
        {
            Storylist.Clear();
            foreach (TextAsset str in Jsons)
            {
                Storylist.Add(new Story(str.text));
            }
        }
    }
}