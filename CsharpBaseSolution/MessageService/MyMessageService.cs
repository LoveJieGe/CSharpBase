using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MessageService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“MessageService”。
    public class MyMessageService : IMyMessage
    {
        public void MessageToServer(string message)
        {
            IMyMessageCallBack callBack = OperationContext.Current.GetCallbackChannel<IMyMessageCallBack>();
            Console.WriteLine("message from the client:" + message);
            callBack.OnCallBack("message from server");
            Task.Factory.StartNew(new Action<object>(TaskCallBack), callBack);
        }
        private async void TaskCallBack(object sender)
        {
            IMyMessageCallBack callBack = sender as IMyMessageCallBack;
            for(int i = 0;i<10;i++)
            {
                callBack.OnCallBack("Message_" + i);
                await Task.Delay(1000);
            }
        }
    }
}
