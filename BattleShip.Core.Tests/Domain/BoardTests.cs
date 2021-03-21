using Battleship.Core.Contracts;
using Battleship.Core.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BattleShip.Core.Tests.Domain
{
    public class BoardTests
    {
        [Fact]
        public void Board_ShouldProvideCorrectStateAfterCreatingNewBoard()
        {
            var mockGenerator = new Mock<IRandomValueGenerator>();
            int size = 2;
            string expectedBoardState = "      A   B   \r\n 1  |   |   |\r\n\r\n 2  |   |   |\r\n\r\n";

            var board = new Board(mockGenerator.Object, size);
            var boardState = board.GetCurrentBoardAsString();

            Assert.Equal(expectedBoardState, boardState);
        }

        [Fact]
        public void Board_IncorrectSize_ShouldThrowArgumentException()
        {
            var mockGenerator = new Mock<IRandomValueGenerator>();
            int size = -2;
            Board board;

            Action act = () => board = new Board(mockGenerator.Object, size);
            ArgumentException exception = Assert.Throws<ArgumentException>(act);

            Assert.Equal("Incorrect board size!", exception.Message);
        }

        [Fact]
        public void Board_CheckTheShot_CorrectCoordinates_ShouldProvideCorrectState()
        {
            var mockGenerator = new Mock<IRandomValueGenerator>();
            int size = 2;
            string expectedBoardState = "      A   B   \r\n 1  |   |   |\r\n\r\n 2  |   | - |\r\n\r\n";

            var board = new Board(mockGenerator.Object, size);

            board.CheckTheShot("B2");

            var state = board.GetCurrentBoardAsString();

            Assert.Equal(expectedBoardState, state);
        }

        [Theory]
        [InlineData("c", "Invalid coordinates!")]
        [InlineData("3", "Invalid coordinates!")]
        [InlineData("c3", "Invalid column!")]
        [InlineData("a3", "Invalid row!")]
        [InlineData("z1", "Invalid column!")]
        [InlineData("ddd", "Invalid column!")]
        [InlineData("555", "Invalid column!")]
        [InlineData("add", "Invalid row!")]
        public void Board_CheckTheShot_IncorrectCoordinates_ShouldThrowArgumentException(string incorrectCoordinates, string exceptionMessage)
        {
            var mockGenerator = new Mock<IRandomValueGenerator>();
            int size = 2;
            var board = new Board(mockGenerator.Object, size);

            Action act = () => board.CheckTheShot(incorrectCoordinates);
            ArgumentException exception = Assert.Throws<ArgumentException>(act);

            Assert.Equal(exceptionMessage, exception.Message);
        }
    }
}
