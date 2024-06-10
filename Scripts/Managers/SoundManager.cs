using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            //씬이 전환되어도 bgm이 계속 재생될 수 있도록 
            DontDestroyOnLoad(gameObject); 
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }
   
}
