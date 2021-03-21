using Battleship.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BattleShip.Core.Tests.Domain
{
    public class BoardSquareTests
    {
        [Fact]
        public void BoardSquare_ShouldNotBeOccupiedByShip()
        {
            var boardSquare = new BoardSquare();

            Assert.False(boardSquare.IsOccupiedByShip());
        }

        [Fact]
        public void BoardSquare_ShouldBeOccupiedByShip()
        {
            var boardSquare = new BoardSquare();

            var shipStub = new Ship(ShipType.Battleship);

            boardSquare.PositionShip(shipStub);

            Assert.True(boardSquare.IsOccupiedByShip());
        }

        [Fact]
        public void BoardSquare_NoShip_ShouldHaveEmptyStateBeforeShot()
        {
            var boardSquare = new BoardSquare();

            Assert.Equal(((char)BoardSquareState.Empty).ToString(), boardSquare.GetCurrentStateAsString());
        }

        [Fact]
        public void BoardSquare_NoShip_ShouldHaveMissedStateAfterShot()
        {
            var boardSquare = new BoardSquare();
            boardSquare.TakeTheShot();

            Assert.Equal(((char)BoardSquareState.Missed).ToString(), boardSquare.GetCurrentStateAsString());
        }

        [Fact]
        public void BoardSquare_ShipPositioned_ShouldHaveHitStateAfterShot()
        {
            var boardSquare = new BoardSquare();
            boardSquare.PositionShip(new Ship(ShipType.Destroyer));
            boardSquare.TakeTheShot();

            Assert.Equal(((char)BoardSquareState.Hit).ToString(), boardSquare.GetCurrentStateAsString());
        }

        [Fact]
        public void BoardSquare_ShipPositioned_ShouldHaveSunkStateAfterShots()
        {
            var boardSquare = new BoardSquare();
            boardSquare.PositionShip(new Ship(ShipType.Destroyer));
            boardSquare.TakeTheShot();
            boardSquare.TakeTheShot();
            boardSquare.TakeTheShot();
            boardSquare.TakeTheShot();

            Assert.Equal(((char)BoardSquareState.Sunk).ToString(), boardSquare.GetCurrentStateAsString());
        }
    }
}
