using UnityEngine;

public class MaterialDrawer : MonoBehaviour
{
	[SerializeField] private Material[] _materials;

	public void RandomMaterial(Renderer renderer)
	{
		int randomMaterial = Random.Range(0, _materials.Length);
		renderer.material = _materials[randomMaterial];
	}
}
