/*
 *  Auteur : Dylan Martins
 *  Date : 27.08.2021
 *  Lieu : ETML
 *  Description : Code principal du jeu Space invader
 */

using SpaceInvaders_dylanMartins;
using System;
using System.Collections.Generic;

namespace SpaceInvaders_dylanMartins
{
    /// <summary>
    /// Main code of the space invader game
    /// </summary>
    class Program
    {
        #region Properties
        // Properties
        static byte choice = 1;                                 // L'option de choix du joueur
        static bool redo = true;                                // Refaire une boucle while jusqu'à ce que le joueur choisisse une option.
        static bool difficulty = false;                         // Le niveau de difficulté
        static int highscore = 0;                               // Le meilleur score du jeu
        static List<int> score = new List<int>();               // Liste de points
        static List<string> scoreName = new List<string>();     // Liste des noms des joueurs
        static string middle = "";                              // Marge le contenu
        #endregion

        #region Method
        /// <summary>
        /// Exécuter les différents choix du joueur
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.WindowWidth = 41;
            Console.WindowHeight = 20;

            for (int i = 0; i != Console.WindowWidth / 3; i++)
            {
                middle += " ";
            }

            // Exécuter le choix de l'utilisateur
            do
            {
                Show();
                if (choice == 1)
                {
                    Console.WindowWidth = 120;
                    Console.WindowHeight = 36;
                    GameSetting NewGame = new GameSetting(difficulty);

                    NewGame.GameStarted();
                    Console.Clear();

                    // Enter the name of the player and him score
                    Console.WindowWidth = 41;
                    Console.WindowHeight = 20;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Entrez votre pseudo : ");
                    scoreName.Add(Console.ReadLine());

                    NewGame = null;

                    Console.Clear();
                }
                else if (choice == 2)
                {
                    Configure();
                }
                else if (choice == 3)
                {
                    ShowHighscore();
                }
                else if (choice == 4)
                {
                    Pertinent();
                }
                else if (choice == 5)
                {
                    Environment.Exit(0);
                }
            }
            while (redo);
        }

        /// <summary>
        /// Afficher le menu principal
        /// </summary>
        /// <returns>le choix du joueur</returns>
        static byte Show()
        {
            #region Properties            
            bool main = true;      // Boucle while pour choisir l'option.
            ConsoleKeyInfo keyInfo; // Vérifie la touche que le joueur a touché.
            byte cursorY = 4;       // La position verticale du curseur
            char cursor = '>';      // La forme du curseur
            #endregion

            choice = 1;

            // Afficher le menu principal avec ses options
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.Write("║");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("             Space Invader             ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("║");
            Console.WriteLine("╚═══════════════════════════════════════╝\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0}{1}", middle, cursor);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Jouer\n");
            Console.WriteLine("{0} Options\n", middle);
            Console.WriteLine("{0} Highscore\n", middle);
            Console.WriteLine("{0} A propos\n", middle);
            Console.WriteLine("{0} Quitter", middle);

            // Vérifier le mouvement de l'utilisateur
            do
            {
                keyInfo = Console.ReadKey(true);
                // Déplacez-vous vers le bas de l'option
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    choice++;
                    if (choice > 5)
                    {
                        Console.MoveBufferArea(middle.Length, cursorY, 1, 1, middle.Length, 4);
                        Console.MoveBufferArea(middle.Length + 2, cursorY, 9, 1, middle.Length + 1, cursorY);
                        Console.MoveBufferArea(middle.Length + 1, 4, 9, 1, middle.Length + 2, 4);
                        choice = 1;
                        cursorY = 4;
                    }
                    else
                    {
                        Console.MoveBufferArea(middle.Length, cursorY, 1, 1, middle.Length, cursorY + 2);
                        Console.MoveBufferArea(middle.Length + 2, cursorY, 9, 1, middle.Length + 1, cursorY);
                        Console.MoveBufferArea(middle.Length + 1, cursorY + 2, 9, 1, middle.Length + 2, cursorY + 2);
                        cursorY += 2;
                    }
                }
                // Monter l'option
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    choice--;
                    if (choice < 1)
                    {
                        Console.MoveBufferArea(middle.Length, cursorY, 1, 1, middle.Length, 12);
                        Console.MoveBufferArea(middle.Length + 2, cursorY, 9, 1, middle.Length + 1, cursorY);
                        Console.MoveBufferArea(middle.Length + 1, 12, 9, 1, middle.Length + 2, 12);
                        choice = 5;
                        cursorY = 12;
                    }
                    else
                    {
                        Console.MoveBufferArea(middle.Length, cursorY, 1, 1, middle.Length, cursorY - 2);
                        Console.MoveBufferArea(middle.Length + 2, cursorY, 9, 1, middle.Length + 1, cursorY);
                        Console.MoveBufferArea(middle.Length + 1, cursorY - 2, 9, 1, middle.Length + 2, cursorY - 2);
                        cursorY -= 2;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Spacebar || keyInfo.Key == ConsoleKey.Enter)
                {
                    main = false;
                }
            }
            while (main);
            Console.Clear();
            return choice; // Retourner le numéro du choix
        }

        /// <summary>
        /// Afficher la configuration
        /// </summary>
        static void Configure()
        {
            #region Properties
            bool main = true;           // Boucle while pour choisir l'option
            ConsoleKeyInfo keyInfo;     // Vérifiez la clé que le joueur touche
            byte option = 1;            // L'option de choix du joueur
            byte cursorY = 4;           // La position verticale du curseur
            char cursor = '>';          // La forme du curseur
            #endregion

            Console.ForegroundColor = ConsoleColor.Green;
            // Écrire l'option de menu
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.Write("║");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("                 Option                ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("║");
            Console.WriteLine("╚═══════════════════════════════════════╝\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0}{1}", middle, cursor);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" Son : ");

            //je mets en attendant difficulty parce que j'ai pas encore faire le son
            if (difficulty)
            {
                Console.WriteLine("ON \n");
            }
            else
            {
                Console.WriteLine("OFF\n");
            }
            Console.Write("{0} Difficulté : ", middle);
            if (difficulty)
            {
                Console.WriteLine("DIFFICILE\n");
            }
            else
            {
                Console.WriteLine("FACILE   \n");
            }
            Console.WriteLine("{0} Retour\n", middle);

            // Vérifier le mouvement de l'utilisateur
            do
            {
                keyInfo = Console.ReadKey(true);
                // Monter l'option
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    option++;
                    if (option > 3)
                    {
                        Console.MoveBufferArea(middle.Length, cursorY, 1, 1, middle.Length, 4);
                        Console.MoveBufferArea(middle.Length + 2, cursorY, 23, 1, middle.Length + 1, cursorY);
                        Console.MoveBufferArea(middle.Length + 1, 4, 23, 1, middle.Length + 2, 4);
                        option = 1;
                        cursorY = 4;
                    }
                    else
                    {
                        Console.MoveBufferArea(middle.Length, cursorY, 1, 1, middle.Length, cursorY + 2);
                        Console.MoveBufferArea(middle.Length + 2, cursorY, 23, 1, middle.Length + 1, cursorY);
                        Console.MoveBufferArea(middle.Length + 1, cursorY + 2, 23, 1, middle.Length + 2, cursorY + 2);
                        cursorY += 2;
                    }
                }
                // Déplacez-vous vers le bas de l'option
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    option--;
                    if (option < 1)
                    {
                        Console.MoveBufferArea(middle.Length, cursorY, 1, 1, middle.Length, 8);
                        Console.MoveBufferArea(middle.Length + 2, cursorY, 23, 1, middle.Length + 1, cursorY);
                        Console.MoveBufferArea(middle.Length + 1, 8, 23, 1, middle.Length + 2, 8);
                        option = 3;
                        cursorY = 8;
                    }
                    else
                    {
                        Console.MoveBufferArea(middle.Length, cursorY, 1, 1, middle.Length, cursorY - 2);
                        Console.MoveBufferArea(middle.Length + 2, cursorY, 23, 1, middle.Length + 1, cursorY);
                        Console.MoveBufferArea(middle.Length + 1, cursorY - 2, 23, 1, middle.Length + 2, cursorY - 2);
                        cursorY -= 2;
                    }
                }

                // Si le joueur appuie sur la barre d'espacement ou sur la touche Entrée, activez ou désactivez l'option en question.
                else if (keyInfo.Key == ConsoleKey.Spacebar || keyInfo.Key == ConsoleKey.Enter)
                {

                    if (option == 1)
                    {


                        Console.SetCursorPosition(middle.Length + 8, cursorY);
                        //je mets en attendant difficulty parce que j'ai pas encore faire le son
                        if (difficulty)
                        {
                            Console.Write("ON ");
                        }
                        else
                        {
                            Console.Write("OFF");
                        }
                    }
                    else if (option == 2)
                    {
                        difficulty = !difficulty;
                        Console.SetCursorPosition(middle.Length + 15, cursorY);
                        if (difficulty)
                        {
                            Console.Write("DIFFICILE");
                        }
                        else
                        {
                            Console.Write("FACILE   ");
                        }
                    }
                    else
                    {
                        main = false;
                    }
                }
            }
            while (main);
            Console.Clear();
        }

        /// <summary>
        /// Montrer au joueur le tableau des scores
        /// </summary>
        static void ShowHighscore()
        {
            string middleHighscore = "";
            for (int i = 0; i != Console.WindowWidth / 3 - 9; i++)
            {
                middleHighscore += " ";
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.Write("║");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("               Highscore               ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("║");
            Console.WriteLine("╚═══════════════════════════════════════╝\n");

            // Ecrivez la liste des joueurs et leur score
            if (scoreName.Count != 0)
            {
                for (int i = 0; i < scoreName.Count; i++)
                {
                    if (score[i] == highscore)
                    {
                        Console.WriteLine("Highscore{0} {1} : {2}\n", middleHighscore, scoreName[i], score[i]);
                    }
                    else
                    {
                        Console.WriteLine("{0} {1} : {2}\n", middle, scoreName[i], score[i]);
                    }
                }
            }
            else
            {
                Console.WriteLine("{0} -", middle);
            }
            Console.ReadKey(true);
            Console.Clear();
        }

        /// <summary>
        /// Montrer la commande au joueur
        /// </summary>
        static void Pertinent()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.Write("║");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("                À propos               ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("║");
            Console.WriteLine("╚═══════════════════════════════════════╝\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0} Fléche directionnel ", middle);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(":\n{0}      Se déplacer\n", middle);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0} Espace ", middle);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(":\n{0}      Tirer\n", middle);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0} P/esc ", middle);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(":\n{0}      Pause\n", middle);

            Console.ReadKey(true);
            Console.Clear();
        }
        #endregion
    }
}
