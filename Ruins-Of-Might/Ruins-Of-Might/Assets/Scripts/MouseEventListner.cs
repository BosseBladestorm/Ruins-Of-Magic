using UnityEngine;

public class MouseEventListner : MonoBehaviour {

    public MouseEventPortObject mouseEventPortObject = null;

    private void OnMouseOver() {
       
        if(Input.GetMouseButtonUp(0))
            mouseEventPortObject.MouseButtonUp(this);

    }

}
