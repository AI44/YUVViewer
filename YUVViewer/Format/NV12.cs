namespace YUVViewer.Format
{
    public class NV12 : YUV420SP
    {
        public NV12(byte[] data, int w, int h) : base(data, w, h)
        {
        }

        protected override int GetUOffset()
        {
            return 0;
        }

        protected override int GetVOffset()
        {
            return 1;
        }
    }
}
