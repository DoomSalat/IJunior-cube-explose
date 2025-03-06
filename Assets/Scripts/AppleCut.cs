using UnityEngine;

public class AppleCut : MonoBehaviour, IClickable
{
	[SerializeField] private Renderer _selfRenderer;
	[SerializeField] private Exploser _selExploser;
	[SerializeField] private Spawner _spawner;

	[SerializeField] private Material[] _materials;
	[SerializeField] private Vector2 _minMaxSplit = new Vector2(2, 6);
	[SerializeField][Range(0, 100)] private float _startChance = 100;
	[SerializeField][Range(0, 100)] private float _chance;

	private float _reduceScale = 2;
	private float _reduceChance = 2;

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

		Destroy(gameObject);
	}

	public void Construct(float chance, float scale)
	{
		Chance = chance;
		transform.localScale = new Vector3(scale, scale, scale);

		int randomMaterial = Random.Range(0, _materials.Length);
		_selfRenderer.material = _materials[randomMaterial];
	}

	private void Split()
	{
		int splitsCount = (int)Random.Range(_minMaxSplit.x, _minMaxSplit.y);
		GameObject[] spawnedObjects = _spawner.Spawn(this.gameObject, splitsCount);
		AppleCut[] apples = System.Array.ConvertAll(spawnedObjects, obj => obj.GetComponent<AppleCut>());

		for (int i = 0; i < apples.Length; i++)
		{
			float nextChance = Chance / _reduceChance;
			float nextScale = transform.localScale.x / _reduceScale;
			apples[i].Construct(nextChance, nextScale);

			_selExploser.Explose(apples[i].gameObject);
		}
	}

	private bool IsSplit()
	{
		return Random.Range(0, _startChance + 1) <= Chance;
	}
}
