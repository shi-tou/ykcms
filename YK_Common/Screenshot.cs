using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;

namespace YK.Common
{
    /// <summary>
    /// 获取图片类
    /// 添加人:杨良斌
    /// 添加时间：2012-11-16
    /// </summary>    
    public class Screenshot
    {
        public WebBrowser MyBrowser = null;
        private int S_Height;
        private int S_Width;
        private int F_Height;
        private int F_Width;
        private string MyURL;
        Bitmap bitMap = null;
        //屏幕高
        public int ScreenHeight
        {
            get { return S_Height; }
            set { S_Height = value; }
        }
        //屏幕宽
        public int ScreenWidth
        {
            get { return S_Width; }
            set { S_Width = value; }
        }
        //图片高
        public int ImageHeight
        {
            get { return F_Height; }
            set { F_Height = value; }
        }
        //图片宽
        public int ImageWidth
        {
            get { return F_Width; }
            set { F_Width = value; }
        }
        //页面URL
        public string WebURL
        {
            get { return MyURL; }
            set { MyURL = value; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="WebSite"></param>
        /// <param name="ScreenWidth"></param>
        /// <param name="ScreenHeight"></param>
        /// <param name="ImageWidth"></param>
        /// <param name="ImageHeight"></param>
        public Screenshot(string WebSite, int ScreenWidth, int ScreenHeight, int ImageWidth, int ImageHeight)
        {
            this.WebURL = WebSite;
            this.ScreenWidth = ScreenWidth;
            this.ScreenHeight = ScreenHeight;
            this.ImageHeight = ImageHeight;
            this.ImageWidth = ImageWidth;
        }
        public Bitmap GetWebPageImage()
        {
            Thread m_thread = new Thread(new ThreadStart(GetBitMap));
            m_thread.SetApartmentState(ApartmentState.STA);
            m_thread.Start();
            m_thread.Join();
            return bitMap;
        }
        /// <summary>
        /// 加载URL内容，获取图片
        /// </summary>
        public void GetBitMap()
        {
            MyBrowser = new WebBrowser();
            MyBrowser.Navigate(this.WebURL);
            MyBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(MyBrowser_DocumentCompleted);
            while (MyBrowser.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }
        }
       
        /// <summary>
        /// 绘制图片
        /// </summary>
        /// <param name="theight"></param>
        /// <param name="twidth"></param>
        /// <returns></returns>
        private void MyBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //WebBrowser m_WebBrowser = (WebBrowser)sender;

            //m_WebBrowser.ClientSize = new Size(int.Parse(m_WebBrowser.Document.Body.GetAttribute("scrollHeight")), int.Parse(m_WebBrowser.Document.Body.GetAttribute("scrollwidth")));

            //m_WebBrowser.ScrollBarsEnabled = false;

            //bitMap = new Bitmap(m_WebBrowser.Bounds.Width, m_WebBrowser.Bounds.Height);

            //m_WebBrowser.BringToFront();

            //m_WebBrowser.DrawToBitmap(bitMap, m_WebBrowser.Bounds);

            //bitMap = (Bitmap)bitMap.GetThumbnailImage(this.ImageWidth, this.ImageHeight, null, IntPtr.Zero);
            //网页控件
            WebBrowser m_WebBrowser = (WebBrowser)sender;
            this.ScreenWidth = int.Parse(m_WebBrowser.Document.Body.GetAttribute("scrollWidth"));
            this.ScreenHeight = int.Parse(m_WebBrowser.Document.Body.GetAttribute("scrollHeight"));
            this.ImageHeight = this.ScreenWidth * this.ScreenHeight / this.ImageWidth;
            m_WebBrowser.ClientSize = new Size(this.ScreenWidth, this.ScreenHeight);
            m_WebBrowser.ScrollBarsEnabled = false;
            m_WebBrowser.ScriptErrorsSuppressed = false;
            //创建临时位图
            Bitmap myBitmap = new Bitmap(this.ImageWidth, this.ImageHeight);
            //设置绘制的区域
            Rectangle DrawRect = new Rectangle(0, 0, this.ScreenWidth, this.ScreenHeight);
            //在临时位图上绘制指定区域的图像
            m_WebBrowser.DrawToBitmap(myBitmap, DrawRect);
            //处理位图
            System.Drawing.Image imgOutput = myBitmap;
            System.Drawing.Image oThumbNail = new Bitmap(this.ImageWidth, this.ImageHeight, imgOutput.PixelFormat);
            Graphics g = Graphics.FromImage(oThumbNail);
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            Rectangle oRectangle = new Rectangle(0, 0, this.ImageWidth, this.ImageHeight);
            g.DrawImage(imgOutput, oRectangle);
            try
            {
                bitMap = (Bitmap)oThumbNail;
            }
            catch (Exception ex)
            { }
            finally
            {
                imgOutput.Dispose();
                imgOutput = null;
            }
        }        
    }
}
