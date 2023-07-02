using System;
using System.Collections.Generic;
using System.Drawing;

namespace Battleships
{
    internal class Battlefield
    {
        public Size Size => new Size(10, 10);

        private BattlefieldCell[,] _battlefieldCells;
        public BattlefieldCell this[int row, int col] => _battlefieldCells[row, col];

        public Battlefield()
        {
            InitBattlefieldCells();
        }

        private void InitBattlefieldCells()
        {
            int asciiLettersStart = 65;

            _battlefieldCells = new BattlefieldCell[Size.Width, Size.Height];

            for (int r = 0; r < Size.Height; r++)
                for (int c = 0; c < Size.Width; c++)
                {
                    _battlefieldCells[r, c] = new BattlefieldCell
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
                if (_battlefieldCells[boardPt.X, boardPt.Y].ShipSquare != null)
                    return false;
                if (!test)
                    _battlefieldCells[boardPt.X, boardPt.Y].ShipSquare = ship.Squares[i];
            }
            return true;
        }

        private void Clear()
        {
            for (int r = 0; r < Size.Height; r++)
                for (int c = 0; c < Size.Width; c++)
                {
                    _battlefieldCells[r, c].Revealed = false;
                    _battlefieldCells[r, c].ShipSquare = null;
                }
        }

        public void Fire(string coordinates)
        {
            for (int r = 0; r < Size.Height; r++)
                for (int c = 0; c < Size.Width; c++)
                    if (!_battlefieldCells[r, c].Revealed && string.Equals(coordinates,
                        $"{_battlefieldCells[r, c].VCoordinate}{_battlefieldCells[r, c].HCoordinate}", StringComparison.OrdinalIgnoreCase))
                    {
                        _battlefieldCells[r, c].Revealed = true;
                        if (_battlefieldCells[r, c].ShipSquare != null)
                            _battlefieldCells[r, c].ShipSquare.IsHit = true;
                        break;
                    }
        }

        public int GetRemainingSquaresCount()
        {
            var count = 0;
            for (int r = 0; r < Size.Height; r++)
                for (int c = 0; c < Size.Width; c++)
                    if (_battlefieldCells[r, c].ShipSquare != null && !_battlefieldCells[r, c].ShipSquare.IsHit)
                        count++;
            return count;

        }
    }
}
