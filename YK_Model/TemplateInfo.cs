using System;
using System.Collections.Generic;
using System.Text;

namespace YK.Model
{
    /// <summary>
    /// SortInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class TemplateInfo
    {
        public TemplateInfo()
        {

        }
        public TemplateInfo(string templatename, string templateurl, string templatedesc, string templatesource, int adminid, string adminname, DateTime createtime)
        {
            _templatename = templatename;
            _templateurl = templateurl;
            _templatedesc = templatedesc;
            _templatesource = templatesource;
            _adminid = adminid;
            _adminname = adminname;
            _createtime = createtime;
        }
        private int _templateid;
        private string _templatename;
        private string _templateurl;
        private string _templatesource;
        private string _templatedesc;
        private int _adminid;
        private string _adminname;
        private DateTime _createtime;
        /// <summary>
        /// 编号
        /// </summary>
        public int TemplateID
        {
            set { _templateid = value; }
            get { return _templateid; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string TemplateName
        {
            set { _templatename = value; }
            get { return _templatename; }
        }
        /// <summary>
        /// URL
        /// </summary>
        public string TemplateUrl
        {
            set { _templateurl = value; }
            get { return _templateurl; }
        }
        /// <summary>
        /// URL
        /// </summary>
        public string TemplateSource
        {
            set { _templatesource = value; }
            get { return _templatesource; }
        }
        /// <summary>
        /// Description
        /// </summary>
        public string TemplateDesc
        {
            set { _templatedesc = value; }
            get { return _templatedesc; }
        }
        /// <summary>
        /// 添加人ID
        /// </summary>
        public int AdminID
        {
            set { _adminid = value; }
            get { return _adminid; }
        }
        /// <summary>
        /// 添加人姓名
        /// </summary>
        public string AdminName
        {
            set { _adminname = value; }
            get { return _adminname; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
    }
}
