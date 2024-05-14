using UnityEngine;

namespace Shmup
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Transform _player;
        [SerializeField] float _speed = 2.0f; // How fast the camera will move up the map

        void Start()
        {
            // Set the Camera Controller's initial position to the Player's X and Y. Leave the Z alone since we are in 2D.
            transform.position = new Vector3(_player.position.x, _player.position.y, transform.position.z);
        }

        void LateUpdate()
        {
            // Use LateUpdate for the camera, so the camera is the last thing to move. Will help prevent camera jitters.'

            // Move the camera along the battlefield at a constant speed
            transform.position += Vector3.up * _speed * Time.deltaTime;
        }
    }
}
