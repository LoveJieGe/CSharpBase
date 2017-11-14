using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chapter30_SimpleCalculator
{
    class Program
    {
        [Import]
        private ICalculator Calculator { get; set; }
        static void Main(string[] args)
        {
            var p = new Program();
            p.Run();
           
        }
        public void Run()
        {
            var catalog = new AssemblyCatalog(Assembly.GetCallingAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
           IList<IOperation> operations =  Calculator.GetOperations();
            var operationDict = new SortedList<string, IOperation>();
            foreach(IOperation item in operations)
            {
                Console.WriteLine(string.Format("Name:{0},NumberOperateds:{1}", item.Name, item.NumberOperands));
                operationDict.Add(item.Name, item);
            }
            Console.WriteLine();
            string selectOp = null;
            do
            {
                try
                {
                    Console.WriteLine("要执行的操作?");
                    selectOp = Console.ReadLine();
                    if ("exit".Equals(selectOp, StringComparison.CurrentCultureIgnoreCase) || !operationDict.ContainsKey(selectOp))
                        continue;
                    var operation = operationDict[selectOp];
                    double[] operands = new double[operation.NumberOperands];
                    for (int i = 0; i < operation.NumberOperands; i++)
                    {
                        Console.Write("\t请输入第{0}个操作数:", i + 1);
                        string selectOperand = Console.ReadLine();
                        operands[i] = double.Parse(selectOperand);
                    }
                    Console.WriteLine("执行计算...");
                    double result = Calculator.Operate(operation, operands);
                    Console.WriteLine("结果：" + result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

            } while (selectOp != "exit");
        }
    }
}
