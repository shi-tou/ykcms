using System;
using System.Data;
namespace YK.IDAL
{
	/// <summary>
	/// 接口层：栏目属性
    /// 创建人：杨良斌
    /// 时间：2012-11-29
	/// </summary>
	public interface ISortAttribute
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int SortAttributeID);
        bool Exists(string SortAttributeName, int SortAttributeID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		void Add(YK.Model.SortAttributeInfo model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(YK.Model.SortAttributeInfo model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int SortAttributeID);
		bool DeleteList(string SortAttributeIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		YK.Model.SortAttributeInfo GetModel(int SortAttributeID);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
	} 
}
