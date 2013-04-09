using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using YK.IDAL;
using YK.DBUtility;//Please add references
namespace YK.DAL
{
    /// <summary>
    /// 数据访问类:Admin
    /// </summary>
    public partial class Admin : IAdmin
    {
        public Admin()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("AdminID", "Admin");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int AdminID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Admin");
            strSql.Append(" where AdminID=@AdminID");
            SqlParameter[] parameters = {
					new SqlParameter("@AdminID", SqlDbType.Int,4)
            };
            parameters[0].Value = AdminID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string AdminAccount, int AdminID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Admin");
            strSql.Append(" where AdminAccount=@AdminAccount and AdminID <> @AdminID");
            SqlParameter[] parameters = {
					new SqlParameter("@AdminAccount", SqlDbType.VarChar,50),
                    new SqlParameter("@AdminID", SqlDbType.Int,4)
            };
            parameters[0].Value = AdminAccount;
            parameters[1].Value = AdminID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(YK.Model.AdminInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Admin(");
            strSql.Append("GroupID,AdminAccount,AdminPwd,AdminName,State,AdminDesc,LastLoginIP,LastLoginTime,CreateTime,CreateAdminID)");
            strSql.Append(" values (");
            strSql.Append("@GroupID,@AdminAccount,@AdminPwd,@AdminName,@State,@AdminDesc,@LastLoginIP,@LastLoginTime,@CreateTime,@CreateAdminID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@AdminAccount", SqlDbType.VarChar,20),
					new SqlParameter("@AdminPwd", SqlDbType.VarChar,20),
					new SqlParameter("@AdminName", SqlDbType.VarChar,15),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@AdminDesc", SqlDbType.VarChar,100),
					new SqlParameter("@LastLoginIP", SqlDbType.VarChar,20),
					new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@CreateAdminID", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.GroupID;
            parameters[1].Value = model.AdminAccount;
            parameters[2].Value = model.AdminPwd;
            parameters[3].Value = model.AdminName;
            parameters[4].Value = model.State;
            parameters[5].Value = model.AdminDesc;
            parameters[6].Value = model.LastLoginIP;
            parameters[7].Value = model.LastLoginTime;
            parameters[8].Value = model.CreateTime;
            parameters[9].Value = model.CreateAdminID;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(YK.Model.AdminInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Admin set ");
            strSql.Append("GroupID=@GroupID,");
            strSql.Append("AdminAccount=@AdminAccount,");
            strSql.Append("AdminPwd=@AdminPwd,");
            strSql.Append("AdminName=@AdminName,");
            strSql.Append("State=@State,");
            strSql.Append("AdminDesc=@AdminDesc,");
            strSql.Append("LastLoginIP=@LastLoginIP,");
            strSql.Append("LastLoginTime=@LastLoginTime,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("CreateAdminID=@CreateAdminID");
            strSql.Append(" where AdminID=@AdminID");
            SqlParameter[] parameters = {
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@AdminAccount", SqlDbType.VarChar,20),
					new SqlParameter("@AdminPwd", SqlDbType.VarChar,20),
					new SqlParameter("@AdminName", SqlDbType.VarChar,15),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@AdminDesc", SqlDbType.VarChar,100),
					new SqlParameter("@LastLoginIP", SqlDbType.VarChar,20),
					new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@CreateAdminID", SqlDbType.Int,4),
					new SqlParameter("@AdminID", SqlDbType.Int,4)};
            parameters[0].Value = model.GroupID;
            parameters[1].Value = model.AdminAccount;
            parameters[2].Value = model.AdminPwd;
            parameters[3].Value = model.AdminName;
            parameters[4].Value = model.State;
            parameters[5].Value = model.AdminDesc;
            parameters[6].Value = model.LastLoginIP;
            parameters[7].Value = model.LastLoginTime;
            parameters[8].Value = model.CreateTime;
            parameters[9].Value = model.CreateAdminID;
            parameters[10].Value = model.AdminID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 登录时更新IP及时间
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="datetime"></param>
        /// <param name="adminid"></param>
        /// <returns></returns>
        public bool UpdateIPAndTime(string ip, DateTime datetime, int adminid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Admin set ");
            strSql.Append("LastLoginIP=@LastLoginIP,");
            strSql.Append("LastLoginTime=@LastLoginTime");
            strSql.Append(" where AdminID=@AdminID");
            SqlParameter[] parameters = {
					new SqlParameter("@LastLoginIP", SqlDbType.VarChar,50),
					new SqlParameter("@LastLoginTime", SqlDbType.DateTime,20),
					new SqlParameter("@AdminID", SqlDbType.Int,4)};
            parameters[0].Value = ip;
            parameters[1].Value = datetime;
            parameters[2].Value = adminid;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int AdminID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Admin ");
            strSql.Append(" where AdminID=@AdminID");
            SqlParameter[] parameters = {
					new SqlParameter("@AdminID", SqlDbType.Int,4)
            };
            parameters[0].Value = AdminID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string AdminIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Admin ");
            strSql.Append(" where AdminID in (" + AdminIDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public YK.Model.AdminInfo GetModel(int AdminID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 AdminID,GroupID,AdminAccount,AdminPwd,AdminName,State,AdminDesc,LastLoginIP,LastLoginTime,CreateTime,CreateAdminID from Admin ");
            strSql.Append(" where AdminID=@AdminID");
            SqlParameter[] parameters = {
					new SqlParameter("@AdminID", SqlDbType.Int,4)
};
            parameters[0].Value = AdminID;

            YK.Model.AdminInfo model = new YK.Model.AdminInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["AdminID"] != null && ds.Tables[0].Rows[0]["AdminID"].ToString() != "")
                {
                    model.AdminID = int.Parse(ds.Tables[0].Rows[0]["AdminID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GroupID"] != null && ds.Tables[0].Rows[0]["GroupID"].ToString() != "")
                {
                    model.GroupID = int.Parse(ds.Tables[0].Rows[0]["GroupID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AdminAccount"] != null && ds.Tables[0].Rows[0]["AdminAccount"].ToString() != "")
                {
                    model.AdminAccount = ds.Tables[0].Rows[0]["AdminAccount"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AdminPwd"] != null && ds.Tables[0].Rows[0]["AdminPwd"].ToString() != "")
                {
                    model.AdminPwd = ds.Tables[0].Rows[0]["AdminPwd"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AdminName"] != null && ds.Tables[0].Rows[0]["AdminName"].ToString() != "")
                {
                    model.AdminName = ds.Tables[0].Rows[0]["AdminName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["State"] != null && ds.Tables[0].Rows[0]["State"].ToString() != "")
                {
                    model.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AdminDesc"] != null && ds.Tables[0].Rows[0]["AdminDesc"].ToString() != "")
                {
                    model.AdminDesc = ds.Tables[0].Rows[0]["AdminDesc"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LastLoginIP"] != null && ds.Tables[0].Rows[0]["LastLoginIP"].ToString() != "")
                {
                    model.LastLoginIP = ds.Tables[0].Rows[0]["LastLoginIP"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LastLoginTime"] != null && ds.Tables[0].Rows[0]["LastLoginTime"].ToString() != "")
                {
                    model.LastLoginTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastLoginTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateAdminID"] != null && ds.Tables[0].Rows[0]["CreateAdminID"].ToString() != "")
                {
                    model.CreateAdminID = int.Parse(ds.Tables[0].Rows[0]["CreateAdminID"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public YK.Model.AdminInfo GetModelByAccount(string account)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 AdminID,GroupID,AdminAccount,AdminPwd,AdminName,State,AdminDesc,LastLoginIP,LastLoginTime,CreateTime,CreateAdminID from Admin ");
            strSql.Append(" where AdminAccount=@AdminAccount");
            SqlParameter[] parameters = {
					new SqlParameter("@AdminAccount", SqlDbType.VarChar,50)
            };
            parameters[0].Value = account;
            YK.Model.AdminInfo model = new YK.Model.AdminInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["AdminID"] != null && ds.Tables[0].Rows[0]["AdminID"].ToString() != "")
                {
                    model.AdminID = int.Parse(ds.Tables[0].Rows[0]["AdminID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GroupID"] != null && ds.Tables[0].Rows[0]["GroupID"].ToString() != "")
                {
                    model.GroupID = int.Parse(ds.Tables[0].Rows[0]["GroupID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AdminAccount"] != null && ds.Tables[0].Rows[0]["AdminAccount"].ToString() != "")
                {
                    model.AdminAccount = ds.Tables[0].Rows[0]["AdminAccount"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AdminPwd"] != null && ds.Tables[0].Rows[0]["AdminPwd"].ToString() != "")
                {
                    model.AdminPwd = ds.Tables[0].Rows[0]["AdminPwd"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AdminName"] != null && ds.Tables[0].Rows[0]["AdminName"].ToString() != "")
                {
                    model.AdminName = ds.Tables[0].Rows[0]["AdminName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["State"] != null && ds.Tables[0].Rows[0]["State"].ToString() != "")
                {
                    model.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AdminDesc"] != null && ds.Tables[0].Rows[0]["AdminDesc"].ToString() != "")
                {
                    model.AdminDesc = ds.Tables[0].Rows[0]["AdminDesc"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LastLoginIP"] != null && ds.Tables[0].Rows[0]["LastLoginIP"].ToString() != "")
                {
                    model.LastLoginIP = ds.Tables[0].Rows[0]["LastLoginIP"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LastLoginTime"] != null && ds.Tables[0].Rows[0]["LastLoginTime"].ToString() != "")
                {
                    model.LastLoginTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastLoginTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateAdminID"] != null && ds.Tables[0].Rows[0]["CreateAdminID"].ToString() != "")
                {
                    model.CreateAdminID = int.Parse(ds.Tables[0].Rows[0]["CreateAdminID"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.*,b.AdminName as CreateAdminName,c.GroupName ");
            strSql.Append(" FROM Admin as a left join Admin as b on a.CreateAdminID=b.AdminID left join AdminGroup as c on a.GroupID=c.GroupID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" AdminID,GroupID,AdminAccount,AdminPwd,AdminName,State,AdminDesc,LastLoginIP,LastLoginTime,CreateTime,CreateAdminID ");
            strSql.Append(" FROM Admin ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "Admin";
            parameters[1].Value = "AdminID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method
    }
}

