using Chapter43_RoomReservationContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter43_RoomReservationService
{
    public class StateService : IStateService
    {
        private int i;
        public void Close()
        {
        }

        public int GetSatte()
        {
            return i;
        }

        public void Init(int i)
        {
            this.i = i;
        }

        public void SetState(int i)
        {
            this.i = i;
        }
    }
}
