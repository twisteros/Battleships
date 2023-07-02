using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Battleships
{
    internal class Battlefield
    {
        public Size Size => new Size(10, 10);

        public BattlefieldCell[,] BattlefieldCells { get; private set; }

        public Battlefield()
        {
            InitBattlefieldCells();
        }

        private void InitBattlefieldCells()
        {
            int asciiLettersStart = 65;

            BattlefieldCells = new BattlefieldCell[Size.Width, Size.Height];

            for (int r = 0; r < Size.Height; r++)
                for (int c = 0; c < Size.Width; c++)
                {
                    BattlefieldCells[r, c] = new BattlefieldCell
                    {
                        VCoordinate = $"{Convert.ToChar(asciiLettersStart + r)}",
                        HCoordinate = $"{c + 1}",
                    };
                }
        }

        public void DeployShips(List<ShipBase> ships)
        {
            Clear();

            foreach (var ship in ships)
            {
                bool conflict = false;
                var startPt = default(Point);

                do
                {
                    startPt = new Point(RandomSingleton.GetInstance().Next(Size.Width), RandomSingleton.GetInstance().Next(Size.Height));
                    conflict = !DeployShip(ships, ship, startPt, true);

                } while (conflict);

                DeployShip(ships, ship, startPt);
            }
        }

        private bool DeployShip(List<ShipBase> ships, ShipBase ship, Point startPt, bool test = false)
        {
            for (int i = 0; i < ship.Length; i++)
            {
                //does it fit in the board
                var boardPt = new Point(startPt.X + ship.Squares[i].Position.X, startPt.Y + ship.Squares[i].Position.Y);
                if (boardPt.X < 0 || boardPt.Y < 0 || boardPt.X >= Size.Width || boardPt.Y >= Size.Height)
                    return false;
                //is there already another ship
                if (BattlefieldCells[boardPt.X, boardPt.Y].ShipSquare != null)
                    return false;
                if (!test)
                {
                    BattlefieldCells[boardPt.X, boardPt.Y].ShipSquare = ship.Squares[i];
                    BattlefieldCells[boardPt.X, boardPt.Y].Ship = ship;
                }
            }
            return true;
        }

        private void Clear()
        {
            for (int r = 0; r < Size.Height; r++)
                for (int c = 0; c < Size.Width; c++)
                {
                    BattlefieldCells[r, c].Revealed = false;
                    BattlefieldCells[r, c].ShipSquare = null;
                }
        }

        public void Fire(string coordinates)
        {
            for (int r = 0; r < Size.Height; r++)
                for (int c = 0; c < Size.Width; c++)
                    if (!BattlefieldCells[r, c].Revealed && string.Equals(coordinates,
                        $"{BattlefieldCells[r, c].VCoordinate}{BattlefieldCells[r, c].HCoordinate}", StringComparison.OrdinalIgnoreCase))
                    {
                        BattlefieldCells[r, c].Revealed = true;
                        if (BattlefieldCells[r, c].ShipSquare != null)
                        {
                            BattlefieldCells[r, c].ShipSquare.IsHit = true;
                            if (BattlefieldCells[r, c].Ship.Squares.All(s => s.IsHit))
                                BattlefieldCells[r, c].Ship.Sunk = true;
                        }
                        break;
                    }
        }

        public int GetRemainingSquaresCount()
        {
            var count = 0;
            for (int r = 0; r < Size.Height; r++)
                for (int c = 0; c < Size.Width; c++)
                    if (BattlefieldCells[r, c].ShipSquare != null && !BattlefieldCells[r, c].ShipSquare.IsHit)
                        count++;
            return count;

        }
    }
}
