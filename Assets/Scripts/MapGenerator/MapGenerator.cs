using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] gridPrefabs;
    public int rows;
    public int cols;
    public float roomWidth = 50.0f;
    public float roomHeight = 50.0f;
    private Room[,] grid;

    public bool isMapSeed;
    public bool isCurrentTime;
    public bool isMapOfTheDay;

    public int mapSeed;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];
    }

    public void GenerateMap()
    {
        // Set our seed
        if (isMapSeed)
        {
            UnityEngine.Random.InitState(mapSeed);
        }
        if (isCurrentTime)
        {
            mapSeed = DateToInt(DateTime.Now);
            UnityEngine.Random.InitState(mapSeed);
        }
        if (isMapOfTheDay)
        {
            mapSeed = DateToInt(DateTime.Now.Date);
            UnityEngine.Random.InitState(mapSeed);
        }
        // Clear out the grid - "column" is our X, "row" is our Y
        grid = new Room[cols, rows];

        // For each grid row
        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            // For each column in that row, spawn a room tile
            for (int currentCol = 0; currentCol < cols; currentCol++)
            {
                // Figure out the location of our room tile
                float xPosition = roomWidth * currentCol;
                float zPosition = roomHeight * currentRow;
                Vector3 newPosition = new Vector3(xPosition, 0.0f, zPosition);

                // Create a new tile at the appropriate location
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity);
                
                // Set its parent
                tempRoomObj.transform.parent = transform;

                // Give it a meaningful name
                tempRoomObj.name = "Room_" + currentCol + "," + currentRow;

                // Get the room component
                Room tempRoom = tempRoomObj.GetComponent<Room>();

                // Save it to the grid array
                grid[currentCol, currentRow] = tempRoom;

                // This opens the necessary North and South doors
                if (currentRow == 0)
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                else if (currentRow == rows - 1)
                {
                    tempRoom.doorSouth.SetActive(false);
                }
                else
                {
                    tempRoom.doorNorth.SetActive(false);
                    tempRoom.doorSouth.SetActive(false);
                }

                // This opens the necessary East and West doors
                if (currentCol == 0)
                {
                    tempRoom.doorEast.SetActive(false);
                }
                else if (currentCol == cols - 1)
                {
                    tempRoom.doorWest.SetActive(false);
                }
                else
                {
                    tempRoom.doorEast.SetActive(false);
                    tempRoom.doorWest.SetActive(false);
                }
            }
        }
    }

    public void DestroyMap()
    {
        /*
        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            Room[] row = grid[currentRow];
        }
        */
        
        // For each grid row
        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            // For each column in that row
            for (int currentCol = 0; currentCol < cols; currentCol++)
            {
                // Destroy the current room
                Destroy(grid[currentRow, currentCol].gameObject);
            }
        }
        
    }

    public int DateToInt(DateTime dateToUse)
    {
        // Add our date up and return the result
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }
}
