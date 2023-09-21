using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    public class HierarchyTools : EditorWindow
    {
        /*
         *  Hierarchy Tools for duplicating stuff in Unity and properly adding numeric suffixes to each item.
         *
         *  by Daniel Willett
         */
        private int count;
        private Vector2 offset;
        
        [MenuItem("Tools/Hierarchy Util")]
        private static void Open()
        {
            GetWindow(typeof(HierarchyTools));
        }
        private void Awake()
        {
            Selection.selectionChanged += Repaint;
        }
        private void OnDestroy()
        {
            Selection.selectionChanged -= Repaint;
        }
        public static void DuplicateObject(GameObject obj, int n, Vector2 offset = default)
        {
            int offset2 = 0;
            string nOld = obj.name;
            int splitLast = nOld.LastIndexOf('_');
            if (splitLast != -1)
                int.TryParse(nOld.Substring(splitLast + 1), NumberStyles.Number, CultureInfo.InvariantCulture, out offset2);
            Vector2 o1 = default;
            if (obj.TryGetComponent(out RectTransform transform))
            {
                o1 = transform.anchoredPosition;
            }

            offset = new Vector2(offset.x, -offset.y);
            nOld = obj.name;
            splitLast = nOld.LastIndexOf('_');
            for (int i = 0; i < n; ++i)
            {
                int index = i + offset2 + 1;
                string name;
                if (splitLast != -1 && int.TryParse(nOld.Substring(splitLast + 1), NumberStyles.Number, CultureInfo.InvariantCulture, out _))
                    name = nOld.Substring(0, splitLast + 1) + index;
                else
                    name = nOld + "_" + index;
                if (obj.transform.parent != null)
                {
                    Transform t = obj.transform.parent.Find(name);
                    if (t != null)
                        DestroyImmediate(t.gameObject);
                }
                GameObject @new = Instantiate(obj, obj.transform.parent, false);
                if (@new.TryGetComponent(out RectTransform t2))
                {
                    t2.anchoredPosition = (i + 1) * offset + o1;
                }

                @new.name = name;
                PrefabUtility.RecordPrefabInstancePropertyModifications(@new);
                RecursiveRenameChildren(@new, index);
            }
            Debug.Log("Cloned " + n + " object(s) and their children.");
        }
        public static void RecursiveRenameChildren(GameObject obj, int index)
        {
            int len = obj.transform.childCount;
            for (int i = 0; i < len; ++i)
            {
                GameObject child = obj.transform.GetChild(i).gameObject;
                string nOld = child.name;
                int splitLast = nOld.LastIndexOf('_');
                if (splitLast != -1 && int.TryParse(nOld.Substring(splitLast + 1), NumberStyles.Number, CultureInfo.InvariantCulture, out _))
                    child.name = nOld.Substring(0, splitLast + 1) + index;
                else
                    child.name = nOld + "_" + index;
                PrefabUtility.RecordPrefabInstancePropertyModifications(child);
                RecursiveRenameChildren(child, index);
            }
        }
        
        private void OnGUI()
        {
            GUILayout.Label("Hierarchy Util", EditorStyles.boldLabel);
            Transform t = Selection.activeTransform;
            if (t != null && t.gameObject != null)
            {
                EditorGUILayout.ObjectField("Selected object", t.gameObject, typeof(GameObject), true);
                count = EditorGUILayout.IntField("Duplicate Count", count);
                offset = EditorGUILayout.Vector2Field("Offset", offset);
                if (count > 0 && GUILayout.Button("Duplicate"))
                {
                    DuplicateObject(t.gameObject, count, offset);
                }
            }
            else return;
        }
    }
}
