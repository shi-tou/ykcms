using System;
namespace YK.Model
{
	/// <summary>
	/// AdminGroupInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AdminGroupInfo
	{
		public AdminGroupInfo()
		{}
		#region Model
		private int _groupid;
		private string _groupname;
		private string _describe;
		private string _groupauth;
		private DateTime _createtime;
		private int _adminid;
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
		public string GroupName
		{
			set{ _groupname=value;}
			get{return _groupname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Describe
		{
			set{ _describe=value;}
			get{return _describe;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GroupAuth
		{
			set{ _groupauth=value;}
			get{return _groupauth;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int AdminID
		{
			set{ _adminid=value;}
			get{return _adminid;}
		}
		#endregion Model

	}
}

