using UnityEngine;

public class CubeCutter : MonoBehaviour
{
	[SerializeField] private Exploser _exploser;
	[SerializeField] private CubeSpawner _spawner;
	[SerializeField] private ClickerDetected _clickerDetected;

	[SerializeField] private Vector2Int _minMaxSplit = new Vector2Int(2, 6);
	[SerializeField][Range(0, 100)] private float _startChance = 100;

	private void OnEnable()
	{
		_clickerDetected.HittedCube += OnClick;
	}

	private void OnDisable()
	{
		_clickerDetected.HittedCube -= OnClick;
	}

	private void OnClick(Cube hitCube)
	{
		if (CanSplit(hitCube))
		{
			Split(hitCube);
		}

		DestroyCube(hitCube);
	}

	private void Split(Cube cube)
	{
		if (cube == null)
			return;

		int splitsCount = Random.Range(_minMaxSplit.x, _minMaxSplit.y);
		Cube[] spawnedCubes = _spawner.Spawn(cube, splitsCount);

		foreach (Cube spawnCube in spawnedCubes)
		{
			_exploser.Explose(spawnCube.SelfRigidbody, cube.transform.position);
		}

		cube.Destroy();
	}

	private bool CanSplit(Cube cube)
	{
		float randomValue = Random.Range(0, _startChance + 1);
		return randomValue <= cube.Chance;
	}

	private void DestroyCube(Cube cube)
	{
		if (cube == null)
			return;

		cube.Destroy();
	}
}
