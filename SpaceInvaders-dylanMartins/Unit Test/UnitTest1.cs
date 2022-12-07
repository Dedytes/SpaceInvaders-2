using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SpaceInvaders_dylanMartins;



namespace SpaceInvaders_dylanMartins
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]

        public void Collision_entre_2_missiles()
        {
            // Organiser
            List<int> posXBunker = new List<int>();
            Enemy[] enemies = new Enemy[20];
            PlayerShip player = new PlayerShip(1, 20, false, posXBunker, enemies);
            Enemy enemy = new Enemy(1, 10, false, true, posXBunker, player, false);


            // Acte
            enemy.MissileEnemy.MissileLive = true;
            player.MissilePlayer.MissileLive = true;
            while (player.MissilePlayer.MissileY != 0)
            {
                // Vérifier si le missile touche le missile d'un autre joueur
                if (enemy.MissileEnemy.MissileX == player.MissilePlayer.MissileX && enemy.MissileEnemy.MissileY >= player.MissilePlayer.MissileY)
                {
                    enemy.MissileEnemy.MissileLive = false;
                    player.MissilePlayer.MissileLive = false;
                    break;
                }
                player.MissilePlayer.MissileY--;
            }


            //Assert
            Assert.AreEqual(false, enemy.MissileEnemy.MissileLive, "le missile doit être mort");
            Assert.AreEqual(false, player.MissilePlayer.MissileLive, "le missile doit être mort");
        }
    }
}