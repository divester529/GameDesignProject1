using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject floorTile;
    public GameObject wallTile;
    public TileType[] tileTypes;
    //public GameObject player;

    public int[,] tiles;

    public int roomSizeX = 11;
    public int roomSizeY = 11;
    public Room[,] rooms;
    public int mapSizeX, mapSizeY;
    public List<Vector2> takenPositions = new List<Vector2>();
    public int floorNumber = 1;
    public int enemiesAlive = 0;
    public GameObject[] enemyTypes;
    public int[,] clearedRooms;


    // Handle to the player
    private GameObject player;

    public void setPlayer(GameObject player)
    {
        this.player=player;
    }

    public GameObject getPlayer(){
        return player;
    }

    protected GameManager() { }

    private void Awake()
    {
        //floor 1: map width and height of 7 rooms
        //floor 4: map width and height of 13 rooms
        mapSizeX = roomSizeX * (5 + floorNumber * 2);
        mapSizeY = roomSizeY * (5 + floorNumber * 2);
        tiles = new int[mapSizeX, mapSizeY];
        for (int a = 0; a < mapSizeX; a++)
        {
            for (int b = 0; b < mapSizeY; b++)
            {
                tiles[a, b] = -1;
            }
        }
        clearedRooms = new int[mapSizeX, mapSizeY];
        enemyTypes = new GameObject[3];
        enemyTypes[0] = Resources.Load<GameObject>("Enemy");
        enemyTypes[1] = Resources.Load<GameObject>("Slime");
        enemyTypes[2] = Resources.Load<GameObject>("BigSlime");

        tileTypes = new TileType[10];

        //player = Resources.Load<GameObject>("player");


        TileType tt1 = new TileType();
        TileType tt2 = new TileType();
        TileType tt3 = new TileType();
        TileType tt4 = new TileType();
        TileType tt5 = new TileType();
        TileType tt6 = new TileType();
        TileType tt7 = new TileType();
        TileType tt8 = new TileType();
        TileType tt9 = new TileType();
        TileType tt10 = new TileType();

        tt1.name = "FloorTile";
        tt1.tileVisualPrefab = Resources.Load<GameObject>("FloorTile");
        tt1.movementCost = 1;
        tt1.isWalkable = true;
        tileTypes[0] = tt1;
        tt2.name = "WallTile";
        tt2.tileVisualPrefab = Resources.Load<GameObject>("WallTile");
        tt2.movementCost = 1;
        tt2.isWalkable = false;
        tileTypes[1] = tt2;
        tt3.name = "doorTopClosed";
        tt3.tileVisualPrefab = Resources.Load<GameObject>("doorTopClosed");
        tt3.movementCost = 1;
        tt3.isWalkable = false;
        tileTypes[2] = tt3;
        tt4.name = "doorTopOpen";
        tt4.tileVisualPrefab = Resources.Load<GameObject>("doorTopOpen");
        tt4.movementCost = 1;
        tt4.isWalkable = true;
        tileTypes[3] = tt4;
        tt5.name = "doorLeftClosed";
        tt5.tileVisualPrefab = Resources.Load<GameObject>("doorLeftClosed");
        tt5.movementCost = 1;
        tt5.isWalkable = false;
        tileTypes[4] = tt5;
        tt6.name = "doorLeftOpen";
        tt6.tileVisualPrefab = Resources.Load<GameObject>("doorLeftOpen");
        tt6.movementCost = 1;
        tt6.isWalkable = true;
        tileTypes[5] = tt6;
        tt7.name = "doorRightClosed";
        tt7.tileVisualPrefab = Resources.Load<GameObject>("doorRightClosed");
        tt7.movementCost = 1;
        tt7.isWalkable = false;
        tileTypes[6] = tt7;
        tt8.name = "doorRightOpen";
        tt8.tileVisualPrefab = Resources.Load<GameObject>("doorRightOpen");
        tt8.movementCost = 1;
        tt8.isWalkable = true;
        tileTypes[7] = tt8;
        tt9.name = "doorBottomClosed";
        tt9.tileVisualPrefab = Resources.Load<GameObject>("doorBottomClosed");
        tt9.movementCost = 1;
        tt9.isWalkable = false;
        tileTypes[8] = tt9;
        tt10.name = "doorBottomOpen";
        tt10.tileVisualPrefab = Resources.Load<GameObject>("doorBottomOpen");
        tt10.movementCost = 1;
        tt10.isWalkable = true;
        tileTypes[9] = tt10;

    }
}
