using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Prefabs
    public GameObject playerControllerPrefab;
    public GameObject[] enemyControllerPrefabs;
    public GameObject tankPawnPrefab;
    public GameObject cameraPrefab;

    // Player camera offsets
    public float cameraOffsetBack;
    public float cameraOffsetUp;

    // public Transform playerSpawnTransform;

    // Reference to our Map Generator
    public MapGenerator mapGenerator;

    // List of Player Controllers
    public List<PlayerController> players = new List<PlayerController>();

    // List of AI Controllers
    public List<AIController> enemies = new List<AIController>();

    // List of PawnSpawnPoints
    public PawnSpawnPoint[] pawnSpawnPoints;

    // public int playerSpawnPointIndex;

    // Game States
    public GameObject TitleScreenStateObject;
    public GameObject MainMenuStateObject;
    public GameObject OptionsScreenStateObject;
    public GameObject CreditsScreenStateObject;
    public GameObject GameplayStateObject;
    public GameObject GameOverScreenStateObject;

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
        // ActivateTitleScreen();

        
        DeactivateAllStates();

        mapGenerator.GenerateMap();
        SpawnTanks();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /*
    public void SpawnPlayer()
    {
        Transform spawnPoint = null;

        // Find PawnSpawnPoints by type
        pawnSpawnPoints = FindObjectsByType<PawnSpawnPoint>(FindObjectsSortMode.None);

        if (pawnSpawnPoints.Length > 0)
        {
            playerSpawnPointIndex = Random.Range(0, pawnSpawnPoints.Length);
            // Randomly select a spawnPoint
            spawnPoint = pawnSpawnPoints[playerSpawnPointIndex].transform;
        }

        SpawnPlayer(spawnPoint);
    }
    */

    public void SpawnPlayer(PawnSpawnPoint spawnPoint)
    {
        if (spawnPoint != null)
        {
            Transform spawnPointTf = spawnPoint.transform;
            // Spawn the Player Controller at (0,0,0) with no rotation
            GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            // Spawn our Pawn and connect it to our Controller
            GameObject newPawnObj = Instantiate(tankPawnPrefab, spawnPointTf.position, spawnPointTf.rotation) as GameObject;

            // Spawn our camera behind the tank (UPDATE TO REMOVE MAGIC NUMBERS)
            GameObject newCameraObj = Instantiate(cameraPrefab, spawnPointTf.position + (Vector3.back * cameraOffsetBack) + (Vector3.up * cameraOffsetUp), spawnPointTf.rotation) as GameObject;

            // Get the Player Controller component and Pawn component
            Controller newController = newPlayerObj.GetComponent<Controller>();
            Pawn newPawn = newPawnObj.GetComponent<Pawn>();

            // Hook them up!
            newController.pawn = newPawn;
            newPawn.controller = newController;

            // Parent the new Player Controller component to the Pawn component
            newController.transform.parent = newPawnObj.transform;

            // Parent the new Camera component to the Pawn component
            newCameraObj.transform.parent = newPawnObj.transform;
        }
    }

    /*
    public void SpawnEnemy()
    {
        Transform spawnPoint = null;

        // Find PawnSpawnPoints by type
        pawnSpawnPoints = FindObjectsByType<PawnSpawnPoint>(FindObjectsSortMode.None);

        if (pawnSpawnPoints.Length > 0)
        {
            playerSpawnPointIndex = Random.Range(0, pawnSpawnPoints.Length);
            // Randomly select a spawnPoint
            spawnPoint = pawnSpawnPoints[playerSpawnPointIndex].transform;
        }

        if (spawnPoint != null)
        {
            GameObject enemyControllerPrefab = enemyControllerPrefabs[Random.Range(0, enemyControllerPrefabs.Length)];

            // Spawn the Player Controller at (0,0,0) with no rotation
            GameObject newEnemyObj = Instantiate(enemyControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            // Spawn our Pawn and connect it to our Controller
            GameObject newPawnObj = Instantiate(tankPawnPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;

            // Get the Player Controller component and Pawn component
            Controller newController = newEnemyObj.GetComponent<Controller>();
            Pawn newPawn = newPawnObj.GetComponent<Pawn>();

            // Hook them up!
            newController.pawn = newPawn;

            newEnemyObj.transform.parent = newPawnObj.transform;
        }
    }
    */

    public void SpawnEnemy(PawnSpawnPoint spawnPoint)
    {
        if (spawnPoint != null)
        {
            Transform spawnPointTf = spawnPoint.transform;
            GameObject enemyControllerPrefab = enemyControllerPrefabs[Random.Range(0, enemyControllerPrefabs.Length)];

            // Spawn the AI Controller at (0,0,0) with no rotation
            GameObject newEnemyObj = Instantiate(enemyControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            // Spawn our Pawn and connect it to our Controller
            GameObject newPawnObj = Instantiate(tankPawnPrefab, spawnPointTf.position, spawnPointTf.rotation) as GameObject;

            // Get the AI Controller component and Pawn component
            Controller newController = newEnemyObj.GetComponent<Controller>();
            Pawn newPawn = newPawnObj.GetComponent<Pawn>();

            // Hook them up!
            newController.pawn = newPawn;
            newPawn.controller = newController;

            // Parent the new AI Controller component to the Pawn component
            newEnemyObj.transform.parent = newPawnObj.transform;

            // Pass the waypoints stored in the PawnSpawnPoint to the new AI Controller
            newController.GetComponent<AIController>().waypoints = spawnPoint.waypoints;
        }
    }


    public void SpawnTanks()
    {
        // Debug.Log(players.Count);

        // Find PawnSpawnPoints by type
        pawnSpawnPoints = FindObjectsByType<PawnSpawnPoint>(FindObjectsSortMode.None);

        if (pawnSpawnPoints.Length > 0)
        {
            // playerSpawnPointIndex = Random.Range(0, pawnSpawnPoints.Length);

            PawnSpawnPoint playerSpawnPoint = pawnSpawnPoints[Random.Range(0, pawnSpawnPoints.Length)];

            
            foreach (PawnSpawnPoint pawnSpawnPoint in pawnSpawnPoints)
            {
                if (pawnSpawnPoint == playerSpawnPoint)
                {
                    Debug.Log("Spawning player");
                    SpawnPlayer(pawnSpawnPoint);
                }
                else
                {
                    Debug.Log("Spawning enemy");
                    SpawnEnemy(pawnSpawnPoint);
                }
            }
            
        }
    }

    public void RespawnPlayer(Controller playerToRespawn)
    {
        if (playerToRespawn.lives > 0)
        {
            Transform spawnPoint = null;

            // Find PawnSpawnPoints by type
            pawnSpawnPoints = FindObjectsByType<PawnSpawnPoint>(FindObjectsSortMode.None);

            if (pawnSpawnPoints.Length > 0)
            {
                // Randomly select a spawnPoint
                spawnPoint = pawnSpawnPoints[Random.Range(0, pawnSpawnPoints.Length)].transform;
            }

            if (spawnPoint != null)
            {
                GameObject newPawnObj = Instantiate(tankPawnPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;

                Pawn newPawn = newPawnObj.GetComponent<Pawn>();

                playerToRespawn.pawn = newPawn;

                newPawn.controller = playerToRespawn;

                //
            }
        }
    }

    // Helper function for deactivating all game states
    private void DeactivateAllStates()
    {
        // Deactivate all game states
        TitleScreenStateObject.SetActive(false);
        MainMenuStateObject.SetActive(false);
        OptionsScreenStateObject.SetActive(false);
        CreditsScreenStateObject.SetActive(false);
        GameplayStateObject.SetActive(false);
        GameOverScreenStateObject.SetActive(false);
    }

    // Game state transition functions

    public void ActivateTitleScreen()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the title screen
        TitleScreenStateObject.SetActive(true);
    }

    public void ActivateMainMenu()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the main menu
        MainMenuStateObject.SetActive(true);
    }

    public void ActivateOptionsMenu()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the options menu
        OptionsScreenStateObject.SetActive(true);
    }

    public void ActivateCredits()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the credits
        CreditsScreenStateObject.SetActive(true);
    }

    public void ActivateGameplay()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate gameplay
        GameplayStateObject.SetActive(true);
    }

    public void ActivateGameOver()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the game over screen
        GameOverScreenStateObject.SetActive(true);
    }

    // Stub helper function to Toggle Map Of Day
    public void ActivateMapOfTheDay()
    {
        if (mapGenerator != null)
        {
            mapGenerator.isMapOfTheDay = true; // Activate map of the day
            mapGenerator.isMapSeed = false;
            mapGenerator.isCurrentTime = false;
        }
    }

    // Stub helper function to Toggle Random Map
    public void ActivateRandomMap()
    {
        if (mapGenerator != null)
        {
            mapGenerator.isMapOfTheDay = false;
            mapGenerator.isMapSeed = false;
            mapGenerator.isCurrentTime = true; // Activate random map


        }
    }


    // Stub helper function to enable Random Map Seed
    public void ActivateRandomMapSeed()
    {
        mapGenerator.isMapOfTheDay = false;
        mapGenerator.isMapSeed = true; // Activate random map seed
        mapGenerator.isCurrentTime = false;
    }
}
