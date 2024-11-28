using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offon : MonoBehaviour
{
    public GameObject knifehand;
    

    // Start is called before the first frame update
    void Start()
    {
        knifehand.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.K))
        {
            knifehand.SetActive(true);
           // Debug.Log("J");
        }

        if (Input.GetKey(KeyCode.L))
        {
            knifehand.SetActive(false);
           // Debug.Log("L");
        }

    }
}