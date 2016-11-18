using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour
{

    [System.Serializable]
    public struct PowerUp
    {
        // Object of the obstacle
        public GameObject powerUpObject;
        // Rough x & y values that obstacle will be placed at
        public Vector2 roughCenter;
        // Orientation of the object when it is placed
        public Vector3 orientation;
        // Amount that the object can vary from roughCenter.x
        public float xVariance;
    }

    //List of powerUps
    public List<PowerUp> powerUps;

    // Distance from front fog that powerups are generated at
    public float distanceFromFog = 5.0f;

    // Parent of active obstacles
    public GameObject parent;
    

    // Queue of obstacles
    private Queue<GameObject> powerUpObjectQueue = new Queue<GameObject>();

    // Use this for initialization
    void Start()
    {

    }

    // Checks if powerUps need to be destroyed
    void Update()
    {
        // Get Z position of rear fog
        float rearFogZ = GameObject.FindGameObjectWithTag("Player")
                         .transform.FindChild("RearFog").transform.position.z;
        // Check if objects are in queue
        if (powerUpObjectQueue.Count > 0)
        {
            // See if object is past rear fog
            if ((rearFogZ - powerUpObjectQueue.Peek().transform.position.z) >= 0)
            {
                // Destroy object at front of queue
                Destroy(powerUpObjectQueue.Dequeue());
            }
        }
    }

    public void generatePowerUp()
    {
        // Choose an obstacle randomly from the list
        PowerUp currentPowerUp = powerUps[Random.Range(0, powerUps.Count)];
        // Sanity check
        if (currentPowerUp.powerUpObject != null)
        {
            // Get Z position of front fog
            float frontFogZ = GameObject.FindGameObjectWithTag("Player")
                              .transform.FindChild("FrontFog").transform.position.z;
            // Determine position of obstacle
            Vector3 position = (Vector3)currentPowerUp.roughCenter +
                               new Vector3(Random.Range(-currentPowerUp.xVariance, currentPowerUp.xVariance),
                                           0, frontFogZ + distanceFromFog);
            // Instantiate obstacle
            GameObject currentObstacleObject = (GameObject)Instantiate(currentPowerUp.powerUpObject, position,
                                                                        Quaternion.Euler(currentPowerUp.orientation));
            // Set parent
            currentObstacleObject.transform.parent = parent.transform;
            // Enqueue object
            powerUpObjectQueue.Enqueue(currentObstacleObject);
        }
    }
}