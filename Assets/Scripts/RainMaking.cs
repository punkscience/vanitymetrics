using UnityEngine;
using System.Collections;

public class RainMaking : MonoBehaviour {

	[SerializeField]
    private float heaviness = 5;

	[SerializeField]
    private GameObject drop;


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
