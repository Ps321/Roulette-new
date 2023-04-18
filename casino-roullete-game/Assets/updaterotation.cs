using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commands;

public class updaterotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!ButtonDict.wheelanim){
            Debug.Log("tramsformmm");
            transform.localScale=new Vector3(0.01f,0.01f,0.01f);
        }
       transform.rotation=Quaternion.identity;
    }
}
