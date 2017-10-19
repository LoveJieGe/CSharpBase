using Chapter15_ReflectionLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Chapter15_LookUpWhatsNew
{
    internal class WhatsNewChecker
    {
        private readonly static StringBuilder output = new StringBuilder(1000);
        private static DateTime backDateTo = DateTime.Today; 
        static void Main(string[] args)
        {
            Assembly theAssembly = Assembly.Load("Chapter15_VectorLib");
            Attribute supportsAttribute = Attribute.GetCustomAttribute(theAssembly, typeof(SupportsWhatsNewAttribute));
            string name = theAssembly.FullName;
            AddToMessage(name);
            if(supportsAttribute==null)
            {
                AddToMessage("这个程序集不支持SupportsWhatsNew属性");
                return;
            }
            else
                AddToMessage("定义的类型：");
            Type[] types = theAssembly.GetTypes();
            foreach(Type definedType in types)
                DisplayTypeTo(definedType);
            MessageBox.Show(output.ToString(),"New Since:"+backDateTo.ToLongDateString());
            Console.Read();
        }

        static void AddToMessage(string msg)
        {
            output.Append("\n" + msg);
        }
        static void DisplayTypeTo(Type type)
        {
            //只对类进行处理
            if (!type.IsClass)
                return;
            AddToMessage("\n类名：" + type.Name + ";");
            Attribute[] attributes = Attribute.GetCustomAttributes(type);
            if (attributes.Length == 0)
                AddToMessage(string.Format("没有属性在该类[{0}]上", type.Name));
            else
            {
                foreach (Attribute attr in attributes)
                {
                    WriteAttributeInfo(attr);
                }
            }

            MethodInfo[] methods = type.GetMethods();
            if (methods.Length == 0)
                AddToMessage(string.Format("该类[{0}]中没有方法",type.Name));
            foreach (MethodInfo method in methods)
            {
                object[] attrs = method.GetCustomAttributes(typeof(LastModifiedAttribute), false);
                if (attrs != null)
                {
                    AddToMessage(string.Format("方法[{0}]的返回类型{1}", method.Name, method.ReturnType));
                    foreach (Attribute attr in attrs)
                    {
                        WriteAttributeInfo(attr);
                    }
                }
            }
        }
        private static void WriteAttributeInfo(Attribute attr)
        { 
            LastModifiedAttribute lastModifiedAttribute = null;
            if (attr is LastModifiedAttribute)
                lastModifiedAttribute = (LastModifiedAttribute)attr;
            if (lastModifiedAttribute == null) 
                return;
            DateTime modefiedDate = lastModifiedAttribute.DateModified;
            if (modefiedDate < backDateTo)
                return;
            AddToMessage("修改日期：" + modefiedDate.ToLongDateString() + "\n修改信息：" + lastModifiedAttribute.Change);
            if (!string.IsNullOrEmpty(lastModifiedAttribute.Issues))
                AddToMessage("附加信息："+lastModifiedAttribute.Issues);
        }
    }
}
