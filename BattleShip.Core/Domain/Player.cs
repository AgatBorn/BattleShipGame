using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Battleship.Core.Contracts;

namespace Battleship.Core.Domain
{
    public class Player
    {
        private Board _board;
        private List<Ship> _ships;

        public void CreateBoard(IRandomValueGenerator valueGenerator, int size)
        {
            _board = new Board(valueGenerator, size);
            _ships = new List<Ship>();
        }

        public void AddShips(Dictionary<ShipType, int> fleetPlans)
        {
            _ships.Clear();

            foreach (var shipPlans in fleetPlans)
            {
                var shipType = shipPlans.Key;
                var numberOfShips = shipPlans.Value;

                for (int currentShipNumber = 0; currentShipNumber < numberOfShips; currentShipNumber++)
                {
                    _ships.Add(new Ship(shipType));
                }
            }

            _board.ArrangeShips(_ships);
        }

        public void CheckTheShot(string coordinates)
        {
            _board.CheckTheShot(coordinates);
        }

        public string ShowBoardAsString()
        {
            var boardState = _board.GetCurrentBoardAsString();

            var sunkShips = _ships.Where(x => x.Sunk).Select(x => x.Type.ToString());

            if (sunkShips.Any())
            {
                boardState += $"Sunk ships: {string.Join(", ", sunkShips)}";
            }

            return boardState;
        }

        public bool AllShipsSunk()
        {
            return _ships.All(x => x.Sunk);
        }
    }
}
