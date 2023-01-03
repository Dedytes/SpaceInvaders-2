/*
 *  Auteur : Dylan Martins
 *  Date : 07.12.22
 *  Lieu : ETML
 *  Description : Code gerant le vaisseau du joueur
 */
using SpaceInvaders_dylanMartins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders_dylanMartins
{
    public class PlayerShip
    {
        private bool _gamePause = false;        // Vérifier si le jeu est en pause
        private bool _over = false;             // Vérifier si le jeu est terminé ou non
        private bool _soundGame;                // Le son est activé ou désactivé
        private const byte _SHIPSPEED = 1;      // La vitesse de déplacement du navire
        private Enemy[] _enemies;               // Liste d'ennemis

        //Getter - Setter
        /// <summary>
        /// Définition de la propriété ShipForm, La forme du vaisseau du joueur
        /// </summary>
        public string ShipForm { get; }

        /// <summary>
        /// Définition de la propriété ShipX, La position latérale du navire
        /// </summary>
        public int ShipX { get; set; }

        /// <summary>
        /// Définition de la propriété ShipY, La position verticale du navire
        /// </summary>
        public int ShipY { get; }

        /// <summary>
        /// Définition de la propriété ShipLife, La durée de vie limite du joueur
        /// </summary>
        public byte ShipLife { get; set; }

        /// <summary>
        /// Définition de la propriété du score, Score du jeu
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Définition de la pré-propriété MissilePlayer, Créer un missile
        /// </summary>
        public Missile MissilePlayer { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="shipX">La position du vaisseau latéral</param>
        /// <param name="shipY">La position du vaissaeau vertical</param>
        /// <param name="soundGame"> le son du jeu</param>
        /// <param name="posXBunker">position du bunker</param>
        /// <param name="enemies"> les ennemis</param>
        public PlayerShip(int shipX, int shipY, List<int> posXBunker, Enemy[] enemies)
        {
            ShipForm = "├─┴─┤";
            this.ShipX = shipX;
            this.ShipY = shipY;
            this._enemies = enemies;
            this.ShipLife = 3;
            this.Score = 0;
            this.MissilePlayer = new Missile(ShipX, ShipY, false, posXBunker, _enemies, this);
        }
        [DllImport("User32.dll")]                       // Importez le fichier User32.dll
        static extern short GetAsyncKeyState(int key);  // touche du clavier enfoncée

        /// <summary>
        /// Permet au joueur de se déplacer et de tirer avec le vaisseau et de mettre le jeu en pause.
        /// </summary>
        /// <param name="move">The enemies movement</param>
        public void ShipAction(Move move)
        {
            _over = false;
            short GetAsyncKeyStateResult = GetAsyncKeyState(32); // Résultat de la touche du clavier pressée
            do
            {
                // Si le résultat de la touche du clavier pressée est positif et qu'il est égal à la touche flèche gauche
                if ((GetAsyncKeyStateResult & 0x8000) > 0 && GetAsyncKeyStateResult == GetAsyncKeyState(37))
                {
                    // Si le jeu n'est pas en pause
                    if (_gamePause == false)
                    {
                        // Si la position latérale ne touche pas le bord gauche de la fenêtre
                        if (ShipX != Console.WindowLeft)
                        {
                            ShipX--;
                            Console.MoveBufferArea(ShipX + 1, ShipY, ShipForm.Length, 1, ShipX, ShipY); // Move the ship to the left
                        }
                        // Sinon, ne rien faire
                        else { }
                        System.Threading.Thread.Sleep(_SHIPSPEED);
                    }
                }
                // Else si le résultat de la touche du clavier pressée est positif et qu'il est égal à la touche flèche droite
                else if ((GetAsyncKeyStateResult & 0x8000) > 0 && GetAsyncKeyStateResult == GetAsyncKeyState(39))
                {
                    // Si le jeu n'est pas en pause
                    if (_gamePause == false)
                    {
                        // Si la position latérale plus la longueur du navire ne touchent pas le bord droit de la fenêtre.
                        if (ShipX + ShipForm.Length != Console.WindowWidth)
                        {
                            ShipX++;
                            Console.MoveBufferArea(ShipX - 1, ShipY, ShipForm.Length, 1, ShipX, ShipY); // Move the ship to the right
                        }
                        // Sinon, ne rien faire
                        else { }
                        System.Threading.Thread.Sleep(_SHIPSPEED);
                    }
                }
                // Si une touche est pressée
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true); // Lire la touche pressée
                    // Si la touche pressée est la flèche gauche
                    if (key.Key == ConsoleKey.LeftArrow)
                    {
                        GetAsyncKeyStateResult = GetAsyncKeyState(37); // Initialiser le GetAsynKeyState dans la flèche gauche
                    }
                    // If the key pressed is the right arrow
                    else if (key.Key == ConsoleKey.RightArrow)
                    {
                        GetAsyncKeyStateResult = GetAsyncKeyState(39); // Initialiser le GetAsynKeyState dans la flèche de droite
                    }
                    // Si la touche pressée est la barre d'espacement
                    else if (key.Key == ConsoleKey.Spacebar)
                    {
                        // Vérifier si un missile est déjà lancé
                        if (MissilePlayer.MissileLive == false)
                        {
                            MissilePlayer.MissileLive = true;
                            // Repositionner l'emplacement du missile
                            MissilePlayer.MissileY = ShipY - 1;
                            MissilePlayer.MissileX = ShipX + (ShipForm.Length / 2);
                            MissilePlayer.MissilePlayerCreate();
                        }
                    }
                    // Si la touche pressée est la touche P ou escape
                    else if (key.Key == ConsoleKey.P || key.Key == ConsoleKey.Escape)
                    {
                        // Le jeu est en pause
                        _gamePause = !_gamePause;
                        MissilePlayer.StopShoot(_gamePause);
                        move.StopMove(_gamePause);
                        foreach (Enemy x in _enemies)
                        {
                            if (x != null)
                            {
                                x.StopShoot(_gamePause);
                            }
                        }
                    }
                }
                // si le joueur est mort, le jeu est terminé
                if (ShipLife == 0)
                {
                    _over = true;
                }
                byte i = 0;
                foreach (Enemy x in _enemies)
                {
                    if (x != null)
                    {
                        i++;
                    }
                }
                // S'il n'y a plus d'ennemis, le jeu est terminé mais il redémarre immédiatement.
                if (i == 0)
                {
                    _over = true;
                }
                i = 0;
            }
            while (_over == false);
            MissilePlayer.StopShoot(_gamePause);
        }
    }
}
