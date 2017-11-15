using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter31Lib_ADO.NET
{
    public class SqlHelper
    {
        /// <summary>
        /// 获取数据库的连接
        /// </summary>
        /// <param name="name">配置文件中ConnectionStrings节点的name属性值</param>
        /// <returns></returns>
        public static DbConnection GetConnection(string name)
        {
            //string connectionString = "server=.;integrated security=SSPI;database=Test";
            ConnectionStringSettings settiings = ConfigurationManager.ConnectionStrings[name];//ConnectionStringSettings
            DbProviderFactory factory = DbProviderFactories.GetFactory(settiings.ProviderName);
            DbConnection conn = factory.CreateConnection();
            conn.ConnectionString = settiings.ConnectionString;
            return conn;
        }
        /// <summary>
        /// 执行命令类
        /// </summary>
        /// <param name="name">配置文件中ConnectionStrings节点的name属性值</param>
        /// <returns></returns>
        public static DbCommand GetCommand(string name)
        {
            ConnectionStringSettings settiings = ConfigurationManager.ConnectionStrings[name];//ConnectionStringSettings
            DbProviderFactory factory = DbProviderFactories.GetFactory(settiings.ProviderName);
            DbConnection conn = factory.CreateConnection();
            conn.ConnectionString = settiings.ConnectionString;
            DbCommand cmd = factory.CreateCommand();
            cmd.Connection = conn;
            return cmd;
        }
    }
}
