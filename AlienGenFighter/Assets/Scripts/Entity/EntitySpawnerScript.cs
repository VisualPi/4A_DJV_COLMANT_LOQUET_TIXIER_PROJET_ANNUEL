using UnityEngine;

public class EntitySpawnerScript : MonoBehaviour
{
	[SerializeField] private int _nbDefaultEntity = 100;

	[SerializeField] private Transform _transform;
	// Use this for initialization
	void Start()
	{
		for (var i = 0; i < _nbDefaultEntity; ++i)
		{
			var e = EntityManagerScript.GetFromQueue();
			e.transform.position = new Vector3(_transform.position.x + Random.Range(0.0f, 15f), _transform.position.y, _transform.position.z + Random.Range(0.0f, 15f));
			e.Init();
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
