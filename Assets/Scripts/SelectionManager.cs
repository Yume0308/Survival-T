using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public GameObject Interaction_Info_UI;
    Text interaction_Text;

    private void Start()
    {
        interaction_Text = Interaction_Info_UI.GetComponent<Text>();  
    }
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            var selectionTransform = hit.transform;
            if(selectionTransform.GetComponent<InteractableObject>())
            {
                interaction_Text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();

                Interaction_Info_UI.SetActive(true);
            }
        }
        else
        {
            Interaction_Info_UI.SetActive(false);
        }
    }
}
