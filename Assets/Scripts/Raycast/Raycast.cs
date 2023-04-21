using UnityEngine;

public class Raycast : MonoBehaviour {

    [SerializeField] private float MaxDistance = 100f;

    private GameObject outlineObject = null;
    private Camera _camera;

    private void Start() {
        _camera = Camera.main;
    }

    private void Update() {

        // find laser ends + mouse in 3D space + direction of laser
        var transf = transform;
        var origin = transf.position + transf.forward * 0.5f * transf.lossyScale.z;

        var mousePos = Input.mousePosition;
        mousePos.z = MaxDistance;
        var target = _camera.ScreenToWorldPoint(mousePos);

        var dir = (target - origin).normalized;

        if (Physics.Raycast(origin, dir, out var hit, MaxDistance)) {
            
            var hitObject = hit.collider.gameObject;
            var interactable = hitObject.GetComponentInParent<InteractableObject>();

            if (interactable && interactable.CanInteract()) {  // looking at an interactable object

                // display outline effect
                if (!outlineObject) {
                    var newComponent = hitObject.AddComponent<Outline>();
                    newComponent.OutlineWidth = 10;
                    outlineObject = hitObject;
                } else if (outlineObject != hitObject) {
                    Destroy(outlineObject.GetComponent<Outline>());
                    hitObject.AddComponent<Outline>();
                    outlineObject = hitObject;
                }

                // when the object is clicked
                if (Input.GetMouseButtonDown(0))
                    interactable.OnInteract();
            } else
                StopOutline();
        } else
            StopOutline();
    }

    private void StopOutline() {
        if (outlineObject) {
            Destroy(outlineObject.GetComponent<Outline>());
            outlineObject = null;
        }
    }

}
