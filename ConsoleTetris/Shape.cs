using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris
{
    /// <summary>
    /// Base class for Shape.
    /// </summary>
    class Shape
    {
        #region Fields

        // The position of the Shape.
        private Position position;

        // The map of the Shaape.
        private char[,] shapeMap;

        // The type of the Shape.
        private ShapeTypes shapeType;

        // The types of the Shape.
        public enum ShapeTypes
        {
            I,
            J,
            L,
            O,
            S,
            T,
            Z
        }

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Position Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Gets or sets the shapeMap.
        /// </summary>
        /// <value>
        /// The shapeMap.
        /// </value>
        public char[,] ShapeMap
        {
            get { return shapeMap; }
            set { shapeMap = value; }
        }

        /// <summary>
        /// Gets or sets the shapeType.
        /// </summary>
        /// <value>
        /// The shapeType.
        /// </value>
        public ShapeTypes ShapeType
        {
            get { return shapeType; }
            set { shapeType = value; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Shape"/> class.
        /// </summary>
        public Shape()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shape"/> class.
        /// </summary>
        public Shape(Position position, ShapeTypes shapetype)
        {
            this.Position = position;
            this.ShapeType = shapetype;

            switch (ShapeType)
            {
                case ShapeTypes.I:
                    this.shapeMap = new char[4, 4]
                    {
                        { ' ', 'I', ' ', ' ' },
                        { ' ', 'I', ' ', ' ' },
                        { ' ', 'I', ' ', ' ' },
                        { ' ', 'I', ' ', ' ' }
                    };
                    break;
                case ShapeTypes.J:
                    this.shapeMap = new char[4, 4]
                    {
                        { 'J', ' ', ' ', ' ' },
                        { 'J', 'J', 'J', ' ' },
                        { ' ', ' ', ' ', ' ' },
                        { ' ', ' ', ' ', ' ' }
                    };
                    break;
                case ShapeTypes.L:
                    this.shapeMap = new char[4, 4]
                    {
                        { ' ', ' ', ' ', ' ' },
                        { 'L', 'L', 'L', ' ' },
                        { 'L', ' ', ' ', ' ' },
                        { ' ', ' ', ' ', ' ' }
                    };
                    break;
                case ShapeTypes.O:
                    this.shapeMap = new char[4, 4]
                    {
                        { ' ', 'O', 'O', ' ' },
                        { ' ', 'O', 'O', ' ' },
                        { ' ', ' ', ' ', ' ' },
                        { ' ', ' ', ' ', ' ' }
                    };
                    break;
                case ShapeTypes.S:
                    this.shapeMap = new char[4, 4]
                    {
                        { ' ', ' ', ' ', ' ' },
                        { ' ', 'S', 'S', ' ' },
                        { 'S', 'S', ' ', ' ' },
                        { ' ', ' ', ' ', ' ' }
                    };
                    break;
                case ShapeTypes.T:
                    this.shapeMap = new char[4, 4]
                    {
                        { ' ', ' ', ' ', ' ' },
                        { 'T', 'T', 'T', ' ' },
                        { ' ', 'T', ' ', ' ' },
                        { ' ', ' ', ' ', ' ' }
                    };
                    break;
                case ShapeTypes.Z:
                    this.shapeMap = new char[4, 4]
                    {
                        { ' ', ' ', ' ', ' ' },
                        { 'Z', 'Z', ' ', ' ' },
                        { ' ', 'Z', 'Z', ' ' },
                        { ' ', ' ', ' ', ' ' }
                    };
                    break;
            }
        }

        #endregion Constructors

        #region Methods

        public void Rotate(ref char[,] map, int mapWidth, int mapHeight)
        {
            try
            {
                #region AllMaps

                char[,] shapeIMap1 = new char[4, 4]
                {
                    { ' ', 'I', ' ', ' ' },
                    { ' ', 'I', ' ', ' ' },
                    { ' ', 'I', ' ', ' ' },
                    { ' ', 'I', ' ', ' ' }
                };

                char[,] shapeIMap2 = new char[4, 4]
                {
                    { ' ', ' ', ' ', ' ' },
                    { 'I', 'I', 'I', 'I' },
                    { ' ', ' ', ' ', ' ' },
                    { ' ', ' ', ' ', ' ' }
                };

                char[,] shapeTMap1 = new char[4, 4]
                {
                    { ' ', ' ', ' ', ' ' },
                    { 'T', 'T', 'T', ' ' },
                    { ' ', 'T', ' ', ' ' },
                    { ' ', ' ', ' ', ' ' }
                };

                char[,] shapeTMap2 = new char[4, 4]
                {
                    { ' ', 'T', ' ', ' ' },
                    { 'T', 'T', ' ', ' ' },
                    { ' ', 'T', ' ', ' ' },
                    { ' ', ' ', ' ', ' ' }
                };

                char[,] shapeTMap3 = new char[4, 4]
                {
                    { ' ', 'T', ' ', ' ' },
                    { 'T', 'T', 'T', ' ' },
                    { ' ', ' ', ' ', ' ' },
                    { ' ', ' ', ' ', ' ' }
                };

                char[,] shapeTMap4 = new char[4, 4]
                {
                    { ' ', 'T', ' ', ' ' },
                    { ' ', 'T', 'T', ' ' },
                    { ' ', 'T', ' ', ' ' },
                    { ' ', ' ', ' ', ' ' }
                };

                char[,] shapeLMap1 = new char[4, 4]
                {
                    { ' ', ' ', ' ', ' ' },
                    { 'L', 'L', 'L', ' ' },
                    { 'L', ' ', ' ', ' ' },
                    { ' ', ' ', ' ', ' ' }
                };

                char[,] shapeLMap2 = new char[4, 4]
                {
                    { 'L', 'L', ' ', ' ' },
                    { ' ', 'L', ' ', ' ' },
                    { ' ', 'L', ' ', ' ' },
                    { ' ', ' ', ' ', ' ' }
                };

                char[,] shapeLMap3 = new char[4, 4]
                {
                    { ' ', ' ', 'L', ' ' },
                    { 'L', 'L', 'L', ' ' },
                    { ' ', ' ', ' ', ' ' },
                    { ' ', ' ', ' ', ' ' }
                };

                char[,] shapeLMap4 = new char[4, 4]
                {
                    { ' ', 'L', ' ', ' ' },
                    { ' ', 'L', ' ', ' ' },
                    { ' ', 'L', 'L', ' ' },
                    { ' ', ' ', ' ', ' ' }
                };

                char[,] shapeJMap1 = new char[4, 4]
                {
                    { 'J', ' ', ' ', ' ' },
                    { 'J', 'J', 'J', ' ' },
                    { ' ', ' ', ' ', ' ' },
                    { ' ', ' ', ' ', ' ' }
                };

                char[,] shapeJMap2 = new char[4, 4]
                {
                    { ' ', 'J', 'J', ' ' },
                    { ' ', 'J', ' ', ' ' },
                    { ' ', 'J', ' ', ' ' },
                    { ' ', ' ', ' ', ' ' }
                };

                char[,] shapeJMap3 = new char[4, 4]
                {
                    { ' ', ' ', ' ', ' ' },
                    { 'J', 'J', 'J', ' ' },
                    { ' ', ' ', 'J', ' ' },
                    { ' ', ' ', ' ', ' ' }
                };

                char[,] shapeJMap4 = new char[4, 4]
                {
                    { ' ', 'J', ' ', ' ' },
                    { ' ', 'J', ' ', ' ' },
                    { 'J', 'J', ' ', ' ' },
                    { ' ', ' ', ' ', ' ' }
                };

                char[,] shapeZMap1 = new char[4, 4]
                {
                    { ' ', ' ', ' ', ' ' },
                    { 'Z', 'Z', ' ', ' ' },
                    { ' ', 'Z', 'Z', ' ' },
                    { ' ', ' ', ' ', ' ' }
                };

                char[,] shapeZMap2 = new char[4, 4]
                {
                    { ' ', ' ', 'Z', ' ' },
                    { ' ', 'Z', 'Z', ' ' },
                    { ' ', 'Z', ' ', ' ' },
                    { ' ', ' ', ' ', ' ' }
                };

                char[,] shapeSMap1 = new char[4, 4]
                {
                    { ' ', ' ', ' ', ' ' },
                    { ' ', 'S', 'S', ' ' },
                    { 'S', 'S', ' ', ' ' },
                    { ' ', ' ', ' ', ' ' }
                };

                char[,] shapeSMap2 = new char[4, 4]
                {
                    { ' ', 'S', ' ', ' ' },
                    { ' ', 'S', 'S', ' ' },
                    { ' ', ' ', 'S', ' ' },
                    { ' ', ' ', ' ', ' ' }
                };

                char[,] shapeOMap1 = new char[4, 4]
                {
                    { ' ', 'O', 'O', ' ' },
                    { ' ', 'O', 'O', ' ' },
                    { ' ', ' ', ' ', ' ' },
                    { ' ', ' ', ' ', ' ' }
                };

                #endregion AllMaps

                switch (ShapeType)
                {
                    case ShapeTypes.I:
                        if (ShapeMap == shapeIMap1)
                        {
                            ShapeMap = shapeIMap2;
                        }
                        else if (ShapeMap == shapeIMap2)
                        {
                            ShapeMap = shapeIMap1;
                        }
                        break;
                    case ShapeTypes.J:
                        if (ShapeMap == shapeJMap1)
                        {
                            ShapeMap = shapeJMap2;
                        }
                        else if (ShapeMap == shapeJMap2)
                        {
                            ShapeMap = shapeJMap3;
                        }
                        else if (ShapeMap == shapeJMap3)
                        {
                            ShapeMap = shapeJMap4;
                        }
                        else if (ShapeMap == shapeJMap4)
                        {
                            ShapeMap = shapeJMap1;
                        }
                        break;
                    case ShapeTypes.L:
                        if (ShapeMap == shapeLMap1)
                        {
                            ShapeMap = shapeLMap2;
                        }
                        else if (ShapeMap == shapeLMap2)
                        {
                            ShapeMap = shapeLMap3;
                        }
                        else if (ShapeMap == shapeLMap3)
                        {
                            ShapeMap = shapeLMap4;
                        }
                        else if (ShapeMap == shapeLMap4)
                        {
                            ShapeMap = shapeLMap1;
                        }
                        break;
                    case ShapeTypes.O:
                        break;
                    case ShapeTypes.S:
                        if (ShapeMap == shapeSMap1)
                        {
                            ShapeMap = shapeSMap2;
                        }
                        else if (ShapeMap == shapeSMap2)
                        {
                            ShapeMap = shapeSMap1;
                        }
                        break;
                    case ShapeTypes.T:
                        if (ShapeMap == shapeTMap1)
                        {
                            ShapeMap = shapeTMap2;
                        }
                        else if (ShapeMap == shapeTMap2)
                        {
                            ShapeMap = shapeTMap3;
                        }
                        else if (ShapeMap == shapeTMap3)
                        {
                            ShapeMap = shapeTMap4;
                        }
                        else if (ShapeMap == shapeTMap4)
                        {
                            ShapeMap = shapeTMap1;
                        }
                        break;
                    case ShapeTypes.Z:
                        if (ShapeMap == shapeZMap1)
                        {
                            ShapeMap = shapeZMap2;
                        }
                        else if (ShapeMap == shapeZMap2)
                        {
                            ShapeMap = shapeZMap1;
                        }
                        break;
                }
            }
            catch
            { }
        }

        public void Descend()
        {
            try
            {

            }
            catch
            { }
        }

        public void Reposition(Position newposition)
        {
            try
            {
                
            }
            catch
            { }
        }

        #endregion Methods
    }
}
