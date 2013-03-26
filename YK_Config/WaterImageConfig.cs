using System;
using System.Collections.Generic;
using System.Text;
using YK.Common;

namespace YK.Config
{
    /// <summary>
    /// 网站信息设置类
    /// </summary>
    public class WaterImageConfig
    {
        /// <summary>
        /// 锁对象
        /// </summary>
        private static object lockHelper = new object();
        /// <summary>
        /// 更新定时器
        /// </summary>
        private static System.Timers.Timer generalConfigTimer = new System.Timers.Timer(15000);
        /// <summary>
        /// 网站信息设置类对象
        /// </summary>
        private static WaterImageConfigInfo _configinfo;
        /// <summary>
        /// 静态构造函数初始化相应实例和定时器
        /// </summary>
        static WaterImageConfig()
        {
            _configinfo = WaterImageConfigManager.LoadConfig();

            generalConfigTimer.AutoReset = true;
            generalConfigTimer.Enabled = true;
            generalConfigTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            generalConfigTimer.Start();
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ResetConfig();
        }

        /// <summary>
        /// 重设配置类实例
        /// </summary>
        public static void ResetConfig()
        {
            _configinfo = WaterImageConfigManager.LoadConfig();
        }

        public static WaterImageConfigInfo GetConfig()
        {
            return _configinfo;
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <param name="generalconfiginfo"></param>
        /// <returns></returns>
        public static bool SaveConfig(WaterImageConfigInfo generalconfiginfo)
        {
            WaterImageConfigManager gcf = new WaterImageConfigManager();
            WaterImageConfigManager.ConfigInfo = generalconfiginfo;
            return gcf.SaveConfig();
        }

        #region Helper

        /// <summary>
        /// 序列化配置信息为XML
        /// </summary>
        /// <param name="configinfo">配置信息</param>
        /// <param name="configFilePath">配置文件完整路径</param>
        public static WaterImageConfigInfo Serialiaze(WaterImageConfigInfo configinfo, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(configinfo, configFilePath);
            }
            return configinfo;
        }
        public static WaterImageConfigInfo Deserialize(string configFilePath)
        {
            return (WaterImageConfigInfo)SerializationHelper.Load(typeof(WaterImageConfigInfo), configFilePath);
        }

        #endregion
    }
}
