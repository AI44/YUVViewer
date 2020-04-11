namespace YUVViewer.Format
{
    public class YV12 : YUV420P
    {
        public YV12(byte[] data, int w, int h) : base(data, w, h)
        {
        }

        protected override int GetUOffset()
        {
            return GetUBitCount();
        }

        protected override int GetVOffset()
        {
            return 0;
        }
    }
}
