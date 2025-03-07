using UnityEngine;

public class DrawCubeMaterial : MonoBehaviour
{
	[SerializeField] private Material[] _materials;

	public void RandomMaterial(Cube cube)
	{
		int randomMaterial = Random.Range(0, _materials.Length);
		cube.ChangeMaterial(_materials[randomMaterial]);
	}
}
