using UnityEngine;

public class CubeCutter : MonoBehaviour
{
	[SerializeField] private Exploser _exploser;
	[SerializeField] private CubeSpawner _spawner;
	[SerializeField] private ClickerDetected _clickerDetected;
	[SerializeField] private DrawCubeMaterial _drawer;

	[SerializeField] private Vector2 _minMaxSplit = new Vector2(2, 6);
	[SerializeField][Range(0, 100)] private float _startChance = 100;

	private float _reduceScale = 2;
	private float _reduceChance = 2;

	private void OnEnable()
	{
		_clickerDetected.HittedCube += ClickedHit;
	}

	private void OnDisable()
	{
		_clickerDetected.HittedCube -= ClickedHit;
	}

	private void ClickedHit(Cube hitCube)
	{
		if (IsSplit(hitCube))
		{
			Split(hitCube);
		}

		DestroyCube(hitCube);
	}

	private void Split(Cube cube)
	{
		if (cube == null)
			return;

		int splitsCount = (int)Random.Range(_minMaxSplit.x, _minMaxSplit.y);
		Cube[] spawnedCubes = _spawner.Spawn(cube, splitsCount);

		foreach (Cube spawnCube in spawnedCubes)
		{
			float nextChance = spawnCube.Chance / _reduceChance;
			float nextScale = spawnCube.transform.localScale.x / _reduceScale;
			spawnCube.Construct(nextChance, nextScale);
			_drawer.RandomMaterial(spawnCube);

			_exploser.Explose(spawnCube.SelfRigidbody, cube.transform.position);
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

		cube.Destroy();
	}
}
