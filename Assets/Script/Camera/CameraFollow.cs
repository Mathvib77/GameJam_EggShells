using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 5f; // Speed at which the camera follows the player
    public Transform Target; // The player or object the camera will follow

    private void FixedUpdate()
    {
        if (Target == null) return;

        // Construire la position cible en forçant Z à -10
        Vector3 targetPos = new Vector3(Target.position.x, Target.position.y, -10f);

        // Interpoler vers la position cible
        Vector3 smoothed = Vector3.Slerp(transform.position, targetPos, FollowSpeed * Time.deltaTime);

        // Clamp final de l'axe Z pour être absolument sûr
        smoothed.z = -10f;

        transform.position = smoothed;
    }
}
