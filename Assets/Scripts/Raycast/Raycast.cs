using TMPro;
using UnityEngine;

public class Raycast : MonoBehaviour {

    [SerializeField] private float MaxDistance = 100f;
    [SerializeField] private TMP_Text TextInteractHint;

    private Camera _camera;
    private string _textInteractHintContents;
    private GameObject outlineObject = null;

    private void Start() {
        _camera = Camera.main;

        _textInteractHintContents = TextInteractHint.text;
        TextInteractHint.text = "";
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

                // show interact hint
                TextInteractHint.text = _textInteractHintContents;

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
            }
            else {
                TextInteractHint.text = "";
                StopOutline();
            }
        }
        else {
            TextInteractHint.text = "";
            StopOutline();
        }
    }

    private void StopOutline() {
        if (outlineObject) {
            Destroy(outlineObject.GetComponent<Outline>());
            outlineObject = null;
        }
    }

}
