using System;
namespace YK.Model
{
	/// <summary>
	/// SortAttributeInfo:栏目属性实体类
    /// 创建人：杨良斌
    /// 时间：2012-11-29
	/// </summary>
	[Serializable]
	public partial class SortAttributeInfo
	{
		public SortAttributeInfo()
		{}
        public SortAttributeInfo(string sortattributename, string sortattributedesc, int adminid, string adminname, DateTime createtime)
        {
            _sortattributename = sortattributename;
            _sortattributedesc = sortattributedesc;
            _adminid = adminid;
            _adminname = adminname;
            _createtime = createtime;
        }
		#region Model
		private int _sortattributeid;
		private string _sortattributename;
		private string _sortattributedesc;
		private int _adminid;
		private string _adminname;
		private DateTime _createtime;
		/// <summary>
		/// 栏目属性ID
		/// </summary>
		public int SortAttributeID
		{
			set{ _sortattributeid=value;}
			get{return _sortattributeid;}
		}
		/// <summary>
        /// 栏目属性名称
		/// </summary>
		public string SortAttributeName
		{
			set{ _sortattributename=value;}
			get{return _sortattributename;}
		}
		/// <summary>
        /// 栏目属性描述
		/// </summary>
		public string SortAttributeDesc
		{
			set{ _sortattributedesc=value;}
			get{return _sortattributedesc;}
		}		
		/// <summary>
		/// 创建人ID
		/// </summary>
		public int AdminID
		{
			set{ _adminid=value;}
			get{return _adminid;}
		}
		/// <summary>
		/// 创建人姓名
		/// </summary>
		public string AdminName
		{
			set{ _adminname=value;}
			get{return _adminname;}
		}
		/// <summary>
		/// 时间
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		#endregion Model

	}
}

