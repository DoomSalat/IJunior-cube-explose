using UnityEngine;

public class Cube : MonoBehaviour, IClickable
{
	[SerializeField] private Renderer _selfRenderer;
	[SerializeField] private Material[] _materials;

	[SerializeField][Range(0, 100)] private float _chance;

	public float Chance
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

	public void Construct(float chance, float scale)
	{
		Chance = chance;
		transform.localScale = new Vector3(scale, scale, scale);

		int randomMaterial = Random.Range(0, _materials.Length);
		_selfRenderer.material = _materials[randomMaterial];
	}

	public void OnClick() { }

	public void Destroy()
	{
		Destroy(gameObject);
	}
}
