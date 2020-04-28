using System.Drawing;
using ZXing;
using ZXing.Common;
using BarcodeFormat = ZXing.BarcodeFormat;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 条形码帮助类
    /// </summary>
    public class BarCodeHelper
    {
        /// <summary>
        /// 生成条形码
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns>位图</returns>
        public static Bitmap Encoder(string content)
        {
            return Encoder(content, 320, 50);
        }
        /// <summary>
        /// 生成条形码
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        public static Bitmap Encoder(string content, int width, int height)
        {
            BarcodeWriter<Bitmap> writer = new BarcodeWriter<Bitmap>();

            writer.Format = BarcodeFormat.CODE_128;
            EncodingOptions options = new EncodingOptions()
            {
                Width = width,
                Height = height,
                Margin = 2,
                PureBarcode = true,
            };
            writer.Options = options;
            Bitmap map = writer.Write(content);
            return map;
        }
    }
}

