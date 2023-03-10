using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_InteractionText;

    [SerializeField]
    private float m_InteractionRange;

    [SerializeField]
    private LayerMask m_InteractionMask;

    private Interactable hovering;

    private void Start()
    {
        m_InteractionText.color *= new Vector4(1, 1, 1, 0);
        hovering = null;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Interactable interactable = null;

        if(Physics.SphereCast(transform.position, 0.025f, transform.forward, out hit, m_InteractionRange, m_InteractionMask))
        {
            interactable = hit.collider.GetComponentInParent<Interactable>();
        }


        if(interactable != null) //Hovering something
        {
            if (interactable.GetUsable()) //The thing we hover is usable
            {
                if (hovering != null) //Wasn't hovering something before
                {
                    hovering = interactable;
                    m_InteractionText.color = new Color(m_InteractionText.color.r, m_InteractionText.color.g, m_InteractionText.color.b, 1);
                    m_InteractionText.text = "PRESS E " + hovering.InteractionText;
                }
                else //Was hovering something before
                {
                    if (hovering != interactable) //If it's something new
                    {
                        hovering = interactable;
                        m_InteractionText.color = new Color(m_InteractionText.color.r, m_InteractionText.color.g, m_InteractionText.color.b, 1);
                        m_InteractionText.text = "PRESS E " + hovering.InteractionText;
                    }
                }
            } else //Hovering something not usable
            {
                Hide();
                hovering = null;
            }
        } else //Not hovering anything
        {
            if(hovering != null) //Was hovering something before
            {
                Hide();
                hovering = null;
            }
        }
        

        if(hovering != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                hovering.Interact();

                if (!hovering.GetUsable())
                {
                    hovering = null;

                    Hide();
                }
            }
        }
    }

    private void Hide()
    {
        m_InteractionText.color *= new Vector4(1, 1, 1, 0);
    }
}
