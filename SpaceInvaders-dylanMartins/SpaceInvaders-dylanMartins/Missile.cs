using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SpaceInvaders_dylanMartins
{
    public class Missile
    {

        //Properties
        private string _missileShap = "|";              
        private Timer _shootEnemy = new Timer(50);      
        private Timer _shootPlayer = new Timer(50);     
        private List<int> _posXBunker;                  
        private Enemy[] _enemies;                      
        private PlayerShip _player;                     

    }
}
