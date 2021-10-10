using System;
using UnityEditor;
using UnityEngine;

namespace CodeDk
{

    public static class ProjectTestUtility
    {
        public static string[] TEST_SUBJECT_FOLDER = { "Assets/CodeDk.Testing/TestSubjects" };

        public static T FindAndLoadAsset<T>(string name)
            where T : UnityEngine.Object
        {
            T[] matchingAssets = FindAndLoadAssets<T>(name, 1);

            if (matchingAssets.Length > 0)
            {
                return matchingAssets[0];
            }
            else
            {
                return default;
            }
        }

        public static T[] FindAndLoadAssets<T>(string nameFilter, int maxCount = Int32.MaxValue)
            where T : UnityEngine.Object
        {
            string[] assetGuids = AssetDatabase.FindAssets(nameFilter + " t: " + typeof(T).Name, TEST_SUBJECT_FOLDER);

            int assetLoadCount = Mathf.Min(maxCount, assetGuids.Length);

            T[] matchingAssets = new T[assetLoadCount];

            for (int i = 0; i < assetLoadCount; i++)
            {
                string currentGuid = assetGuids[i];
                string assetPath = AssetDatabase.GUIDToAssetPath(currentGuid);
                matchingAssets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }

            return matchingAssets;
        }
    }
}
