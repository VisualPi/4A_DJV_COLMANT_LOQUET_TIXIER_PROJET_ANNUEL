using UnityEngine;
using Assets.Scripts.Group;
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
    SerializedProperty _propTabGroups;
#endif
    private float _nbEntity;
    private float _nbGroup;

    private GameObject _entityPrefab;
    private GameObject _groupsPrefab;

    EntityScript _content;
    GroupScript _contentGroups;

    private GameObject _parent;
    private GameObject _parentGroups;

#if UNITY_EDITOR
    void OnEnable()
    {
        _propTab = serializedObject.FindProperty("tab");
        _propTabGroups = serializedObject.FindProperty("tabGroups");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.DrawDefaultInspector();
        bool enlarge = false;
        bool enlargeG = false;
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField(string.Format("nb Entity in tab = {0}", _propTab.arraySize));
        _entityPrefab = (GameObject)EditorGUILayout.ObjectField("Entity Prefab", _entityPrefab, typeof(GameObject), true);
        _nbEntity = EditorGUILayout.Slider("Nb Entity", _nbEntity, 0, 50000);
        if ( GUILayout.Button("Add") )
            enlarge = true;
        if ( GUILayout.Button("Clear Array") )
        {
            DeleteArray();
            serializedObject.ApplyModifiedProperties();
            DestroyImmediate(_parent);
        }
        //GROUPES
        EditorGUILayout.LabelField(string.Format("nb group in tab = {0}", _propTabGroups.arraySize));
        _groupsPrefab = (GameObject)EditorGUILayout.ObjectField("Group Prefab", _groupsPrefab, typeof(GameObject), true);
        _nbGroup = EditorGUILayout.Slider("Nb Groups", _nbGroup, 0, 1000);
        if ( GUILayout.Button("Add Group") )
            enlargeG = true;
        if ( GUILayout.Button("Clear Array Group") )
        {
            DeleteArrayGroups();
            serializedObject.ApplyModifiedProperties();
            DestroyImmediate(_parentGroups);
        }

        EditorGUILayout.EndVertical();

        if ( enlarge )
        {
            _parent = new GameObject();
            _parent.name = "Entities";
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
        if ( enlargeG )
        {
            _parentGroups = new GameObject();
            _parentGroups.name = "Groups";
            EntityManagerScript t = target as EntityManagerScript;
            EnlargeArrayGroups();
            serializedObject.ApplyModifiedProperties();
            for ( int i = 0 ; i < _nbGroup ; ++i )
            {
                var go = Instantiate(_groupsPrefab);
                go.transform.position = new Vector3(-1000 + i * 2, 1000, -5000);
                go.transform.parent = _parentGroups.transform;
                t.tabGroups[i] = go.GetComponent<GroupScript>();
            }
            enlargeG = false;
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
    void EnlargeArrayGroups()
    {
        for ( int i = 0 ; i < _nbGroup ; i++ )
            _propTabGroups.InsertArrayElementAtIndex(i);
    }
    void DeleteArrayGroups()
    {
        _propTabGroups.ClearArray();
    }
#endif
}
