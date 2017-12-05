using Cahpter43_RoomReservationData;
using Chapter43_RoomReservationContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Chapter43_RoomReservationService
{
    [ServiceBehavior(IncludeExceptionDetailInFaults =true)]
    public class RoomReservationService : IRoomService
    {
        public RoomReservaton[] GetRoomReservaton(DateTime fromDatetime, DateTime toDatetime)
        {
            RoomReservationData data = new RoomReservationData();
            return data.GetRoomReservaton(fromDatetime, toDatetime);
        }

        public bool ReserveRoom(RoomReservaton roomReservaton)
        {
            RoomReservationData data = new RoomReservationData();
            return data.ReserveRoom(roomReservaton);
        }
    }
}
