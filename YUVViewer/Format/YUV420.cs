namespace YUVViewer.Format
{
    public abstract class YUV420 : YUV
    {
        public YUV420(byte[] data, int w, int h) : base(data, w, h)
        {
        }

        //获取u或v的的字节大小
        protected int GetUBitCount()
        {
            return ((GetWidth() + 1) >> 1) * ((GetHeight() + 1) >> 1);
        }

        //获取整个yuv图片的字节大小
        protected override int GetBitCount()
        {
            return GetWidth() * GetHeight() + (GetUBitCount() << 1);
        }

        private class Counter
        {
            private int mW;
            private int mStride;
            private int mStep;
            private int mCount = 0;

            private int mOffset;
            private bool mWAdd = false;
            private bool mHAdd = false;

            public Counter(int w, int stride, int step, int offset)
            {
                mW = w;
                mStride = stride;//一行占的字节数
                mStep = step;
                mOffset = offset;
            }

            public int Count()
            {
                int re = mOffset;
                mCount++;
                if (mCount >= mW)
                {
                    //跳到下一行，注意行末可能单像素使用一个uv
                    mCount = 0;
                    mWAdd = false;
                    mOffset += mStep;
                    if (mHAdd)
                    {
                        mHAdd = false;
                    }
                    else
                    {
                        //隔行，纵向2像素使用一个uv
                        mHAdd = true;
                        mOffset -= mStride;
                    }
                }
                else
                {
                    if (mWAdd)
                    {
                        //横向2像素使用一个uv
                        mWAdd = false;
                        mOffset += mStep;
                    }
                    else
                    {
                        mWAdd = true;
                    }
                }
                //System.Console.WriteLine("offset : " + re);
                return re;
            }
        }

        public override void ToRGB24(byte[] dstBuf, int dstIndex)
        {
            //************test start*****************
            //const int SIZE = 5;
            //Counter indexer = new Counter(SIZE, ((SIZE + 1) >> 1) << 1, 2, 0);
            //for (int i = 0; i < SIZE * SIZE; i++)
            //{
            //    System.Console.WriteLine("i : " + indexer.Count());
            //}
            //************test end*******************

            byte[] src = GetData();
            if (src != null)
            {
                int w = GetWidth();
                int h = GetHeight();
                int len = w * h;
                Counter uvOffset = new Counter(w, GetUVStride(), GetUVStep(), len);
                int uOffset = GetUOffset();
                int vOffset = GetVOffset();

                int uvIndex;
                for (int i = 0; i < len; i++)
                {
                    uvIndex = uvOffset.Count();

                    ToRGB(src[i], src[uvIndex + uOffset], src[uvIndex + vOffset], dstBuf, dstIndex);
                    dstIndex += 3;
                }
            }
        }

        protected abstract int GetUVStride();//获取图片一行的uv字节跨度

        protected abstract int GetUVStep();//2个u/v之间的跨度

        protected abstract int GetUOffset();

        protected abstract int GetVOffset();
    }
}
