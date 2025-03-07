using UnityEngine;

public class Cube : MonoBehaviour
{
	[SerializeField] private Rigidbody _selfRigidbody;
	[SerializeField] private Renderer _selfRenderer;
	[SerializeField] private CubeMaterialDrawer _drawer;

	[SerializeField][Range(0, 100)] private float _chance;

	public float Chance
	{
		get
		{
			return _chance;
		}
		private set
		{
			_chance = Mathf.Max(0, value);
		}
	}

	public Rigidbody SelfRigidbody => _selfRigidbody;

	public void Construct(float chance, float scale)
	{
		Chance = chance;
		transform.localScale = new Vector3(scale, scale, scale);

		_drawer.RandomMaterial(this);
	}

	public void ChangeMaterial(Material material)
	{
		_selfRenderer.material = material;
	}

	public void Destroy()
	{
		Destroy(gameObject);
	}
}
