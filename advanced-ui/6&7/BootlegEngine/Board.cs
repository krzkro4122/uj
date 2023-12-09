using System.ComponentModel;

namespace BootlegEngine
{
    public class Board
    {
        public List<Tile> Tiles { get; private set; } = new();

        public Board() 
        {
            Tiles = TileFactory.CreateNewTileSet();
            BattleShipFactory.MutateSomeTilesToShips(Tiles);  
        }

        public uint attackTile(uint tileIndex)
        {
            uint reward = Tiles[(int)tileIndex].Strike();            
            return reward;
        }
    }
}
