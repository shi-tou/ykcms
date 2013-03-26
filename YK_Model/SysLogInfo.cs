using System;
namespace YK.Model
{
    /// <summary>
    /// 说明：日志实体类
    /// 时间：2012-08-20
    /// 作者：杨良斌
    /// </summary>
	[Serializable]
	public partial class SysLogInfo
	{
		public SysLogInfo()
		{}
        public SysLogInfo(string area, string type, string detail,string ip, int adminid, string adminname, DateTime createtime)
        {
            _area = area;
            _type = type;
            _detail = detail;
            _ip = ip;
            _adminid = adminid;
            _adminname = adminname;
            _createtime = createtime;
        }
		#region Model
		private int _syslogid;
		private string _area;
		private string _type;
		private string _detail;
        private string _ip;
		private int _adminid=0;
		private string _adminname;
		private DateTime _createtime;
		/// <summary>
		/// 日志编号
		/// </summary>
		public int SysLogID
		{
			set{ _syslogid=value;}
			get{return _syslogid;}
		}
		/// <summary>
		/// 操作区域
		/// </summary>
		public string Area
		{
			set{ _area=value;}
			get{return _area;}
		}
		/// <summary>
		/// 操作类型
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 操作详细内容
		/// </summary>
		public string Detail
		{
			set{ _detail=value;}
			get{return _detail;}
		}
        /// <summary>
        /// IP
        /// </summary>
        public string IP
        {
            set { _ip = value; }
            get { return _ip; }
        }
		/// <summary>
		/// 管理员编号
		/// </summary>
		public int AdminID
		{
			set{ _adminid=value;}
			get{return _adminid;}
		}
		/// <summary>
		/// 管理员姓名
		/// </summary>
		public string AdminName
		{
			set{ _adminname=value;}
			get{return _adminname;}
		}
		/// <summary>
		/// 添加时间
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		#endregion Model

	}
}

