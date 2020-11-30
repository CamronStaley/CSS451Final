using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneObjectScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform children in transform)
        {
            children.gameObject.AddComponent<PlaneRedirectionScript>();
        }
    }
}
