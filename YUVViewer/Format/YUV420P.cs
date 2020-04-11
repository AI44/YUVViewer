namespace YUVViewer.Format
{
    public abstract class YUV420P : YUV420
    {
        public YUV420P(byte[] data, int w, int h) : base(data, w, h)
        {
        }

        protected override int GetUVStride()
        {
            return (GetWidth() + 1) >> 1;
        }

        protected override int GetUVStep()
        {
            return 1;
        }
    }
}
