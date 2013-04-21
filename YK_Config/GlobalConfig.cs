using System;
using System.Collections.Generic;
using System.Text;
using YK.Common;
using System.IO;

namespace YK.Config
{
    /// <summary>
    /// 网站信息设置类
    /// </summary>
    public class GlobalConfig
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
        /// 网站信息设置类对象
        /// </summary>
        private static GlobalConfigInfo _configinfo;
        /// <summary>
        /// 静态构造函数初始化相应实例和定时器
        /// </summary>
        static GlobalConfig()
        {
            _fileoldchange = System.IO.File.GetLastWriteTime(ConfigFilePath);
            try
            {
                _configinfo = (GlobalConfigInfo)BaseConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(GlobalConfigInfo));
            }
            catch
            {
                if (File.Exists(ConfigFilePath))
                {
                    _configinfo = (GlobalConfigInfo)BaseConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(GlobalConfigInfo));
                }
            }
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
            set { _configinfo = (GlobalConfigInfo)value; }
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
                    filename = Utils.GetMapPath(@"/config/global.config");
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
        public static GlobalConfigInfo GetConfig()
        {
            return _configinfo;
        }        

        /// <summary>
        /// 返回配置类实例
        /// </summary>
        /// <returns></returns>
        public static GlobalConfigInfo LoadConfig()
        {
            try
            {
                ConfigInfo = BaseConfigFileManager.LoadConfig(ref _fileoldchange, ConfigFilePath, ConfigInfo, true);
            }
            catch
            {
                ConfigInfo = BaseConfigFileManager.LoadConfig(ref _fileoldchange, ConfigFilePath, ConfigInfo, true);
            }
            return ConfigInfo as GlobalConfigInfo;
        }
        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <returns></returns>
        public static bool SaveConfig(GlobalConfigInfo generalconfiginfo)
        {
            return BaseConfigFileManager.SaveConfig(ConfigFilePath, ConfigInfo);
        }


        #region Helper

        /// <summary>
        /// 序列化配置信息为XML
        /// </summary>
        /// <param name="configinfo">配置信息</param>
        /// <param name="configFilePath">配置文件完整路径</param>
        public static GlobalConfigInfo Serialiaze(GlobalConfigInfo configinfo, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(configinfo, configFilePath);
            }
            return configinfo;
        }
        public static GlobalConfigInfo Deserialize(string configFilePath)
        {
            return (GlobalConfigInfo)SerializationHelper.Load(typeof(GlobalConfigInfo), configFilePath);
        }

        #endregion
    }
}
