using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FindWS : MonoBehaviour
{
    public Canvas NPCUI;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6 && GameManager.Instance.havePizza)
        {
            NPCUIManager.Instance.MeetWS();
            
        }
    }
}
