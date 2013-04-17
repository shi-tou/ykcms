using System;
namespace YK.Model
{
	/// <summary>
	/// NewsInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class NewsInfo
	{
		public NewsInfo()
		{}
		#region Model
		private int _newsid;
		private int _typeid;
		private string _title;
		private string _content;
		private string _linkurl;
		private string _author;
		private string _pagetitle;
		private string _pagekeyword;
		private string _pagedesc;
		private int _state;
		private int _adminid;
		private DateTime _createtime;
		/// <summary>
		/// 
		/// </summary>
		public int NewsID
		{
			set{ _newsid=value;}
			get{return _newsid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int TypeID
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LinkUrl
		{
			set{ _linkurl=value;}
			get{return _linkurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Author
		{
			set{ _author=value;}
			get{return _author;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PageTitle
		{
			set{ _pagetitle=value;}
			get{return _pagetitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PageKeyword
		{
			set{ _pagekeyword=value;}
			get{return _pagekeyword;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PageDesc
		{
			set{ _pagedesc=value;}
			get{return _pagedesc;}
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
		public int AdminID
		{
			set{ _adminid=value;}
			get{return _adminid;}
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

