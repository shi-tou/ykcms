using System;
namespace YK.Model
{
	/// <summary>
	/// AdminInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AdminInfo
	{
		public AdminInfo()
		{}
		#region Model
		private int _adminid;
		private int _groupid;
		private string _adminaccount;
		private string _adminpwd;
		private string _adminname;
		private int _state;
		private string _admindesc;
		private string _lastloginip;
		private DateTime _lastlogintime;
        private int _createadminid;
		private DateTime _createtime;
		/// <summary>
		/// 
		/// </summary>
		public int AdminID
		{
			set{ _adminid=value;}
			get{return _adminid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int GroupID
		{
			set{ _groupid=value;}
			get{return _groupid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AdminAccount
		{
			set{ _adminaccount=value;}
			get{return _adminaccount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AdminPwd
		{
			set{ _adminpwd=value;}
			get{return _adminpwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AdminName
		{
			set{ _adminname=value;}
			get{return _adminname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int State
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AdminDesc
		{
			set{ _admindesc=value;}
			get{return _admindesc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LastLoginIP
		{
			set{ _lastloginip=value;}
			get{return _lastloginip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime LastLoginTime
		{
			set{ _lastlogintime=value;}
			get{return _lastlogintime;}
		}
        public int CreateAdminID
        {
            set { _createadminid = value; }
            get { return _createadminid; }
        }
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		#endregion Model

	}
}

