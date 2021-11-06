using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{

    public enum building
    {
        Kinsley,
        NorthSide,
        TylerI,
        TylerII,
        TylerIII,
        Manor_North,
        Manor_NorthEast,
        Manor_West,
        Manor_South,
        Penn_Hall,
        Beard_Hall,
        Naylor,
        Campbell_Hall,
        Business_Center,
        Life_Sciences,
        Humanities,
        Grumbacher,
        Little_Run
    }

    public class Destination
    {
        public Destination(building building , int roomNumber)
        {
            this.building = building ;
            this.roomNumber = roomNumber;
        }
        
        public building building { get; set; }
        public int roomNumber { get; set; }
    }
}
