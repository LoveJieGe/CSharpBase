using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Chapter43_RoomReservationContracts
{
    [ServiceContract]
    public interface IRoomService
    {
        [OperationContract]
        bool ReserveRoom(RoomReservaton roomReservaton);
        [OperationContract]
        RoomReservaton[] GetRoomReservaton(DateTime fromDatetime, DateTime toDatetime);
    }
}
