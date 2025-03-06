using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField][Min(0)] private float _spawnRange = 2f;

	public GameObject[] Spawn(GameObject spawnObject, int count)
	{
		GameObject[] apples = new GameObject[count];

		for (int i = 0; i < count; i++)
		{
			Vector3 center = spawnObject.transform.position;

			float randomX = Random.Range(-_spawnRange, _spawnRange);
			float randomZ = Random.Range(-_spawnRange, _spawnRange);
			Vector3 randomPosition = new Vector3(center.x + randomX, center.y, center.z + randomZ);

			GameObject apple = Instantiate(spawnObject, randomPosition, Quaternion.identity);
			apples[i] = apple;
		}

		return apples;
	}
}
