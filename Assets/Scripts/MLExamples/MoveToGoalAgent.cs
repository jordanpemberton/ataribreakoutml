using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{
    //observation -> decision -> action -> reward :) -> ...[repeat]
    //Override these methods:
    // - CollectObservations(sensor)
    // - OnActionReceived(actions)

    [SerializeField]
    private Transform targetTransform;  // target position reference (goal)


    public override void CollectObservations(VectorSensor sensor)       // Unity.MLAgents.Sensors.VectorSensor
    {
        // this is 2 positions, each with 3 values = 6 inputs (is this still correct in 2D?)
        sensor.AddObservation(transform.position);  // pass in player curr position
        sensor.AddObservation(targetTransform.position); // pass in goal target position
    }

    public override void OnActionReceived(ActionBuffers actions)        // Unity.MLAgents.Actuators.ActionBuffers
    {
        //Debug.Log(actions.DiscreteActions[0]);       // for discrete (int) or
        //Debug.Log(actions.ContinuousActions[0]);     // for continuous (float)

        float moveSpeed = 100f;
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1]; // i'm doing 2D

        transform.position += new Vector3(moveX, moveY, 0f) * Time.deltaTime * moveSpeed;
    }

//    public override void OnActionReceived(float[] vectorActions) // <-- float[] input?
//    {
//        Debug.Log(vectorActions.DiscreteActions[0]);       // for discrete (int) or
//        Debug.Log(vectorActions.ContinuousActions[0]);     // for continuous (float)
//    }


    // Only for testing, to modify actions
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    public override void OnEpisodeBegin()
    {
        transform.position = Vector3.zero; // reset to starting point
    }

    private void OnTriggerEnter(Collider other)
    {
        // You can use either AddReward (increment) or SetReward (set).
        // (Have to set up these objects correctly first)
        //if (other.TryGetComponent<Goal>(out Goal goal))
        // {
        //     SetReward(1f);
        //     EndEpisode();  // need to reset state (with OnEpisodeBegin)
        // }
        // if (other.TryGetComponent<Wall>(out Wall wall))
        // {
        //     SetReward(-1f);
        //     EndEpisode(); // end state here
        // }
    }
}


