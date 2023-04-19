using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ObjectNote))]
public class ObjectNoteEditor : Editor
{
    ObjectNote note;

    [DrawGizmo(GizmoType.InSelectionHierarchy)]
    static void DrawSelectedNote(Transform transform, GizmoType gizmoType)
    {
        DrawObjectNote(transform, gizmoType, true);
    }
    [DrawGizmo(GizmoType.NotInSelectionHierarchy)]
    static void DrawUnselectedNote(Transform transform, GizmoType gizmoType)
    {
        DrawObjectNote(transform, gizmoType, false);
    }

    static void DrawObjectNote(Transform transform, GizmoType gizmoType, bool selected)
    {
        ObjectNote onote = transform.GetComponent<ObjectNote>();
        if (onote != null)
        {
            if (onote.Style == null) {
                onote.SetStyle();
            }

            if ((selected && onote.ShowWhenSelected) || (!selected && onote.ShowWhenUnselected))
            {
                float dist = HandleUtility.GetHandleSize(transform.position);
                GUI.backgroundColor = onote.Color;
                Handles.Label(transform.position + new Vector3(0f, -onote.Offset * dist, 0f), onote.Text, onote.Style);
            }
        }
    }

    public void OnEnable()
    {
        note = (ObjectNote)target;
        if (note.IsNew) {
            note.Text = target.name;
            int comps = note.gameObject.GetComponents<Component>().Length;
            for (int i = 0; i < comps; i++)
            {
                UnityEditorInternal.ComponentUtility.MoveComponentUp(note);
            }
            note.IsNew = false;
        }
    }

    public override void OnInspectorGUI()
    {
        if (note != null)
        {
            GUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            string text = EditorGUILayout.TextArea(note.Text);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(note, "Changed Object Note");
                note.Text = text;
                SceneView.RepaintAll();
            }
            GUILayout.BeginVertical(GUILayout.MaxWidth(50));
            GUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            bool show = EditorGUILayout.Toggle(note.ShowWhenSelected, GUILayout.Width(16));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(note, "Changed Object Note Visibility");
                note.ShowWhenSelected = show;
                SceneView.RepaintAll();
            }
            GUILayout.Label("Show when selected", GUILayout.Width(140));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
            EditorGUI.BeginChangeCheck();
            bool showUnsel = EditorGUILayout.Toggle(note.ShowWhenUnselected, GUILayout.Width(16));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(note, "Changed Object Note Unselected Visibility");
                note.ShowWhenUnselected = showUnsel;
                SceneView.RepaintAll();
            }
            GUILayout.Label("Show when unselected", GUILayout.Width(140));
            GUILayout.EndHorizontal();
            GUILayout.Label("Y Offset:", GUILayout.Width(140));
            EditorGUI.BeginChangeCheck();
            float offset = EditorGUILayout.Slider(note.Offset, 0f, 3f, GUILayout.Width(150));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(note, "Changed Object Note Y Offset");
                note.Offset = offset;
                SceneView.RepaintAll();
            }
            GUILayout.Label("Background color:", GUILayout.Width(140));
            EditorGUI.BeginChangeCheck();
            Color col = EditorGUILayout.ColorField(note.Color, GUILayout.Width(150));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(note, "Changed Object Note Color");
                note.Color = col;
                SceneView.RepaintAll();
            }
            GUILayout.Label("Font size:", GUILayout.Width(140));
            EditorGUI.BeginChangeCheck();
            int size = EditorGUILayout.IntSlider(note.FontSize, 6, 20, GUILayout.Width(150));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(note, "Changed Object Note Font Size");
                note.FontSize = size;
                note.SetStyle();
                SceneView.RepaintAll();
            }
            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
            EditorGUI.BeginChangeCheck();
            bool bold = EditorGUILayout.Toggle(note.Bold, GUILayout.Width(16));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(note, "Changed Object Note Bold text");
                note.Bold = bold;
                note.SetStyle();
                SceneView.RepaintAll();
            }
            GUILayout.Label("Bold text", GUILayout.Width(140));
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
    }
}
