using UnityEngine;

public class AppleCut : MonoBehaviour, IClickable
{
	[SerializeField] private Renderer _selfRenderer;

	[SerializeField] private Material[] _materials;
	[SerializeField] private Vector2 _minMaxSplit = new Vector2(2, 6);
	[SerializeField][Range(0, 100)] private float _startChance = 100;
	[SerializeField][Min(0)] private float spawnRange = 5f;
	[SerializeField][Range(0, 100)] private float _chance;

	[Header("Explose")]
	[SerializeField] private float explosionForce = 1000f;
	[SerializeField] private float explosionRadius = 5f;
	[SerializeField] private float upwardsModifier = 0.5f;

	private float Chance
	{
		get
		{
			return _chance;
		}
		set
		{
			_chance = Mathf.Max(0, value);
		}
	}

	public void OnClick()
	{
		if (IsSplit())
		{
			Split();
		}

		Explose();
	}

	public void Create(float chance, float scale)
	{
		Chance = chance;
		transform.localScale = new Vector3(scale, scale, scale);

		int randomMaterial = Random.Range(0, _materials.Length);
		_selfRenderer.material = _materials[randomMaterial];
	}

	private void Split()
	{
		int splitsCount = (int)Random.Range(_minMaxSplit.x, _minMaxSplit.y);

		for (int i = 0; i < splitsCount; i++)
		{
			SpawnApple();
		}
	}

	private bool IsSplit()
	{
		return Random.Range(0, _startChance + 1) <= Chance;
	}

	public void SpawnApple()
	{
		float randomX = Random.Range(-spawnRange, spawnRange);
		float randomZ = Random.Range(-spawnRange, spawnRange);
		Vector3 randomPosition = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

		AppleCut apple = Instantiate(this, randomPosition, Quaternion.identity);

		float nextChance = Chance / 2;
		float nextScale = transform.localScale.x / 2;
		apple.Create(nextChance, nextScale);
	}

	private void Explose()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

		foreach (Collider hit in colliders)
		{
			Rigidbody hitRigidbody = hit.GetComponent<Rigidbody>();

			if (hitRigidbody != null)
			{
				hitRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier);
			}
		}

		Destroy(gameObject);
	}
}
