using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Beans.Unity.Editor;
using Piranha;

namespace PirahnaEditor
{
	[CustomEditor (typeof (PiranhaTarget)), CanEditMultipleObjects]
	public class PiranhaTargetEditor : Editor
	{
		private class Content
		{
			public static readonly GUIContent Piranhas = new GUIContent ("Piranhas");
			public static readonly GUIContent Force = new GUIContent (text: "Force", tooltip: "Magnitude of the force (impulse) to apply to each rigidbody.");
		}

		private class Properties
		{
			public SerializedProperty Force;
			public SerializedProperty Piranhas;

			public Properties (SerializedObject obj)
			{
				Force = obj.FindProperty ("force");
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

			EditorGUILayout.PropertyField (properties.Piranhas, Content.Piranhas, true);

			var dndRigidbodies = EditorGUILayoutx.DragAndDropArea<Rigidbody> ();
			if (dndRigidbodies != null)
			{
				Undo.RecordObjects (targets, "Added rigidbodies");
				foreach (var t in targets)
				{
					((PiranhaTarget)t).piranhas.AddRange (dndRigidbodies);
				}
			}

			serializedObject.ApplyModifiedProperties ();
		}
	}
}