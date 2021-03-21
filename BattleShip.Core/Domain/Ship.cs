using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Core.Domain
{
    public class Ship
    {
        private int _currentSize;

        public Ship(ShipType type)
        {
            Type = type;
            _currentSize = (int)type;
        }

        public ShipType Type { get; }

        public bool Sunk 
        {
            get
            {
                return _currentSize == 0;
            }
        }

        public void SinkOnePart()
        {
            if (!Sunk)
            {
                _currentSize = _currentSize - 1;
            }
        }
    }
}
