using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class JimBandAgent : Agent
{
    //observation -> decision -> action -> reward :) -> ...[repeat]
    //Override these methods:
    // - CollectObservations(sensor)
    // - OnActionReceived(actions)

    // [SerializeField] private Transform microfilmTransform;   // target position reference
    // [SerializeField] private Transform sharkTransform;       // enemy position reference
    public Transform microfilmTransform;   // target position reference
    public Transform sharkTransform;       // enemy position reference

    public override void CollectObservations(VectorSensor sensor)       // Unity.MLAgents.Sensors.VectorSensor
    {
        // this is 2 positions, each with 3 values = 6 inputs (is this still correct in 2D?)
        sensor.AddObservation(transform.position);  // pass in player curr position
        sensor.AddObservation(microfilmTransform.position); // pass in goal target position
    }

    public override void OnActionReceived(ActionBuffers actions)        // Unity.MLAgents.Actuators.ActionBuffers
    {
        //Debug.Log(actions.DiscreteActions[0]);       // for discrete (int) or
        //Debug.Log(actions.ContinuousActions[0]);     // for continuous (float)

        float moveSpeed = 2f;
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];  // for 2D!
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.name);

        // You can use either AddReward (increment) or SetReward (set).
        if (other.gameObject.name == "Microfilm")
        {
            Debug.Log("Win!");
            SetReward(1f);
            EndEpisode();  // need to reset state (with OnEpisodeBegin)
        }
        else if (other.gameObject.name == "Shark")
        {
            Debug.Log("Lose!");
            SetReward(-1f);
            EndEpisode(); // end state here
        }
    }
}


