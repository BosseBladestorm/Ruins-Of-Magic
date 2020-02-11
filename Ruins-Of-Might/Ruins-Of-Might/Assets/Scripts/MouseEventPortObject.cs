using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Ports/MouseEventPort", fileName = "MouseEventPort")]
public class MouseEventPortObject : ScriptableObject {

    public UnityAction<MouseEventListner> OnMouseButtonUp = delegate { };

    public void MouseButtonUp(MouseEventListner mouseEventListner) {
        OnMouseButtonUp(mouseEventListner);

    }

}
