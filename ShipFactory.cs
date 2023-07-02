using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Battleships
{
    internal class ShipFactory
    {
        public T GetShip<T>() where T : ShipBase, new()
        {
            var ship = new T();
            var positions = GetSquaresPositions(ship.Length);
            for (int i = 0; i < ship.Length; i++)
                ship.Squares[i] = new ShipSquare { Position = positions[i] };
            return ship;
        }

        private Point[] GetSquaresPositions(int length)
        {
            var shape = new List<Point> { new Point(0, 0) };
            var count = 1;

            while (count < length)
            {
                bool overlap;
                Point startPos;
                int directionFromLeft;
                var nextPos = default(Point);

                do
                {
                    overlap = false;
                    startPos = shape.ElementAt(RandomSingleton.GetInstance().Next(0, shape.Count));
                    directionFromLeft = RandomSingleton.GetInstance().Next(0, 4);

                    switch (directionFromLeft)
                    {
                        case 0:
                            nextPos = new Point(startPos.X - 1, startPos.Y);
                            break;
                        case 1:
                            nextPos = new Point(startPos.X, startPos.Y - 1);
                            break;
                        case 2:
                            nextPos = new Point(startPos.X + 1, startPos.Y);
                            break;
                        case 3:
                            nextPos = new Point(startPos.X, startPos.Y + 1);
                            break;
                    }

                    if (shape.Any(p => p.X == nextPos.X && p.Y == nextPos.Y))
                        overlap = true;

                } while (overlap);

                shape.Add(nextPos);
                count++;
            }

            return shape.ToArray();
        }
    }
}
