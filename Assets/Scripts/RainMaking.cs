using UnityEngine;
using System.Collections;

public class RainMaking : MonoBehaviour {

    
    float heaviness = 5;
    [SerializeField]
    GameObject drop;


	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < heaviness; i++)
        {
            Instantiate(drop);
        }

	}
}
