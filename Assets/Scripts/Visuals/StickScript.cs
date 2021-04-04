using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickScript : MonoBehaviour
{
    public GameObject parentCharacter;
    public GameObject anchor;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float yHeight = this.GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
        
        float xLocation = parentCharacter.transform.position.x;
        float yLocation = parentCharacter.transform.position.y;
        
        anchor.transform.position = new Vector3(xLocation, yLocation, 0);
    }
}
