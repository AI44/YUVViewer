ffmpeg -i org_1920x1200.jpg -pix_fmt nv12 nv12_1920x1200.yuv
ffmpeg -i org_1920x1200.jpg -pix_fmt nv21 nv21_1920x1200.yuv
ffmpeg -i org_1920x1200.jpg -pix_fmt yuv420p yuv420p_1920x1200.yuv

ffmpeg -i org_1281x801.jpg -pix_fmt nv12 nv12_1281x801.yuv
ffmpeg -i org_1281x801.jpg -pix_fmt nv21 nv21_1281x801.yuv
ffmpeg -i org_1281x801.jpg -pix_fmt yuv420p yuv420p_1281x801.yuv

ffmpeg -i org_5x5.jpg -pix_fmt nv12 nv12_5x5.yuv
ffmpeg -i org_5x5.jpg -pix_fmt nv21 nv21_5x5.yuv
ffmpeg -i org_5x5.jpg -pix_fmt yuv420p yuv420p_5x5.yuv