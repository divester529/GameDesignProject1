using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System;

public class MapGenerator : MonoBehaviour
{
  private GameManager gameManager;

    private void Start()
    {
      gameManager=GameManager.Instance;

        GenerateFloorLayout();
        GenerateMapVisual();
        //InstantiatePlayer();

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
        //and it's upside down, here's a lazy fix

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
            roomName = "";
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


    /*public void InstantiatePlayer()
    {
        Vector3 position = new Vector3(0, 0, -.75f);
        gameManager.player = Instantiate(gameManager.player, position, Quaternion.identity) as GameObject;
    }
    */
}
