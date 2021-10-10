using System;
using UnityEditor;

namespace CodeDk
{

    public static class FakePlayModeUtility
    {
        public static string[] PLAYMODE_ENTER_CALLS = { "OnEnable", "Start" };

        public static string[] PLAYMODE_EXIT_CALLS = { "OnDisable" };

        public static void Enter(object subject, Action<PlayModeStateChange> playmodeEventHandler)
        {
            playmodeEventHandler(PlayModeStateChange.ExitingEditMode);
            playmodeEventHandler(PlayModeStateChange.EnteredPlayMode);

            foreach (string methodCall in PLAYMODE_ENTER_CALLS)
            {
                ReflectionUtility.TryExecuteMethod(subject, methodCall);
            }
        }

        public static void Exit(object subject, Action<PlayModeStateChange> playmodeEventHandler)
        {
            foreach (string methodCall in PLAYMODE_EXIT_CALLS)
            {
                ReflectionUtility.TryExecuteMethod(subject, methodCall);
            }

            playmodeEventHandler(PlayModeStateChange.ExitingPlayMode);
            playmodeEventHandler(PlayModeStateChange.EnteredEditMode);
        }
    }
}
