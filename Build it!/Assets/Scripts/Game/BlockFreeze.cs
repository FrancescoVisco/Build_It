using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFreeze : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    
    void Start()
    {
       m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
      if(GameObject.Find("LevelGoal").GetComponent<Goal>().time < 0)
      {
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
      }
    }
}
