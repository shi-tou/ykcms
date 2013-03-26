using System;
using System.Collections.Generic;
using System.Text;
using YK.Common;
using System.IO;

namespace YK.Config
{
    /// <summary>
    /// 网站信息设置管理类
    /// </summary>
    class GlobalBaseConfigFileManager : BaseConfigFileManager
    {
        /// <summary>
        /// 网站信息设置类对象
        /// </summary>
        private static GlobalConfigInfo _configinfo;
        /// <summary>
        /// 文件修改时间
        /// </summary>
        private static DateTime _fileoldchange;
        /// <summary>
        /// 初始化文件修改时间和对象实例
        /// </summary>
        static GlobalBaseConfigFileManager()
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
        }
        /// <summary>
        /// 当前配置类的实例
        /// </summary>
        public new static IConfigInfo ConfigInfo
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
        public new static string ConfigFilePath
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
        public override bool SaveConfig()
        {
            return base.SaveConfig(ConfigFilePath, ConfigInfo);
        }
    }
}
