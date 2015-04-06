using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
using UnityEditor;

public class EntityInstanciater : EditorWindow
{

	public EntityManagerScript _entityManager;
	public GameObject _entityPrefab;

	//string myString = "Hello World";
	//bool groupEnabled;
	//bool myBool = true;
	//float myFloat = 1.23f;
	private float _nbEnity = 1000;

	[MenuItem("Window/My Window")]

	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(EntityInstanciater));
	}

	void OnGUI()
	{
		_entityManager = (EntityManagerScript)EditorGUILayout.ObjectField("Entity Manager", _entityManager, typeof(EntityManagerScript), true);
		_entityPrefab = (GameObject)EditorGUILayout.ObjectField("Entity Prefab", _entityPrefab, typeof(GameObject), true);
		_nbEnity = EditorGUILayout.Slider("Nb Entity", _nbEnity, 1000, 99999);
		if(GUILayout.Button("Generate"))
			GenerateAllEntities();
		if(GUILayout.Button("Delete all"))
			DeleteAllEntities();

	}

	public void GenerateAllEntities()
	{
		for (var i = 0; i < _nbEnity; ++i)
		{
			var go = Instantiate(_entityPrefab);
			go.transform.position = new Vector3(10000, 1, 10000 + i * 2);
			_entityManager.AddToQueue(go.GetComponent<EntityScript>());
			go.tag = "entity";
			
			//go = Instantiate(_entityPrefab); //sera peut etre pas le meme prefab pour les mort
			//go.transform.position = new Vector3(10500, 1, 10000 + i * 2);
			//_entityManager.AddNewDeadEntity(go.GetComponent<EntityScript>());
			//go.tag = "entity";
		}
	}

	public void DeleteAllEntities()
	{
		for (var index = 0; index < GameObject.FindGameObjectsWithTag("entity").Length; index++)
		{
			var go = GameObject.FindGameObjectsWithTag("entity")[index];
			DestroyImmediate(go);
		}
	}
}
