namespace Battleships
{
    public abstract class ShipBase
    {
        abstract public string Name { get; }
        abstract public int Length { get; }
        private ShipSquare[] _squares;
        public ShipSquare[] Squares => _squares ?? (_squares = new ShipSquare[Length]);
        public bool Sunk { get; set; }
    }
}
