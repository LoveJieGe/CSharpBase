using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterceptorDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Castle.DynamicProxy.ProxyGenerator genrator = new ProxyGenerator();
            Castle.DynamicProxy.IInterceptor interceptor = new SimpleInterceptor();
            Person p = (Person)genrator.CreateClassProxy<Person>(interceptor);
            p.SayHello();
            p.SayName("忽而个");
            p.SayOther();
            Console.Read();
        }
    }
}
