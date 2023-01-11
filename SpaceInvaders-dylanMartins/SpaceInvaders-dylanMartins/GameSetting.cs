/*
 *  Auteur : Dylan Martins
 *  Date : 10.12.22
 *  Lieu : ETML
 *  Description : Code principal du jeu Space invader
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceInvaders_dylanMartins
{
    public class GameSetting
    {
        #region Properties
        // Properties
        private List<int> _posXBunker = new List<int>();    // Liste des positions de bunker
        private Enemy[] _enemies = new Enemy[20];           // Liste des ennemis
        private PlayerShip _player;                         // Le vaisseau du joueur
        private Move _move;                                 // Déplacer le vaisseau des ennemis
        private int _score = 0;                             // Le score du match
        private int _stage = 1;                             // L'étape actuelle où se trouve le joueur
        private bool _difficulty;                           // Le niveau de difficulté
        private bool _redo = true;                          // Refaire une boucle while
        #endregion

        #region Getter - Setter
        //Getter - Setter
        #endregion

        #region Method
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="difficulty"></param>
        public GameSetting(bool difficulty)
        {
            this._difficulty = difficulty;
        }

        /// <summary>
        /// Créez les ennemis et le joueur et donnez le contrôle au joueur.
        /// </summary>
        public void GameStarted()
        {
            for (int i = 1; i != 5; i++)
            {
                Bunker bunker = new Bunker(i);                                                              // Créer un bunker

                for (int j = 0; j != 18; j++)
                {
                    _posXBunker.Add(Console.WindowWidth / 4 * i - 23 + j);                                  // Sauvegarder la position du bunker
                }
            }

            _player = new PlayerShip(Console.WindowWidth / 2 - 3, Console.WindowHeight - 7, _posXBunker, _enemies);       // Créer le joueur 
            Console.SetCursorPosition(_player.ShipX, _player.ShipY);
            Console.Write(_player.ShipForm);


            CreatEnemy();
            Console.SetCursorPosition(Console.WindowLeft, Console.WindowHeight - 6);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("─");
            }
            Console.SetCursorPosition(Console.WindowLeft + 10, Console.WindowHeight - 3);
            Console.Write("Vie : ♥ ♥ ♥");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight - 3);
            Console.Write("Score : {0}", _score);
            Console.SetCursorPosition(Console.WindowWidth - 20, Console.WindowHeight - 3);
            Console.Write("Stage : {0}", _stage);

            _move = new Move(_enemies);
            _player.ShipAction(_move);

            while (_redo)
            {
                // Quand le joueur est mort
                Thread.Sleep(50);
                if (_player.ShipLife == 0)
                {
                    Console.Clear();
                    _move.StopMove(true);
                    _move = null;
                    for (byte i = 0; i < _enemies.Length; i++)
                    {
                        if (_enemies[i] != null)
                        {
                            _enemies[i].Dead();
                        }
                        _enemies[i] = null;
                    }
                    _score = _player.Score;
                    _player = null;
                    _redo = false;
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2);
                    Console.Write("Game Over");
                    Console.ReadKey(true);
                }
                // Quand tous les ennemis sont morts
                else
                {
                    _move.StopMove(true);
                    _move = null;
                    for (byte i = 0; i < _enemies.Length; i++)
                    {
                        if (_enemies[i] != null)
                        {
                            _enemies[i].Dead();
                        }
                        _enemies[i] = null;
                    }

                    Console.MoveBufferArea(_player.ShipX, _player.ShipY, _player.ShipForm.Length, 1, Console.WindowWidth / 2 - 3, Console.WindowHeight - 7);
                    _player.ShipX = Console.WindowWidth / 2 - 3;
                    Console.SetCursorPosition(_player.ShipX, _player.ShipY);
                    Console.Write(_player.ShipForm);
                    _stage++;
                    Console.SetCursorPosition(Console.WindowWidth - 12, Console.WindowHeight - 3);
                    Console.Write(_stage);

                    CreatEnemy();
                    _move = new Move(_enemies);
                    _player.ShipAction(_move);
                }
            }
        }

        /// <summary>
        /// Créer les ennemis et les ajouter dans le tableau
        /// </summary>
        public void CreatEnemy()
        {
            int x = 0;
            for (int i = 0; i != 4; i++)
            {
                Enemy enemy = new Enemy(Console.WindowWidth / 3 + 5, Console.WindowTop + 2 * i + 2, true, _posXBunker, _player, _difficulty);      // Créer un ennemi
                _enemies[x] = enemy;
                x++;
                Console.SetCursorPosition(enemy.EnemyX, enemy.EnemyY);    // Positionner le curseur sur la coordonnée du navire
                Console.Write(enemy.ShipForm);
                Thread.Sleep(100);
            }

            for (int i = 0; i != 4; i++)
            {
                Enemy enemy = new Enemy(Console.WindowWidth / 3 + 5 * 2 + 1, Console.WindowTop + 2 * i + 2, true, _posXBunker, _player, _difficulty);      // Créer un ennemi
                _enemies[x] = enemy;
                x++;
                Console.SetCursorPosition(enemy.EnemyX, enemy.EnemyY);    // Positionner le curseur sur la coordonnée du navire
                Console.Write(enemy.ShipForm);
                Thread.Sleep(100);
            }

            for (int i = 0; i != 4; i++)
            {
                Enemy enemy = new Enemy(Console.WindowWidth / 3 + 5 * 3 + 2, Console.WindowTop + 2 * i + 2, true, _posXBunker, _player, _difficulty);      // Créer un ennemi
                _enemies[x] = enemy;
                x++;
                Console.SetCursorPosition(enemy.EnemyX, enemy.EnemyY);    // Positionner le curseur sur la coordonnée du navire
                Console.Write(enemy.ShipForm);
                Thread.Sleep(100);
            }

            for (int i = 0; i != 4; i++)
            {
                Enemy enemy = new Enemy(Console.WindowWidth / 3 + 5 * 4 + 3, Console.WindowTop + 2 * i + 2, true, _posXBunker, _player, _difficulty);      // Créer un ennemi
                _enemies[x] = enemy;
                x++;
                Console.SetCursorPosition(enemy.EnemyX, enemy.EnemyY);    // Positionner le curseur sur la coordonnée du navire
                Console.Write(enemy.ShipForm);
                Thread.Sleep(100);
            }

            for (int i = 0; i != 4; i++)
            {
                Enemy enemy = new Enemy(Console.WindowWidth / 3 + 5 * 5 + 4, Console.WindowTop + 2 * i + 2, true, _posXBunker, _player, _difficulty);      // Créer un ennemi
                _enemies[x] = enemy;
                x++;
                Console.SetCursorPosition(enemy.EnemyX, enemy.EnemyY);    // Positionner le curseur sur la coordonnée du navire
                Console.Write(enemy.ShipForm);
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Obtenez le score du jeu
        /// </summary>
        /// <returns>le score</returns>
        public int GetScore()
        {
            return _score;
        }
        #endregion
    }
}

