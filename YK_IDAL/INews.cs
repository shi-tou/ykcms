using System;
using System.Data;
namespace YK.IDAL
{
	/// <summary>
	/// 接口层News
	/// </summary>
	public interface INews
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int NewsID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		void Add(YK.Model.NewsInfo model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(YK.Model.NewsInfo model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int NewsID);
		bool DeleteList(string NewsIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		YK.Model.NewsInfo GetModel(int NewsID);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
	} 
}
