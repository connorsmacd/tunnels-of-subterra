using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementManager : MonoBehaviour {

    // List of elements
    public List<Element> elements { get; set; }

    // Distance from front fog that elements are generated at
    private float distanceFromFog = 5.0f;

    // Parent of instantiated elements
    public GameObject parent { get; set; }

    // Queue of elements
    private Queue<GameObject> objectQueue;

    void Start () {
        objectQueue = new Queue<GameObject>();
    }

    // Checks if powerUps need to be destroyed
    void Update() {
        // Get Z position of rear fog
        float rearFogZ = GameObject.FindGameObjectWithTag("Player")
                         .transform.FindChild("Rear Fog").transform.position.z;
        // Check if objects are in queue
        while ((objectQueue.Count > 0) && ((rearFogZ - objectQueue.Peek().transform.position.z) >= 0)) {
            // Destroy object at front of queue
            Destroy(objectQueue.Dequeue());
        }
    }

    public void generateElement() {
        // Choose an element randomly from the list
        Element currentPowerUp = elements[Random.Range(0, elements.Count)];
        // Sanity check
        if (currentPowerUp.elementObject != null) {
            // Get Z position of front fog
            float frontFogZ = GameObject.FindGameObjectWithTag("Player")
                              .transform.FindChild("Front Fog").transform.position.z;
            // Determine position of element
            Vector3 position = (Vector3)currentPowerUp.roughCenter +
                               new Vector3(Random.Range(-currentPowerUp.xVariance, currentPowerUp.xVariance),
                                           0, frontFogZ + distanceFromFog);
            // Instantiate obstacle
            GameObject currentObstacleObject = (GameObject)Instantiate(currentPowerUp.elementObject, position,
                                                                        Quaternion.Euler(currentPowerUp.orientation));
            // Set parent
            currentObstacleObject.transform.parent = parent.transform;
            // Enqueue object
            if ((objectQueue != null) && (currentObstacleObject != null))
                objectQueue.Enqueue(currentObstacleObject);
        }
    }
}
