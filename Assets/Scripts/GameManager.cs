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
        tiles = new int[mapSizeX, mapSizeY];
        tileTypes = new TileType[2];

        //player = Resources.Load<GameObject>("player");

        //i was getting a null reference exception
        TileType yeet1 = new TileType();
        TileType yeet2 = new TileType();

        yeet1.name = "FloorTile";
        yeet1.tileVisualPrefab = Resources.Load<GameObject>("FloorTile");
        yeet1.movementCost = 1;
        yeet1.isWalkable = true;
        tileTypes[0] = yeet1;
        yeet2.name = "WallTile";
        yeet2.tileVisualPrefab = Resources.Load<GameObject>("WallTile");
        yeet2.movementCost = 1;
        yeet2.isWalkable = false;
        tileTypes[1] = yeet2;
    }
}
