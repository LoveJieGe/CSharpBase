using Chapter43_RoomReservationContracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cahpter43_RoomReservationData
{
    public class RoomReservationContext:DbContext
    {
        public RoomReservationContext():base("name=RoomReservation")
        { }
        public DbSet<RoomReservaton> RoomReservaton { get; set; }
    }
}
