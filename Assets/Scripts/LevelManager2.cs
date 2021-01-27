using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor.Tilemaps;

public class LevelManager2 : MonoBehaviour{

    public TileBase tile;
    public Tilemap mainmap;

    // Start is called before the first frame update
    void Start()
    {
        int[,] mapArrayTop = GenerateArray(10, 6, true);
        //int[,] mapArrayBottom = GenerateArray(10, 6, true);
        int[,] newMapArrTop = RandomWalkTop(mapArrayTop, 3343f);
        //int[,] newMapArrBot = RandomWalkBottom(mapArrayBottom, 3343f);
        RenderMap(newMapArrTop, mainmap, tile);
        //RenderMap(newMapArrBot, mainmap, tile);
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

    public static int[,] RandomWalkTop(int[,] map, float seed){
        //Generate random seed
        System.Random rand = new System.Random(seed.GetHashCode()); 
        //Find a random starting height (Might add bottom pref)
        int lastHeight = Random.Range(0, map.GetUpperBound(1));
        
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
        int lastHeight = Random.Range(0, map.GetUpperBound(1));
        
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

            //Circle through from the lastheight to the top
            for (int y = lastHeight; y <= map.GetUpperBound(1); y++) {
                map[x, y] = 1;
            }
        }
        //Return the map
        return map; 
    }
}
