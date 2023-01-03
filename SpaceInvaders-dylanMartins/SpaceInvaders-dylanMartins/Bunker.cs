/*
 *  Auteur : Dylan Martins
 *  Date : 10.12.22
 *  Lieu : ETML
 *  Description : Code permet de gérer les bunkers
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders_dylanMartins
{
    public class Bunker
    {
        public Bunker(int decalage)
        {
            Console.SetCursorPosition(Console.WindowWidth / 4 * decalage - 23, 20);
            Console.WriteLine("██████████████████");

            //Console.SetCursorPosition(Console.WindowWidth / 4 * decalage - 23, 20);
            //Console.WriteLine("      █████");
            //Console.SetCursorPosition(Console.WindowWidth / 4 * decalage - 23, 21);
            //Console.WriteLine("    ██     ██");
            //Console.SetCursorPosition(Console.WindowWidth / 4 * decalage - 23, 22);
            //Console.WriteLine("  ██         ██");
            //Console.SetCursorPosition(Console.WindowWidth / 4 * decalage - 23, 23);
            //Console.WriteLine("██            ██");
        }
    }
}
