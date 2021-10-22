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

    Vector3 agentStartPos = new Vector3(-2f, -0f, 0f);
    Vector3 microfilmStartPos = new Vector3(1f, 1f, 0f);
    Vector3 sharkStartPos = new Vector3(1f, -1f, 0f);

    [SerializeField] private Transform  microfilmTransform;     // target position reference
    [SerializeField] private Transform  sharkTransform;         // enemy position reference
    [SerializeField] private GameObject background;             // background game object reference
    // public Transform microfilmTransform;     // target position reference
    // public Transform sharkTransform;         // enemy position reference
    // public GameObject background;

    IEnumerator SleepFor(float secs)            // call as coroutine
    {
        yield return new WaitForSeconds(secs);
    }

    public Vector3 NewRandomPos(float xLo, float xHi, float yLo, float yHi)
    {
        return new Vector3(Random.Range(xLo, xHi), Random.Range(yLo, yHi), 0f);  // because I am using 2D
    }

    public override void CollectObservations(VectorSensor sensor)       // Unity.MLAgents.Sensors.VectorSensor
    {
        // this is 2 positions, each with 3 values = 6 inputs (is this still correct in 2D?)
        sensor.AddObservation(transform.localPosition);  // pass in player curr position
        sensor.AddObservation(microfilmTransform.localPosition); // pass in goal target position
    }

    public override void OnActionReceived(ActionBuffers actions)        // Unity.MLAgents.Actuators.ActionBuffers
    {
        //Debug.Log(actions.DiscreteActions[0]);       // for discrete (int) or
        // Debug.Log(actions.ContinuousActions[0]);     // for continuous (float)

        float moveSpeed = 2f;
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];  // for 2D!
        transform.position += new Vector3(moveX, moveY, 0f) * Time.deltaTime * moveSpeed;
    }

    // Only for testing, to modify actions
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    public override void OnEpisodeBegin()
    {
        Vector3 pos1, pos2, pos3;

        pos1 = NewRandomPos(-2f, 2f, -2f, 2f);
        pos2 = NewRandomPos(-2f, 2f, -2f, 2f);
        pos3 = NewRandomPos(-2f, 2f, -2f, 2f);
        while ((System.Math.Abs(pos3.x - pos2.x) < 3f) && (System.Math.Abs(pos3.y - pos2.y) < 3f))
            pos3 = NewRandomPos(-2f, 2f, -2f, 2f);

        transform.localPosition = pos1;           // agentStartPos;
        sharkTransform.localPosition = pos2;      // sharkStartPos;
        microfilmTransform.localPosition = pos3;  // microfilmStartPos;
        background.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.name);

        // You can use either AddReward (increment) or SetReward (set).
        if (other.gameObject.name == "Microfilm")
        {
            Debug.Log("Win!");
            SetReward(1f);
            background.GetComponent<SpriteRenderer>().color = new Color(0.2f, 1f, 0.2f);
            // StartCoroutine(SleepFor(0.5f));     // sleep to be able to see result before resetting
            EndEpisode();  // need to reset state (with OnEpisodeBegin)
        }
        else if (other.gameObject.name == "Shark" || other.gameObject.tag == "Wall")
        {
            Debug.Log("Lose!");
            SetReward(-1f);
            background.GetComponent<SpriteRenderer>().color = new Color(1f, 0.2f, 0.2f);
            // StartCoroutine(SleepFor(0.5f));     // sleep to be able to see result before resetting
            EndEpisode(); // end state here
        }
    }
}


