using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePrimitive: MonoBehaviour {
    public Color MyColor = new Color(0.1f, 0.1f, 0.2f, 1.0f);
    public Vector3 Pivot;
    public bool rotateFlag = false;
    private bool forward = true;

	// Use this for initialization
	void Start () {
    }

    void Update()
    {
        if (rotateFlag)
        {
            Vector3 rot = transform.eulerAngles;
            rot.z = 45.0f * Mathf.Sin(Time.timeSinceLevelLoad);
            transform.eulerAngles = rot;
        }
    }
	
  
	public void LoadShaderMatrix(ref Matrix4x4 nodeMatrix)
    {
        Matrix4x4 p = Matrix4x4.TRS(Pivot, Quaternion.identity, Vector3.one);
        Matrix4x4 invp = Matrix4x4.TRS(-Pivot, Quaternion.identity, Vector3.one);
        Matrix4x4 trs = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
        Matrix4x4 m = nodeMatrix * p * trs * invp;
        GetComponent<Renderer>().material.SetMatrix("MyXformMat", m);
        GetComponent<Renderer>().material.SetColor("MyColor", MyColor);
    }
}