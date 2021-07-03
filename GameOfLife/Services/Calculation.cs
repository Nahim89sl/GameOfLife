using GameOfLife.Models;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Services
{
    public class Calculation
    {
        #region Private fields

        private readonly int _heigth;
        private readonly int _width;
        private List<List<AliveSquare>> _gridList;

        #endregion

        #region Constructor

        public Calculation(List<List<AliveSquare>> gridList)
        {
            if (gridList != null)
            {
                _heigth = gridList.Count;
                if (gridList.Any())
                {
                    _width = gridList.First().Count;
                }
                _gridList = gridList;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Change generation accoding rules
        /// </summary>
        public void Calculate()
        {
            var gridArray = new bool[_heigth, _width];

            for (int x = 0; x < _heigth; x++)
            {
                for (int y = 0; y < _width; y++)
                {
                    int numOfAliveNeighbors = GetNeighbors(x, y);

                    if (_gridList.ElementAt(x).ElementAt(y).IsSelected)
                    {
                        //a live cell with zero or one live neighbors will die
                        if (numOfAliveNeighbors <= 1)
                        {
                            gridArray[x, y] = false; 
                        }

                        //a live cell with two or three live neighbors will remain alive
                        if ((numOfAliveNeighbors == 3) || (numOfAliveNeighbors == 2))
                        {
                            gridArray[x, y] = true;  // Just for watching
                        }

                        //a live cell with four or more live neighbors will die 
                        if (numOfAliveNeighbors > 3)
                        {
                            gridArray[x, y] = false; // Just for watching
                        }
                    }
                    else
                    {
                        //a dead cell with exactly tree live neighbors becomes alive
                        if (numOfAliveNeighbors == 3)
                        {
                            gridArray[x, y] = true;
                        }
                        //in all other cases a dead cell will stay dead
                    }
                }
            }

            ConvertToGridList(gridArray);
        }

        /// <summary>
        /// Clear all space
        /// </summary>
        public void ClearSpace()
        {
            ConvertToGridList(new bool[_heigth, _width]);
        }

        /// <summary>
        /// Count how many alive squares
        /// </summary>
        /// <returns>Count of alive squares</returns>
        public int CountAlive()
        {
            int count = 0;
            for (int x = 0; x < _heigth; x++)
            {
                for (int y = 0; y < _width; y++)
                {
                    if (_gridList.ElementAt(x).ElementAt(y).IsSelected)
                        count++;
                }
            }
            return count;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Mapping array values to grid
        /// </summary>
        /// <param name="gridArray">New generation array</param>
        private void ConvertToGridList(bool[,] gridArray)
        {
            for (int x = 0; x < _heigth; x++)
            {
                for (int y = 0; y < _width; y++)
                {
                    _gridList.ElementAt(x).ElementAt(y).IsSelected = gridArray[x, y];
                }
            }
        }

        /// <summary>
        /// Checks how many alive neighbors.
        /// </summary>
        /// <param name="x">X-coordinate of the cell.</param>
        /// <param name="y">Y-coordinate of the cell.</param>
        /// <returns>The number of alive neighbors.</returns>
        private int GetNeighbors(int x, int y)
        {
            int counterAliveNeighbors = 0;

            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (!((i < 0 || j < 0) || (i >= _heigth || j >= _width) || (x == i && y == j)))
                    {
                        if (_gridList.ElementAt(i).ElementAt(j).IsSelected)
                            counterAliveNeighbors++;
                    }
                }
            }
            return counterAliveNeighbors;
        }

        #endregion

    }
}
