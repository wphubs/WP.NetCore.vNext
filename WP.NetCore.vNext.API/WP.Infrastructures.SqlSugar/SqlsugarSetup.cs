using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.Core;

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

                sqlSugar.QueryFilter.Add(new SqlFilterItem()
                {
                    FilterName = "",
                    FilterValue = it =>
                    {
                        return new SqlFilterResult() { Sql = " IsDeleted = 0 " };
                    },
                });
                if (Appsettings.Get("SeedData").ToBool())
                {
                    sqlSugar.DbMaintenance.CreateDatabase();
                    foreach (Type item in types)
                    {
                        Type[] t = item.Assembly.GetTypes().Where(it => it.FullName.Contains("Entities.") || it.FullName.Contains("StoredEvent")).ToArray();
                        sqlSugar.CodeFirst.SetStringDefaultLength(255).InitTables(t);
                    }

                }
            });


            //INSERT INTO `wpdb2`.`sysuserrole`(`UserId`, `RoleId`, `IsDeleted`, `CreateBy`, `CreateTime`, `ModifyBy`, `ModifyTime`, `Id`) VALUES(353939986944069, 353939986944062, b'0', 0, '2022-11-16 13:40:26', NULL, NULL, 353939986944063);
            //INSERT INTO `wpdb2`.`sysuser`(`Account`, `Avatar`, `Name`, `Password`, `Salt`, `Sex`, `IsDeleted`, `CreateBy`, `CreateTime`, `ModifyBy`, `ModifyTime`, `Id`) VALUES('admin', NULL, '系统管理员', 'B83EDF3632329E2BD58A6B17DC429EAE', 'no0es', 0, b'0', 0, '2022-11-16 13:25:32', NULL, NULL, 353939986944069);
            //INSERT INTO `wpdb2`.`sysrole`(`Name`, `Sort`, `PId`, `Desc`, `IsDeleted`, `CreateBy`, `CreateTime`, `ModifyBy`, `ModifyTime`, `Id`) VALUES('系统管理员', 1, 0, '1', b'0', 0, '2022-11-16 13:40:45', NULL, NULL, 353939986944062);


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
