using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WP.Infrastructures.SqlSugar
{
    public static class SqlsugarSetup
    {
        public static void AddSqlsugarSetup(this IServiceCollection services,string connectionString, List<Type> types)
        {

            //var strConn = Appsettings.Get("DBConnection");
            var configConnection = new ConnectionConfig()
            {
                DbType = DbType.MySql,
                ConnectionString = connectionString,
                IsAutoCloseConnection = true,
                ConfigureExternalServices = new ConfigureExternalServices
                {
                    EntityService = (c, p) =>
                    {
                        if (c.PropertyType.IsGenericType &&
                        c.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            p.IsNullable = true;
                        }
                    }
                }
            };

            services.AddSqlSugar(configConnection, sqlSugar => 
            {
                sqlSugar.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Console.WriteLine(sql);
                };

                sqlSugar.Aop.DataExecuting = (oldValue, entityInfo) =>
                {
                    //inset生效
                    if (entityInfo.PropertyName == "CreateTime" && entityInfo.OperationType == DataFilterType.InsertByObject)
                    {
                        entityInfo.SetValue(DateTime.Now);//修改CreateTime字段
                                                          //entityInfo有字段所有参数
                    }
                    //update生效        
                    if (entityInfo.PropertyName == "ModifyTime" && entityInfo.OperationType == DataFilterType.UpdateByObject)
                    {
                        entityInfo.SetValue(DateTime.Now);//修改UpdateTime字段
                    }

                    //根据当前列修改另一列 可以么写
                    //if(当前列逻辑==XXX)
                    //var properyDate = entityInfo.EntityValue.GetType().GetProperty("Date");
                    //if(properyDate!=null)
                    //properyDate.SetValue(entityInfo.EntityValue,1);
                };


                //if (Appsettings.Get("SeedData").ToBool())
                //{
                //    sqlSugar.DbMaintenance.CreateDatabase();
                //    foreach (Type item in types)
                //    {
                //        Type[] t = item.Assembly.GetTypes().Where(it => it.FullName.Contains("Entities.")|| it.FullName.Contains("StoredEvent")).ToArray();
                //        sqlSugar.CodeFirst.SetStringDefaultLength(255).InitTables(t);
                //    }
            
                //}
            });


            //SqlSugarScope sqlSugar = new SqlSugarScope(configConnection,
            //    db =>
            //    {
            //        db.Aop.OnLogExecuting = (sql, pars) =>
            //        {
            //            Console.WriteLine(sql);
            //        };
            //    });
            //services.AddSingleton<ISqlSugarClient>(sqlSugar);//单例注入



    


        }
    }
}
