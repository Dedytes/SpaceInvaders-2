/*
 *  Auteur : Dylan Martins
 *  Date : 10.12.22
 *  Lieu : ETML
 *  Description : Cette page va permettre de gérer les mouvements su SpaceShip
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SpaceInvaders_dylanMartins
{
    public class Move
    {
        // Liste des ennemis
        private Enemy[] _enemies;

        // Boucle pour déplacer le vaisseau des ennemis
        private Timer _enemyMovement = new Timer(200);

        // regarder si le vaisseau des ennemis monte ou descends
        private bool _upDown = false;                       

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="enemies"> Liste d'ennemies</param>
        public Move(Enemy[] enemies)
        {
            this._enemies = enemies;
            if (_enemies[0].Difficulty == true)
            {
                _enemyMovement = new Timer(120);
            }
            _enemyMovement.Elapsed += new ElapsedEventHandler(EnemyControl);
            _enemyMovement.Start();
        }

        public void EnemyControl(object source, ElapsedEventArgs e)
        {
            for (int i = _enemies.Length - 1; i >= 0; i--)
            {
                if (_enemies[i] != null && _upDown == false)
                {
                    // Si la position latérale touche le bord droit de la fenêtre
                    if (_enemies[i].EnemyX + 5 == Console.WindowWidth)
                    {

                        foreach (Enemy enemy in _enemies)
                        {
                            if (enemy != null)
                            {
                                Console.MoveBufferArea(enemy.EnemyX, enemy.EnemyY, 5, 1, enemy.EnemyX - 1, enemy.EnemyY + 1);           // descendre le navire d'un étage
                                enemy.EnemyY++;
                                enemy.EnemyX--;
                                enemy.EnemyDirection = !enemy.EnemyDirection;                                                           // Changer la direction du navire
                            }
                        }
                        // Accélérer le navire
                        if (_enemyMovement.Interval != 80)
                        {
                            _enemyMovement.Interval -= 10;
                        }
                        // Modifier la direction verticale du navire
                        if (_enemies[i].EnemyY == 12)
                        {
                            _upDown = !_upDown;
                        }
                    }

                    // Sinon, déplacez le navire vers la droite
                    else if (_enemies[i].EnemyDirection == false && _enemies[i].EnemyX + 5 != Console.WindowWidth)
                    {
                        Console.MoveBufferArea(_enemies[i].EnemyX, _enemies[i].EnemyY, 5, 1, _enemies[i].EnemyX + 1, _enemies[i].EnemyY);
                        _enemies[i].EnemyX++;
                    }
                }

                else if (_enemies[i] != null)
                {
                    // Si la position latérale touche le bord droit de la fenêtre
                    if (_enemies[i].EnemyX + 5 == Console.WindowWidth)
                    {
                        foreach (Enemy enemy in _enemies)
                        {
                            if (enemy != null)
                            {
                                Console.MoveBufferArea(enemy.EnemyX, enemy.EnemyY, 5, 1, enemy.EnemyX - 1, enemy.EnemyY - 1);           // descendre le navire d'un étage
                                enemy.EnemyX--;
                                enemy.EnemyY--;
                                enemy.EnemyDirection = !enemy.EnemyDirection;                                                           // Changer la direction du navire
                            }
                        }
                        // Accélérer le navire
                        if (_enemyMovement.Interval != 80)
                        {
                            _enemyMovement.Interval -= 10;
                        }
                        // Modifier la direction verticale du navire
                        if (_enemies[i].EnemyY == 3)
                        {
                            _upDown = !_upDown;
                        }
                    }

                    // Sinon, déplacez le navire vers la droite
                    else if (_enemies[i].EnemyDirection == false && _enemies[i].EnemyX + 5 != Console.WindowWidth)
                    {
                        Console.MoveBufferArea(_enemies[i].EnemyX, _enemies[i].EnemyY, 5, 1, _enemies[i].EnemyX + 1, _enemies[i].EnemyY);
                        _enemies[i].EnemyX++;
                    }
                }
            }


            for (int i = 0; i != _enemies.Length; i++)
            {
                if (_enemies[i] != null && _upDown == false)
                {
                    // Si la position latérale touche le bord gauche de la fenêtre
                    if (_enemies[i].EnemyX == Console.WindowLeft)
                    {
                        foreach (Enemy enemy in _enemies)
                        {
                            if (enemy != null)
                            {
                                Console.MoveBufferArea(enemy.EnemyX, enemy.EnemyY, 5, 1, enemy.EnemyX + 1, enemy.EnemyY + 1);           // descendre le navire d'un étage
                                enemy.EnemyY++;
                                enemy.EnemyX++;
                                enemy.EnemyDirection = !enemy.EnemyDirection;                                                           // Changer la direction du navire
                            }
                        }
                        // Accélérer le navire
                        if (_enemyMovement.Interval != 80)
                        {
                            _enemyMovement.Interval -= 10;
                        }
                        // Modifier la direction verticale du navire
                        if (_enemies[i].EnemyY == 12)
                        {
                            _upDown = !_upDown;
                        }
                    }

                    // Sinon, déplacez le navire vers la gauche
                    else if (_enemies[i].EnemyDirection == true && _enemies[i].EnemyX != Console.WindowLeft)
                    {
                        Console.MoveBufferArea(_enemies[i].EnemyX, _enemies[i].EnemyY, 5, 1, _enemies[i].EnemyX - 1, _enemies[i].EnemyY);
                        _enemies[i].EnemyX--;
                    }
                }

                else if (_enemies[i] != null)
                {
                    // Si la position latérale touche le bord gauche de la fenêtre
                    if (_enemies[i].EnemyX == Console.WindowLeft)
                    {

                        foreach (Enemy enemy in _enemies)
                        {
                            if (enemy != null)
                            {
                                Console.MoveBufferArea(enemy.EnemyX, enemy.EnemyY, 5, 1, enemy.EnemyX + 1, enemy.EnemyY - 1);           // descendre le navire d'un étage
                                enemy.EnemyX++;
                                enemy.EnemyY--;
                                enemy.EnemyDirection = !enemy.EnemyDirection;                                                           // Changer la direction du navire
                            }
                        }
                        // Accélérer le navire
                        if (_enemyMovement.Interval != 80)
                        {
                            _enemyMovement.Interval -= 10;
                        }
                        // Accélérer le navire
                        if (_enemies[i].EnemyY == 3)
                        {
                            _upDown = !_upDown; 
                        }
                    }

                    // Sinon, déplacez le navire vers la gauche
                    else if (_enemies[i].EnemyDirection == true && _enemies[i].EnemyX != Console.WindowLeft)
                    {
                        Console.MoveBufferArea(_enemies[i].EnemyX, _enemies[i].EnemyY, 5, 1, _enemies[i].EnemyX - 1, _enemies[i].EnemyY);
                        _enemies[i].EnemyX--;
                    }
                }
            }
            for (int i = 0; i != _enemies.Length; i++)
            {
                // si l'ennemi peut tirer ou non
                if (_enemies[i] != null)
                {
                    // Le premier vaisseau peut tirer
                    if (i == 3 || i == 7 || i == 11 || i == 15 || i == 19)
                    {
                        _enemies[i].Shoot = true;
                    }
                    // Pour chaque navire, vérifiez si tous les navires devant sont morts et si c'est le cas, ils peuvent tirer.
                    else if (i == 2 || i == 6 || i == 10 || i == 14 || i == 18)
                    {
                        if (_enemies[i + 1] == null)
                        {
                            _enemies[i].Shoot = true;
                        }
                        else
                        {
                            _enemies[i].Shoot = false;
                        }
                    }
                    else if (i == 1 || i == 5 || i == 9 || i == 13 || i == 17)
                    {
                        if (_enemies[i + 1] == null && _enemies[i + 2] == null)
                        {
                            _enemies[i].Shoot = true;
                        }
                        else
                        {
                            _enemies[i].Shoot = false;
                        }
                    }
                    else if (i == 0 || i == 4 || i == 8 || i == 12 || i == 16)
                    {
                        if (_enemies[i + 1] == null && _enemies[i + 2] == null && _enemies[i + 3] == null)
                        {
                            _enemies[i].Shoot = true;
                        }
                        else
                        {
                            _enemies[i].Shoot = false;
                        }
                    }
                }
            }
        }
        public void StopMove(bool pause)
        {
            if (pause == true)
            {
                _enemyMovement.Stop();
            }
            else
            {
                _enemyMovement.Start();
            }
        }
    }
}
