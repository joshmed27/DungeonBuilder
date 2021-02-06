using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeWalls : MonoBehaviour{

    [SerializeField] private Tilemap mainmap;
    [SerializeField] private Grid grid;
    //[SerializeField] private Button wallBreak;

    // Update is called once per frame
    void Update(){
        /*
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cell= mainmap.WorldToCell(new Vector3(pos.x, pos.y));
        TileBase tile = mainmap.GetTile(cell);

        Vector3 worldPos = grid.CellToWorld(cell); // go from cell-space to world-space
        var screenPoint = Camera.main.WorldToScreenPoint(worldPos); // go from world-space to screen-space

        RectTransform tr = (myBreak.transform as RectTransform);
        tr.position = screenPoint;
        */
    }
}
