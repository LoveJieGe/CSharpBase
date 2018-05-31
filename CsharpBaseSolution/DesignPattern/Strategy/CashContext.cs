using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Strategy
{
    public class CashContext
    {
        private BaseCash cash;
        public CashContext(DiscountEnum type)
        {
            this.cash = CashFactory.GetCash(type);
        }
        public double GetResult(double money)
        {
            return cash.GetResult(money);
        }
    }
}
