using System;

namespace YUVViewer.Format
{
    public abstract class YUV
    {
        private byte[] mData;
        private int mW;
        private int mH;

        public YUV(byte[] data, int w, int h)
        {
            mData = data;
            mW = w;
            mH = h;
        }

        public static byte PixelRound(double v)
        {
            int result = (int)Math.Round(v);
            if (result < 0)
            {
                result = 0;
            }
            else if (result > 255)
            {
                result = 255;
            }
            return (byte)result;
        }

        //rgb转yuv
        public static void ToRGB(byte y, byte Cb, byte Cr, byte[] dstRGB, int dstIndex)
        {
            dstRGB[dstIndex + 0] = PixelRound(1.164 * (y - 16) + 1.596 * (Cr - 128));
            dstRGB[dstIndex + 1] = PixelRound(1.164 * (y - 16) - 0.813 * (Cr - 128) - 0.391 * (Cb - 128));
            dstRGB[dstIndex + 2] = PixelRound(1.164 * (y - 16) + 2.018 * (Cb - 128));
        }

        protected byte[] GetData()
        {
            if (mData != null && mW > 0 && mH > 0)
            {
                int size = GetBitCount();
                if (mData.Length < size)
                {
                    byte[] buf = new byte[size];
                    Array.Copy(mData, 0, buf, 0, mData.Length);
                    mData = buf;
                }
                return mData;
            }

            return null;
        }

        protected int GetWidth()
        {
            return mW;
        }

        protected int GetHeight()
        {
            return mH;
        }

        protected abstract int GetBitCount();

        public abstract void ToRGB24(byte[] dstBuf, int dstIndex);
    }
}
