using UnityEngine;
using UnityEditor;
using Piranha;
using UnityEditorInternal;

namespace PirahnaEditor
{
	[CustomEditor (typeof (PiranhaTarget)), CanEditMultipleObjects]
	public class PiranhaTargetEditor : Editor
	{
		private class Content
		{
			public static readonly GUIContent ListHeader = new GUIContent ("Rigidbodies");
			public static readonly GUIContent Force = new GUIContent (text: "Force", tooltip: "Magnitude of the force (impulse) to apply to each rigidbody.");
		}

		private class Properties
		{
			public SerializedProperty Force;
			public SerializedProperty Rigidbodies;

			public Properties (SerializedObject obj)
			{
				Force = obj.FindProperty ("force");
				Rigidbodies = obj.FindProperty ("rigidbodies");
			}
		}

		private Properties properties;
		private ReorderableList list;

		private void OnEnable ()
		{
			properties = new Properties (serializedObject);

			list = new ReorderableList (serializedObject, properties.Rigidbodies);
			list.drawHeaderCallback = (r) => EditorGUI.LabelField (r, Content.ListHeader);
		}

		public override void OnInspectorGUI ()
		{
			serializedObject.UpdateIfRequiredOrScript ();

			EditorGUILayout.PropertyField (properties.Force, Content.Force);
			list.DoLayoutList ();

			serializedObject.ApplyModifiedProperties ();
		}
	}
}