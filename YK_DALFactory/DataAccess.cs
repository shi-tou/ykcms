using System;
using System.Reflection;
using System.Configuration;
using YK.Common;
namespace YK.DALFactory
{
    /// <summary>
    /// 抽象工厂模式创建DAL。
    /// web.config 需要加入配置：(利用工厂模式+反射机制+缓存机制,实现动态创建不同的数据层对象接口)  
    /// DataCache类在导出代码的文件夹里
    /// <appSettings>  
    /// <add key="DAL" value="YK.OleDbDAL" /> (这里的命名空间根据实际情况更改为自己项目的命名空间)
    /// </appSettings> 
    /// </summary>
    public sealed class DataAccess
    {
        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["DAL"];
        /// <summary>
        /// 创建对象或从缓存获取
        /// </summary>
        public static object CreateObject(string AssemblyPath, string ClassNamespace)
        {
            object objType = DataCache.GetCache(ClassNamespace);//从缓存读取
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(AssemblyPath).CreateInstance(ClassNamespace);//反射创建
                    DataCache.SetCache(ClassNamespace, objType);// 写入缓存
                }
                catch
                { }
            }
            return objType;
        }
        /// <summary>
        /// 创建AdminGroup数据层接口。
        /// </summary>
        public static YK.IDAL.IAdmin CreateAdmin()
        {
            string ClassNamespace = AssemblyPath + ".Admin";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (YK.IDAL.IAdmin)objType;
        }
        /// <summary>
        /// 创建AdminGroup数据层接口。
        /// </summary>
        public static YK.IDAL.IAdminGroup CreateAdminGroup()
        {
            string ClassNamespace = AssemblyPath + ".AdminGroup";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (YK.IDAL.IAdminGroup)objType;
        }
        /// <summary>
        /// 创建AdminGroup数据层接口。
        /// </summary>
        public static YK.IDAL.ISysLog CreateSysLog()
        {
            string ClassNamespace = AssemblyPath + ".SysLog";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (YK.IDAL.ISysLog)objType;
        }
        /// <summary>
        /// 创建SortAttribute数据层接口。
        /// </summary>
        public static YK.IDAL.ISortAttribute CreateSortAttribute()
        {
            string ClassNamespace = AssemblyPath + ".SortAttribute";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (YK.IDAL.ISortAttribute)objType;
        }
        /// <summary>
        /// 创建SortAttribute数据层接口。
        /// </summary>
        public static YK.IDAL.ISort CreateSort()
        {
            string ClassNamespace = AssemblyPath + ".Sort";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (YK.IDAL.ISort)objType;
        }

        public static YK.IDAL.ITemplate CreateTemplate()
        {
            string ClassNamespace = AssemblyPath + ".Template";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (YK.IDAL.ITemplate)objType;
        }
    }
}