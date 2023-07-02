using System.Drawing;

namespace Battleships
{
    public abstract class ShipBase
    {
        abstract public string Name { get; }
        abstract public int Length { get; }
        //private Point[] _shape;
        //public Point[] Shape => _shape ?? (_shape = new Point[Length]);
        //private Point[] _coordinates;
        //public Point[] Coordinates => _coordinates ?? (_coordinates = new Point[Length]);
        private ShipSquare[] _squares;
        public ShipSquare[] Squares => _squares ?? (_squares = new ShipSquare[Length]);
    }
}
