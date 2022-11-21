using TKOU.SimAI.Interfaces;
using UnityEngine;

namespace TKOU.SimAI.Levels.Tiles
{
    /// <summary>
    /// Visualisation of the <see cref="Tile"/> object.
    /// </summary>
    public class TileEntity : MonoBehaviour, IAmEntity, IHavePosition
    {
        #region Properties

        public Tile Tile
        {
            get;
            private set;
        }

        public Vector3 Position
        {
            get
            {
                return transform.position;
            }

            private set
            {
                transform.position = value;
            }
        }

        #endregion Properties

        #region Public methods

        public static TileEntity SpawnTileEntity(Tile ownerTile)
        {
            TileData tileData = ownerTile.TileData;

            if (tileData == null)
            {
                Debug.LogError($"Can't spawn a tile, {nameof(tileData.TileEntityPrefab)} is null!");

                return null;
            }

            TileEntity tileEntity = GameObject.Instantiate(tileData.TileEntityPrefab);

            tileEntity.Initialize(ownerTile);

            return tileEntity;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public override string ToString()
        {
            return Tile.ToString();
        }

        #endregion Public methods

        #region Private methods

        private void Initialize(Tile tile)
        {
            Tile = tile;

            Position = tile.Position;
        }

        #endregion Private methods
    }
}