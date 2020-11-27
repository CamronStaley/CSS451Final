using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject ballLauncher;
    public GameObject handle;
    private BallLauncherScript launcherScript;
    // Start is called before the first frame update
    void Start()
    {
        launcherScript = ballLauncher.GetComponent<BallLauncherScript>();
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
                    launcherScript.Select(true);
                }
            }
        } else
        {
            launcherScript.Select(false);
        }
    }
}
