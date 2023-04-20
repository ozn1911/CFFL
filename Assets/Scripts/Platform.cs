using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "Platform", menuName = "Dat?/Platform")]
    public class Platform : ScriptableObject
    {
        public static Platform instance;
        public PlatformEnum _Platform;

        private void Awake()
        {
            instance = this;
        }
        //public static explicit operator PlatformEnum(Platform plt)
        //{
        //    return Platform.instance._Platform;
        //}


        private void OnEnable()
        {
            instance = this;
        }
    }
    
    public enum PlatformEnum
    {
        Windows,
        Linux,
        Mac,
        Android,
        IOS,
        WebGL
    }
}