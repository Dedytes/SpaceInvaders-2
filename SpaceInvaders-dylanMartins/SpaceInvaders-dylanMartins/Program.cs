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
        static byte choice = 1;
        // Refaire une boucle while jusqu'à ce que le joueur choisisse une option.
        static bool redo = true;
        // Le niveau de difficulté
        static bool difficulty = false;
        // Le score le plus élevé du jeu
        static int highscore = 0;
        // Liste des scores
        static List<int> score = new List<int>();
        // Liste des noms des joueurs
        static List<string> scoreName = new List<string>();
        // Marge du contenu
        static string middle = "";                              
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.WindowWidth = 41;
            Console.WindowHeight = 20;

            for (int i = 0; i != Console.WindowWidth / 3; i++)
            {
                middle += " ";
            }

        }
    }
}
