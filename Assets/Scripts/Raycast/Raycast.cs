using UnityEngine;

public class RaycastPC : MonoBehaviour {

    [SerializeField] private LineRenderer Line;
    [SerializeField] private float MaxDistance = 100f;

    private Camera _camera;

    private void Start() {
        Line.startWidth = Line.endWidth = 0.1f;
        _camera = Camera.main;
    }

    private void Update() {
        if (Input.GetMouseButton(0))
            UpdateLaser();
        else
            Line.enabled = false;
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
        Line.SetPosition(0, origin);
        Line.SetPosition(1, target);
        Line.enabled = true;
    }

}
