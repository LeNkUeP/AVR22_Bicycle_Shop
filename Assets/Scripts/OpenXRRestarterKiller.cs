using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;

// This class finds and kills a coroutine that throws errors inside the editor every 5 seconds if no headset is connected
// The coroutine was introduced in the OpenXR Plugin [1.5.1] - 2022-08-11
// There absolutely has to be a better way, and this code should NOT be maintained incase the issue is resolved
public class OpenXRRestarterKiller : MonoBehaviour
{
    private static bool isHookedIntoUpdate = false;
    private static object restarterInstance = null;

    private static Type restarterType = null;
    private static FieldInfo singletonInstanceField = null;
    private static FieldInfo restarterCoroutine = null;
    private static MethodInfo stopMethod = null;

    [InitializeOnLoadMethod]
    [ExecuteInEditMode]
    private static void Init()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange change)
    {
        switch (change)
        {
            case PlayModeStateChange.EnteredPlayMode:
                GatherReflectionData();
                SetHooked(true);
                break;

            case PlayModeStateChange.ExitingPlayMode:
                SetHooked(false);
                break;
        }
    }

    private static void GatherReflectionData()
    {
        restarterType = Type.GetType("UnityEngine.XR.OpenXR.OpenXRRestarter, Unity.XR.OpenXR, Version=0.0.0.0, Culture=neutral, PublicKeyToken=nul", true);
        singletonInstanceField = restarterType.GetField("s_Instance", BindingFlags.NonPublic | BindingFlags.Static);
        restarterCoroutine = restarterType.GetField("m_pauseAndRestartCoroutine", BindingFlags.NonPublic | BindingFlags.Instance);
        stopMethod = typeof(MonoBehaviour).GetMethod("StopCoroutine", new Type[] { typeof(Coroutine) });
    }

    // Enables/Disables looking for the restarter coroutine
    private static void SetHooked(bool isHooked)
    {
        if (isHookedIntoUpdate == isHooked)
            return; // Already have the desired state

        isHookedIntoUpdate = isHooked;
        if (isHooked)
        {
            EditorApplication.update += OnUpdate;
        }
        else
        {
            EditorApplication.update -= OnUpdate;
        }
    }

    private static void OnUpdate()
    {
        // Search for the singleton instance, we run before it initializes
        if (restarterInstance == null && singletonInstanceField.GetValue(null) != null)
        {
            restarterInstance = singletonInstanceField.GetValue(null);
        }

        // Check if we have an instance with an active restarter coroutine.
        if (restarterInstance != null && restarterCoroutine.GetValue(singletonInstanceField.GetValue(null)) != null)
        {
            Debug.Log("Killing internal OpenXR restart coroutine after connection failure! This is an editor hack to avoid reoccuring reconnection errors!");
            stopMethod.Invoke(restarterInstance, new object[] { restarterCoroutine.GetValue(restarterInstance) });
            restarterCoroutine.SetValue(restarterInstance, null);

            // Our job is done, no reason to linger anymore
            SetHooked(false);
        }
    }
}