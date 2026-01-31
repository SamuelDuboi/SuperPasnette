using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Range(1,20)]
    public float movementSpeed;
    private Rigidbody rigidbody;
    private Item itemInRange;
    private List<string> lCarryingItem;
    private bool bIsListOpen = false;
    private UIManager UIManager;
    private CameraHandler camHandler;
    public bool cameraRelative;

    private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        UIManager = FindObjectOfType<UIManager>();
        camHandler = FindObjectOfType<CameraHandler>();
        lCarryingItem = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused) return;

        Vector3 m_Input =  new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        if(cameraRelative && !camHandler.bWasMaxLenght)
        {
            m_Input = camHandler.activeCam.transform.TransformDirection(m_Input);
            m_Input.y = 0;
        }

        rigidbody.MovePosition(transform.position + m_Input * Time.fixedDeltaTime * movementSpeed);
        if(itemInRange && Input.GetKeyDown(KeyCode.E)) 
        {
            UIManager.getHud().ToggleInteraction(false);

            lCarryingItem.Add(itemInRange.name);
           UIManager.UpdateItemInList(itemInRange.OnPickUp());
        }

       // if (Input.GetKeyDown(KeyCode.L))                 
       // {
       //     UIManager.OnGroceriesList(!bIsListOpen);
       //     bIsListOpen = !bIsListOpen;
       // }
    }

    public void setPause(bool isPause)
	{
        isPaused = isPause;
	}

    private void OnTriggerExit(Collider other)
    {
        if (itemInRange && other.gameObject.layer == 6) // interactionLayer
        {
            itemInRange = null;
            UIManager.getHud().ToggleInteraction(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6) // interactionLayer
        {
            UIManager.getHud().ToggleInteraction(true);
            itemInRange = other.GetComponent<Item>();
        }
    }

}
