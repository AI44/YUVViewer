namespace YUVViewer.Format
{
    public abstract class YUV420SP : YUV420
    {
        public YUV420SP(byte[] data, int w, int h) : base(data, w, h)
        {
        }

        protected override int GetUVStride()
        {
            return ((GetWidth() + 1) >> 1) << 1;
        }

        protected override int GetUVStep()
        {
            return 2;
        }
    }
}
