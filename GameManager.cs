using System.Collections.Generic;
using System.Drawing;

namespace Battleships
{
    internal class GameManager
    {
        private Battlefield Battlefield { get; set; }

        public BattlefieldCell[,] BattlefieldCells => Battlefield.BattlefieldCells;
        public Size BattlefieldSize => Battlefield.Size;
        public int Attempts {get; private set;}

        public GameManager()
        {
            Battlefield = new Battlefield();
        }

        public void Start()
        {
            Attempts = 0;

            var shipFactory = new ShipFactory();
            var ships = new List<ShipBase>
            {
                shipFactory.GetShip<Battleship>(),
                shipFactory.GetShip<Destroyer>(),
                shipFactory.GetShip<Destroyer>()
            };

            Battlefield.DeployShips(ships);
        }

        public void Fire(string coordinates)
        {
            Battlefield.Fire(coordinates);

            Attempts++;
        }

        public bool AreAllSquaresHit() => Battlefield.GetRemainingSquaresCount() == 0;
    }
}
