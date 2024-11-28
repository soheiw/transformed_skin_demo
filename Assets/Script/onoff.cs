using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onoff : MonoBehaviour
{
    public GameObject hand;
    

    // Start is called before the first frame update
    void Start()
    {
        hand.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.K))
        {
            hand.SetActive(false);
           // Debug.Log("J");
        }

        if (Input.GetKey(KeyCode.L))
        {
            hand.SetActive(true);
           // Debug.Log("L");
        }

    }
}