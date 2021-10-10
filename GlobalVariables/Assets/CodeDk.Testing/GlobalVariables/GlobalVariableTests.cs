using NUnit.Framework;
using UnityEngine;

namespace CodeDk
{
    public class GlobalVariableTests
    {
        public class EditorPlaymode
        {

            [Test]
            public void BacksUpVariableOnce_IfReadonly()
            {
                // Arrange
                //

                // We need a asset for this test, as backups are only created on persistent objects.
                const string TEST_SUBJECT_NAME = "ReadonlyGlobalInt";
                IntVariable original = ProjectTestUtility.FindAndLoadAsset<IntVariable>(TEST_SUBJECT_NAME);

                if (original == null)
                {
                    Debug.LogErrorFormat("Could not find test subject {2} in folder {0}. Aborting Test.\n" +
                        "Possible fixes: \n" +
                        "\t- Ensure the folder {0} exists.\n" +
                        "\t- Ensure an asset named {2} of type {1} exists somewhere in that folder.\n" +
                        "\t- If the test subject can still not be found, delete and create the asset again."
                        , ProjectTestUtility.TEST_SUBJECT_FOLDER, typeof(IntVariable).Name, TEST_SUBJECT_NAME);
                    return;
                }

                original.ReadOnly = true;

                int ORIGINAL_VALUE = original.Value;

                // Act
                //

                // Pretend we are entering play mode
                FakePlayModeUtility.Enter(original, original.OnPlaymodeChange);

                original.Value = ORIGINAL_VALUE + 1;

                // Call OnEnable again, to test if existing backup is detected (i.e. exiting backup isn't overwritten)
                // This can happen on script compilation during playmode, for example
                original.OnEnable();

                // Pretend we are entering edit mode
                FakePlayModeUtility.Exit(original, original.OnPlaymodeChange);

                // Assert
                //

                Assert.That(ORIGINAL_VALUE == original.Value);
            }

            [Test]
            public void ApplyChanges_IfReadAndWrite()
            {
                // Arrange
                //

                // We need a asset for this test, as backups are only created on persistent objects.
                const string TEST_SUBJECT_NAME = "ReadAndWriteGlobalInt";
                IntVariable intVariable = ProjectTestUtility.FindAndLoadAsset<IntVariable>(TEST_SUBJECT_NAME);

                if (intVariable == null)
                {
                    Debug.LogErrorFormat("Could not find test subject {2} in folder {0}. Aborting Test.\n" +
                        "Possible fixes: \n" +
                        "\t- Ensure the folder {0} exists.\n" +
                        "\t- Ensure an asset named {2} of type {1} exists somewhere in that folder.\n" +
                        "\t- If the test subject can still not be found, delete and create the asset again."
                        , ProjectTestUtility.TEST_SUBJECT_FOLDER, typeof(IntVariable).Name, TEST_SUBJECT_NAME);
                    return;
                }

                intVariable.ReadOnly = false;

                int ORIGINAL_VALUE = intVariable.Value;
                int NEW_VALUE = intVariable.Value + 1;

                // Act
                //

                // Pretend we are entering play mode
                FakePlayModeUtility.Enter(intVariable, intVariable.OnPlaymodeChange);

                intVariable.Value = NEW_VALUE;

                // Call OnEnable again, to test if existing backup is detected (i.e. exiting backup isn't overwritten)
                // This can happen on script compilation during playmode, for example
                intVariable.OnEnable();

                // Pretend we are entering edit mode
                FakePlayModeUtility.Exit(intVariable, intVariable.OnPlaymodeChange);

                // Assert
                //

                Assert.That(NEW_VALUE == intVariable.Value);
            }
        }
    }

}