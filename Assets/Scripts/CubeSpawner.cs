using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
	[SerializeField][Min(0)] private float _spawnRange = 2f;

	private float _reduceScale = 2;
	private float _reduceChance = 2;

	public Cube[] Spawn(Cube spawnCube, int count)
	{
		Cube[] cubes = new Cube[count];

		for (int i = 0; i < count; i++)
		{
			Vector3 center = spawnCube.transform.position;

			float randomX = Random.Range(-_spawnRange, _spawnRange);
			float randomZ = Random.Range(-_spawnRange, _spawnRange);
			Vector3 randomPosition = new Vector3(center.x + randomX, center.y, center.z + randomZ);

			Cube cube = Instantiate(spawnCube, randomPosition, Quaternion.identity);
			cubes[i] = cube;

			float nextChance = spawnCube.Chance / _reduceChance;
			float nextScale = spawnCube.transform.localScale.x / _reduceScale;
			cube.Construct(nextChance, nextScale);
		}

		return cubes;
	}
}
