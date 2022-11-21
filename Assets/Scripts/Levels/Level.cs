using System;
using System.Collections.Generic;
using TKOU.SimAI.Interfaces;
using TKOU.SimAI.Levels.Generators;
using TKOU.SimAI.Levels.Tiles;
using UnityEngine;

namespace TKOU.SimAI.Levels
{
    /// <summary>
    /// Current level that contains all data.
    /// </summary>
    public class Level : IDisposable
    {
        #region Properties

        /// <summary>
        /// generators that were used to create this level.
        /// </summary>

        public List<IAmLevelGenerator> UsedLevelGenerators
        {
            get;
            private set;
        }

        /// <summary>
        /// All level entities.
        /// </summary>
        public List<IAmEntity> Entities
        {
            get;
            private set;
        }

        /// <summary>
        /// Tiles that make this level.
        /// </summary>
        public Tile[] Tiles
        {
            get;
            private set;
        }

        /// <summary>
        /// Config that is used to generate this level.
        /// </summary>
        public LevelGeneratorConfig LevelGeneratorConfig
        {
            get;
            private set;
        }

        #endregion Properties

        #region Constructors

        public Level(LevelGeneratorConfig levelGeneratorConfig)
        {
            UsedLevelGenerators = new List<IAmLevelGenerator>();

            Entities = new List<IAmEntity>();

            LevelGeneratorConfig = levelGeneratorConfig;

            Tiles = new Tile[LevelGeneratorConfig.Width * LevelGeneratorConfig.Height];
        }

        #endregion Constructors

        #region Public methods

        public void AddUsedGenerator(IAmLevelGenerator levelGenerator)
        {
            UsedLevelGenerators.Add(levelGenerator);
        }

        public Tile GetTile(int x, int y)
        {
            return Tiles[x + y * LevelGeneratorConfig.Width];
        }

        public Tile GetTile(int index)
        {
            return Tiles[index];
        }

        public void SetTile(int x, int y, TileData tileData)
        {
            Vector3 positionVector = new Vector3(x, 0, y);

            positionVector.Scale(LevelGeneratorConfig.TileSize);

            Tile tile = new Tile(tileData, x, y, positionVector);

            Tiles[x + y * LevelGeneratorConfig.Width] = tile;
        }

        public void SetTile(int index, TileData tileData)
        {
            Vector3 positionVector = new Vector3(index % LevelGeneratorConfig.Width, 0, index / LevelGeneratorConfig.Width);

            positionVector.Scale(LevelGeneratorConfig.TileSize);

            int xIndex = index % LevelGeneratorConfig.Width;
            int yIndex = index / LevelGeneratorConfig.Width;

            Tile tile = new Tile(tileData, xIndex, yIndex, positionVector);

            Tiles[index] = tile;
        }

        public void GenerateEntities()
        {
            for (int i = 0; i < Tiles.Length; i++)
            {
                Entities.Add(TileEntity.SpawnTileEntity(Tiles[i]));
            }
        }

        public void Dispose()
        {
            UsedLevelGenerators.Clear();

            for (int i = 0; i < Entities.Count; i++)
            {
                Entities[i].Destroy();
            }

            Entities.Clear();

            LevelGeneratorConfig = null;

            Tiles = new Tile[0];
        }

        #endregion Public methods
    }
}