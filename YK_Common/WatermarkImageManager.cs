using System;
using System.Collections.Generic;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace YK.Common
{
    /// <summary>
    /// 有关图片水印管理的实体类
    /// 添加人：杨良斌
    /// 添加时间：2012-11-15
    /// </summary>
    public class WatermarkImageManager
    {
        #region 主要公开方法

        /// <summary>
        /// 图片保存质量值
        /// </summary>
        private static int _imageSaveQualityValue = 100;

        /// <summary>
        /// 为图片添加图片水印
        /// </summary>
        /// <param name="parameters">添加图片水印相关参数体</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns>已加水印的图片数据流</returns>
        public static MemoryStream DrawImage(ImageWatermarkParameters parameters, out string errorMsg)
        {
            errorMsg = string.Empty;

            try
            {
                // 参数检查
                if (!parameters.CheckAndSetDefault(out errorMsg))
                    throw new InvalidDataException(errorMsg);

                // 为源图片添加图片水印
                Bitmap finalImage = AddWatermarkImageForSourceImage(parameters, out errorMsg);
                if (finalImage == null && !string.IsNullOrEmpty(errorMsg))
                    throw new InvalidDataException("生成水印图片失败：" + errorMsg);

                // 获取Bitmap的MemoryStream
                return GetMemoryStreamFromBitmap(finalImage, parameters.SourceImageFileExtensionName);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message.ToString();
                return null;
            }
        }

        /// <summary>
        /// 为图片添加文字水印
        /// </summary>
        /// <param name="parameters">文字水印过程需要的参数体</param>
        /// <param name="errorMsg">异常信息</param>
        /// <returns>已加水印的图片数据流</returns>
        public static MemoryStream DrawText(TextWatermarkParameters parameters, out string errorMsg)
        {
            errorMsg = string.Empty;

            try
            {
                // 参数检查
                if (!parameters.CheckAndSetDefault(out errorMsg))
                    throw new InvalidDataException(errorMsg);

                // 为源图片添加文字水印
                Bitmap finalImage = AddWatermarkTextForSourceImage(parameters, out errorMsg);
                if (finalImage == null && !string.IsNullOrEmpty(errorMsg))
                    throw new InvalidDataException("生成水印图片失败：" + errorMsg);

                return GetMemoryStreamFromBitmap(finalImage, parameters.SourceImageFileExtensionName);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message.ToString();
                return null;
            }
        }

        /// <summary>
        /// 根据文件路径获取图片数据流
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static MemoryStream GetImageMemoryStream(string filePath, out string errorMsg)
        {
            MemoryStream ms = new MemoryStream();
            errorMsg = string.Empty;

            try
            {
                using (FileStream fs = File.OpenRead(filePath))
                { // 读取图片数据流
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    ms.Write(buffer, 0, buffer.Length);
                }

                return ms;
            }
            catch (Exception e)
            {
                errorMsg = e.Message.ToString();
                return null;
            }
        }

        #endregion

        #region 添加水印功能主体函数

        /// <summary>
        /// 为图片添加文字水印
        /// </summary>
        /// <param name="parameters">文字水印相关参数体</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>添加完水印的位图对象</returns>
        private static Bitmap AddWatermarkTextForSourceImage(TextWatermarkParameters parameters, out string errMsg)
        {
            Image srcImage = null;
            Graphics srcGraphics = null;
            errMsg = string.Empty;

            try
            {
                //
                // 获取源图片数据流
                MemoryStream scMs = parameters.SourceImageStream == null ?
                    GetImageMemoryStream(parameters.SourceImagePath, out errMsg) : parameters.SourceImageStream;
                if (scMs == null && !string.IsNullOrEmpty(errMsg))
                    throw new InvalidDataException("获取图片数据流失败！");

                //
                //
                srcImage = Image.FromStream(scMs as Stream);
                // 新建位图并设定分辨率，需要根据图像的像素格式来判断是否需要重新绘制该图像
                Bitmap srcBitmap = (IsPixelFormatIndexed(srcImage.PixelFormat)) ? 
                    RedrawBitmapByImage(srcImage) : 
                    new Bitmap(srcImage.Width, srcImage.Height, srcImage.PixelFormat);
                srcBitmap.SetResolution(srcImage.HorizontalResolution, srcImage.VerticalResolution);
                // 封装一个GDI+绘图图面，并设置图形品质
                srcGraphics = Graphics.FromImage(srcBitmap);
                srcGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                // 向源图片绘制到位图中
                srcGraphics.DrawImage(srcImage, new Rectangle(0, 0, srcImage.Width, srcImage.Height), 0, 0, srcImage.Width, srcImage.Height, GraphicsUnit.Pixel);


                //
                // 矩形的宽度和高度，SizeF有三个属性，分别为Height高，width宽，IsEmpty是否为空 
                SizeF crSize = srcGraphics.MeasureString(parameters.WatermarkText, parameters.WmTextFont);
                // 获取水印文字的起始位置
                float xPosOfWm, yPosOfWm = 0;
                GetWatermarkTextPosition(parameters.WmPosition, srcImage.Width, srcImage.Height, crSize.Width, crSize.Height, out xPosOfWm, out yPosOfWm);

                //
                // 文本样式
                StringFormat vmTextFormat = GetStringFormat();
                // 阴影画刷
                SolidBrush shadowBrush = GetShadowBrush();
                // 文字画刷
                SolidBrush semiTransBrush = GetTextBrush(parameters.Alpha);
                // 第一步，绘制阴影
                srcGraphics.DrawString(parameters.WatermarkText, parameters.WmTextFont, shadowBrush,
                    new PointF(xPosOfWm + parameters.TextShadowWidth, yPosOfWm + parameters.TextShadowWidth), vmTextFormat);
                // 第二步，绘制文字
                srcGraphics.DrawString(parameters.WatermarkText, parameters.WmTextFont, semiTransBrush, new PointF(xPosOfWm, yPosOfWm), vmTextFormat);

                return srcBitmap;
            }
            catch (Exception e)
            {
                errMsg = e.Message.ToString();
                return null;
            }
            finally
            { // 释放资源
                if (srcImage != null)
                    srcImage.Dispose();
                if (srcGraphics != null)
                    srcGraphics.Dispose();
            }
        }

        /// <summary>
        /// 为源图片添加图片水印
        /// </summary>
        /// <param name="parameters">参数集合</param>
        /// <param name="errMsg">错误消息</param>
        /// <returns>添加完水印的位图对象</returns>
        private static Bitmap AddWatermarkImageForSourceImage(ImageWatermarkParameters parameters, out string errMsg)
        {
            Image sourceImage = null;
            Bitmap sourceBitmap = null;
            Graphics sourceGraphics = null;
            Image watermarkImage = null;
            Graphics watermarkGraphics = null;
            errMsg = string.Empty;

            try
            {
                // 获取源图片和水印图片的数据流
                MemoryStream sourceImageStream, waterImageStream = null;
                if (GetSourceAndWatermarkImageStream(parameters, out sourceImageStream, out waterImageStream, out errMsg) == false)
                    throw new InvalidDataException(errMsg);


                sourceImage = Image.FromStream(sourceImageStream as Stream);
                // 实例化一个与源图大小一样、分辨率一致的位图对象，并将其载入Graphics对象中，以便后续编辑；
                // 需要根据图像的像素格式来判断是否需要重新绘制该图像
                sourceBitmap = (IsPixelFormatIndexed(sourceImage.PixelFormat)) ?
                    RedrawBitmapByImage(sourceImage) :
                    new Bitmap(sourceImage.Width, sourceImage.Height, sourceImage.PixelFormat);
                sourceBitmap.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
                sourceGraphics = Graphics.FromImage(sourceBitmap);

                // SmoothingMode：指定是否将平滑处理（消除锯齿）应用于直线、曲线和已填充区域的边缘。 
                // 成员名称 说明 
                // AntiAlias 指定消除锯齿的呈现。 
                // Default 指定不消除锯齿。 
                // HighQuality 指定高质量、低速度呈现。 
                // HighSpeed 指定高速度、低质量呈现。 
                // Invalid 指定一个无效模式。 
                // None 指定不消除锯齿。 
                sourceGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                // 将源图像Image对象添加到绘图板中
                sourceGraphics.DrawImage(sourceImage, new Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
                                            0, 0, sourceImage.Width, sourceImage.Height, GraphicsUnit.Pixel);


                //
                // 按照源图片的操作方式，需要创建水印图片的Image对象、位图对象，
                // 只是需要根据输入参数来调整水印图片在源图片中的位置、显示效果等。
                watermarkImage = new Bitmap(waterImageStream);
                // 这里尤其注意，生成新位图的时候，载入的是已经添加了源图片作为背景的位图哦！
                // 并将设定好分辨率的位图载入到新的Graphics实例中，以进一步绘制水印。
                Bitmap watermarkBitmap = new Bitmap(sourceBitmap);
                watermarkBitmap.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
                watermarkGraphics = Graphics.FromImage(watermarkBitmap);

                // 通过imageAttributes实例来设定水印图片的绘制效果，包括位置和现实效果等
                ImageAttributes imageAttributes = GetImageAttributesInstance(parameters.Alpha);
                // 获取水印图片的起始位置
                int xPosOfWm, yPosOfWm = 0;
                GetWatermarkImagePosition(parameters.WmPosition, sourceImage.Width, sourceImage.Height, watermarkImage.Width, watermarkImage.Height, out xPosOfWm, out yPosOfWm);
                // 绘制水印图片
                watermarkGraphics.DrawImage(watermarkImage, new Rectangle(xPosOfWm, yPosOfWm, watermarkImage.Width, watermarkImage.Height),
                                                0, 0, watermarkImage.Width, watermarkImage.Height, GraphicsUnit.Pixel, imageAttributes);

                return watermarkBitmap;
            }
            catch (Exception e)
            {
                errMsg = e.Message.ToString();
                return null;
            }
            finally
            {
                // 释放
                if (sourceImage != null)
                    sourceImage.Dispose();
                if (sourceBitmap != null)
                    sourceBitmap.Dispose();
                if (sourceGraphics != null)
                    sourceGraphics.Dispose();
                if (watermarkImage != null)
                    watermarkImage.Dispose();
                if (watermarkGraphics != null)
                    watermarkGraphics.Dispose();
            }
        }

        #endregion

        #region 内部辅助函数

        /// <summary>
        /// 获取编码参数数组
        /// </summary>
        /// <returns></returns>
        private static EncoderParameters GetEncoderParameters()
        {
            EncoderParameters eps = new EncoderParameters(1);
            EncoderParameter ep = new EncoderParameter(Encoder.Quality, _imageSaveQualityValue);
            eps.Param[0] = ep;

            return eps;
        }

        /// <summary>
        /// 根据图片格式获取编码信息
        /// </summary>
        /// <param name="mimeType">图片格式</param>
        /// <returns>编码信息</returns>
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        /// <summary>
        /// 获取最终图片保存的图像格式
        /// </summary>
        /// <param name="imageExtensionName">图片扩展名称</param>
        /// <returns>图片格式</returns>
        private static ImageCodecInfo GetFinalImageCodecInfo(string imageExtensionName)
        {
            switch (imageExtensionName)
            { 
                case ".gif":
                    return GetEncoderInfo("image/gif");
                case ".png":
                    return GetEncoderInfo("image/png");
                case ".jpg":
                default:
                    return GetEncoderInfo("image/jpeg");
            }
        }

        /// <summary>
        /// 获取文字画刷
        /// </summary>
        /// <param name="alpha">透明度</param>
        /// <returns></returns>
        private static SolidBrush GetTextBrush(float alpha)
        {
            // 从四个 ARGB 分量（alpha、红色、绿色和蓝色）值创建 Color 结构，这里设置透明度为153 
            // 这个画笔为描绘正式文字的笔刷，呈白色 
            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(Convert.ToInt32(255 * alpha), Color.White));
            return semiTransBrush;
        }

        /// <summary>
        /// 获取阴影画刷
        /// </summary>
        /// <returns></returns>
        private static SolidBrush GetShadowBrush()
        {
            // SolidBrush:定义单色画笔。画笔用于填充图形形状，如矩形、椭圆、扇形、多边形和封闭路径。 
            // 这个画笔为描绘阴影的画笔，呈灰色
            return new SolidBrush(Color.FromArgb(153, Color.Blue));
        }

        /// <summary>
        /// 获取文本布局信息
        /// </summary>
        /// <returns></returns>
        private static StringFormat GetStringFormat()
        {
            // 封装文本布局信息（如对齐、文字方向和 Tab 停靠位），显示操作（如省略号插入和国家标准 (National) 数字替换）和 OpenType 功能
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;

            return StrFormat;
        }

        /// <summary>
        /// Get image attribute instance.
        /// </summary>
        /// <param name="alpha">transparency value</param>
        /// <returns></returns>
        private static ImageAttributes GetImageAttributesInstance(float alpha)
        {
            // imageattributes instance which will control the related infomations for image.
            ImageAttributes imageAttributes = new ImageAttributes();

            // 图片映射：水印图被定义成拥有绿色背景色的图片被替换成透明 
            ColorMap colorMap = new ColorMap();
            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            imageAttributes.SetRemapTable(new ColorMap[] { colorMap }, ColorAdjustType.Bitmap);

            float[][] colorMatrixElements = new float[][] 
            { 
	            new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f}, // red红色 
	            new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f}, // green绿色 
	            new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f}, // blue蓝色 
	            new float[] {0.0f, 0.0f, 0.0f, alpha, 0.0f}, // 透明度 
	            new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
            };

            // ColorMatrix:定义包含 RGBA 空间坐标的 5 x 5 矩阵。 
            // ImageAttributes:类的若干方法通过使用颜色矩阵调整图像颜色。 
            imageAttributes.SetColorMatrix(new ColorMatrix(colorMatrixElements), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            return imageAttributes;
        }

        /// <summary>
        /// Calculate the watermark image position.
        /// </summary>
        /// <param name="position">position</param>
        /// <param name="srcWidth">source image width</param>
        /// <param name="srcHeight">source image height</param>
        /// <param name="wmWidth">watermark image width</param>
        /// <param name="wmHeight">watermark image height</param>
        /// <param name="xPosOfWm">final x position</param>
        /// <param name="yPosOfWm">final y position</param>
        private static void GetWatermarkImagePosition(ImagePosition position, int srcWidth, int srcHeight, int wmWidth, int wmHeight, out int xPosOfWm, out int yPosOfWm)
        {
            switch (position)
            {
                case ImagePosition.BottomMiddle:
                    xPosOfWm = (srcWidth - wmWidth) / 2;
                    yPosOfWm = srcHeight - wmHeight - 10;
                    break;
                case ImagePosition.Center:
                    xPosOfWm = (srcWidth - wmWidth) / 2;
                    yPosOfWm = (srcHeight - wmHeight) / 2;
                    break;
                case ImagePosition.LeftBottom:
                    xPosOfWm = 10;
                    yPosOfWm = srcHeight - wmHeight - 10;
                    break;
                case ImagePosition.LeftTop:
                    xPosOfWm = 10;
                    yPosOfWm = 10;
                    break;
                case ImagePosition.RightTop:
                    xPosOfWm = srcWidth - wmWidth - 10;
                    yPosOfWm = 10;
                    break;
                case ImagePosition.RigthBottom:
                    xPosOfWm = srcWidth - wmWidth - 10;
                    yPosOfWm = srcHeight - wmHeight - 10;
                    break;
                case ImagePosition.TopMiddle:
                    xPosOfWm = (srcWidth - wmWidth) / 2;
                    yPosOfWm = 10;
                    break;
                default:
                    xPosOfWm = 10;
                    yPosOfWm = srcHeight - wmHeight - 10;
                    break;
            }
        }

        /// <summary>
        /// Calculate the watermark image position.
        /// </summary>
        /// <param name="position">position</param>
        /// <param name="srcWidth">source image width</param>
        /// <param name="srcHeight">source image height</param>
        /// <param name="wmWidth">watermark image width</param>
        /// <param name="wmHeight">watermark image height</param>
        /// <param name="xPosOfWm">final x position</param>
        /// <param name="yPosOfWm">final y position</param>
        private static void GetWatermarkTextPosition(ImagePosition position, int srcWidth, int srcHeight, float wmWidth, float wmHeight, out float xPosOfWm, out float yPosOfWm)
        {
            switch (position)
            {
                case ImagePosition.BottomMiddle:
                    xPosOfWm = srcWidth / 2;
                    yPosOfWm = srcHeight - wmHeight - 10;
                    break;
                case ImagePosition.Center:
                    xPosOfWm = srcWidth / 2;
                    yPosOfWm = srcHeight / 2;
                    break;
                case ImagePosition.LeftBottom:
                    xPosOfWm = wmWidth;
                    yPosOfWm = srcHeight - wmHeight - 10;
                    break;
                case ImagePosition.LeftTop:
                    xPosOfWm = wmWidth / 2;
                    yPosOfWm = wmHeight / 2;
                    break;
                case ImagePosition.RightTop:
                    xPosOfWm = srcWidth - wmWidth - 10;
                    yPosOfWm = wmHeight;
                    break;
                case ImagePosition.RigthBottom:
                    xPosOfWm = srcWidth - wmWidth - 10;
                    yPosOfWm = srcHeight - wmHeight - 10;
                    break;
                case ImagePosition.TopMiddle:
                    xPosOfWm = srcWidth / 2;
                    yPosOfWm = wmWidth;
                    break;
                default:
                    xPosOfWm = wmWidth;
                    yPosOfWm = srcHeight - wmHeight - 10;
                    break;
            }
        }

        /// <summary>
        /// Check image extension type.
        /// </summary>
        /// <param name="sourceImagePath">image path</param>
        /// <returns>examine result</returns>
        private static bool CheckImageExtensionType(string imagePath)
        {
            string fileExtensionType = System.IO.Path.GetExtension(imagePath).ToLower();
            return (System.IO.File.Exists(imagePath) && (fileExtensionType == ".gif" || fileExtensionType == ".jpg" || fileExtensionType == ".png"));
        }

        /// <summary>
        /// 获取Bitmap的MemoryStream
        /// </summary>
        /// <param name="finalImage"></param>
        /// <returns></returns>
        private static MemoryStream GetMemoryStreamFromBitmap(Bitmap finalImage, string sourceImageFileExName)
        {
            MemoryStream ms = new MemoryStream();
            // 将位图保存到数据流中
            finalImage.Save(ms, GetFinalImageCodecInfo(sourceImageFileExName), GetEncoderParameters());
            return ms;
        }

        /// <summary>
        /// 获取源图片和水印图片的数据流
        /// </summary>
        /// <param name="parameters">参数实例</param>
        /// <param name="sourceImageStream">源图片数据流</param>
        /// <param name="waterImageStream">水印图片数据流</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns>是否获取成功</returns>
        private static bool GetSourceAndWatermarkImageStream(ImageWatermarkParameters parameters,
            out MemoryStream sourceImageStream, out MemoryStream waterImageStream, out string errorMsg)
        {
            errorMsg = string.Empty;

            // 根据是否设置图片路径来判断如何获取图片数据流，分别获取源图片和水印图片的数据流。
            string scErrMsg = string.Empty, wmErrMsg = string.Empty;
            sourceImageStream = parameters.SourceImageStream == null ?
                GetImageMemoryStream(parameters.SourceImagePath, out scErrMsg) : parameters.SourceImageStream;

            waterImageStream = parameters.WatermarkImageStream == null ?
                GetImageMemoryStream(parameters.WatermarkImagePath, out wmErrMsg) : parameters.WatermarkImageStream;

            if (!string.IsNullOrEmpty(scErrMsg) || !string.IsNullOrEmpty(wmErrMsg))
            {
                errorMsg = "通过图片路径获取数据流失败：" + scErrMsg + "；" + wmErrMsg;
                return false;
            }

            return true;
        }

        /// <summary>
        /// 判断图片的PixelFormat 是否在 引发异常的 PixelFormat 之中
        /// </summary>
        /// <param name="imgPixelFormat">原图片的PixelFormat</param>
        /// <returns></returns>
        private static bool IsPixelFormatIndexed(PixelFormat imgPixelFormat)
        {
            PixelFormat[] indexedPixelFormats = { 
                PixelFormat.Undefined, 
                PixelFormat.DontCare,
                PixelFormat.Format16bppArgb1555, 
                PixelFormat.Format1bppIndexed, 
                PixelFormat.Format4bppIndexed,
                PixelFormat.Format8bppIndexed
            };

            // 遍历匹配
            foreach (PixelFormat pf in indexedPixelFormats)
                if (pf.Equals(imgPixelFormat))
                    return true;

            return false;
        }

        /// <summary>
        /// 将原图未索引像素格式之类的Image转化为Bitmap
        /// </summary>
        /// <param name="image">图像实体</param>
        /// <returns>位图实体</returns>
        private static Bitmap RedrawBitmapByImage(Image image) 
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(image, 0, 0);
            }

            return bmp;
        }

        /// <summary>
        /// 保证得到内部统一使用的文件扩展格式
        /// </summary>
        /// <param name="extensionName"></param>
        /// <returns></returns>
        private static string TransferContentTypeToUnifyExName(string contentType)
        {
            string exName = string.Empty;
            switch (contentType.ToLower())
            {
                case "image/jpeg":
                case "image/pjpeg":
                    exName = ".jpg";
                    break;
                case "image/gif":
                    exName = ".gif";
                    break;
                case "image/x-png":
                case "image/png":
                    exName = ".png";
                    break;
                default:
                    exName = contentType;
                    break;
            }

            return exName;
        }

        #endregion

        #region 内部辅助枚举、结构

        /// <summary>
        /// 水印位置枚举
        /// </summary>
        public enum ImagePosition
        {
            LeftTop, //左上 
            LeftBottom, //左下 
            RightTop, //右上 
            RigthBottom, //右下 
            TopMiddle, //顶部居中 
            BottomMiddle, //底部居中 
            Center //中心 
        }

        /// <summary>
        /// 图片水印参数结构体
        /// </summary>
        public struct ImageWatermarkParameters
        {
            /// <summary>
            /// 源图片路径
            /// </summary>
            public string SourceImagePath;
            /// <summary>
            /// 水印图片路径
            /// </summary>
            public string WatermarkImagePath;
            /// <summary>
            /// 源图片数据流（即如果设置数据流，则忽略SourceImagePath属性值）
            /// </summary>
            public MemoryStream SourceImageStream;
            /// <summary>
            /// 水印图片数据流（即如果设置数据流，则忽略WatermarkImagePath属性值）
            /// </summary>
            public MemoryStream WatermarkImageStream;
            /// <summary>
            /// 透明度，数值介于0.1 - 1.0之间，不包括0.0
            /// </summary>
            public float Alpha;
            /// <summary>
            /// 水印位置
            /// </summary>
            public ImagePosition WmPosition;
            private string _sourceImageFileExtensionName;
            /// <summary>
            /// 源图片文件扩展名
            /// </summary>
            public string SourceImageFileExtensionName
            {
                set { _sourceImageFileExtensionName = value; }
                get
                {
                    if (string.IsNullOrEmpty(this.SourceImagePath))
                    {
                        // 如果设置的图片扩展名为ContentType类型，则进行转化
                        if (_sourceImageFileExtensionName.IndexOf("/") > 0)
                            _sourceImageFileExtensionName = TransferContentTypeToUnifyExName(_sourceImageFileExtensionName);

                        return _sourceImageFileExtensionName;
                    }
                    else
                        return Path.GetExtension(SourceImagePath).ToLower();
                }
            }


            public ImageWatermarkParameters(string sourceImagePath, string watermarkImagePath, float alpha, ImagePosition wmPosition)
            {
                this.SourceImageStream = null;
                this.WatermarkImageStream = null;
                this._sourceImageFileExtensionName = string.Empty;

                this.SourceImagePath = sourceImagePath;
                this.WatermarkImagePath = watermarkImagePath;
                this.Alpha = alpha;
                this.WmPosition = wmPosition;
            }

            public ImageWatermarkParameters(MemoryStream sourceImageStream, MemoryStream watermarkImageStream, 
                string sourceImageFileExtensionName, float alpha, ImagePosition wmPosition)
            {
                this.SourceImagePath = string.Empty;
                this.WatermarkImagePath = string.Empty; ;

                this.SourceImageStream = sourceImageStream;
                this.WatermarkImageStream = watermarkImageStream;
                this.Alpha = alpha;
                this.WmPosition = wmPosition;
                this._sourceImageFileExtensionName = sourceImageFileExtensionName;
            }

            /// <summary>
            /// 参数检查并设定默认值
            /// </summary>
            /// <param name="errorMsg"></param>
            /// <returns></returns>
            public bool CheckAndSetDefault(out string errorMsg)
            {
                errorMsg = string.Empty;

                // 前置检查
                if ((string.IsNullOrEmpty(this.SourceImagePath) && SourceImageStream == null)
                    || (string.IsNullOrEmpty(this.WatermarkImagePath) && WatermarkImageStream == null)
                    || (SourceImageStream != null && string.IsNullOrEmpty(_sourceImageFileExtensionName))
                    || this.Alpha <= 0.0 || this.Alpha > 1.0
                    || this.WmPosition == null)
                {
                    errorMsg = "文字水印参数实体中含有非法的参数项。";
                    return false;
                }

                // 检查图片路径是否合法
                if ((SourceImageStream == null && !string.IsNullOrEmpty(this.SourceImagePath) && !File.Exists(this.SourceImagePath)) // 仅赋值源图片路径，且路径不存在的情况
                    || (WatermarkImageStream == null && !string.IsNullOrEmpty(this.WatermarkImagePath) && !File.Exists(this.WatermarkImagePath))) // 仅赋值水印图片路径，但文件不存在的情况
                {
                    errorMsg = "输入的源图片或水印图片路径不存在。";
                    return false;
                }


                // 检查图片扩展名
                bool validExName = true;
                if (!string.IsNullOrEmpty(this.SourceImagePath) && !string.IsNullOrEmpty(this.WatermarkImagePath))
                {
                    if (!CheckImageExtensionType(this.SourceImagePath) || !CheckImageExtensionType(this.WatermarkImagePath))
                        validExName = false;
                }
                else if (this.SourceImageStream != null && this.WatermarkImageStream != null)
                {
                    if ((_sourceImageFileExtensionName != ".gif" && _sourceImageFileExtensionName != ".jpg" && _sourceImageFileExtensionName != ".png"))
                        validExName = false;
                }
                else
                    validExName = false;

                if (!validExName)
                {
                    errorMsg = "暂不支持源图片或水印图片的格式类型。";
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// 文字水印参数结构体
        /// </summary>
        public struct TextWatermarkParameters
        {
            /// <summary>
            /// 源图片路径
            /// </summary>
            public string SourceImagePath;
            /// <summary>
            /// 水印文字
            /// </summary>
            public string WatermarkText;
            /// <summary>
            /// 源图片数据流
            /// </summary>
            public MemoryStream SourceImageStream;
            /// <summary>
            /// 透明度，数值介于0.1 - 1.0之间，不包括0.0
            /// </summary>
            public float Alpha;
            /// <summary>
            /// 水印位置
            /// </summary>
            public ImagePosition WmPosition;
            /// <summary>
            /// 水印文字字体样式
            /// </summary>
            public Font WmTextFont;
            /// <summary>
            /// 水印文字阴影宽度
            /// </summary>
            public int TextShadowWidth;
            private string _sourceImageFileExtensionName;
            /// <summary>
            /// 源图片文件扩展名
            /// </summary>
            public string SourceImageFileExtensionName
            {
                set { _sourceImageFileExtensionName = value; }
                get
                {
                    if (string.IsNullOrEmpty(this.SourceImagePath))
                    {
                        // 如果设置的图片扩展名为ContentType类型，则进行转化
                        if (_sourceImageFileExtensionName.IndexOf("/") > 0)
                            _sourceImageFileExtensionName = TransferContentTypeToUnifyExName(_sourceImageFileExtensionName);

                        return _sourceImageFileExtensionName;
                    }
                    else
                        return Path.GetExtension(SourceImagePath).ToLower();
                }
            }


            public TextWatermarkParameters(MemoryStream sourceImageStream, string sourceImageFileExtensionName, string watermarkText, float alpha, 
                ImagePosition wmPosition, Font wmTextFont, int textShadowWidth = 5)
            {
                this.SourceImagePath = string.Empty ;

                this.SourceImageStream = sourceImageStream;
                this.WatermarkText = watermarkText;
                this.Alpha = alpha;
                this.WmPosition = wmPosition;
                this.WmTextFont = wmTextFont;
                this.TextShadowWidth = textShadowWidth;
                this._sourceImageFileExtensionName = sourceImageFileExtensionName;
            }

            public TextWatermarkParameters(string sourceImagePath, string watermarkText, float alpha,
                ImagePosition wmPosition, Font wmTextFont, int textShadowWidth = 5)
            {
                this.SourceImageStream = null;
                this._sourceImageFileExtensionName = string.Empty;

                this.SourceImagePath = sourceImagePath;
                this.WatermarkText = watermarkText;
                this.Alpha = alpha;
                this.WmPosition = wmPosition;
                this.WmTextFont = wmTextFont;
                this.TextShadowWidth = textShadowWidth;
            }

            /// <summary>
            /// 参数检查并设定默认值
            /// </summary>
            /// <param name="errorMsg"></param>
            /// <returns></returns>
            public bool CheckAndSetDefault(out string errorMsg)
            {
                errorMsg = string.Empty;

                // 前置检查
                if ((string.IsNullOrEmpty(this.SourceImagePath) && this.SourceImageStream == null) || (!string.IsNullOrEmpty(this.SourceImagePath) && !File.Exists(this.SourceImagePath)) 
                    || string.IsNullOrEmpty(this.WatermarkText)
                    || (SourceImageStream != null && string.IsNullOrEmpty(_sourceImageFileExtensionName))
                    || this.Alpha <= 0.0 || this.Alpha > 1.0
                    || this.WmPosition == null
                    || this.WmTextFont == null)
                {
                    errorMsg = "文字水印参数实体中含有非法的参数项。";
                    return false;
                }

                // 检查图片路径是否合法
                if ((SourceImageStream == null && !string.IsNullOrEmpty(this.SourceImagePath) && !File.Exists(this.SourceImagePath))) // 仅赋值源图片路径，且路径不存在的情况
                {
                    errorMsg = "输入的源图片路径不存在。";
                    return false;
                }

                // 检查图片扩展名
                bool validExName = true;
                if (!string.IsNullOrEmpty(this.SourceImagePath))
                {
                    if (!CheckImageExtensionType(this.SourceImagePath))
                        validExName = false;
                }
                else if (this.SourceImageStream != null)
                {
                    if ((_sourceImageFileExtensionName != ".gif" && _sourceImageFileExtensionName != ".jpg" && _sourceImageFileExtensionName != ".png"))
                        validExName = false;
                }
                else
                    validExName = false;

                if (!validExName)
                {
                    errorMsg = "暂不支持源图片的格式类型。";
                    return false;
                }

                return true;
            }
        }

        #endregion
    }
}