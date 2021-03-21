using Battleship.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Battleship.Core.Domain
{
    public class Board
    {
        private const string columnNames = "ABCDEFGHIJKLMNOPRSTUWXYZ";
        private readonly int _size;
        private BoardSquare[,] _board;
        private readonly IRandomValueGenerator _valueGenerator;

        public Board(IRandomValueGenerator valueGenerator, int size)
        {
            _valueGenerator = valueGenerator;

            if (size <= 1)
            {
                throw new ArgumentException("Incorrect board size!");
            }

            _size = size;
            _board = new BoardSquare[_size, _size];

            CreateBoardSquares();
        }

        public void CheckTheShot(string coordinates)
        {
            if (coordinates.Length < 2)
            {
                throw new ArgumentException("Invalid coordinates!");
            }

            var columnAsLetter = coordinates.Substring(0, 1);
            int column = columnNames.IndexOf(columnAsLetter.ToUpper());
            if (column == -1 || column >= _size)
            {
                throw new ArgumentException("Invalid column!");
            }

            var rowAsString = coordinates.Substring(1, coordinates.Length - 1);
            if (!int.TryParse(rowAsString, out int row) || row > _size)
            {
                throw new ArgumentException("Invalid row!");
            }

            _board[row - 1, column].TakeTheShot();
        }

        public string GetCurrentBoardAsString()
        {
            StringBuilder boardState = new StringBuilder();

            boardState.Append("      ");

            for (int currentColumn = 0; currentColumn < _size; currentColumn++)
            {
                boardState.Append($"{columnNames[currentColumn]}   ");
            }

            boardState.AppendLine();

            for (int currentRow = 1; currentRow <= _size; currentRow++)
            {
                if (currentRow < 10)
                {
                    boardState.Append($" {currentRow}  |");
                }
                else
                {
                    boardState.Append($" {currentRow} |");
                }

                for (int currentColumn = 0; currentColumn < _size; currentColumn++)
                {
                    boardState.Append($" {_board[currentRow - 1, currentColumn].GetCurrentStateAsString()} |");
                }

                boardState.AppendLine();
                boardState.AppendLine();
            }

            return boardState.ToString();
        }

        public void ArrangeShips(IEnumerable<Ship> ships)
        {
            foreach (var ship in ships)
            {
                var shipPositioned = false;

                while (!shipPositioned)
                {
                    var horizontal = _valueGenerator.GetRandomBool();

                    if (horizontal)
                    {
                        shipPositioned = TryPositionHorizontalShip(ship);
                    }
                    else
                    {
                        shipPositioned = TryPositionVerticalShip(ship);
                    }
                }
            }
        }

        private void CreateBoardSquares()
        {
            for (int currentRow = 0; currentRow < _size; currentRow++)
            {
                for (int currentColumn = 0; currentColumn < _size; currentColumn++)
                {
                    _board[currentRow, currentColumn] = new BoardSquare();
                }
            }
        }

        private bool TryPositionVerticalShip(Ship ship)
        {
            bool shipPositioned = false;

            var columnPosition = _valueGenerator.GetRandomIntValue(0, _size - 1);

            var shipSize = (int)ship.Type;

            var maxVerticalStartPosition = _size - shipSize;
            var rowStartPosition = _valueGenerator.GetRandomIntValue(0, maxVerticalStartPosition);

            bool placeAlreadyTaken = false;
            for (int rowOffset = 0; rowOffset < shipSize; rowOffset++)
            {
                if (_board[rowStartPosition + rowOffset, columnPosition].IsOccupiedByShip())
                {
                    placeAlreadyTaken = true;
                    break;
                }
            }

            if (!placeAlreadyTaken)
            {
                for (int rowOffset = 0; rowOffset < shipSize; rowOffset++)
                {
                    _board[rowStartPosition + rowOffset, columnPosition].PositionShip(ship);
                }

                shipPositioned = true;
            }

            return shipPositioned;
        }

        private bool TryPositionHorizontalShip(Ship ship)
        {
            bool shipPositioned = false;

            var rowPosition = _valueGenerator.GetRandomIntValue(0, _size - 1);

            var shipSize = (int)ship.Type;

            var maxHorizontalStartPosition = _size - shipSize;
            var columnStartPosition = _valueGenerator.GetRandomIntValue(0, maxHorizontalStartPosition);

            bool placeAlreadyTaken = false;
            for (int columnOffset = 0; columnOffset < shipSize; columnOffset++)
            {
                if (_board[rowPosition, columnStartPosition + columnOffset].IsOccupiedByShip())
                {
                    placeAlreadyTaken = true;
                    break;
                }
            }

            if (!placeAlreadyTaken)
            {
                for (int columnOffset = 0; columnOffset < shipSize; columnOffset++)
                {
                    _board[rowPosition, columnStartPosition + columnOffset].PositionShip(ship);
                }

                shipPositioned = true;
            }

            return shipPositioned;
        }
    }
}
