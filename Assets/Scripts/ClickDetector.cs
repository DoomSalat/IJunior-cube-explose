using UnityEngine;

public class ClickerDetected : MonoBehaviour
{
	[SerializeField] private LayerMask _layerMask;

	public event System.Action<Cube> HittedCube;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
			{
				if (hit.collider.TryGetComponent<Cube>(out Cube cube))
				{
					HittedCube?.Invoke(cube);
				}
			}
		}
	}
}