namespace Battleship.Core.Domain
{
    public class BoardSquare
    {
        private Ship _ship;
        private bool _hit;

        public string GetCurrentStateAsString()
        {
            char state = (char)BoardSquareState.Empty;

            if (_hit)
            {
                if (_ship != null)
                {
                    state = _ship.Sunk ? (char)BoardSquareState.Sunk : (char)BoardSquareState.Hit;
                }
                else
                {
                    state = (char)BoardSquareState.Missed;
                }
            }

            return state.ToString();
        }

        public void TakeTheShot()
        {
            if (_ship != null)
            {
                _ship.SinkOnePart();
            }

            _hit = true;
        }

        public bool IsOccupiedByShip()
        {
            return _ship != null;
        }

        public void PositionShip(Ship ship)
        {
            _ship = ship;
        }
    }
}
