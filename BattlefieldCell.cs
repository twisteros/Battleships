namespace Battleships
{
    internal class BattlefieldCell
    {
        public string VCoordinate { get; set; }
        public string HCoordinate { get; set; }
        public ShipSquare ShipSquare { get; set; }
        public ShipBase Ship {get; set;}
        public bool Revealed { get; set; }
    }
}
