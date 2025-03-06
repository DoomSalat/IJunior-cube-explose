using UnityEngine;

public class Exploser : MonoBehaviour
{
	[SerializeField] private float _explosionForce = 1000f;
	[SerializeField] private float _upwardsModifier = 0.5f;
	[SerializeField] private float _torqueForce = 50f;
	[SerializeField] private float _explosionRadius = 3f;

	public void Explose(GameObject recline)
	{
		Rigidbody rigidbody = recline.GetComponent<Rigidbody>();

		if (rigidbody != null)
		{
			rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _upwardsModifier);

			Vector3 randomTorque = Random.insideUnitSphere * _torqueForce;
			rigidbody.AddTorque(randomTorque, ForceMode.Impulse);
		}
	}
}
