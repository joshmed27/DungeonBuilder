using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tile;
    //Calculates the width and length to account for regular and irregular tiles
    public float TileWidth {
        get {
            return tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        }
    }
    public float TileHeight {
        get {
            return tile.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        }
    }

    // Start is called before the first frame update
    void Start(){
        GenerateLevel();

    }

    // Update is called once per frame
    void Update(){
        
    }

    //Levelmanager generates a level in order to create randomization
    private void GenerateLevel() {

        Vector3 StartPos = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height));

        //A loop creating tile assets and positioning them to form a grid
        for (int y=0; y<5; y++) {
            for (int x=0; x<5; x++) {
                placeTile(x,y,StartPos);
            }
        }
    }

    private void placeTile(int x, int y, Vector3 start){
        GameObject newTile = Instantiate(tile);
        newTile.transform.position = new Vector3(start.x + (TileWidth * x), start.y - (TileHeight * y),0);
    }
}
