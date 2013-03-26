using System;
using System.Data;
namespace YK.IDAL
{
	/// <summary>
	/// 接口层Admin
	/// </summary>
	public interface IAdmin
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int AdminID);
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string AdminAccount, int AdminID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(YK.Model.AdminInfo model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(YK.Model.AdminInfo model);
        bool UpdateIPAndTime(string ip, DateTime dateTime, int adminid);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int AdminID);
		bool DeleteList(string AdminIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		YK.Model.AdminInfo GetModel(int AdminID);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.AdminInfo GetModelByAccount(string account);
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
