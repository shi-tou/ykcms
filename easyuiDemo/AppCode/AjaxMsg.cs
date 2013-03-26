using System;
using System.Collections.Generic;
using System.Web;

namespace YkCms.AppCode
{
    public static class AjaxMsg
    {
        /// <summary>
        /// 保存异常信息(字符串格式)
        /// </summary>
        public static string ex = "";
        /// <summary>
        /// 操作正确完成后返回的信息（JSON串）
        /// </summary>
        public static string msg = "";
        /// <summary>
        /// 一个返回给前台js的状态标志，指示ajax处理是否按照预期被成功处理（默认为true）
        /// </summary>
        public static bool msgOK = true;
        //清空AjaxMsg信息
        public static void Clear()
        {
            ex = "";
            msg = "";
            msgOK = true;
        }
    }
}