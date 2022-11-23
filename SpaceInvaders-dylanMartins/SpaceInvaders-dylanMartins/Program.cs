using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders_dylanMartins
{

    public class Program
    {
        // L'option de choix du joueur
        static byte choice = 0;

        // Refaire une boucle while jusqu'à ce que le joueur choisisse une option.
        static bool redo = true;

        // Marge du contenu
        static string middle = "";


        //Le menu Start
        static byte Start()
        {
            #region Properties
            bool main = true;           // Boucle d'attente pour choisir l'option
            ConsoleKeyInfo keyInfo;     // Vérifiez la clé que le joueur touche
            char cursor = '>';          // La forme du curseur
            #endregion

            choice = 1;

            // Afficher le menu principal avec ses options
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.Write("║");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("          Menu de Selection            ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("║");
            Console.WriteLine("╚═══════════════════════════════════════╝\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0}{1}", middle, cursor);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Jouer\n");


            // Vérifier le mouvement de l'utilisateur
            do
            {
                keyInfo = Console.ReadKey(true);
                
                if (keyInfo.Key == ConsoleKey.Spacebar || keyInfo.Key == ConsoleKey.Enter)
                {
                    main = false;
                }
                for (int i = 0; i != Console.WindowWidth / 3; i++)
                {
                    middle += " ";
                }

                // Exécuter le choix de l'utilisateur
                do
                {

                    if (choice == 1)
                    {
                        Select();
                    }
                    else if (choice == 2)
                    {
                        Environment.Exit(0);
                    }

                }
                while (redo);
            }

            while (main);
            Console.Clear();
            return choice; // Retourner le numéro du choix


        }

        //Le menu select
        static byte Select()
        {
            Console.Clear();
            #region Properties
            bool main = true;           // Boucle d'attente pour choisir l'option
            ConsoleKeyInfo keyInfo;     // Vérifiez la clé que le joueur touche
            byte cursorY = 4;           // La position verticale du curseur
            char cursor = '>';          // La forme du curseur
            #endregion

            choice = 2;

            // Show the main menu with its option
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.Write("║");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("          Menu de Selection            ");
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
                // Déplcacement vers le bas avec la flèche du bas
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
                // Déplcacement vers le haut avec la flèche du haut
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
                for (int i = 0; i != Console.WindowWidth / 3; i++)
                {
                    middle += " ";
                }

                // Exécuter le choix de l'utilisateur
                do
                {

                    if (choice == 1)
                    {
                        //Play
                    }
                    else if (choice == 2)
                    {
                        //Configure();
                    }
                    else if (choice == 3)
                    {
                        //ShowHighscore();
                    }
                    else if (choice == 4)
                    {
                        //Option();
                    }
                    else if (choice == 5)
                    {
                        Environment.Exit(0);
                    }
                }
                while (redo);
            }

            while (main);
            Console.Clear();
            return choice; // Retourner le numéro du choix
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.WindowWidth = 41;
            Console.WindowHeight = 20;
            Start();
     
        }

    }
    
}
