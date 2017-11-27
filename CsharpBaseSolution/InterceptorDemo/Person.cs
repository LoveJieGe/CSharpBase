using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterceptorDemo
{
    public  class Person: IPerson
    {
        public virtual void SayHello()
        {
            Console.WriteLine("你好");
        }
        public virtual void SayName(string name)
        {
            Console.WriteLine("名字：{0}", name);
        }
        public void SayOther()
        {
            Console.WriteLine("其他");
        }
    }
    public interface IPerson
    {
        void SayOther();
    }
}
