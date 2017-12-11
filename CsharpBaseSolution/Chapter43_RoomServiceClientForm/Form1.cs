using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chapter43_RoomServiceClientForm.RoomReservationService;
using System.ServiceModel;

namespace Chapter43_RoomServiceClientForm
{
    public partial class Form1 : Form
    {
        private RoomReservaton roomReservice = null;
        public Form1()
        {
            InitializeComponent();
            this.startDate.Value = DateTime.Now;
            this.endDate.Value = DateTime.Now.AddDays(1);
        }

        private async void OnReserveRoom(object sender, EventArgs e)
        {
            try
            {
                roomReservice = new RoomReservaton();
                roomReservice.RoomName = this.txtRoom.Text;
                roomReservice.StartTime = this.startDate.Value;
                roomReservice.EndTime = this.endDate.Value;
                roomReservice.Contact = this.txtContract.Text;
                roomReservice.Text = this.text.Text;
                var client = new RoomServiceClient();
                //bool reserve = await client.ReserveRoomAsync(roomReservice);
                //client.Close();
                var binding = new BasicHttpBinding();
                var address = new EndpointAddress("http://localhost:9000/RoomReservation");
                var factory = new ChannelFactory<IRoomService>(binding, address);
                IRoomService roomService = factory.CreateChannel();
                bool reserve = await roomService.ReserveRoomAsync(roomReservice);
                if (reserve)
                    MessageBox.Show("注册成功!", "提示");
                else
                    MessageBox.Show("注册失败!", "提示");
            }
            catch(Exception ex)
            {
                MessageBox.Show("服务器忙!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
    }
}
