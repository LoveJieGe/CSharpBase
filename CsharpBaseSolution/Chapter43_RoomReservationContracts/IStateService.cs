using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Chapter43_RoomReservationContracts
{
    [ServiceContract(SessionMode =SessionMode.Required)]
    public interface IStateService
    {
        [OperationContract(IsInitiating =true)]
        void Init(int i);
        [OperationContract]
        void SetState(int i);
        [OperationContract]
        int GetSatte();
        [OperationContract(IsTerminating =true)]
        void Close();
    }
}
