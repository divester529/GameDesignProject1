using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System;

public class MapGenerator : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject player;


    private void Start()
    {
      gameManager=GameManager.Instance;

        GenerateFloorLayout();
        GenerateMapVisual();
        //InstantiatePlayer();

    }

    private void Update()
    {
        if(gameManager.clearedRooms[(int)player.transform.position.x / gameManager.roomSizeX, (int)player.transform.position.y / gameManager.roomSizeY] == 1)
        {
            //round the players position to the room positions
            SpawnEnemies((((int)player.transform.position.x) / gameManager.roomSizeX) * gameManager.roomSizeX, (((int)player.transform.position.y) / gameManager.roomSizeY) * gameManager.roomSizeY);
            gameManager.clearedRooms[(int)player.transform.position.x / gameManager.roomSizeX, (int)player.transform.position.y / gameManager.roomSizeY] = 0;
        }
        checkDoors();
    }
    //this should be called after gameManager.enemiesAlive is changed, which is after an enemy death or enemy spawn
    public void checkDoors()
    {
        GameObject[] topdoors, leftdoors, rightdoors, bottomdoors;
        Vector2 doorCoords;
        if (gameManager.enemiesAlive != 0)
        {
            //find all open doors, destroy them, and replace them with closed doors
            topdoors = GameObject.FindGameObjectsWithTag("doorTopOpen");
            foreach (GameObject topdoor in topdoors)
            {
                doorCoords = topdoor.transform.position;
                Destroy(topdoor);
                GameObject door = (GameObject)Instantiate(gameManager.tileTypes[2].tileVisualPrefab, doorCoords, Quaternion.identity);
            }
            leftdoors = GameObject.FindGameObjectsWithTag("doorLeftOpen");
            foreach (GameObject leftdoor in leftdoors)
            {
                doorCoords = leftdoor.transform.position;
                Destroy(leftdoor);
                GameObject door = (GameObject)Instantiate(gameManager.tileTypes[4].tileVisualPrefab, doorCoords, Quaternion.identity);
            }
            rightdoors = GameObject.FindGameObjectsWithTag("doorRightOpen");
            foreach (GameObject rightdoor in rightdoors)
            {
                doorCoords = rightdoor.transform.position;
                Destroy(rightdoor);
                GameObject door = (GameObject)Instantiate(gameManager.tileTypes[6].tileVisualPrefab, doorCoords, Quaternion.identity);
            }
            bottomdoors = GameObject.FindGameObjectsWithTag("doorBottomOpen");
            foreach (GameObject bottomdoor in bottomdoors)
            {
                doorCoords = bottomdoor.transform.position;
                Destroy(bottomdoor);
                GameObject door = (GameObject)Instantiate(gameManager.tileTypes[8].tileVisualPrefab, doorCoords, Quaternion.identity);
            }
        }
        else
        {
            //find all closed doors, destroy them, and replace them with open doors
            topdoors = GameObject.FindGameObjectsWithTag("doorTopClosed");
            foreach (GameObject topdoor in topdoors)
            {
                doorCoords = topdoor.transform.position;
                Destroy(topdoor);
                GameObject door = (GameObject)Instantiate(gameManager.tileTypes[3].tileVisualPrefab, doorCoords, Quaternion.identity);
            }
            leftdoors = GameObject.FindGameObjectsWithTag("doorLeftClosed");
            foreach (GameObject leftdoor in leftdoors)
            {
                doorCoords = leftdoor.transform.position;
                Destroy(leftdoor);
                GameObject door = (GameObject)Instantiate(gameManager.tileTypes[5].tileVisualPrefab, doorCoords, Quaternion.identity);
            }
            rightdoors = GameObject.FindGameObjectsWithTag("doorRightClosed");
            foreach (GameObject rightdoor in rightdoors)
            {
                doorCoords = rightdoor.transform.position;
                Destroy(rightdoor);
                GameObject door = (GameObject)Instantiate(gameManager.tileTypes[7].tileVisualPrefab, doorCoords, Quaternion.identity);
            }
            bottomdoors = GameObject.FindGameObjectsWithTag("doorBottomClosed");
            foreach (GameObject bottomdoor in bottomdoors)
            {
                doorCoords = bottomdoor.transform.position;
                Destroy(bottomdoor);
                GameObject door = (GameObject)Instantiate(gameManager.tileTypes[9].tileVisualPrefab, doorCoords, Quaternion.identity);
            }
        }
    }
    public void GetRoomTiles(string name, int x, int y)
    {
        //if (name == "") return;
        
        StreamReader reader = new StreamReader(File.OpenRead(@"Assets\Rooms\" + name + ".csv"));
        int count = 0;
        List<string> list = new List<string>();
        string[] l = new string[gameManager.roomSizeX];

        //kinda awkward way to get it into an array, I'll probably change this later
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            if (!string.IsNullOrWhiteSpace(line))
            {
                string[] values = line.Split(',');
                for(int i = 0; i < gameManager.roomSizeX; i++)
                {
                    list.Add(values[i]);
                }
            }
            l = list.ToArray();

            for(int j = 0; j < gameManager.roomSizeX; j++)
            {
                gameManager.tiles[x+j, y+gameManager.roomSizeY - count] = System.Int32.Parse(l[j]);
            }
            count++;
            list.Clear();
        }
       

    }

    void GenerateFloorLayout()
    {
        //gameManager.tiles = new int[gameManager.mapSizeX, gameManager.mapSizeY];

        //first floor should have 5-10 rooms, last floor should have 20-25
        int numRooms = gameManager.floorNumber * 5 + UnityEngine.Random.Range(0, 5);
        int mapWidth = gameManager.mapSizeX / gameManager.roomSizeX;
        int mapHeight = gameManager.mapSizeY / gameManager.roomSizeY;
        int[,] roomMap = new int[mapWidth, mapHeight];


        List<Vector2> roomCoords = new List<Vector2>();
        numRooms--;
        //starting position
        roomMap[mapWidth / 2, mapHeight / 2] = 1;
        roomCoords.Add(new Vector2((mapWidth/2) * gameManager.roomSizeX, (mapHeight/2) * gameManager.roomSizeY));


        //now, choose rooms at random, check if they have any open neighbors, and check if those neighbors don't have too many neighbors
        while(numRooms > 0)
        {
            int roomSelection = UnityEngine.Random.Range(0, roomCoords.Count());
            Vector2 selectionCoords = roomCoords.ElementAt(roomSelection);
            int x = (int)(selectionCoords.x / gameManager.roomSizeX);
            int y = (int)(selectionCoords.y / gameManager.roomSizeY);
            int numNeighbors = 0;

            //first level of neighbors
            int connectionSelection = UnityEngine.Random.Range(0, 3);
            switch (connectionSelection)
            {
                case 0:
                    //we're checking x+1, y
                    x++;
                    break;
                case 1:
                    //we're checking x, y+1
                    y++;
                    break;
                case 2:
                    //we're checking x-1, y
                    x--;
                    break;
                case 3:
                    //we're checking x, y-1
                    y--;
                    break;

                default:
                    break;
            }
            //first check that this isn't taken or outside the range
            if (x >= mapHeight || x <= -1 || y >= mapWidth || y <= 0 || roomMap[x, y] == 1) continue;

            //otherwise check that it doesn't have too many neighbors/map boundaries
            if (x == mapHeight-1 || roomMap[x + 1, y] == 1) numNeighbors++;
            if (y == mapWidth-1 || roomMap[x, y + 1] == 1) numNeighbors++;
            if (x == 0 || roomMap[x - 1, y] == 1) numNeighbors++;
            if (y == 0 || roomMap[x, y - 1] == 1) numNeighbors++;

            
            if (numNeighbors < 2)
            {
                roomMap[x, y] = 1;
                numRooms--;
                roomCoords.Add(new Vector2(x * gameManager.roomSizeX, y * gameManager.roomSizeY));
            }

        }

        
        //at this point, we've filled the array with rooms. Now we want to figure out what types of rooms they are
        string roomName = "";
        bool firstRoom = true;
        //so, check which sides have connections for each room
        foreach (Vector2 coord in roomCoords)
        {
            
            //have to check first to avoid index out of range exceptions
            //y+1 for top
            if (coord.y + gameManager.roomSizeY <= gameManager.mapSizeY)
                if (roomMap[(int)(coord.x) / gameManager.roomSizeX, (int)(coord.y + gameManager.roomSizeY) / gameManager.roomSizeY] == 1) roomName = roomName + "T";

            //x-1 for left
            if (coord.x - gameManager.roomSizeX >= 0)
                if (roomMap[(int)(coord.x - gameManager.roomSizeX) / gameManager.roomSizeX, (int)(coord.y) / gameManager.roomSizeY] == 1) roomName = roomName + "L";

            //x+1 for right
            if (coord.x + gameManager.roomSizeX <= gameManager.mapSizeX)
                if (roomMap[(int)(coord.x + gameManager.roomSizeX) / gameManager.roomSizeX, (int)(coord.y) / gameManager.roomSizeY] == 1) roomName = roomName + "R";

            //y-1 for bottom
            if (coord.y - gameManager.roomSizeY >= 0)
                if (roomMap[(int)(coord.x) / gameManager.roomSizeX, (int)(coord.y - gameManager.roomSizeY) / gameManager.roomSizeY] == 1) roomName = roomName + "B";

           
            //now we have the connections, so we set up rooms at these coordinates
            GetRoomTiles(roomName,(int)coord.x, (int)coord.y);
            if (!firstRoom)
                gameManager.clearedRooms[(int)(coord.x) / gameManager.roomSizeX, (int)(coord.y) / gameManager.roomSizeY] = 1;
            else
                firstRoom = false;
            roomName = "";
        }

        player.GetComponent<Transform>().position = new Vector3(roomCoords[0].x+gameManager.roomSizeX/2, roomCoords[0].y+gameManager.roomSizeY/2, -1);
    }
    public void SpawnEnemies(int roomX, int roomY)
    {
        
        if (gameManager.clearedRooms[roomX / gameManager.roomSizeX, roomY / gameManager.roomSizeY] == 0)
           return;
        //select enemy type
        int enemySelection = UnityEngine.Random.Range(0, 0);

        //select random number of enemies to spawn
        int numEnemies = UnityEngine.Random.Range(1, 2) + UnityEngine.Random.Range(0, gameManager.floorNumber);
        gameManager.enemiesAlive = numEnemies;
        //instantiate them towards the center of the room
        for(int i = 0; i < numEnemies; i++)
        {
            int enemyX = UnityEngine.Random.Range(3, 8);
            int enemyY = UnityEngine.Random.Range(3, 8);

            GameObject enemy = (GameObject)Instantiate(gameManager.enemyTypes[enemySelection], new Vector3(roomX + enemyX, roomY + enemyY, -1), Quaternion.identity);
        }
    }

    public void GenerateMapVisual()
    {
      for (int x = 0; x < gameManager.mapSizeX; x++)
      {
          for (int y = 0; y < gameManager.mapSizeY; y++)
          {
                if (gameManager.tiles[x, y] == -1) continue;
              TileType tt = gameManager.tileTypes[gameManager.tiles[x,y]];
              GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);

              // If tile is a wall
              if(tt.isWalkable==false)
              {
                go.AddComponent<BoxCollider2D>();
              }
          }
      }
    }

    public static Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, y, 0);
    }

    /*
    public void InstantiatePlayer()
    {
        Vector3 position = new Vector3(0, 0, -.75f);
        gameManager.player = Instantiate(gameManager.player, position, Quaternion.identity) as GameObject;
    }*/
    
}
