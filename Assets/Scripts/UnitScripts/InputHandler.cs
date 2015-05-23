using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

    public bool Interactible = true;

    void OnMouseDown()
    {
        if (Interactible)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    //Debug.Log(hit.collider.gameObject.name);
                    if (hit.collider.gameObject.GetComponent<Unit>() != null)
                    {
                        Unit unit = hit.collider.gameObject.GetComponent<Unit>();
                        InputController.Instance.OnUnitClicked(unit);
                    }
                }
            }
        }
    }
}
