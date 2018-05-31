using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Strategy
{
    /// <summary>
    /// 使用工厂模式进行实现
    /// </summary>
    public class CashFactory
    {
        public static BaseCash GetCash(DiscountEnum type)
        {
            BaseCash cash = null;
            switch (type)
            {
                case DiscountEnum.Normal:
                    cash = new NormalCash();
                    break;
                case DiscountEnum.Discount8:
                    cash = new DiscountCash(0.8);
                    break;
                case DiscountEnum.Return:
                    cash = new ReturnCash(300, 50);
                    break;
            }
            return cash;
        }
    }
    /// <summary>
    /// 现金收费抽象类
    /// </summary>
    public abstract class BaseCash
    {
        public abstract double GetResult(double money);
    }
    /// <summary>
    /// 正常的收费
    /// </summary>
    public class NormalCash : BaseCash
    {
        public override double GetResult(double money)
        {
            return money;
        }
    }
    /// <summary>
    /// 打折
    /// </summary>
    public class DiscountCash:BaseCash
    {
        private double moneyDiscount = 1;
        public DiscountCash(double moneyDiscount)
        {
            this.moneyDiscount = moneyDiscount;
        }

        public override double GetResult(double money)
        {
            return money * moneyDiscount;
        }
    }
    /// <summary>
    /// 满多少返多少
    /// </summary>
    public class ReturnCash : BaseCash
    {
        public double moneyCondition = 0;
        public double moneyReturn = 0;
        public ReturnCash(double moneyCondition, double moneyReturn)
        {
            this.moneyCondition = moneyCondition;
            this.moneyReturn = moneyReturn;
        }
        public override double GetResult(double money)
        {
            if (money >= this.moneyCondition)
            {
                money = money - Math.Floor(money / this.moneyCondition) * moneyReturn;
            }
            return money;
        }
    }
}
