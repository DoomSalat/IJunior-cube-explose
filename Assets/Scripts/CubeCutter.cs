using System.Collections.Generic;
using UnityEngine;

public class CubeCutter : MonoBehaviour
{
	[SerializeField] private Exploser _exploser;
	[SerializeField] private Spawner _spawner;

	[SerializeField] private Vector2 _minMaxSplit = new Vector2(2, 6);
	[SerializeField][Range(0, 100)] private float _startChance = 100;

	private float _reduceScale = 2;
	private float _reduceChance = 2;

	private List<Cube> _cubes = new List<Cube>();

	private void Start()
	{
		RefreshCubesList();
	}

	private void OnDisable()
	{
		foreach (Cube cube in _cubes)
		{
			cube.Clicked -= ClickedSplit;
		}
	}

	private void ClickedSplit(Cube cube)
	{
		if (IsSplit(cube))
		{
			Split(cube);
		}

		DestroyCube(cube);
	}

	private void RefreshCubesList()
	{
		_cubes.Clear();
		_cubes.AddRange(FindObjectsByType<Cube>(FindObjectsSortMode.None));

		foreach (Cube cube in _cubes)
		{
			cube.Chance = _startChance;
			cube.Clicked += ClickedSplit;
		}
	}

	private void Split(Cube cube)
	{
		if (cube == null)
			return;

		int splitsCount = (int)Random.Range(_minMaxSplit.x, _minMaxSplit.y);
		GameObject[] spawnedObjects = _spawner.Spawn(cube.gameObject, splitsCount);

		foreach (GameObject spawnedObject in spawnedObjects)
		{
			Cube spawnedCube = spawnedObject.GetComponent<Cube>();
			if (spawnedCube == null) continue;

			float nextChance = cube.Chance / _reduceChance;
			float nextScale = cube.transform.localScale.x / _reduceScale;
			spawnedCube.Construct(nextChance, nextScale);

			Rigidbody spawnRigidbody = spawnedCube.GetComponent<Rigidbody>();

			if (spawnRigidbody != null)
			{
				_exploser.Explose(spawnRigidbody, cube.transform.position);
			}

			_cubes.Add(spawnedCube);
			spawnedCube.Clicked += ClickedSplit;
		}
	}

	private bool IsSplit(Cube cube)
	{
		float randomValue = Random.Range(0, _startChance + 1);
		return randomValue <= cube.Chance;
	}

	private void DestroyCube(Cube cube)
	{
		if (cube == null)
			return;

		cube.Clicked -= ClickedSplit;
		_cubes.Remove(cube);

		cube.Destroy();
	}
}
