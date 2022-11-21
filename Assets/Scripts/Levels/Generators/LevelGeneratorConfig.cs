using System;
using UnityEngine;

namespace TKOU.SimAI.Levels.Generators
{
    /// <summary>
    /// Configuration to be used by the <see cref="LevelGeneratorBasicLogic"/>
    /// </summary>
    [Serializable]
    public class LevelGeneratorConfig
    {
        #region Properties

        /// <summary>
        /// Single map tile size.
        /// </summary>
        public Vector3 TileSize
        {
            get;
            private set;
        }

        /// <summary>
        /// Single map tile offset.
        /// </summary>
        public Vector3 TileOffset
        {
            get;
            private set;
        }

        /// <summary>
        /// Map size.
        /// </summary>
        public int Width
        {
            get;
            private set;
        }

        /// <summary>
        /// Map size.
        /// </summary>
        public int Height
        {
            get;
            private set;
        }

        /// <summary>
        /// Size of level in units.
        /// </summary>
        public Vector2 SizeInUnits
        {
            get
            {
                return new Vector2(TileSize.x * Width, TileSize.z * Height);
            }
        }

        /// <summary>
        /// Position where the level should start, in units.
        /// </summary>
        public Vector2 LevelMinPosition
        {
            get
            {
                return new Vector2(TileOffset.x, TileOffset.z);
            }
        }

        /// <summary>
        /// Position where the level ends, in units.
        /// </summary>
        public Vector2 LevelMaxPosition
        {
            get
            {
                return LevelMinPosition + SizeInUnits;
            }
        }

        #endregion Properties

        #region Constructors

        public LevelGeneratorConfig(Vector3 tileSize, Vector3 tileOffset, int width, int height)
        {
            TileSize = tileSize;

            TileOffset = tileOffset;

            Width = width;

            Height = height;
        }

        #endregion Constructors
    }
}