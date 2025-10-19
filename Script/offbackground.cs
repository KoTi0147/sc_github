using UnityEngine;

namespace Playniax.Ignition
{
    [AddComponentMenu("Playniax/Ignition/OffCamera")]
    // This class manages the behavior of objects when they move out of the camera's view.
    //
    // Depending on the selected mode, objects can either be destroyed or repositioned 
    //
    // (looped) to the opposite side of the screen. It supports customizable directions,
    //
    // margins, and camera settings to determine how off-screen behavior is handled.
    public class Offbackgound : MonoBehaviour
    {
        [System.Serializable]
        public class LoopSettings
        {
            [Tooltip("Directions can be AllDirections, Left, Right, Up or Down.")]
            public Directions directions;

            [Tooltip("A sprite is 'cut off' exactly once it's outside the camera view. With margin you can give it extra space.")]
            public float margin;

            // The camera to use for visibility checks.
            public Camera camera;
        }

        // Determines what action to take when the object goes off-camera: Destroy, Loop, or None.
        public enum Mode { Destroy, Loop, None };

        // Specifies the directions for looping.
        public enum Directions { AllDirections, Left, Right, Top, Bottom };

        [Tooltip("Mode can be Mode.Destroy or Mode.Loop.")]
        public Mode mode;

        // Holds the settings for looping behavior.
        public LoopSettings loopSettings = new LoopSettings();

        // The renderers associated with this object.
        public Renderer[] renderers;

        void Awake()
        {
            // Sets the default camera if none is specified in the loop settings.
            if (loopSettings.camera == null) loopSettings.camera = Camera.main;

            // Initializes the renderers array with child renderers.
            if (renderers == null) renderers = GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0) renderers = GetComponentsInChildren<Renderer>();
        }

        void Update()
        {
            // Skips processing if there are no renderers.
            if (renderers.Length == 0) return;

            // Handles off-camera behavior based on the selected mode.
            if (mode == Mode.Destroy)
            {
                _Destroy();
            }
            else if (mode == Mode.Loop)
            {
                _Loop();
            }
        }

        void _Destroy()
        {
            // Checks if the object is on any camera, and destroys it if not.
            bool isOnCamera = _IsOnCamera();

            if (isOnCamera == false)
            {
                Destroy(gameObject);
            }
        }

        Bounds _GetBounds(GameObject gameObject)
        {
            // Calculates the bounding box of the object based on its renderers.
            Bounds bounds = new Bounds(gameObject.transform.position, Vector3.zero);

            for (int i = 0; i < renderers.Length; i++)
            {
                Renderer renderer = renderers[i];

                if (renderer == null) continue;

                bounds.Encapsulate(renderer.bounds);
            }

            return bounds;
        }

        bool _IsOnCamera()
        {
            // Checks if the object is visible on any camera.
            Camera[] allCameras = Camera.allCameras;

            for (int i = 0; i < allCameras.Length; i++)
            {
                for (int j = 0; j < renderers.Length; j++)
                {
                    Camera cam = allCameras[i];
                    Renderer renderer = renderers[j];

                    if (renderer == null) continue;

                    if (RendererHelpers.IsRendererVisible(cam, renderer) == true) return true;
                }
            }

            return false;
        }

        void _Loop()
        {
            // Handles looping of the object when it goes off-camera.
            Camera camera = loopSettings.camera;
            if (camera == null) return;

            // Calculates the bounds of the object.
            Bounds bounds = _GetBounds(gameObject);
            Vector2 extends = bounds.extents;

            // Determines the minimum and maximum positions based on the camera's viewport.
            var min = camera.ViewportToWorldPoint(new Vector3(0, 1, transform.position.z - camera.transform.position.z));
            var max = camera.ViewportToWorldPoint(new Vector3(1, 0, transform.position.z - camera.transform.position.z));

            // Adjusts the bounds based on the margin and object size.
            min.x -= extends.x;
            max.x += extends.x;
            min.y += extends.y;
            max.y -= extends.y;
            min.x -= loopSettings.margin;
            max.x += loopSettings.margin;
            min.y += loopSettings.margin;
            max.y -= loopSettings.margin;

            var position = transform.position;

            // Loops the object to the opposite side if it goes out of bounds in a specified direction.
            if ((loopSettings.directions == Directions.Left || loopSettings.directions == Directions.AllDirections) && transform.position.x < min.x)
            {
                position.x = max.x;
                transform.position = position;
            }
            else if ((loopSettings.directions == Directions.Right || loopSettings.directions == Directions.AllDirections) && transform.position.x > max.x)
            {
                position.x = min.x;
                transform.position = position;
            }
            else if ((loopSettings.directions == Directions.Bottom || loopSettings.directions == Directions.AllDirections) && transform.position.y < max.y)
            {
                position.y = min.y;
                transform.position = position;
            }
            else if ((loopSettings.directions == Directions.Top || loopSettings.directions == Directions.AllDirections) && transform.position.y > min.y)
            {
                position.y = max.y;
                transform.position = position;
            }
        }
    }
}