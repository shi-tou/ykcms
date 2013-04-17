using System;
using System.Collections.Generic;
using System.Web;
using YK.Model;
using YK.BLL;
using YK.Common;
using System.Data;

namespace YkCms.AppCode
{
    /// <summary>
    /// 文件：AjaxGroup
    /// 描述：产品操作类
    /// 创建人：杨良斌
    /// </summary>
    public class AjaxGoods
    {
        AdminInfo adminInfo = Function.GetCookiAdmin();
        Goods goods = new Goods();
        SysLog log = new SysLog();
        /// <summary>
        /// 添加/修改产品
        /// </summary>
        public void AddGoodsModel()
        {
            int goodsid = RequestHelper.GetRequestInt("goodsid", 0);
            int typeid = RequestHelper.GetRequestInt("typeid", 0);
            string goodsname = RequestHelper.GetRequestStr("goodsname", "");
            string unit = RequestHelper.GetRequestStr("unit", "");
            string price = RequestHelper.GetRequestStr("price", "");
            int disocout = RequestHelper.GetRequestInt("discount", 0);
            int count = RequestHelper.GetRequestInt("count", 0);
            string image = RequestHelper.GetRequestStr("image", "");
            string describe = RequestHelper.GetRequestStr("describe", "");                      
            GoodsInfo ginfo = new GoodsInfo();
            ginfo.TypeID = typeid;
            ginfo.GoodsName = goodsname;
            ginfo.Unit = unit;
            ginfo.Price = decimal.Parse(price);
            
            ginfo.Discount = disocout;
            ginfo.Count = count;
            ginfo.Image = image;
            ginfo.Describe = describe;
            ginfo.AdminID = adminInfo.AdminID;
            ginfo.CreateTime = DateTime.Now;
            if (goodsid == 0)//添加
            {
                goods.Add(ginfo);
                log.Add(new SysLogInfo("产品管理", "添加", "添加名称为【" + goodsname + "】的产品信息", Function.GetIP(), adminInfo.AdminID, adminInfo.AdminName, DateTime.Now));
                AjaxMsg.msg = "\"msg\":\"添加成功\"";
            }
            else//修改
            {
                ginfo.GoodsID = goodsid;
                goods.Update(ginfo);
                log.Add(new SysLogInfo("产品管理", "修改", "修改编号为【" + goodsid + "】的产品名称为", Function.GetIP(), adminInfo.AdminID, adminInfo.AdminName, DateTime.Now));
                AjaxMsg.msg = "\"msg\":\"修改成功\"";
            }
        }
        /// <summary>
        /// 获取产品列表
        /// </summary>
        public void GetGoodsList()
        {
            DataTable dt = goods.GetGoodsFroJoin("");
            AjaxMsg.msg = "\"rows\":" + JsonHelper.ToJson(dt, "") + ",\"total\":" + dt.Rows.Count;
        }
        /// <summary>
        /// 删除管理员
        /// </summary>
        public void DeleteGoods()
        {
            string goodsids = RequestHelper.GetRequestStr("goodsids", "0");
            goods.DeleteList(goodsids);
            new SysLog().Add(new SysLogInfo("产品管理", "删除", "删除了编号为【" + goodsids + "】的产品信息。", Function.GetIP(), adminInfo.AdminID, adminInfo.AdminName, DateTime.Now));
            AjaxMsg.msg = "\"msg\":\"删除成功\"";
        }
        /// <summary>
        /// 删除管理员
        /// </summary>
        public void GetGoodsModel()
        {
            string goodsid = RequestHelper.GetRequestStr("goodsid", "0");
            DataTable dt=goods.GetList("GoodsID=" + goodsid).Tables[0];
            if (dt.Rows.Count == 1)
            {
                AjaxMsg.msg = "\"msg\":" + JsonHelper.ToJson(dt, "").Replace("[", "").Replace("]", "") ;
            }
            else
            {
                AjaxMsg.msgOK = false;
                AjaxMsg.ex = "\"msg\":\"数据不存在！\"";
            }
            
        }
    }
}