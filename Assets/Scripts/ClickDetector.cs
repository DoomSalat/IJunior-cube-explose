using UnityEngine;

public class ClickerDetected : MonoBehaviour
{
	[SerializeField] private LayerMask _layerMask;

	public event System.Action<GameObject> Hitted;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
			{
				IClickable clickable = hit.collider.GetComponent<IClickable>();

				if (clickable != null)
				{
					clickable.OnClick();
				}

				Hitted?.Invoke(hit.collider.gameObject);
			}
		}
	}
}