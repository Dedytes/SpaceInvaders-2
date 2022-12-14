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
         public PlayerShip(int shipX, int shipY, bool soundGame, List<int> posXBunker, Enemy[] enemies)
        {
            ShipForm = "├─┴─┤";
            this.ShipX = shipX;
            this.ShipY = shipY;
            this._soundGame = soundGame;
            this._enemies = enemies;
            this.ShipLife = 3;
            this.Score = 0;
            this.MissilePlayer = new Missile(ShipX, ShipY, false, posXBunker, _enemies, this);
        }
        
    }
}
