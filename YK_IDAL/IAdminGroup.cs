using System;
using System.Data;
namespace YK.IDAL
{
	/// <summary>
	/// 接口层AdminGroup
	/// </summary>
	public interface IAdminGroup
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int GroupID);
        bool Exists(string GroupName,int GroupID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(YK.Model.AdminGroupInfo model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(YK.Model.AdminGroupInfo model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int GroupID);
		bool DeleteList(string GroupIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		YK.Model.AdminGroupInfo GetModel(int GroupID);
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
