using System.Reflection;

namespace BootlegEngine
{
    public class BattleShipFactory
    {
        static readonly Random random = new();
        static readonly int gridSize = 10;

        public static void MutateSomeTilesToShips(List<Tile> tiles)   
        {
            PlaceShips(tiles);
        }

        static void PlaceShips(List<Tile> tiles)
        {
            PlaceShip(tiles, 4, 1);
            PlaceShip(tiles, 3, 2);
            PlaceShip(tiles, 2, 3);
            PlaceShip(tiles, 1, 4);
        }

        static void PlaceShip(List<Tile> tiles, int size, int count)
        {
            for (int i = 0; i < count; i++)
            {
                bool placed = false;
                while (!placed)
                {
                    int index = random.Next(tiles.Count);
                    bool horizontal = random.Next(2) == 0;

                    if (CanPlaceShip(tiles, index, size, horizontal))
                    {
                        MutateTileIntoAShip(tiles, index, size, horizontal);
                        placed = true;
                    }
                }
            }
        }

        static bool CanPlaceShip(List<Tile> tiles, int startIndex, int size, bool horizontal)
        {
            for (int i = 0; i < size; i++)
            {
                int tileIndex = horizontal ? startIndex + i : startIndex + i * gridSize;

                // Check if the tile is out of bounds
                if (tileIndex < 0 || tileIndex >= tiles.Count)
                {
                    return false;
                }

                // Check if the tile is already occupied by another ship
                if (tiles[tileIndex].reward == 1)
                {
                    return false;
                }

                // Check neighboring tiles for collisions
                List<int> neighbors = GetNeighbors(tileIndex);
                foreach (int neighborIndex in neighbors)
                {
                    if (neighborIndex >= 0 && neighborIndex < tiles.Count && tiles[neighborIndex].reward == 1)
                    {
                        return false; // Collision with neighboring ship
                    }
                }
            }

            return true;
        }

        static void MutateTileIntoAShip(List<Tile> tiles, int startIndex, int size, bool horizontal)
        {
            for (int i = 0; i < size; i++)
            {
                int tileIndex = horizontal ? startIndex + i : startIndex + i * gridSize;
                tiles[tileIndex].reward = 1;
            }
        }

        static List<int> GetNeighbors(int index)
        {
            List<int> neighbors = new List<int>();

            int row = index / gridSize;
            int col = index % gridSize;

            // Add top, bottom, left, and right neighbors
            if (row > 0) neighbors.Add(index - gridSize); // Top
            if (row < gridSize - 1) neighbors.Add(index + gridSize); // Bottom
            if (col > 0) neighbors.Add(index - 1); // Left
            if (col < gridSize - 1) neighbors.Add(index + 1); // Right

            // Add diagonal neighbors
            if (row > 0 && col > 0) neighbors.Add(index - gridSize - 1); // Top Left
            if (row > 0 && col < gridSize - 1) neighbors.Add(index - gridSize + 1); // Top Right
            if (row < gridSize - 1 && col > 0) neighbors.Add(index + gridSize - 1); // Bottom Left
            if (row < gridSize - 1 && col < gridSize - 1) neighbors.Add(index + gridSize + 1); // Bottom Right

            return neighbors;
        }
    }
}
