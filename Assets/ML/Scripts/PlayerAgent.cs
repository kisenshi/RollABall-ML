using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System.Linq;

public class PlayerAgent : Agent
{
    Rigidbody agentRb;
    private RayPerception rayPer;
    PlayerControllerSquare playerController;
    private TestAcademy envAcademy;

    private GameObject[][] collectiblesObjects;

    void Start()
    {
        playerController = GetComponent<PlayerControllerSquare>();
        agentRb = GetComponent<Rigidbody>();
        rayPer = GetComponent<RayPerception>();
        envAcademy = FindObjectOfType<TestAcademy>();


    }

    public override void AgentReset()
    {
        agentRb.velocity = Vector3.zero;
        //this.transform.position = new Vector3(0, 0.5f, 0);

        playerController.resetCount();
        envAcademy.moveCollectibles();
       
        //envAcademy.AcademyReset();
    }

    public override void CollectObservations()
    {
        // We include the position and velocity of the agent
        //AddVectorObs(this.transform.position);

        AddVectorObs(agentRb.velocity.x);
        AddVectorObs(agentRb.velocity.z);

        // Add Raycast to be able to detect the objects around it
        float rayDistance = 50f;
        float[] rayAngles = { 20f, 90f, 160f, 45f, 135f, 70f, 110f };
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, envAcademy.detectableObjects, 0f, 0f));

    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        /*
        // Actions, size = 2, move agent
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];

        agentRb.AddForce(controlSignal * playerController.speed);
        */

        Vector3 dirToGo = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;

        dirToGo = transform.forward * Mathf.Clamp(vectorAction[0], -1f, 1f);
        rotateDir = transform.up * Mathf.Clamp(vectorAction[1], -1f, 1f);

        agentRb.AddForce(dirToGo * 2, ForceMode.VelocityChange);
        transform.Rotate(rotateDir, Time.fixedDeltaTime * 300);
    }

    void OnTriggerEnter(Collider other)
    {
        if (envAcademy.collectibles.Contains(other.gameObject.tag))
        {
            AddReward(1f);
            Done();

            /*if (playerController.getCount() == 10)
            {
                Done();
            }*/
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            AddReward(-0.01f);
        }
    }
}
