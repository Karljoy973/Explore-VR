using UnityEngine;

public class RaycastPC : MonoBehaviour {

    [SerializeField] private float MaxDistance = 100f;

    private LineRenderer _line;
    private Camera _camera;

    private void Start() {
        _line = gameObject.AddComponent<LineRenderer>();
        _line.startWidth = _line.endWidth = 0.1f;
        _camera = Camera.main;
    }

    private void Update() {
        if (Input.GetMouseButton(0))
            UpdateLaser();
        else
            _line.enabled = false;
    }

    private void UpdateLaser() {
        
        // find laser ends + mouse in 3D space + direction of laser
        var transf = transform;
        var origin = transf.position + transf.forward * 0.5f * transf.lossyScale.z;

        var mousePos = Input.mousePosition;
        mousePos.z = MaxDistance;
        var target = _camera.ScreenToWorldPoint(mousePos);
        
        var dir = (target - origin).normalized;
        
        // raycast and find hit object
        if (Physics.Raycast(origin, dir, out var hit, MaxDistance)) {
            target = hit.point;
            
            var interactable = hit.collider.gameObject.GetComponentInParent<InteractableObject>();
            if (interactable)
                interactable.OnInteract();
        }
        
        // draw laser
        _line.SetPosition(0, origin);
        _line.SetPosition(1, target);
        _line.enabled = true;
    }

}
