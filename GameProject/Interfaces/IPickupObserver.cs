using GameProject.Pickups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Interfaces
{
    internal interface IPickupObserver
    {
        void OnPickup(Pickup pickup);
    }
}
