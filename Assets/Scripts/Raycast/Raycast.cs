using UnityEngine;

public class RaycastPC : MonoBehaviour {

    [SerializeField] private LineRenderer Line;
    [SerializeField] private float MaxDistance = 100f;

    private GameObject outlineObject = null;

    private Camera _camera;

    private void Start() {
        Line.startWidth = Line.endWidth = 0.1f;
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

            target = hit.point;
            var hitObject = hit.collider.gameObject;
            var interactable = hitObject.GetComponentInParent<InteractableObject>();

            if (Input.GetMouseButton(0))
                UpdateLaser(origin, target);
            else
                Line.enabled = false;

            // when am interactable object is hovered
            if (interactable) {

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
            } else {
                StopOutline();
            }
        } else {
            StopOutline();
            Line.enabled = false;
        }
    }

    private void StopOutline() {
        if (outlineObject) {
            Destroy(outlineObject.GetComponent<Outline>());
            outlineObject = null;
        }
    }

    private void UpdateLaser(Vector3 origin, Vector3 target) {

        // draw laser
        Line.SetPosition(0, origin);
        Line.SetPosition(1, target);
        Line.enabled = true;
    }

}
