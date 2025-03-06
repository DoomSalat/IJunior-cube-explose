using UnityEngine;

public class SpawnerApple : MonoBehaviour
{
	[SerializeField][Min(0)] private float _spawnRange = 2f;

	public AppleCut[] Spawn(GameObject spawnObject, int count)
	{
		AppleCut[] apples = new AppleCut[count];

		for (int i = 0; i < count; i++)
		{
			Vector3 center = spawnObject.transform.position;

			float randomX = Random.Range(-_spawnRange, _spawnRange);
			float randomZ = Random.Range(-_spawnRange, _spawnRange);
			Vector3 randomPosition = new Vector3(center.x + randomX, center.y, center.z + randomZ);

			AppleCut apple = Instantiate(spawnObject, randomPosition, Quaternion.identity).GetComponent<AppleCut>();
			apples[i] = apple;
		}

		return apples;
	}
}
