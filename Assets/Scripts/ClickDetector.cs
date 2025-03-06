using UnityEngine;

public class ClickerDetected : MonoBehaviour
{
	[SerializeField] private LayerMask layerMask;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
			{
				IClickable clickable = hit.collider.GetComponent<IClickable>();

				if (clickable != null)
				{
					clickable.OnClick();
				}
			}
		}
	}
}