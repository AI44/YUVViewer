using System.Windows.Media.Imaging;

namespace YUVViewer
{
    public class RGB24Data
    {
        public System.Windows.Media.PixelFormat format;
        public int stride;
        public byte[] data;

        public RGB24Data()
        {
        }

        public RGB24Data(byte[] data, System.Windows.Media.PixelFormat format, int stride)
        {
            this.data = data;
            this.format = format;
            this.stride = stride;
        }
    }

    public class PixelFactory
    {
        public static RGB24Data CreateRGB24Data(byte[] yuvData, PixelFormat format, int w, int h)
        {
            System.Windows.Media.PixelFormat pf = System.Windows.Media.PixelFormats.Rgb24;
            int rawStride = (w * pf.BitsPerPixel + 7) / 8;
            byte[] rawImage = new byte[rawStride * h];

            //********test start*************
            //for (int i = 1; i < rawImage.Length; i += 3)
            //{
            //    rawImage[i] = 0xff;
            //}
            //********test end****************
            if (yuvData != null)
            {
                switch (format)
                {
                    case PixelFormat.YUV420P:
                        new Format.YU12(yuvData, w, h).ToRGB24(rawImage, 0);
                        break;
                    case PixelFormat.YV12:
                        new Format.YV12(yuvData, w, h).ToRGB24(rawImage, 0);
                        break;
                    case PixelFormat.YUYV422:
                        //fixme 未完成
                        break;
                    case PixelFormat.YUV422P:
                        //fixme 未完成
                        break;
                    case PixelFormat.YUV444P:
                        //fixme 未完成
                        break;
                    case PixelFormat.YUV410P:
                        //fixme 未完成
                        break;
                    case PixelFormat.YUV411P:
                        //fixme 未完成
                        break;
                    case PixelFormat.NV12:
                        new Format.NV12(yuvData, w, h).ToRGB24(rawImage, 0);
                        break;
                    case PixelFormat.NV21:
                        new Format.NV21(yuvData, w, h).ToRGB24(rawImage, 0);
                        break;
                    case PixelFormat.YUV440P:
                        //fixme 未完成
                        break;
                }
            }

            return new RGB24Data(rawImage, pf, rawStride);
        }

        public static BitmapSource CreateBmp(byte[] yuvData, PixelFormat format, int w, int h)
        {
            //long time = DateTime.Now.Ticks;
            RGB24Data rgb24 = CreateRGB24Data(yuvData, format, w, h);
            //Console.WriteLine("time : " + ((DateTime.Now.Ticks - time) / 10000));

            return BitmapSource.Create(w, h, 96, 96, rgb24.format, null, rgb24.data, rgb24.stride);
        }
    }
}
