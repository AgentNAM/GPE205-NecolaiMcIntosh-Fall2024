using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

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

    // Map Seed Toggle Buttons
    public Toggle MapOfDayToggle;
    public Toggle RandomMapToggle;
    public Toggle RandomMapSeedToggle;

    // Map Seed Input Field
    public TMP_InputField RandomMapSeedInputField;

    // Music and SFX Volume Sliders
    public Slider MainVolumeSlider;
    public Slider MusicVolumeSlider;
    public Slider SFXVolumeSlider;

    // Variable for audio mixer
    public AudioMixer audioMixer;

    /*
    // Variable for music
    public AudioClip backgroundMusic;
    */

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
        ActivateTitleScreen();

        /*
        DeactivateAllStates();

        mapGenerator.GenerateMap();
        SpawnTanks();
        */
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
            // Disable this new camera's AudioListener component
            newCameraObj.GetComponent<AudioListener>().enabled = false;

            // Get the Player Controller component and Pawn component
            Controller newController = newPlayerObj.GetComponent<Controller>();
            Pawn newPawn = newPawnObj.GetComponent<Pawn>();

            // Hook them up!
            newController.pawn = newPawn;
            newPawn.controller = newController;

            // Rename the new Pawn to match its Controller
            newPawn.name = newPawn.name + ": " + newController.name;

            // Parent the new Player Controller component to the Pawn component
            // newController.transform.parent = newPawnObj.transform;

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

            // Rename the new Pawn to match its Controller
            newPawn.name = newPawn.name + ": " + newController.name;

            // Parent the new AI Controller component to the Pawn component
            // newEnemyObj.transform.parent = newPawnObj.transform;

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
            // TODO: Add support for multiple players

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

    public void RespawnPlayer(PlayerController playerToRespawn)
    {
        if (playerToRespawn.lives > 0)
        {
            Transform spawnPointTf = null;

            // Find PawnSpawnPoints by type
            pawnSpawnPoints = FindObjectsByType<PawnSpawnPoint>(FindObjectsSortMode.None);

            if (pawnSpawnPoints.Length > 0)
            {
                // Randomly select a spawnPoint
                spawnPointTf = pawnSpawnPoints[Random.Range(0, pawnSpawnPoints.Length)].transform;
            }

            if (spawnPointTf != null)
            {
                GameObject newPawnObj = Instantiate(tankPawnPrefab, spawnPointTf.position, spawnPointTf.rotation) as GameObject;

                // Spawn our camera behind the tank (UPDATE TO REMOVE MAGIC NUMBERS)
                GameObject newCameraObj = Instantiate(cameraPrefab, spawnPointTf.position + (Vector3.back * cameraOffsetBack) + (Vector3.up * cameraOffsetUp), spawnPointTf.rotation) as GameObject;
                // Disable this new camera's AudioListener component
                newCameraObj.GetComponent<AudioListener>().enabled = false;

                Pawn newPawn = newPawnObj.GetComponent<Pawn>();

                playerToRespawn.pawn = newPawn;

                newPawn.controller = playerToRespawn;

                // Parent the new Camera component to the Pawn component
                newCameraObj.transform.parent = newPawnObj.transform;

                // Rename the new Pawn to match its Controller
                newPawn.name = newPawn.name + ": " + playerToRespawn.name;
            }
        }
        else
        {
            // Remove this player from the list of players
            players.Remove(playerToRespawn);

            // If there are no more players, switch to Game Over state
            if (players.Count <= 0)
            {
                ActivateGameOver();
            }
        }
    }

    public void RespawnEnemy(AIController enemyToRespawn)
    {
        if (enemyToRespawn.lives > 0)
        {
            Transform spawnPointTf = null;

            // Find PawnSpawnPoints by type
            pawnSpawnPoints = FindObjectsByType<PawnSpawnPoint>(FindObjectsSortMode.None);

            if (pawnSpawnPoints.Length > 0)
            {
                // Randomly select a spawnPoint
                spawnPointTf = pawnSpawnPoints[Random.Range(0, pawnSpawnPoints.Length)].transform;
            }

            if (spawnPointTf != null)
            {
                GameObject newPawnObj = Instantiate(tankPawnPrefab, spawnPointTf.position, spawnPointTf.rotation) as GameObject;


                Pawn newPawn = newPawnObj.GetComponent<Pawn>();

                enemyToRespawn.pawn = newPawn;

                newPawn.controller = enemyToRespawn;

                // Rename the new Pawn to match its Controller
                newPawn.name = newPawn.name + ": " + enemyToRespawn.name;
            }
        }
        else
        {
            enemies.Remove(enemyToRespawn);
        }
    }


    public void DespawnPlayers()
    {
        PlayerController[] playerControllers = FindObjectsByType<PlayerController>(FindObjectsSortMode.None);

        foreach (PlayerController player in playerControllers)
        {
            if (player.pawn != null)
            {
                Destroy(player.pawn.gameObject);
            }
            Destroy(player.gameObject);
        }

        players.Clear();
    }

    public void DespawnEnemies()
    {
        AIController[] enemyControllers = FindObjectsByType<AIController>(FindObjectsSortMode.None);

        foreach (AIController enemy in enemyControllers)
        {
            if (enemy.pawn != null)
            {
                Destroy(enemy.pawn.gameObject);
            }
            Destroy(enemy.gameObject);
        }

        enemies.Clear();
    }

    public void DespawnPickups()
    {
        PickupSpawner[] pickupSpawners = FindObjectsByType<PickupSpawner>(FindObjectsSortMode.None);

        foreach(PickupSpawner pickupSpawner in pickupSpawners)
        {
            pickupSpawner.DestroySpawnedPickup();
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

        // Generate map and spawn tanks
        mapGenerator.GenerateMap();
        SpawnTanks();
    }

    public void ActivateGameOver()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the game over screen
        GameOverScreenStateObject.SetActive(true);

        // Despawn pickups
        DespawnPickups();

        // Destroy map
        mapGenerator.DestroyMap();

        // Despawn players and enemies
        DespawnPlayers();
        DespawnEnemies();

    }

    // Stub helper function to Toggle Map Of Day
    public void ActivateMapOfTheDay()
    {
        if (mapGenerator != null)
        {
            mapGenerator.isMapOfTheDay = true; // Activate map of the day
            mapGenerator.isCurrentTime = false;
            mapGenerator.isMapSeed = false;
        }

        ///*
        if (MapOfDayToggle != null)
        {
            // MapOfDayToggle.isOn = true;
            MapOfDayToggle.interactable = false;
        }
        //*/

        if (RandomMapToggle != null)
        {
            RandomMapToggle.isOn = false;
            RandomMapToggle.interactable = true;
        }

        if (RandomMapSeedToggle != null)
        {
            RandomMapSeedToggle.isOn = false;
            RandomMapSeedToggle.interactable = true;
            RandomMapSeedInputField.interactable = false;
        }
    }

    // Stub helper function to Toggle Random Map
    public void ActivateRandomMap()
    {
        if (mapGenerator != null)
        {
            mapGenerator.isMapOfTheDay = false;
            mapGenerator.isCurrentTime = true; // Activate random map
            mapGenerator.isMapSeed = false;
        }

        if (MapOfDayToggle != null)
        {
            MapOfDayToggle.isOn = false;
            MapOfDayToggle.interactable = true;
        }

        ///*
        if (RandomMapToggle != null)
        {
            // RandomMapToggle.isOn = true;
            RandomMapToggle.interactable = false;
        }
        //*/

        if (RandomMapSeedToggle != null)
        {
            RandomMapSeedToggle.isOn = false;
            RandomMapSeedToggle.interactable = true;
            RandomMapSeedInputField.interactable = false;
        }
    }

    // Stub helper function to enable Random Map Seed
    public void ActivateRandomMapSeed()
    {
        mapGenerator.isMapOfTheDay = false;
        mapGenerator.isCurrentTime = false;
        mapGenerator.isMapSeed = true; // Activate random map seed

        if (MapOfDayToggle != null)
        {
            MapOfDayToggle.isOn = false;
            MapOfDayToggle.interactable = true;
        }

        if (RandomMapToggle != null)
        {
            RandomMapToggle.isOn = false;
            RandomMapToggle.interactable = true;
        }

        ///*
        if (RandomMapSeedToggle != null)
        {
            // RandomMapSeedToggle.isOn = true;
            RandomMapSeedToggle.interactable = false;
            RandomMapSeedInputField.interactable = true;
        }
        //*/
    }

    // Stub helper function to set map seed
    public void SetMapSeed()
    {
        // Get text from RandomMapSeedInputField
        string newMapSeedStr = RandomMapSeedInputField.text;

        // If RandomMapSeedInputField is not blank, set the map seed to whatever number the user inputted
        if (newMapSeedStr != "")
        {
            mapGenerator.mapSeed = int.Parse(newMapSeedStr);
        }
        // If RandomMapSeedInputField is blank, set the map seed to 0
        else
        {
            mapGenerator.mapSeed = 0;
        }
        // mapGenerator.mapSeed = newMapSeed;
    }

    // Stub helper function to set main volume
    public void SetMainVolume()
    {
        // Start with the slider value (assuming the slider goes from 0 to 1)
        float newVolume = MainVolumeSlider.value;
        if (newVolume <= 0)
        {
            // If we are at 0, set our volume to the lowest value
            newVolume = -80;
        }
        else
        {
            // We are >0, so start by finding the log10 value
            newVolume = Mathf.Log10(newVolume);
            // Make it in the 0-20 db range (instead of 0-1 db)
            newVolume = newVolume * 20;
        }

        // Set the volume to the new volume setting
        audioMixer.SetFloat("MainVolume", newVolume);
    }

    // Stub helper function to set music volume
    public void SetMusicVolume()
    {
        // Start with the slider value (assuming the slider goes from 0 to 1)
        float newVolume = MusicVolumeSlider.value;
        if (newVolume <= 0)
        {
            // If we are at 0, set our volume to the lowest value
            newVolume = -80;
        }
        else
        {
            // We are >0, so start by finding the log10 value
            newVolume = Mathf.Log10(newVolume);
            // Make it in the 0-20 db range (instead of 0-1 db)
            newVolume = newVolume * 20;
        }

        // Set the volume to the new volume setting
        audioMixer.SetFloat("MusicVolume", newVolume);
    }

    // Stub helper function to set sfx volume
    public void SetSFXVolume()
    {
        // Start with the slider value (assuming the slider goes from 0 to 1)
        float newVolume = SFXVolumeSlider.value;
        if (newVolume <= 0)
        {
            // If we are at 0, set our volume to the lowest value
            newVolume = -80;
        }
        else
        {
            // We are >0, so start by finding the log10 value
            newVolume = Mathf.Log10(newVolume);
            // Make it in the 0-20 db range (instead of 0-1 db)
            newVolume = newVolume * 20;
        }

        // Set the volume to the new volume setting
        audioMixer.SetFloat("SFXVolume", newVolume);
    }
}
