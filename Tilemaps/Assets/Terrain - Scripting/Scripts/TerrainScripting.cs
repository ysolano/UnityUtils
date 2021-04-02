using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainScripting : MonoBehaviour
{
    #region Attributes
    Tilemap tilemap;
    #endregion

    #region Properties
    public TilesScriptableObject tilesConfiguration;

    #endregion

    #region Private Methods
    #region MonoBehaviour methods

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        FillTilemap();
    }
    #endregion

    /// <summary>
    /// Fills the tilemap with the tiles
    /// </summary>
    void FillTilemap()
    {
        for (int i = 0; i < tilesConfiguration.Tiles.Rows; i++)
        {
            for (int j = 0; j < tilesConfiguration.Tiles.Columns; j++)
            {
                if (tilesConfiguration.Tiles.Data[i].ColumnData[j])
                    tilemap.SetTile(new Vector3Int(j, tilesConfiguration.Tiles.Rows - i - 1, 0), tilesConfiguration.RuleTiles);
            }
        }
    }
    #endregion
}
