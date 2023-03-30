using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "Platform", menuName = "Dat?/Platform")]
    public class Platform : ScriptableObject
    {
        public PlatformEnum _Platform;
        public static explicit operator PlatformEnum(Platform plt)
        {
            return plt._Platform;
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