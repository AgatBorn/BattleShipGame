using Battleship.Core.Contracts;
using Battleship.Core.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BattleShip.Core.Tests.Domain
{
    public class PlayerTests
    {
        [Fact]
        public void Player__ShouldProvideCorrectStateAfterCreatingNewBoard()
        {
            var mockGenerator = new Mock<IRandomValueGenerator>();
            int size = 2;

            var player = new Player();
            player.CreateBoard(mockGenerator.Object, size);

            string expectedBoardState = "      A   B   \r\n 1  |   |   |\r\n\r\n 2  |   |   |\r\n\r\n";

            var boardState = player.ShowBoardAsString();

            Assert.Equal(expectedBoardState, boardState);
        }
    }
}
