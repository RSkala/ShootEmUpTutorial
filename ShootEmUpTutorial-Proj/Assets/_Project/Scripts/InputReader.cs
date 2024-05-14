using UnityEngine;
using UnityEngine.InputSystem;

namespace Shmup
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputReader : MonoBehaviour
    {
        // NOTE: Make sure to set the Player Input component to C# events
        PlayerInput _playerInput;
        InputAction _moveAction;

        public Vector2 Move => _moveAction.ReadValue<Vector2>();

        void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _moveAction = _playerInput.actions["Move"];
        }
    }
}
