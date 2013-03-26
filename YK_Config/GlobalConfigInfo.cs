using System;
using System.Collections.Generic;
using System.Text;

namespace YK.Config
{
    /// <summary>
    /// 网站信息设置描述类, 加[Serializable]标记为可序列化
    /// </summary>
    [Serializable]
    public class GlobalConfigInfo : IConfigInfo
    {
        #region 私有字段
        private int _sitestate = 0;
        private string _sitename = "云客软件";
        private string _siteurl = "http://www.yunksoft.com";
        private string _sitetitle = "会员管理系统_云客会员管理系统-官方免费下载";
        private string _sitekeywords = "会员管理系统,会员管理软件,会员积分系统,会员积分软件";
        private string _sitedescription = "【云客科技】致力于通过提供高质量的、符合中国中小企业的、安全稳定的、高易用性的中小企业会员管理系统,会员管理软件,会员积分管理系统,会员积分管理软件,会员卡管理系统、会员卡管理软件，为中国中小企业打造会员管理、客户管理、销售管理、数据分析的解决方案";
        private string _sitemailaddress = "gx_ylb@126.com";
        private string _sitemailserver = "smtp.126.com";
        private string _sitemailpassword = "123456";         
        #endregion

        #region 属性
        /// <summary>
        /// 网站状态
        /// </summary>
        public int SiteState
        {
            get { return _sitestate; }
            set { _sitestate = value; }
        }
        /// <summary>
        /// 网站名称
        /// </summary>
        public string SiteName
        {
            get { return _sitename; }
            set { _sitename = value; }
        }
        /// <summary>
        /// 网站url地址
        /// </summary>
        public string SiteUrl
        {
            get { return _siteurl; }
            set { _siteurl = value; }
        }
        /// <summary>
        /// 网站标题
        /// </summary>
        public string SiteTitle
        {
            get { return _sitetitle; }
            set { _sitetitle = value; }
        }
        /// <summary>
        /// 网站关键词
        /// </summary>
        public string SiteKeywords
        {
            get { return _sitekeywords; }
            set { _sitekeywords = value; }
        }
        /// <summary>
        /// 网站描述
        /// </summary>
        public string SiteDescription
        {
            get { return _sitedescription; }
            set { _sitedescription = value; }
        }
        /// <summary>
        /// 网站邮件地址
        /// </summary>
        public string SiteMailAddress
        {
            get { return _sitemailaddress; }
            set { _sitemailaddress = value; }
        }
        /// <summary>
        /// 网站邮件服务器
        /// </summary>
        public string SiteMailServer
        {
            get { return _sitemailserver; }
            set { _sitemailserver = value; }
        }
        /// <summary>
        /// 网站邮件密码
        /// </summary>
        public string SiteMailPassword
        {
            get { return _sitemailpassword; }
            set { _sitemailpassword = value; }
        }
       
        #endregion
    }
}

