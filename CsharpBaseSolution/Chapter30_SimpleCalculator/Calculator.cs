using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter30_SimpleCalculator
{
    [Export(typeof(ICalculator))]
    public class Calculator : ICalculator
    {
        public IList<IOperation> GetOperations()
        {
            return new List<IOperation>
            {
                new Operation() { Name = "+", NumberOperands = 2 },
                new Operation() { Name = "-", NumberOperands = 2 },
                new Operation() { Name = "*", NumberOperands = 2 },
                new Operation() { Name = "/", NumberOperands = 2 }
            };
        }

        public double Operate(IOperation operation, double[] operands)
        {
            double result = 0;
            switch(operation.Name)
            {
                case "+":
                    result = operands[0] + operands[1];
                    break;
                case "-":
                    result = operands[0] - operands[1];
                    break;
                case "*":
                    result = operands[0] * operands[1];
                    break;
                case "/":
                    result = operands[0] / operands[1];
                    break;
                default:
                    throw new Exception(string.Format("无法解析的操作符[{0}]!",operation.Name));
            }
            return result;
        }
    }
}
