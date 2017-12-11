using Castle.DynamicProxy;
using System;
using System.Text;

namespace InterceptorDemo
{
    public class SimpleInterceptor : StandardInterceptor
    {
        protected override void PreProceed(IInvocation invocation)
        {
            object[] args = invocation.Arguments;
            StringBuilder sb = new StringBuilder();
            foreach(object o in args)
            {
                sb.Append(o.ToString() + "\t");
            }
            Console.WriteLine("拦截方法前，方法名：{0}，参数:{1}",invocation.Method.Name, sb.ToString());
            base.PreProceed(invocation);
        }
        protected override void PerformProceed(IInvocation invocation)
        {
            object[] args = invocation.Arguments;
            StringBuilder sb = new StringBuilder();
            foreach (object o in args)
            {
                sb.Append(o.ToString() + "\t");
            }
            Console.WriteLine("方法执行时，方法名：{0}，参数:{1}", invocation.Method.Name, sb.ToString());
            base.PerformProceed(invocation);
        }
        protected override void PostProceed(IInvocation invocation)
        {
            object[] args = invocation.Arguments;
            StringBuilder sb = new StringBuilder();
            foreach (object o in args)
            {
                sb.Append(o.ToString() + "\t");
            }
            Console.WriteLine("拦截方法执行以后，方法名：{0}，参数:{1}", invocation.Method.Name, sb.ToString());
            base.PostProceed(invocation);
        }
    }
}
