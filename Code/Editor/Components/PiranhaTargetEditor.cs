using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Piranha;

namespace PirahnaEditor
{
	[CustomEditor (typeof (PiranhaTarget)), CanEditMultipleObjects]
	public class PiranhaTargetEditor : Editor
	{
		private class Content
		{
			public static readonly GUIContent Force = new GUIContent (text: "Force", tooltip: "Magnitude of the force (impulse) to apply to each rigidbody.");
			public static readonly GUIContent ActivationMode = new GUIContent (text: "Activation Mode");
			public static readonly GUIContent ActivationDistance = new GUIContent (text: "Activation Distance", tooltip: "The min distance piranhas need to be within to have forces applied.");
			public static readonly GUIContent Piranhas = new GUIContent ("Piranhas", tooltip: "The rigidbodies to make swarm the target.");
		}

		private class Properties
		{
			public SerializedProperty Force;
			public SerializedProperty ActivationMode;
			public SerializedProperty ActivationDistance;
			public SerializedProperty Piranhas;

			public Properties (SerializedObject obj)
			{
				Force = obj.FindProperty ("force");
				ActivationMode = obj.FindProperty ("activationMode");
				ActivationDistance = obj.FindProperty ("activationDistance");
				Piranhas = obj.FindProperty ("piranhas");
			}
		}

		private Properties properties;
		private ReorderableList list;

		private void OnEnable ()
		{
			properties = new Properties (serializedObject);
		}

		public override void OnInspectorGUI ()
		{
			serializedObject.UpdateIfRequiredOrScript ();

			EditorGUILayout.PropertyField (properties.Force, Content.Force);
			EditorGUILayout.PropertyField (properties.ActivationMode, Content.ActivationMode);
			if ((PiranhaTarget.ActivationMode)properties.ActivationMode.enumValueIndex == PiranhaTarget.ActivationMode.Distance || properties.ActivationMode.hasMultipleDifferentValues)
			{
				using (new EditorGUI.IndentLevelScope ())
				{
					EditorGUILayout.PropertyField (properties.ActivationDistance, Content.ActivationDistance);
					properties.ActivationDistance.floatValue = Mathf.Max (0f, properties.ActivationDistance.floatValue);
				}
			}
			EditorGUILayout.PropertyField (properties.Piranhas, Content.Piranhas, true);

			serializedObject.ApplyModifiedProperties ();
		}
	}
}