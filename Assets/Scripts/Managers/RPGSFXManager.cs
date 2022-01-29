using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGSFXManager : MonoBehaviour
{
    public AudioSource roachDead;
    private static bool sfxManExists;
    // Start is called before the first frame update
    void Start()
    {
        if(!sfxManExists)
        {
            sfxManExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
