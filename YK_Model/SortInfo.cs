using System;
namespace YK.Model
{
	/// <summary>
	/// SortInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    [Serializable]
    public partial class SortInfo
    {
        public SortInfo()
        {
        }
        public SortInfo(int parentid, int sortattributeid, string sortname, string sorturl, int sorttemplateid, string pagetitle, string pagekeywords, string pagedesc, string bannerurl, int state,int adminid,string adminname, DateTime createtime)
        {
            _parentid = parentid;
            _sortattributeid = sortattributeid;
            _sortname = sortname;
            _sorturl = sorturl;
            _sorttemplateid = sorttemplateid;
            _pagetitle = pagetitle;
            _pagekeywords = pagekeywords;
            _pagedesc = pagedesc;
            _bannerurl = bannerurl;
            _state = state;
            _adminid = adminid;
            _adminname = adminname;
            _createtime = createtime;
        }
        #region Model
        private int _sortid;
        private int _parentid = 0;
        private int _sortattributeid = 0;
        private string _sortname;
        private string _sorturl;
        private int _sorttemplateid;
        private string _pagetitle;
        private string _pagekeywords;
        private string _pagedesc;
        private string _bannerurl;
        private int _state;
        private int _adminid;
        private string _adminname;
        private DateTime _createtime;
        /// <summary>
        /// 栏目编号
        /// </summary>
        public int SortID
        {
            set { _sortid = value; }
            get { return _sortid; }
        }
        /// <summary>
        /// 栏目父类
        /// </summary>
        public int ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 栏目类别
        /// </summary>
        public int SortAttributeID
        {
            set { _sortattributeid = value; }
            get { return _sortattributeid; }
        }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string SortName
        {
            set { _sortname = value; }
            get { return _sortname; }
        }
        /// <summary>
        /// 栏目URL
        /// </summary>
        public string SortUrl
        {
            set { _sorturl = value; }
            get { return _sorturl; }
        }
        /// <summary>
        /// 栏目模板名称
        /// </summary>
        public int SortTemplateID
        {
            set { _sorttemplateid = value; }
            get { return _sorttemplateid; }
        }
        /// <summary>
        /// 页面标题
        /// </summary>
        public string PageTitle
        {
            set { _pagetitle = value; }
            get { return _pagetitle; }
        }
        /// <summary>
        /// 页面关键字
        /// </summary>
        public string PageKeywords
        {
            set { _pagekeywords = value; }
            get { return _pagekeywords; }
        }
        /// <summary>
        /// 页面描述
        /// </summary>
        public string PageDesc
        {
            set { _pagedesc = value; }
            get { return _pagedesc; }
        }
        /// <summary>
        /// 栏目BannerUrl
        /// </summary>
        public string BannerUrl
        {
            set { _bannerurl = value; }
            get { return _bannerurl; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
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
        #endregion Model

    }
}

