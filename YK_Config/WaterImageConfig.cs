using System;
using System.Collections.Generic;
using System.Text;
using YK.Common;

namespace YK.Config
{
    /// <summary>
    /// 水印信息设置类
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
        /// 文件修改时间
        /// </summary>
        private static DateTime _fileoldchange;
        /// <summary>
        /// 水印信息设置类对象
        /// </summary>
        private static WaterImageConfigInfo _configinfo;
        /// <summary>
        /// 静态构造函数初始化相应实例和定时器
        /// </summary>
        static WaterImageConfig()
        {
            _fileoldchange = System.IO.File.GetLastWriteTime(ConfigFilePath);
            LoadConfig();

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
        /// 当前配置类的实例
        /// </summary>
        public static IConfigInfo ConfigInfo
        {
            get { return _configinfo; }
            set { _configinfo = (WaterImageConfigInfo)value; }
        }
        /// <summary>
        /// 配置文件所在路径
        /// </summary>
        public static string filename = null;
        /// <summary>
        /// 获取配置文件所在路径
        /// </summary>
        public static string ConfigFilePath
        {
            get
            {
                if (filename == null)
                {
                    filename = Utils.GetMapPath(@"/config/WaterImage.config");
                }

                return filename;
            }
        }
        /// <summary>
        /// 重设配置类实例
        /// </summary>
        public static void ResetConfig()
        {
            _configinfo = LoadConfig();
        }
        /// <summary>
        /// 获取配置类对象
        /// </summary>
        /// <returns></returns>
        public static WaterImageConfigInfo GetConfig()
        {
            return _configinfo;
        }
        /// <summary>
        /// 返回配置类实例
        /// </summary>
        /// <returns></returns>
        public static WaterImageConfigInfo LoadConfig()
        {
            try
            {
                ConfigInfo = BaseConfigFileManager.LoadConfig(ref _fileoldchange, ConfigFilePath, ConfigInfo, true);
            }
            catch
            {
                ConfigInfo = BaseConfigFileManager.LoadConfig(ref _fileoldchange, ConfigFilePath, ConfigInfo, true);
            }
            return ConfigInfo as WaterImageConfigInfo;
        }
        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <returns></returns>
        public static bool SaveConfig(WaterImageConfigInfo generalconfiginfo)
        {
            return BaseConfigFileManager.SaveConfig(ConfigFilePath, ConfigInfo);
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
