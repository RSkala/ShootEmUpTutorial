using UnityEditor.Rendering;
using UnityEngine;

namespace Shmup
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float _speed = 5.0f;
        [SerializeField] float _smoothness = 0.1f; // Smoothing amount for position
        [SerializeField] float _leanAngle = 15.0f; // Turning angle when moving side to side
        [SerializeField] float _leanSpeed = 5.0f; // Turning speed when moving side to side

        [SerializeField] GameObject _playerModel; // Actual Sprite GameObject of the Player object

        // Bounds of camera where player is "allowed" to go. Values will need to be tweaked
        [Header("Camera Bounds")]
        [SerializeField] Transform _cameraFollowTransform;
        [SerializeField] float _minX = -8.0f;
        [SerializeField] float _maxX = 8.0f;
        [SerializeField] float _minY = -4.0f;
        [SerializeField] float _maxY = 4.0f;

        InputReader _inputReader;


        Vector3 _currentVelocity;

        // Where we want the player to move to every frame. Will apply smoothing to make it feel better.
        Vector3 _targetPosition;

        void Start()
        {
            _inputReader = GetComponent<InputReader>();
        }

        void Update()
        {
            //Debug.Log("_inputReader.Move: " + _inputReader.Move);

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // Calculate where do we actually want the player to be
            _targetPosition += new Vector3(_inputReader.Move.x, _inputReader.Move.y, 0.0f) * _speed * Time.deltaTime;

            // Calculate the min & max X & Y positions for the player based on the camera view
            float minPlayerX = _cameraFollowTransform.position.x + _minX;
            float maxPlayerX = _cameraFollowTransform.position.x + _maxX;

            float minPlayerY = _cameraFollowTransform.position.y + _minY;
            float maxPlayerY = _cameraFollowTransform.position.y + _maxY;

            // Clamp the player's position to the camera view
            _targetPosition.x = Mathf.Clamp(_targetPosition.x, minPlayerX, maxPlayerX);
            _targetPosition.y = Mathf.Clamp(_targetPosition.y, minPlayerY, maxPlayerY);

            // Lerp the player's position to the target position
            transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _currentVelocity, _smoothness);

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // Calculate the rotation effect
            float targetRotationAngle = -1.0f * _inputReader.Move.x * _leanAngle;

            float currentYRotation = transform.localEulerAngles.y;
            float newYRotation = Mathf.LerpAngle(currentYRotation, targetRotationAngle, _leanSpeed * Time.deltaTime);

            // Apply the rotation effect
            transform.localEulerAngles = new Vector3(0.0f, newYRotation, 0.0f);
            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++
        }
    }
}
