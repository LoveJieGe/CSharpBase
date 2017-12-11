using Chapter43_RoomReservationContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cahpter43_RoomReservationData
{
    public class RoomReservationData
    {
        public bool ReserveRoom(RoomReservaton roomReservaton)
        {
            using (var data = new RoomReservationContext())
            {
                data.RoomReservaton.Add(roomReservaton);
                return data.SaveChanges()>0;
            }
        }
        public RoomReservaton[] GetRoomReservaton(DateTime fromDatetime, DateTime toDatetime)
        {
            using (var data = new RoomReservationContext())
            {
                return data.RoomReservaton.Where(r => (r.StartTime > fromDatetime && r.EndTime < toDatetime)).ToArray();
            }
        }
    }
}
