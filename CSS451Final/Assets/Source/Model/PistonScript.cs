using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonScript : MonoBehaviour
{
    public GameObject start;
    public GameObject end;
    private Vector3 initScale;
    // Start is called before the first frame update
    void Start()
    {
        initScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        var p1Pos = start.transform.position;
        var p2Pos = end.transform.position;

        var offset = p2Pos - p1Pos;
        var scale = new Vector3(initScale.x, offset.magnitude/2, initScale.z);

        var position = p1Pos + (offset / 2.0f);

        transform.localScale = scale;
        transform.position = position;
    }
}
