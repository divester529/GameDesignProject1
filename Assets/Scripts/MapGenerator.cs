using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class MapGenerator : MonoBehaviour
{
  private GameManager gameManager;

    private void Start()
    {
      gameManager=GameManager.Instance;

        //GenerateFloorLayout();
        GetRoomTiles("TLRB");
        GenerateMapVisual();
        //InstantiatePlayer();

    }
    public void GetRoomTiles(string name)
    {

        gameManager.tiles = new int[gameManager.roomSizeX, gameManager.roomSizeY];

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
                gameManager.tiles[j, count] = System.Int32.Parse(l[j]);
            }
            count++;
            list.Clear();
        }
    }

    void GenerateFloorLayout()
    {
        //first floor should have 5-10 rooms, last floor should have 20-25
        int numRooms = gameManager.floorNumber * 5 + Random.Range(0, 5);
        int mapWidth = gameManager.mapSizeX / gameManager.roomSizeX;
        int mapHeight = gameManager.mapSizeY / gameManager.roomSizeY;
        int[,] roomMap = new int[mapWidth, mapHeight];
        List<Vector2> roomCoords = new List<Vector2>();

        //starting position
        roomMap[mapWidth / 2, mapHeight / 2] = 1;
        roomCoords.Add(new Vector2(mapWidth / 2, mapHeight / 2));

        //now, choose rooms at random, check if they have any open neighbors, and check if those neighbors have only 1 neighbor
        while(numRooms > 0)
        {
            int roomSelection = Random.Range(0, roomCoords.Count());
            Vector2 selectionCoords = roomCoords.ElementAt(roomSelection);
            int x = (int)selectionCoords.x;
            int y = (int)selectionCoords.y;
            int numNeighbors = 0;

            //first level of neighbors
            int connectionSelection = Random.Range(0, 3);
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
            //first check that this isn't taken
            if (roomMap[x, y] == 1) continue;

            //otherwise check that it doesn't have too many neighbors
            if (roomMap[x + 1, y] == 1) numNeighbors++;
            if (roomMap[x, y + 1] == 1) numNeighbors++;
            if (roomMap[x - 1, y] == 1) numNeighbors++;
            if (roomMap[x, y - 1] == 1) numNeighbors++;

            //i want to exit here on failure just to keep it random
            if (numNeighbors > 2) continue;
            else
            {
                roomMap[x, y] = 1;
                numRooms--;
                roomCoords.Add(new Vector2(x, y));
            }

        }

    }
    

    public void GenerateMapVisual()
    {
      for (int x = 0; x < gameManager.roomSizeX; x++)
      {
          for (int y = 0; y < gameManager.roomSizeX; y++)
          {

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
