using System;
using System.Collections.Generic;
using System.Text;

namespace YK.Config
{
    /// <summary>
    /// 网站信息设置描述类, 加[Serializable]标记为可序列化
    /// </summary>
    [Serializable]
    public class WaterImageConfigInfo : IConfigInfo
    {
        #region 私有字段
        private int _waterImageType = 0;
        private string _waterImageUrl = "../../Files/WaterMarkImage/WaterMarkImage.gif";
        private string _waterText = "水印文件";
        private float _transparency = 1f;
        private string _position = "";
        private int _fontSize = 10;
        private string _fontFamily = "宋体";
        private string _fontColor = "#ffffff";
        private int _bold = 0;
        private int _italic = 0;         
        #endregion

        #region 属性
        /// <summary>
        /// 水印方式 =
        /// </summary>
        public int WaterImageType
        {
            get { return _waterImageType; }
            set { _waterImageType = value; }
        }
        /// <summary>
        /// 水印图片
        /// </summary>
        public string WaterImageUrl
        {
            get { return _waterImageUrl; }
            set { _waterImageUrl = value; }
        }
        /// <summary>
        /// 水印文字
        /// </summary>
        public string WaterText
        {
            get { return _waterText; }
            set { _waterText = value; }
        }
        /// <summary>
        /// 水印透明度
        /// </summary>
        public float Transparency
        {
            get { return _transparency; }
            set { _transparency = value; }
        }
        /// <summary>
        /// 水印位置
        /// </summary>
        public string Position
        {
            get { return _position; }
            set { _position = value; }
        }
        /// <summary>
        /// 文字大小
        /// </summary>
        public int FontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; }
        }
        /// <summary>
        /// 文字字体
        /// </summary>
        public string FontFamily
        {
            get { return _fontFamily; }
            set { _fontFamily = value; }
        }
        /// <summary>
        /// 水印文字颜色
        /// </summary>
        public string FontColor
        {
            get { return _fontColor; }
            set { _fontColor = value; }
        }
        /// <summary>
        /// 是否粗体
        /// </summary>
        public int Bold
        {
            get { return _bold; }
            set { _bold = value; }
        }
        /// <summary>
        /// 是否斜体
        /// </summary>
        public int Italic
        {
            get { return _italic; }
            set { _italic = value; }
        }
       
        #endregion
    }
}

