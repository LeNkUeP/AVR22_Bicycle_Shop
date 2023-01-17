using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.Networking
{

}

namespace UnityEngine
{
    public enum KeyCode
    {
        Space,
    }

    public enum AudioType
    {
        OGGVORBIS,
    }

    public class Material : Object
    {
        public Color color { get; set; }
    }

    public class Renderer : Component
    {
        public Material material { get; set; }
    }

    public struct Color
    {
        internal static Color white;
        internal static Color yellow;
        internal static Color red;
        internal static Color magenta;
        internal static Color grey;
        internal static Color green;
        internal static Color gray;
        internal static Color cyan;
        internal static Color clear;
        internal static Color blue;
        internal static Color black;
    }
    public struct Color32
    {
        public Color32(byte r, byte g, byte b, int v) { }

        public static implicit operator Color32(Color c) => default;
        public static implicit operator Color(Color32 c) => default;
    }

    public class WWW : CustomYieldInstruction
    {
        public WWW(string url) { }

        internal AudioClip GetAudioClip(bool v1, bool v2) => default;

        internal AudioClip GetAudioClip(bool v1, bool v2, AudioType oGGVORBIS) => default;
    }

    public class AudioClip : Object
    {

    }

    public abstract class CustomYieldInstruction : IEnumerator
    {
        public object Current { get; }

        public bool MoveNext() => default;
        public void Reset() { }
    }

    public class AudioSource : AudioBehaviour
    {
        public AudioClip clip { get; internal set; }
        internal bool mute { get; set; }

        internal void Play() { }

        internal void Stop() { }
    }
    public class AudioBehaviour : Behaviour { }

    public class UnityMainThreadDispatcher : MonoBehaviour
    {
        public static UnityMainThreadDispatcher Instance() => default;
        public void Enqueue(Action action) { }
    }
    public class Debug
    {
        internal static void Log(string v) { }

        internal static void LogError(string v) { }
    }

    public class Screen
    {
        internal static int width;
    }

    public struct Rect
    {
        public Rect(int v1, int v2, int v3, int v4)
        {
        }
    }

    public class GUI
    {
        internal static void Box(Rect rect, string v) { }

        internal static bool Button(Rect rect, string v) => default;
    }

    public class Component : Object
    {
        public T GetComponent<T>() => default;
        public GameObject gameObject { get; }

    }

    public class MonoBehaviour : Behaviour
    {
        //public MonoBehaviour(string name) : base(name) { }
        public Transform transform;

    }

    public class Behaviour : Component
    { }

    public class GameObject : Object
    {
        public Transform transform;

        public GameObject(string name) { }

        internal T GetComponent<T>() => default;
    }

    public class Transform
    {
        public Vector3 position;
        public Vector3 localEulerAngles;

        public Transform parent { get; internal set; }

        public void LookAt(Vector3 position) { }

        public void Translate(Vector3 vector3) { }
    }

    public static class Input
    {
        internal static Vector3 mousePosition;

        public static float GetAxis(string v) => default;

        internal static bool GetMouseButton(int mBD_LEFT) => default;

        public static bool GetMouseButtonDown(int mBD_LEFT) => default;

        internal static bool GetKeyDown(object space) => default;
    }

    public class Animator
    {
        public AnimatorStateInfo GetCurrentAnimatorStateInfo(int v) => default;
        public RuntimeAnimatorController runtimeAnimatorController;

        internal void SetBool(string v1, bool v2) { }

        internal void Play(string animation) { }
    }
    public struct AnimatorStateInfo
    {

    }

    public class RuntimeAnimatorController : Object
    {
        public AnimationClip[] animationClips { get; }
    }

    public class AnimationClip : Motion
    {
    }
    public class Motion : Object
    { }

    public class Object
    {
        public string name { get; set; }
    }
}