using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IceBehaviour : MonoBehaviour {

    [SerializeField] TileBase tileA;
    [SerializeField] TileBase tileB;
    [SerializeField] string tilemapName;
    [SerializeField] LayerMask m_layerMask;

    private Tilemap tilemap;
    void Start()
    {
        tilemap = GameObject.Find(tilemapName).GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        var hit = Physics2D.Linecast(this.transform.position, collider.transform.position, m_layerMask);
        if (hit) {
            Freezing();
        }
    }

    private void Freezing() {
        tilemap.SwapTile(tileA, tileB);
    }

}


