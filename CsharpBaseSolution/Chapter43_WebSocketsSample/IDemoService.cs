using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;

namespace Chapter43_WebSocketsSample
{
    [ServiceContract(CallbackContract =typeof(IDemoCallBack))]
    public interface IDemoService
    {
        [OperationContract]
        Task StartSendMessage();
    }
    
    [ServiceContract]
    public interface IDemoCallBack
    {
        [OperationContract(IsOneWay = true)]
        Task SendMessage(string message);
    }
}