using UnityEngine;

public class Exploser : MonoBehaviour
{
	[SerializeField] private float _explosionForce = 1000f;
	[SerializeField] private float _upwardsModifier = 0.5f;
	[SerializeField] private float _torqueForce = 50f;
	[SerializeField] private float _explosionRadius = 3f;

	public void Explose(Rigidbody reclineRigidbody, Vector3 central)
	{
		Vector3 direction = (central - reclineRigidbody.transform.position).normalized;
		reclineRigidbody.AddExplosionForce(_explosionForce, central, _explosionRadius, _upwardsModifier);

		Vector3 torque = Vector3.Cross(direction, Vector3.up) * _torqueForce;
		reclineRigidbody.AddTorque(torque, ForceMode.Impulse);
	}
}