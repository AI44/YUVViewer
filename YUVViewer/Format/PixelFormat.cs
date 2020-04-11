namespace YUVViewer
{
    public enum PixelFormat
    {
        YUV420P,    // planar YUV 4:2:0, 12bpp, (1 Cr & Cb sample per 2x2 Y samples)
                    // 是基于 planar 平面模式进行存储，先存储所有的 Y 分量，
                    // 然后存储所有的 U 分量或者 V 分量。
                    // 默认为YU12。
                    // YU12(android平台下的 I420 格式) 和 YV12 格式都属于 YUV420P 类型，
                    // 区别在于：YU12 是先 Y 再 U 后 V，而 YV12 是先 Y 再 V 后 U 。

        YV12,       // as above, but U and V bytes are swapped.
                    // 属于 YUV420P 类型

        YUYV422,    // packed YUV 4:2:2, 16bpp, Y0 Cb Y1 Cr

        YUV422P,    // planar YUV 4:2:2, 16bpp, (1 Cr & Cb sample per 2x1 Y samples)

        YUV444P,    // planar YUV 4:4:4, 24bpp, (1 Cr & Cb sample per 1x1 Y samples)

        YUV410P,    // planar YUV 4:1:0,  9bpp, (1 Cr & Cb sample per 4x4 Y samples)

        YUV411P,    // planar YUV 4:1:1, 12bpp, (1 Cr & Cb sample per 4x1 Y samples)

        NV12,       // planar YUV 4:2:0, 12bpp, 1 plane for Y and 1 plane for the UV components,
                    // which are interleaved (first byte U and the following byte V)
                    // 属于 YUV420SP 类型，是 IOS 中有的模式，它的存储顺序是先存 Y 分量，
                    // 再 UV 进行交替存储。
                    // YUV420SP 是基于 planar 平面模式存储，与 YUV420P 的区别在于
                    // 它的 U、V 分量是按照 UV 或者 VU 交替顺序进行存储。

        NV21,       // as above, but U and V bytes are swapped.
                    // 属于 YUV420SP 类型，是 安卓 中有的模式，它的存储顺序是先存 Y 分量，
                    // 在 VU 交替存储。

        YUV440P,    // planar YUV 4:4:0 (1 Cr & Cb sample per 1x2 Y samples)
    }
}
