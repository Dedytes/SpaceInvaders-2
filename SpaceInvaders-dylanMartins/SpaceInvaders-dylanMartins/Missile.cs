/*
 *  Auteur : Dylan Martins
 *  Date : 30.11.22
 *  Lieu : ETML
 *  Description : Code gerant les parametre des missiles
 */

using SpaceInvaders_dylanMartins;
using System;
using System.Collections.Generic;
using System.Timers;

namespace SpaceInvaders_dylanMartins
{
    /// <summary>
    /// Code gérant les paramètres des missiles
    /// </summary>
    public class Missile
    {
        #region Properties
        //Properties
        private string _missileShap = "|";              // La patterne du missile
        private Timer _shootEnemy = new Timer(50);      // Boucle pour abaisser les missiles ennemis
        private Timer _shootPlayer = new Timer(50);     // Boucle pour abaisser les missiles du joueur
        private List<int> _posXBunker;                  // Postition du bunker
        private Enemy[] _enemies;                       // Liste des ennemis
        private PlayerShip _player;                     // Le vaisseau du joueur
        #endregion

        #region Getter - Setter
        //Getter - Setter
        /// <summary>
        /// Définition de la propriété MissileX, la position latérale du missile.
        /// </summary>
        public int MissileX { get; set; }

        /// <summary>
        /// Définition de la propriété MissileY, La position verticale du missile
        /// </summary>
        public int MissileY { get; set; }

        /// <summary>
        /// Définition de la propriété MissileLive, Le missile est activé ou non
        /// </summary>
        public bool MissileLive { get; set; }
        #endregion

        #region Method
        /// <summary>
        /// Constructor player
        /// </summary>
        /// <param name="missileX">La position latérale du missile</param>.
        /// <param name="missileY">La position verticale du missile</param>.
        /// <param name="missileLive">Le missile est activé ou non</param>.
        /// <param name="posXBunker">Position du bunker</param>
        /// <param name="enemies">Liste des ennemis</param>
        /// <param name="player">Le vaisseau du joueur</param>
        public Missile(int missileX, int missileY, bool missileLive, List<int> posXBunker, Enemy[] enemies, PlayerShip player)
        {
            this.MissileX = missileX;
            this.MissileY = missileY;
            this.MissileLive = missileLive;
            this._posXBunker = posXBunker;
            this._enemies = enemies;
            this._player = player;
            _shootEnemy = null;
            SetTimerPlayer();
        }

        /// <summary>
        /// Constructor enemy
        /// </summary>
        /// <param name="missileX">La position latérale du missile</param>.
        /// <param name="missileY">La position verticale du missile</param>.
        /// <param name="posXBunker">Position du bunker</param>
        /// <param name="player">Le vaisseau du joueur</param>
        public Missile(int missileX, int missileY, List<int> posXBunker, PlayerShip player)
        {
            this.MissileX = missileX;
            this.MissileY = missileY;
            this._posXBunker = posXBunker;
            this._player = player;
            _shootPlayer = null;
            SetTimerEnemy();
        }

        /// <summary>
        /// Timer qui appelle la méthode MissilePlayerMove toutes les 100 millisecondes>.
        /// </summary>
        public void SetTimerPlayer()
        {
            _shootPlayer.Elapsed += new ElapsedEventHandler(MissilePlayerMove);
        }

        /// <summary>
        /// Timer qui appelle la méthode MissileEnemyMove toutes les 250 millisecondes
        /// </summary>
        public void SetTimerEnemy()
        {
            _shootEnemy.Elapsed += new ElapsedEventHandler(MissileEnemyMove);
        }

        /// <summary>
        /// Crée un missile de joueur
        /// </summary>
        public void MissilePlayerCreate()
        {
            // Positionner le curseur à l'emplacement du missile
            Console.SetCursorPosition(MissileX, MissileY);
            // Créer le missile
            Console.Write(_missileShap);
            _shootPlayer.Start();
        }

        /// <summary>
        /// Déplacez le missile du joueur vers le haut de la fenêtre.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void MissilePlayerMove(object source, ElapsedEventArgs e)
        {
            // Vérifier si le missile touche un pixel d'un bunker.
            for (int i = 0; i < _posXBunker.Count; i++)
            {
                if (MissileY == 20 && MissileX == _posXBunker[i])
                {
                    Console.MoveBufferArea(MissileX, MissileY + 1, 1, 1, MissileX, MissileY);
                    _posXBunker[i] = Console.WindowTop;
                    MissileY = Console.WindowTop;
                    _posXBunker.Remove(_posXBunker[i]);
                    _shootPlayer.Stop();
                }
            }

            for (int i = 0; i != _enemies.Length; i++)
            {
                if (_enemies[i] != null && MissileY == _enemies[i].EnemyY)
                {
                    // Vérifier si le missile touche un pixel d'un ennemi
                    if (MissileX >= _enemies[i].EnemyX && MissileX <= _enemies[i].EnemyX + _enemies[i].ShipForm.Length - 1)
                    {
                        Console.MoveBufferArea(0, 0, 1, 1, this.MissileX, this.MissileY);
                        Console.MoveBufferArea(0, 0, 5, 1, _enemies[i].EnemyX, _enemies[i].EnemyY);
                        _enemies[i].MissileEnemy._shootEnemy.Stop();
                        _enemies[i].Dead();
                        _enemies[i] = null;
                        MissileY = Console.WindowTop;
                        _player.Score += 100;
                        Console.SetCursorPosition(Console.WindowWidth / 2 + 3, Console.WindowHeight - 3);
                        Console.Write(_player.Score);
                    }
                }
            }


            // Déplacez le missile tant que le missile ne touche pas le sommet.
            if (MissileY != Console.WindowTop)
            {
                MissileY--;
                Console.MoveBufferArea(MissileX, MissileY + 1, 1, 1, MissileX, MissileY);

            }
            // Sinon, détruisez le missile.
            else
            {
                Console.MoveBufferArea(MissileX - 1, MissileY, 1, 1, MissileX, MissileY);
                MissileLive = false;
                _shootPlayer.Stop();
            }
        }

        /// <summary>
        /// Creates a enemy missile
        /// </summary>
        public void MissileEnemyCreate()
        {
            Console.SetCursorPosition(MissileX, MissileY); // Positionner le curseur à l'emplacement du missile
            Console.Write(_missileShap);                   // Créer le missile
            _shootEnemy.Start();
        }

        /// <summary>
        /// Déplacez le missile du joueur vers le bas de la fenêtre.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void MissileEnemyMove(object source, ElapsedEventArgs e)
        {
            // Vérifier si le missile touche un pixel d'un bunker.
            for (int i = 0; i < _posXBunker.Count; i++)
            {
                if (MissileY == 20 && MissileX == _posXBunker[i])
                {
                    Console.MoveBufferArea(1, 1, 1, 1, MissileX, MissileY);
                    _posXBunker[i] = Console.WindowTop;
                    MissileY = Console.WindowHeight - 1;
                    _posXBunker.Remove(_posXBunker[i]);
                    _shootEnemy.Stop();
                }
            }
            // Vérifier si le missile touche un pixel du joueur.
            if (MissileY == _player.ShipY - 1)
            {
                if (MissileX >= _player.ShipX && MissileX <= _player.ShipX + _player.ShipForm.Length - 1)
                {
                    Console.MoveBufferArea(1, 1, 1, 1, MissileX, MissileY);
                    MissileY = Console.WindowHeight;
                    Console.MoveBufferArea(1, 1, 2, 1, Console.WindowLeft + 21 - (6 / _player.ShipLife), Console.WindowHeight - 3);
                    _player.ShipLife -= 1;
                    _shootEnemy.Stop();
                }
            }

            // Vérifier si le missile touche un missile d'un autre joueur.
            if (MissileX == _player.MissilePlayer.MissileX && MissileY >= _player.MissilePlayer.MissileY)
            {
                Console.MoveBufferArea(0, 0, 1, 1, MissileX, MissileY);
                MissileLive = false;
                _shootEnemy.Stop();
                Console.MoveBufferArea(1, 1, 1, 1, _player.MissilePlayer.MissileX, _player.MissilePlayer.MissileY);
                _player.MissilePlayer.MissileLive = false;
                _player.MissilePlayer._shootPlayer.Stop();
            }

            // Déplacez le missile tant qu'il ne touche pas le fond.
            if (MissileY < Console.WindowHeight - 8)
            {
                MissileY++;
                Console.MoveBufferArea(MissileX, MissileY - 1, 1, 1, MissileX, MissileY);
            }
            // Sinon, détruisez le missile.
            else
            {
                Console.MoveBufferArea(1, 1, 1, 1, MissileX, MissileY);
                MissileLive = false;
                _shootEnemy.Stop();
            }
        }

        /// <summary>
        /// Démarrer ou arrêter le tournage
        /// </summary>
        /// <param name="pause">Vérifier si le jeu est en pause ou non</param>
        public void StopShoot(bool pause)
        {
            if (pause == true)
            {
                if (_shootPlayer != null)
                {
                    _shootPlayer.Stop();
                }
                else
                {
                    _shootEnemy.Stop();
                }
            }
            else
            {
                if (_shootPlayer != null)
                {
                    _shootPlayer.Start();
                }
                else
                {
                    _shootEnemy.Start();
                }
            }
        }
        #endregion
    }
}