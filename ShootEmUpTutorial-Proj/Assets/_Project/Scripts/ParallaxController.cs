using UnityEngine;

namespace Shmup
{
    public class ParallaxController : MonoBehaviour
    {
        [SerializeField] Transform[] _backgrounds; // Array of background layers (Note: Order matters)
        [SerializeField] float _smoothing = 10.0f; // How smooth the parallax effect is
        [SerializeField] float _multiplier = 15.0f; // How much the parallax effect increments per layer

        Transform _mainCameraTransform; // Reference to the Main Camera
        Vector3 _previousCameraPos; // Position of the Camera in the previous frame

        void Awake()
        {
            _mainCameraTransform = Camera.main.transform;
        }

        void Start()
        {
            _previousCameraPos = _mainCameraTransform.position;
        }

        void Update()
        {
            // Iterate through each background layer
            for(int i = 0; i < _backgrounds.Length; ++i)
            {
                // parallax will be the difference -- how far we want to move -- how far has camera has moved by 1 frame, increased by our multiplier
                float parallax = (_previousCameraPos.y - _mainCameraTransform.position.y) * (i * _multiplier); 
                var targetBackgroundYPos = _backgrounds[i].position.y + parallax;

                // Get the new target position for the current background
                Vector3 targetBackgroundPos = new Vector3(_backgrounds[i].position.x, targetBackgroundYPos, _backgrounds[i].position.z);

                // Lerp so it smoothly interpolates between where the background used to be and where it will be this frame
                _backgrounds[i].position = Vector3.Lerp(_backgrounds[i].position, targetBackgroundPos, _smoothing * Time.deltaTime);
            }

            // Set the previous camera position for the next frame update
            _previousCameraPos = _mainCameraTransform.position;
        }
    }
}
