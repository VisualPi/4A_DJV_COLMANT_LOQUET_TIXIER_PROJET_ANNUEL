using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(EntityManagerScript))]
[CanEditMultipleObjects]
public class EntityInstanciater : Editor
{
#else
	public class EntityInstanciater : MonoBehaviour
{
#endif
#if UNITY_EDITOR
    SerializedProperty _propTab;
#endif
    private float _nbEntity;
    private GameObject _entityPrefab;
    EntityScript _content;
    private GameObject _parent;
#if UNITY_EDITOR
    void OnEnable()
    {
        _propTab = serializedObject.FindProperty("tab");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.DrawDefaultInspector();
        bool enlarge = false;
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField(string.Format("Message count = {0}", _propTab.arraySize));
        _entityPrefab = (GameObject)EditorGUILayout.ObjectField("Entity Prefab", _entityPrefab, typeof(GameObject), true);
        _nbEntity = EditorGUILayout.Slider("Nb Entity", _nbEntity, 0, 99999);
        if ( GUILayout.Button("Add") )
            enlarge = true;
        if ( GUILayout.Button("Clear Array") )
        {
            DeleteArray();
            serializedObject.ApplyModifiedProperties();
            DestroyImmediate(_parent);
        }
        EditorGUILayout.EndVertical();

        if ( enlarge )
        {
            _parent = new GameObject();
            EntityManagerScript t = target as EntityManagerScript;
            EnlargeArray();
            serializedObject.ApplyModifiedProperties();
            for ( int i = 0 ; i < _nbEntity ; ++i )
            {
                var go = Instantiate(_entityPrefab);
                go.transform.position = new Vector3(-1000 + i * 2, 1000, -1000);
                go.transform.parent = _parent.transform;
                t.tab[i] = go.GetComponent<EntityScript>();
            }
            enlarge = false;
        }
    }
    void EnlargeArray()
    {
        for ( int i = 0 ; i < _nbEntity ; i++ )
            _propTab.InsertArrayElementAtIndex(i);
    }
    void DeleteArray()
    {
        _propTab.ClearArray();
    }
#endif
}
