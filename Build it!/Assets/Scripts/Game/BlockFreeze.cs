using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFreeze : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    MeshRenderer m_MeshR;
    public Material Material;
    
    void Start()
    {
       m_Rigidbody = GetComponent<Rigidbody>();
       m_MeshR = GetComponent<MeshRenderer>();
    }

    void Update()
    {
      if(GameObject.Find("LevelGoal").GetComponent<Goal>().time < 0)
      {
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        m_MeshR.material = Material;
      }
    }
}
