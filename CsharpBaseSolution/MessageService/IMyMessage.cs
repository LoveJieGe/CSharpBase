using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MessageService
{
    [ServiceContract(CallbackContract =typeof(IMyMessageCallBack))]
    public interface IMyMessage
    {
        [OperationContract]
        void MessageToServer(string message);
    }
    public interface IMyMessageCallBack
    {
        [OperationContract(IsOneWay =true)]
        void OnCallBack(string message);
    }
}
