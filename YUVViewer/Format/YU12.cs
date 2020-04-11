namespace YUVViewer.Format
{
    //又称I420
    public class YU12 : YUV420P
    {
        public YU12(byte[] data, int w, int h) : base(data, w, h)
        {
        }

        protected override int GetUOffset()
        {
            return 0;
        }

        protected override int GetVOffset()
        {
            return GetUBitCount();
        }
    }
}
