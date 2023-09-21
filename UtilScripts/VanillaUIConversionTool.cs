using UnityEditor;
using UnityEngine;

public class VanillaUIConversionTool : EditorWindow
{
    /*
     *  Tool to convert vanilla (Glazier/Sleek) UI measurements to Unity measurements.
     *
     *  by Daniel Willett
     */
    private RectTransform _lastSelected;
    private Vector2 _posScale;
    private Vector2 _posOffset;
    private Vector2 _sizeScale;
    private Vector2 _sizeOffset;

    [MenuItem("Tools/Vanilla UI Conversion Tool")]
    private static void Open()
    {
        GetWindow(typeof(VanillaUIConversionTool));
    }
    private void Awake()
    {
        Selection.selectionChanged += Repaint;
    }
    private void OnDestroy()
    {
        Selection.selectionChanged -= Repaint;
    }

    private void OnGUI()
    {
        GUILayout.Label("Vanilla UI Conversion Tool", EditorStyles.boldLabel);
        RectTransform t = Selection.activeTransform as RectTransform;
        if (t == null)
        {
            GUILayout.Label("Select a UI object to get started.");
            return;
        }

        EditorGUILayout.ObjectField("Selected Object", t.gameObject, typeof(GameObject), true);

        Vector2 anchorMin = t.anchorMin;
        Vector2 anchorMax = t.anchorMax;
        Vector2 anchorPosition = t.anchoredPosition;
        Vector2 sizeDelta = t.sizeDelta;
        GUILayout.Label("Position Scale (0-1): (" + anchorMin.x + ", " + (1f - anchorMax.y) + ").");
        GUILayout.Label("Position Offset (px): (" + anchorPosition.x + " px, " + -anchorPosition.y + " px).");
        GUILayout.Label("Size Scale (0-1): (" + (anchorMax.x - anchorMin.x) + ", " + (anchorMax.y - anchorMin.y) + ").");
        GUILayout.Label("Size Offset (px): (" + sizeDelta.x + " px, " + sizeDelta.y + " px).");

        GUILayout.Space(20f);

        if (GUILayout.Button("From Selected") || _lastSelected != t)
        {
            _posScale = new Vector2(anchorMin.x, 1f - anchorMax.y);
            _posOffset = new Vector2(anchorPosition.x, -anchorPosition.y);
            _sizeScale = new Vector2(anchorMax.x - anchorMin.x, anchorMax.y - anchorMin.y);
            _sizeOffset = sizeDelta;
            _lastSelected = t;
        }

        if (GUILayout.Button("Set Defaults"))
        {
            _posScale = Vector2.zero;
            _posOffset = Vector2.zero;
            _sizeScale = Vector2.zero;
            _sizeOffset = Vector2.zero;
        }

        _posScale = EditorGUILayout.Vector2Field("Position Scale", _posScale);
        _posOffset = EditorGUILayout.Vector2Field("Position Offset", _posOffset);
        _sizeScale = EditorGUILayout.Vector2Field("Size Scale", _sizeScale);
        _sizeOffset = EditorGUILayout.Vector2Field("Size Offset", _sizeOffset);

        if (GUILayout.Button("Apply"))
        {
            SetTransforms(t, _posScale, _posOffset, _sizeScale, _sizeOffset);
        }
    }

    public static void SetTransforms(RectTransform transform, Vector2 posScale, Vector2 posOffset, Vector2 sizeScale, Vector2 sizeOffset)
    {
        Undo.RecordObject(transform, "Apply uGUI Transform");
        transform.anchorMin = new Vector2(posScale.x, 1f - posScale.y - sizeScale.y);
        transform.anchorMax = new Vector2(posScale.x + sizeScale.x, 1f - posScale.y);
        transform.anchoredPosition = new Vector2(posOffset.x, -posOffset.y);
        transform.sizeDelta = sizeOffset;
        transform.pivot = new Vector2(0f, 1f);
        if (EditorUtility.IsPersistent(transform))
        {
            PrefabUtility.RecordPrefabInstancePropertyModifications(transform);
            Debug.Log("Saved object.");
        }
    }
}
