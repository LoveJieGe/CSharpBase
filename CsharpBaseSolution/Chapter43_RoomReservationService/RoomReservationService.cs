using Cahpter43_RoomReservationData;
using Chapter43_RoomReservationContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Chapter43_RoomReservationService
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class RoomReservationService : IRoomService
    {
        [WebGet(UriTemplate = "Reservations?From={fromTime}&To={toTime}")]
        public RoomReservaton[] GetRoomReservaton(DateTime fromTime, DateTime toTime)
        {
            RoomReservationData data = new RoomReservationData();
            return data.GetRoomReservaton(fromTime, toTime);
        }

        public bool ReserveRoom(RoomReservaton roomReservaton)
        {
            RoomReservationData data = new RoomReservationData();
            return data.ReserveRoom(roomReservaton);
        }
    }
}
