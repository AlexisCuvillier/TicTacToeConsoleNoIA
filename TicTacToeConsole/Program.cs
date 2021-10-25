using System;
using System.Collections.Generic;

namespace TicTacToeConsole
{
    class Program
    {

        // Variable
        public static bool quitGame = false; //
        public static bool playerTurn = true; // 
        public static char[,] board; //Plateau de jeu




        // Fonction Main
        static void Main(string[] args)
        {
           // GameLoop

            while(!quitGame) // Tant que le jeu est en route
            {

                // Mon plateau 3 par 3
                board = new char[3, 3]
                {
                    {' ', ' ', ' ' },
                    {' ', ' ', ' ' },
                    {' ', ' ', ' ' },
                };
                while(!quitGame)
                {

                    // Tour du joueur
                    if(playerTurn)
                    {
                        PlayerTurn();
                        if(CheckLines('X'))
                        {
                            EndGame("You Win!");
                            break;
                        }
                    }

                    // Tour de l'ordinateur
                    else
                    {
                        ComputerTurn();
                        if (CheckLines('O'))
                        {
                            EndGame("You Loose!");
                            break;
                        }
                    }
                    // Changement de joueur

                    playerTurn = !playerTurn;

                    // vérifier si match nul

                    if (CheckDraw())
                    {
                        EndGame("Draw!");
                        break;
                    }
                } 
                if (!quitGame) 
                {
                    // Instruction
                    Console.WriteLine("Appuyer sur [Escape] pour quitter, [Enter] pour rejouer. ");
                    // Récupération touche du clavier
                    GetKey:
                    switch (Console.ReadKey(true).Key)
                    {
                        // Rejouer
                        case ConsoleKey.Enter:
                            break;
                        // Quitter le jeu
                        case ConsoleKey.Escape:
                            quitGame = true;
                            Console.Clear();
                            break;
                            // Tester une autre touche du clavier
                        default:
                            goto GetKey;
                    }
                }
            }
        }  // Fin fonction main

        // Fonction 


        // Au tour du joueurs
        public static void PlayerTurn()
        {
            // Ou se trouve le joueurs sur la grille ? 
            // Le curseurs sera sur une ligne et une col
            var (row, column) = (0, 0);
            // Le curseur a t-il etait bouger
            bool moved = false;
            // Boucle pour déplacer le curseur a l'écran
            while(!quitGame && !moved)
            {
                Console.Clear();
                // Afficher la Grille
                RenderBoard();
                Console.WriteLine();
                // Afficher les instruction
                Console.WriteLine("Choisir une case valide puis appuyer sur [Enter].");
                // Afficher le curseur

                Console.SetCursorPosition(column * 6 + 1, row * 4 + 1);
            //Attendre que l'utilisateur réalise une action(key)
          
                switch (Console.ReadKey(true).Key)
                {
                    // Quitter le jeu
                    case ConsoleKey.Escape:
                        quitGame = true;
                        Console.Clear();
                        break;
                    // Gerer les fleche du clavier
                    // Pour déplacer le curseurs à l'écran
                    case ConsoleKey.RightArrow:

                        // Si on est sur la colonne 2
                        if(column >= 2)
                        {
                            // On retourne col 0
                            column = 0;
                        }
                        else
                        {
                            // Sinon on va a droite
                            column = column + 1;
                        }
                        break;

                    case ConsoleKey.LeftArrow:

                        // Si on est sur la colonne 0
                        if (column <= 0)
                        {
                            // On retourne col 2
                            column = 2;
                        }
                        else
                        {
                            // Sinon on va a gauche
                            column = column - 1;
                        }
                        break;


      


                    case ConsoleKey.DownArrow:

                        // Si on est sur la row 2
                        if (row >= 2)
                        {
                            // On retourne row 0
                            row = 0;
                        }
                        else
                        {
                            // Sinon on descend
                            row = row + 1;
                        }
                        break;


                        case ConsoleKey.UpArrow:

                        // Si on est sur la row 0
                        if (row <= 0)
                        {
                            // On retourne row 2
                            row = 2;
                        }
                        else
                        {
                            // Sinon on monte
                            row = row - 1;
                        }
                        break;


                    // Jouer dans la case actuelle

                    case ConsoleKey.Enter:
                        if(board[row, column] is ' ')
                        {
                            board[row, column] = 'X';
                            moved = true;
                        }
                        break;

                }
            }
        }


        // Au tour de l'ordinateur
        public static void ComputerTurn()
        {

            // Liste des cases vides
            var emptyBox = new List<(int X, int Y)>();

            // Double boucle pour parcourir les cases

            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Verif case vide
                    if (board[i, j] == ' ')
                    {
                        emptyBox.Add((i, j));
                    }
                }
            }

            // Ou est ce que l'ordinateur va jouer

            var (X, Y) = emptyBox[new Random().Next(0, emptyBox.Count)];
            board[X, Y] = 'O';
        }

        // Afficher le plateau de jeu 

        public static void RenderBoard()
        {
            Console.WriteLine();
            Console.WriteLine($" {board[0, 0]}  |  {board[0, 1]}  |  {board[0, 2]}");
            Console.WriteLine("    |     |");
            Console.WriteLine("----+-----+----");
            Console.WriteLine("    |     |");
            Console.WriteLine($" {board[1, 0]}  |  {board[1, 1]}  |  {board[1, 2]}");
            Console.WriteLine("    |     |");
            Console.WriteLine("----+-----+----");
            Console.WriteLine("    |     |");
            Console.WriteLine($" {board[2, 0]}  |  {board[2, 1]}  |  {board[2, 2]}");

        }


        // Vérifier si un joueur a gagné
        public static bool CheckLines(char c) =>
            board[0, 0] == c && board[1, 0] == c && board[2, 0] == c ||
            board[0, 1] == c && board[1, 1] == c && board[2, 1] == c ||
            board[0, 2] == c && board[1, 2] == c && board[2, 2] == c ||
            board[0, 0] == c && board[0, 1] == c && board[0, 2] == c ||
            board[1, 0] == c && board[1, 1] == c && board[1, 2] == c ||
            board[2, 0] == c && board[2, 1] == c && board[2, 2] == c ||
            board[0, 0] == c && board[1, 1] == c && board[2, 2] == c ||
            board[2, 0] == c && board[1, 1] == c && board[0, 2] == c;

        // Vérifier si match nul
        public static bool CheckDraw() =>
            board[0, 0] != ' ' && board[1, 0] != ' ' && board[2, 0] != ' ' &&
            board[0, 1] != ' ' && board[1, 1] != ' ' && board[2, 1] != ' ' &&
            board[0, 2] != ' ' && board[1, 2] != ' ' && board[2, 2] != ' ';


        public static void EndGame(string msg)
        {
            Console.Clear();
            RenderBoard();
            Console.WriteLine(msg);
        }


    }
}
