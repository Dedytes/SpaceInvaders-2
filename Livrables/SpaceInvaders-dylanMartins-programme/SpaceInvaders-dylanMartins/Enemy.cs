/*
 *  Auteur : Dylan Martins
 *  Date : 07.12.2022
 *  Lieu : ETML
 *  Description : Code gerant les vaisseaux ennemie
 */

using SpaceInvaders_dylanMartins;
using System;
using System.Collections.Generic;
using System.Timers;

namespace SpaceInvaders_dylanMartins
{
    /// <summary>
    /// Gestion du code du navire ennemi
    /// </summary>
    public class Enemy
    {
        #region Properties
        //Propriétés

        // Générer un nombre aléatoire
        private Random _shoot = new Random();

        // Boucle pour tirer un missile
        private Timer _enemyShooting = new Timer(250);
        #endregion

        #region Getter - Setter
        //Getter - Setter
        /// <summary>
        /// Définition de la propriété SoundGame, Le son est activé ou désactivé.
        /// </summary>
        public bool SoundGame { get; }

        /// <summary>
        /// Définition de la propriété ShipForm, La forme du navire de l'ennemi
        /// </summary>
        public string ShipForm { get; }

        /// <summary>
        /// Définition de la propriété EnemyDirection, La direction du mouvement du navire.
        /// </summary>
        public bool EnemyDirection { get; set; }

        /// <summary>
        /// Définition de la propriété EnemyX, La position latérale du navire
        /// </summary>
        public int EnemyX { get; set; }

        /// <summary>
        /// Définition de la propriété EnemyY, la position verticale du navire.
        /// </summary>
        public int EnemyY { get; set; }

        /// <summary>
        /// Définition de la propriété de tir, Si l'ennemi peut tirer
        /// </summary>
        public bool Shoot { get; set; }

        /// <summary>
        /// Définition de la propriété MissileEnemy, Créer un missile
        /// </summary>
        public Missile MissileEnemy { get; set; }

        /// <summary>
        /// Définition de la propriété de difficulté, Choisir la difficulté
        /// </summary>
        public bool Difficulty { get; }
        #endregion

        #region Method
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="enemyX">La position latérale du navire</param>.
        /// <param name="enemyY">La position verticale du vaisseau</param>.
        /// <param name="soundGame">Le son est activé ou désactivé</param>.
        /// <param name="enemyDirection">La direction du mouvement du vaisseau</param>.
        /// <param name="posXBunker">Position du bunker</param>
        /// <param name="player">Le vaisseau du joueur</param>
        /// <param name="difficulty">Choisissez la difficulté</param>.
        public Enemy(int enemyX, int enemyY, bool enemyDirection, List<int> posXBunker, PlayerShip player, bool difficulty)
        {
            ShipForm = "■─▬─■";
            this.EnemyX = enemyX;
            this.EnemyY = enemyY;
            this.EnemyDirection = enemyDirection;
            this.Difficulty = difficulty;
            this.MissileEnemy = new Missile(EnemyX, EnemyY, posXBunker, player);

            _enemyShooting.Elapsed += new ElapsedEventHandler(EnemyShoot);
            _enemyShooting.Start();
        }

        /// <summary>
        /// Tirer un missile ennemi
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void EnemyShoot(object source, ElapsedEventArgs e)
        {
            // Selon la difficulté, le vaisseau tire plus souvent
            if (Difficulty == false)
            {
                // Si le nombre aléatoire est égal à 0, lancez un missile.
                if (_shoot.Next(0, 11) == 0 && Shoot == true && MissileEnemy.MissileLive == false)
                {
                    try
                    {
                        MissileEnemy.MissileLive = true;
                        MissileEnemy.MissileY = this.EnemyY + 1;
                        MissileEnemy.MissileX = this.EnemyX + ShipForm.Length / 2;
                        MissileEnemy.MissileEnemyCreate();
                    }
                    catch (ArgumentNullException)
                    {

                    }
                }
            }
            else
            {
                // Si le nombre aléatoire est égal à 0, lancez un missile.
                if (_shoot.Next(0, 6) == 0 && Shoot == true && MissileEnemy.MissileLive == false)
                {
                    try
                    {

                        MissileEnemy.MissileLive = true;
                        MissileEnemy.MissileY = this.EnemyY + 1;
                        MissileEnemy.MissileX = this.EnemyX + ShipForm.Length / 2;
                        MissileEnemy.MissileEnemyCreate();
                    }
                    catch (ArgumentNullException)
                    {

                    }
                }
            }
        }

        /// <summary>
        /// Démarrer ou arrêter le mouvement de l'ennemi
        /// </summary>
        /// <param name="pause">Vérifier si le jeu est en pause ou non</param>
        public void StopShoot(bool pause)
        {
            if (pause == true)
            {
                _enemyShooting.Stop();
            }
            else
            {
                _enemyShooting.Start();
            }
            MissileEnemy.StopShoot(pause);
        }

        /// <summary>
        /// Détruire le missile objet et le vaisseau
        /// </summary>
        public void Dead()
        {
            _enemyShooting.Stop();
            Console.MoveBufferArea(0, 0, 1, 1, MissileEnemy.MissileX, MissileEnemy.MissileY);
            MissileEnemy = null;
        }
        #endregion
    }
}
