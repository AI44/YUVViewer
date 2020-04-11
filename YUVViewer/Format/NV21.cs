namespace YUVViewer.Format
{
    public class NV21 : YUV420SP
    {
        public NV21(byte[] data, int w, int h) : base(data, w, h)
        {
        }

        protected override int GetUOffset()
        {
            return 1;
        }

        protected override int GetVOffset()
        {
            return 0;
        }
    }
}
