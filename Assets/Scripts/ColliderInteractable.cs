using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderInteractable : MonoBehaviour
{
    
    public GameObject Text;
    public GameObject art;
    public bool show;

    // Start is called before the first frame update
    void Start()
    {
        show = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (show)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                art.SetActive(!art.activeSelf);
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Text.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                art.SetActive(!art.activeSelf);
            }
        }
        
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Text.SetActive(false);
            art.SetActive(false);
            show = false;
        }
    }

    void OnCollisionStay(Collision col)
    {
        show = true;
    }
}
