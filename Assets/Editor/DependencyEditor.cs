using OneFrame;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Dependency<>), true)]
public class DependencyEditor : PropertyDrawer
{
    private Color _colorResolved = new Color(46f / 255f, 204f / 255f, 113f / 255f);
    private Color _colorUnResolved = new Color(231f / 255f, 76f / 255f, 60f / 255f);

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Type dependencyType = GetGenericTypeFromProperty(property);
        SerializedProperty dependency = property.FindPropertyRelative("_dependency");

        Rect underlineRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, 1.5f);
        Color underlineColor = dependency.objectReferenceValue ? _colorResolved : _colorUnResolved;

        label = new GUIContent($"*{label.text}", $"Require: [{dependencyType.Name}] - From: [{dependencyType.Namespace}]");

        Rect widthLabel = new Rect(position.x, position.y, position.width / 2f, position.height);
        Rect widthField = new Rect(position.x + position.width / 2f, position.y, position.width / 2f, position.height);

        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.DrawRect(underlineRect, underlineColor);

        GUI.enabled = !Application.isPlaying;

        EditorGUI.LabelField(widthLabel, label);
        EditorGUI.ObjectField(widthField, dependency, GUIContent.none);

        GUI.enabled = true;

        EditorGUI.EndProperty();
    }

    private Type GetGenericTypeFromProperty(SerializedProperty property)
    {
        Type propertyType = property.serializedObject.targetObject.GetType();
        FieldInfo fieldInfo = propertyType.GetField(property.name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        if (fieldInfo != null && fieldInfo.FieldType.IsGenericType)
        {
            Type genericType = fieldInfo.FieldType.GetGenericArguments()[0];
            return genericType;
        }

        return null;
    }
}
