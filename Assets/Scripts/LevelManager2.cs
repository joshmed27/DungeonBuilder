using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor.Tilemaps;

public class LevelManager2 : MonoBehaviour{

    //Tile and map asset for easy access
    [SerializeField] private Tile tile;
    [SerializeField] private Tile playerSpawn;
    [SerializeField] private Tile bossRoom;
    [SerializeField] private Tilemap mainmap;
    private static int width = 20;
    private static int height = 10;
    private static int halfway = height/2;

    void Start()
    {
        //generate array of the map (width, height, empty?)
        int[,] mapArray = GenerateArray(width, height, true);

        //Create noise for top layer
        mapArray = RandomWalkTop(mapArray, Random.Range(-1000.0f, 1000.0f));
        //clear the bottom for bottom layer noise
        mapArray = ClearBottom(mapArray);
        //Create bottom layer noise
        mapArray = RandomWalkBottom(mapArray, Random.Range(-1000.0f, 1000.0f));
        //render
        RenderMap(mapArray, mainmap, tile);
        //mapArray = DeleteRandomTiles(mapArray, Random.Range(-1000.0f, 1000.0f), Random.Range(-1000.0f, 1000.0f));
        GenerateEntrance (mapArray);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Generates an array to begin filling with room tiles
    public static int[,] GenerateArray(int width, int height, bool empty){
        int[,] map = new int[width, height];
        for (int x = 0; x < map.GetUpperBound(0); x++){
            for (int y = 0; y < map.GetUpperBound(1); y++){
                if (empty){
                    map[x, y] = 0;
                }
                else{
                    map[x, y] = 1;
                }
            }
        }
        return map;
    }

    public static void RenderMap(int[,] map, Tilemap tilemap, TileBase tile){
        //Clear the map (ensures we dont overlap)
        tilemap.ClearAllTiles(); 
        //Loop through the width of the map
        for (int x = 0; x < map.GetUpperBound(0) ; x++){
            //Loop through the height of the map
            for (int y = 0; y < map.GetUpperBound(1); y++){
                // 1 = tile, 0 = no tile
                if (map[x, y] == 1){
                    tilemap.SetTile(new Vector3Int(x, y, 0), tile); 
                }
            }
        }
    }

    //Update feature for less resource heavy renders, takes in our map and tilemap, setting null tiles where needed
    public static void UpdateMap(int[,] map, Tilemap tilemap) {
        for (int x = 0; x < map.GetUpperBound(0); x++){
            for (int y = 0; y < map.GetUpperBound(1); y++){
                if (map[x, y] == 0){
                    tilemap.SetTile(new Vector3Int(x, y, 0), null);
                }
            }
        }
    }

    public static int[,] ClearBottom(int[,] map) {
        for (int x = 0; x < map.GetUpperBound(0); x++){
            for (int y = 0; y <= halfway; y++){
                if (map[x, y] == 1){
                    map[x,y] = 0;
                }
            }
        }
        return map;
    }

    public static int[,] RandomWalkTop(int[,] map, float seed){
        //Generate random seed
        System.Random rand = new System.Random(seed.GetHashCode()); 

        //Find a random starting height
        int lastHeight = Random.Range(halfway, map.GetUpperBound(1));
        
        //Cycling through widths to generate random heights
        for (int x = 0; x < map.GetUpperBound(0); x++) {
            //Flip a coin (0 heads, 1 tails)
            int nextMove = rand.Next(2);

            //If heads, and we aren't near the bottom, minus some height
            if (nextMove == 0 && lastHeight > 2) {
              lastHeight--;
            }
            //If tails, and we aren't near the top, add some height
            else if (nextMove == 1 && lastHeight < map.GetUpperBound(1) - 2) {
                lastHeight++;
            }

            //Circle through from the lastheight to the bottom
            for (int y = lastHeight; y >= 0; y--) {
                map[x, y] = 1;
            }
        }
        //Return the map
        return map; 
    }

    public static int[,] RandomWalkBottom(int[,] map, float seed){
        //Generate random seed
        System.Random rand = new System.Random(seed.GetHashCode()); 
        //Find a random starting height (Might add bottom pref)
        int lastHeight = Random.Range(0, halfway);
        
        //Cycling through widths to generate random heights
        for (int x = 0; x < map.GetUpperBound(0); x++) {
            //Flip a coin (0 heads, 1 tails)
            int nextMove = rand.Next(2);

            //If heads, and we aren't near the bottom, minus some height
            if (nextMove == 0 && lastHeight > 2) {
                lastHeight--;
            }
            //If tails, and we aren't near the top, add some height
            else if (nextMove == 1 && lastHeight < halfway - 2) {
                lastHeight++;
            }

            //Circle through from the lastheight to the top
            for (int y = lastHeight; y <= halfway; y++) {
                map[x, y] = 1;
            }
        }
        //Return the map
        return map; 
    }

    //Let's make it fun :0
    public static int[,] DeleteRandomTiles (int[,] map, float seed, float seed2) {
        //Generate random seed
        System.Random rand = new System.Random(seed.GetHashCode());
        System.Random rand2 = new System.Random(seed2.GetHashCode());

        //Cycling through widths to generate random nulls
        for (int x=0; x < map.GetUpperBound(0); x++) {
            for(int y=0; y < map.GetUpperBound(1); y++) {
                //Flip a coin (0 heads, 1 tails)
                int nextMove = rand.Next(2);
                int nextMove2 = rand2.Next(2);

                //If heads, and we aren't near the bottom, minus some height
                if (nextMove == 0 && nextMove2 == 0) {
                   map[x, y] = 0;
                }
                //If tails, and we aren't near the top, add some height
                else if (nextMove == 1 && nextMove2 == 1) {
                    map[x, y] = 0;
                }
            }
        }
        //Return the map
        return map; 
    }
    
    public void GenerateEntrance (int[,] mapArray) {
        for(int y=0; y < mapArray.GetUpperBound(1); y++) {
            if (mapArray[0,y] == 1) {
                mainmap.SetTile(new Vector3Int(0, y, 0), playerSpawn);
                break;
            }
        }
    }
    
 }
