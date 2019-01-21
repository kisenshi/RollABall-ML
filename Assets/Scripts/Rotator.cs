using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    private float xmax;
    private float zmax;

    private void Start()
    {
        GameObject ground = GameObject.Find("Ground");
        xmax = ground.GetComponent<Renderer>().bounds.size.x/2 - 1;
        zmax = ground.GetComponent<Renderer>().bounds.size.z/2 - 1;

        moveRandomnly();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    public void moveRandomnly()
    {

        float randomx = Random.Range(-xmax, xmax);
        float randomz = Random.Range(-zmax, zmax);

        this.transform.position = new Vector3(randomx, 0.5f, randomz);
    }
}
