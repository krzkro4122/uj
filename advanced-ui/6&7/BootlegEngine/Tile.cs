namespace BootlegEngine
{
    public class Tile
    {
        public uint index;
        private Position position;
        public bool isHit;

        public uint reward { get; set; } = 0;

        public Tile(Position position, uint index) 
        {
            this.position = position;
            this.index = index;

            isHit = false;
        }

        public uint Strike()
        {
            this.isHit = true;
            return reward;
        }
    }
}
