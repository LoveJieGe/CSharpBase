using Chapter33_Concurrent.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter33_Concurrent
{
    class Program
    {
        static void Main(string[] args)
        {
            Transfer();
            Console.Read();
        }

        static void Initialize()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DonateContext>());
        }

        static void Transfer()
        {
            decimal transferAcount = 500m;
            using (var context = new DonateContext())
            {
                using (DbContextTransaction tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        string sql = @"update OutputAccount set Balance=Balance-@acount where id=@outputId";
                        context.Database.ExecuteSqlCommand(sql, new SqlParameter("@acount", transferAcount), new SqlParameter("@outputId", 1));
                        InputAccount input = context.InputAccount.Find(1);
                        input.Balance += transferAcount;
                        context.SaveChanges();
                        tran.Commit();
                        Console.WriteLine("操作成功");
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("操作失败");
                        tran.Rollback();
                    }

                }
            }
        }
        static void AddAccount()
        {
            using (var db = new DonateContext())
            {
                OutputAccount o = new OutputAccount();
                o.Name = "甲";
                o.Balance = 1000;
                db.OutputAccount.Add(o);
                InputAccount input = new InputAccount();
                input.Name = "乙";
                input.Balance = 1000;
                db.InputAccount.Add(input);
                db.SaveChanges();
            }
        }

        static void AddDonator()
        {
            using (var db = new DonateContext())
            {
                List<Donator> list = new List<Donator>();
                for(int i = 0;i<10;i++)
                {
                    Donator d = new Donator() { Name = "Name_" + i, DonateDate = DateTime.Today, DonateTime = TypeHelper.GetTime(), Amount = 10 };
                    list.Add(d);
                }
                db.Donator.AddRange(list);
                db.SaveChanges();
            }
        }
        static Donator GetDonator(int id)
        {
            using (var db = new DonateContext())
            {
                return db.Donator.Find(id);
            }
        }
        static void UpdateDoonator(Donator entity)
        {
            using (var db = new DonateContext())
            {
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();   
            }
        }
        //下面实现一个并发的场景
        static void Concurrent()
        {
            //甲用户
            Donator donator1 = GetDonator(1);
            //乙用户
            Donator donator2 = GetDonator(1);
            //甲用户更新名字
            donator1.Name = "胡歌";
            donator2.Amount = 100m;
            UpdateDoonator(donator1);
            try
            {
                UpdateDoonator(donator2);
            }
            catch(DbUpdateConcurrencyException e)
            {
                Console.WriteLine("信息更改失败");
            }
            

        }
    }
}
