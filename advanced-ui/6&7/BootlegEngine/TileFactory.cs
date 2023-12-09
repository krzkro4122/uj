using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootlegEngine
{
    internal class TileFactory
    {
        public static List<Tile> CreateNewTileSet()
        {
            List<Tile> tileSet = new();

            for (uint i = 0; i < 100; i++) { 
                tileSet.Add(CreateNewTile(i));
            }

            return tileSet;
        }

        private static Tile CreateNewTile(uint index)
        {
            Position generatedPosition = GenerateNextPosition(index);
            return new Tile(generatedPosition, index);
        }

        private static Position GenerateNextPosition(uint index)
        {
            uint x = index % 10;
            uint y = index / 10;

            return new Position(x, y);
        }
    }
}
