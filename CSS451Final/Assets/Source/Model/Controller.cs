using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject handle;
    private HandleScript handleScript;
    // Start is called before the first frame update
    void Start()
    {
        handleScript = handle.GetComponent<HandleScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, 100))
            {
                if(hitInfo.collider.gameObject == handle)
                {
                    handleScript.Select(true);
                }
            }
        } else
        {
            handleScript.Select(false);
        }
    }
}
