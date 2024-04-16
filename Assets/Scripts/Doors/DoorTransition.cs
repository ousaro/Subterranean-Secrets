using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTransition : MonoBehaviour
{
    public static DoorTransition instance;

    [SerializeField] Transform targetTransition;

    [SerializeField] Canvas doorCanvas;
    
    private void Start()
    {
       instance = this;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            doorCanvas.gameObject.SetActive(true);
            PlayerController.Instance.TatgetPos(targetTransition);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            doorCanvas.gameObject.SetActive(false);
            PlayerController.Instance.TatgetPos(collision.transform);

        }
    }

}
