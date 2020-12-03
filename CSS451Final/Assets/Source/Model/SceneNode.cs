using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneNode : MonoBehaviour {

    protected Matrix4x4 mCombinedParentXform;
    
    public Vector3 NodeOrigin = Vector3.zero;
    public List<NodePrimitive> PrimitiveList;
    public bool LeftArm = false;
    public float endRot;
    private Vector3 initialRot;
    private Vector3 initialLocalRot;
    public float rotationSpeed;
    // Use this for initialization
    protected void Start () {
        InitializeSceneNode();
        initialRot = transform.eulerAngles;
        initialLocalRot = transform.localEulerAngles;
        // Debug.Log("PrimitiveList:" + PrimitiveList.Count);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (LeftArm)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.rotation *= Quaternion.Euler(0, 0, Time.deltaTime * rotationSpeed);
                if (transform.localEulerAngles.z < 360f - endRot && transform.localEulerAngles.z > initialRot.z)
                {
                    var rot = transform.localEulerAngles;
                    rot.z = -endRot;
                    transform.localEulerAngles = rot;
                }
            }
            else
            {
                transform.rotation *= Quaternion.Euler(0, 0, Time.deltaTime * -rotationSpeed);
                if (transform.localEulerAngles.z >= initialLocalRot.z && transform.localEulerAngles.z < 360f - endRot)
                {
                    transform.eulerAngles = initialRot;
                }
            } 
        } else // right arm
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.rotation *= Quaternion.Euler(0, 0, Time.deltaTime * rotationSpeed);
                if (transform.localEulerAngles.z < 360f - endRot && transform.localEulerAngles.z > initialRot.z)
                {
                    var rot = transform.localEulerAngles;
                    rot.z = -endRot;
                    transform.localEulerAngles = rot;
                }
            }
            else
            {
                transform.rotation *= Quaternion.Euler(0, 0, Time.deltaTime * -rotationSpeed);
                if (transform.localEulerAngles.z >= initialLocalRot.z && transform.localEulerAngles.z < 360f - endRot)
                {
                    transform.eulerAngles = initialRot;
                }
            }
        }
    }

    private void InitializeSceneNode()
    {
        mCombinedParentXform = Matrix4x4.identity;
    }

    // This must be called _BEFORE_ each draw!! 
    public void CompositeXform(ref Matrix4x4 parentXform)
    {
        Matrix4x4 orgT = Matrix4x4.Translate(NodeOrigin);
        Matrix4x4 trs = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
        
        mCombinedParentXform = parentXform * orgT * trs;


        // propagate to all children
        foreach (Transform child in transform)
        {
            SceneNode cn = child.GetComponent<SceneNode>();
            if (cn != null)
            {
                cn.CompositeXform(ref mCombinedParentXform);
            }
        }
        
        // disenminate to primitives
        foreach (NodePrimitive p in PrimitiveList)
        {
            p.LoadShaderMatrix(ref mCombinedParentXform);
        }
    }
}