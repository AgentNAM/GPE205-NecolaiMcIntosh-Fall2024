using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Prefabs
    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;

    public Transform playerSpawnTransform;

    // Awake is called when the object is first created - before even Start can run!
    private void Awake()
    {
        // If the instance doesn't exist yet
        if (instance == null)
        {
            // This is the instance
            instance = this;
            // Don't destroy this object if a new scene is loaded
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If instance does already have a Game Manager
            Destroy(gameObject);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer()
    {
        // Spawn the Player Controller at (0,0,0) with no rotation
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        // Spawn our Pawn and connect it to our Controller
        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation) as GameObject;

        // Get the Player Controller component and Pawn component
        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        // Hook them up!
        newController.pawn = newPawn;
    }
}
