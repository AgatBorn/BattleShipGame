using Battleship.Core.Domain;
using Battleship.Infrastructure;
using System;
using System.Collections.Generic;

namespace BattleShip.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Player computer = InitializeGame();
            string message = string.Empty;

            while (!computer.AllShipsSunk())
            {
                Console.Clear();

                ShowTitle();
                ShowBoard(computer);
                ShowAdditionalMessage(message);

                try
                {
                    Shoot(computer);
                    message = string.Empty;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
            }

            Console.Clear();

            ShowTitle();
            ShowBoard(computer);

            ShowEnding();
        }

        private static Player InitializeGame()
        {
            var computer = new Player();

            computer.CreateBoard(new RandomValueGenerator(), 10);

            var fleet = new Dictionary<ShipType, int>
            {
                { ShipType.Battleship, 1 },
                { ShipType.Destroyer, 2 }
            };

            computer.AddShips(fleet);

            return computer;
        }

        private static void ShowTitle()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine();
            Console.WriteLine("          ---- | BATTLESHIP | ----         ");
            Console.WriteLine();
        }

        private static void ShowBoard(Player player)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(player.ShowBoardAsString());
            Console.WriteLine();
        }

        private static void ShowAdditionalMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(message);
            }
        }

        private static void Shoot(Player player)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Select coordinates (e.g. A5): ");
            string coordinates = Console.ReadLine();

            player.CheckTheShot(coordinates);
        }

        private static void ShowEnding()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Battle won!");
            Console.ReadLine();
        }
    }
}
