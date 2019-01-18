using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System.Linq;

public class PlayerAgent : Agent
{
    Rigidbody agentRb;
    private RayPerception rayPer;
    PlayerController playerController;
    private TestAcademy envAcademy;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        agentRb = GetComponent<Rigidbody>();
        rayPer = GetComponent<RayPerception>();
        envAcademy = FindObjectOfType<TestAcademy>();
    }

    public override void AgentReset()
    {
        agentRb.velocity = Vector3.zero;
        this.transform.position = new Vector3(0, 0.5f, 0);

        playerController.resetCount();
        envAcademy.AcademyReset();
    }

    public override void CollectObservations()
    {
        // We include the position and velocity of the agent
        AddVectorObs(this.transform.position);

        AddVectorObs(agentRb.velocity.x);
        AddVectorObs(agentRb.velocity.z);

        // Add Raycast to be able to detect the objects around it
        float rayDistance = 50f;
        float[] rayAngles = { 20f, 90f, 160f, 45f, 135f, 70f, 110f };
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, envAcademy.detectableObjects, 0f, 0f));

    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        // Actions, size = 2, move agent
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];
        agentRb.AddForce(controlSignal * playerController.speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (envAcademy.collectibles.Contains(other.gameObject.tag))
        {
            AddReward(1f);

            if (playerController.getCount() == 10)
            {
                Done();
            }
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
