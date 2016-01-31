using UnityEngine;
using System.Collections;

public class Rainfall : MonoBehaviour {
    //Vector3 windDir;


	void Start () {
        float zRot = Random.Range(5, 15);
        transform.Rotate(0, 0, zRot);
        transform.position = new Vector3(Random.Range(-20, 20), 20, Random.Range(-5, 30));

    }
	

	void Update () {
        transform.Translate(0, -2f, 0);
        if (transform.position.y<-40)
        {
            Debug.Log("rained");
            Destroy(this.GetComponent<Mesh>());
            Destroy(this);
        }
	}
}
