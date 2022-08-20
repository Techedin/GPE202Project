using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] gridPrefabs;
    public List<GameObject> instantiatedRooms;
    public GameObject playerSpawnRoom;
    public int rows;
    public int cols;
    public float roomWidth = 50.0f;
    public float roomHeight = 50.0f;
    private Room[,] grid;
    public int mapSeed;
    public bool isMapoftheDay;
    public GameObject RandomRoomPrefab()
    {
        //the zero is the number i need to change to make seeds
        return gridPrefabs[UnityEngine.Random.Range( 0, gridPrefabs.Length)];
    }
    // Start is called before the first frame update
    void Start()
    {
        if(isMapoftheDay != false)
        {
            UnityEngine.Random.InitState(DateToInt(DateTime.Now));
        }
        else
        {
            UnityEngine.Random.InitState(mapSeed);
        }
        
        instantiatedRooms = new List<GameObject>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateMap()
    {
        
        // Clear out the grid - "column" is our X, "row" is our Y
        grid = new Room[cols, rows];
        
        // For each grid row...
        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            // for each column in that row
            for (int currentCol = 0; currentCol < cols; currentCol++)
            {
                // Figure out the location. 
                float xPosition = roomWidth * currentCol;
                float zPosition = roomHeight * currentRow;
                Vector3 newPosition = new Vector3(xPosition, 0.0f, zPosition);


                // Create a new grid at the appropriate location
                GameObject mapTile = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;
                instantiatedRooms.Add(mapTile);

                if (playerSpawnRoom == null)
                {
                     mapTile = Instantiate(playerSpawnRoom, newPosition, Quaternion.identity) as GameObject;
                }
                // Set its parent
                mapTile.transform.parent = this.transform;

                // Give it a meaningful name
                mapTile.name = "Room_" + currentCol + "," + currentRow;

                // Get the room object
                Room mapRoom = mapTile.GetComponent<Room>();

                // Save it to the grid array
                grid[currentCol, currentRow] = mapRoom;

                // Open the doors
                // If we are on the bottom row, open the north door
                if (currentRow == 0)
                {
                    mapRoom.doorWest.SetActive(false);
                }
                else if (currentRow == rows - 1)
                {
                    // Otherwise, if we are on the top row, open the south door
                    Destroy(mapRoom.doorEast);
                }
                else
                {
                    // Otherwise, we are in the middle, so open both doors
                    Destroy(mapRoom.doorEast);
                    Destroy(mapRoom.doorWest);
                }


                if (currentCol == 0)
                {
                    mapRoom.doorNorth.SetActive(false);
                }
                else if (currentCol == cols - 1)
                {
                    // Otherwise, if we are on the top row, open the south door
                    Destroy(mapRoom.doorSouth);
                }
                else
                {
                    // Otherwise, we are in the middle, so open both doors
                    Destroy(mapRoom.doorSouth);
                    Destroy(mapRoom.doorNorth);
                }



            }

        }
        PlayerSpawnPoint[] playerSpawns = FindObjectsOfType<PlayerSpawnPoint>();
    }

    public int DateToInt(DateTime dateToUse)
    {
        // Add our date up and return it
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }
    public void DeleteMap()
    {
        foreach(GameObject room in instantiatedRooms)
        {
            Destroy(room);
        }
        instantiatedRooms.Clear();
    }

}
