using Battleship.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BattleShip.Core.Tests.Domain
{
    public class ShipTests
    {
        [Theory]
        [InlineData(ShipType.Battleship)]
        [InlineData(ShipType.Destroyer)]
        public void Ship_ShouldHaveProperType(ShipType type)
        {
            var ship = new Ship(type);

            Assert.Equal(type, ship.Type);
        }

        [Fact]
        public void Ship_ShouldNotSink()
        {
            var ship = new Ship(ShipType.Destroyer);

            ship.SinkOnePart();

            Assert.False(ship.Sunk);
        }

        [Fact]
        public void Ship_ShouldSink()
        {
            var ship = new Ship(ShipType.Destroyer);

            ship.SinkOnePart();
            ship.SinkOnePart();
            ship.SinkOnePart();
            ship.SinkOnePart();

            Assert.True(ship.Sunk);
        }
    }
}
