using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace YUVViewer
{
    /// <summary>
    /// 生成测试文件命令行：
    /// ffmpeg -i org.jpg -s 1280x720 -pix_fmt nv12 nv12.yuv
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static readonly PixelFormat[] FORMATS = {
            PixelFormat.YUV420P,
            PixelFormat.YV12,
            PixelFormat.YUYV422,
            PixelFormat.YUV422P,
            PixelFormat.YUV444P,
            PixelFormat.YUV410P,
            PixelFormat.YUV411P,
            PixelFormat.NV12,
            PixelFormat.NV21,
            PixelFormat.YUV440P,
        };

        public MainWindow()
        {
            InitializeComponent();

            if (!Enum.TryParse(Properties.Settings.Default.YuvType, out mYuvType))
            {
                mYuvType = PixelFormat.NV21;
            }
            mW = Properties.Settings.Default.Width;
            if (mW < 1)
            {
                mW = 1920;
            }
            mH = Properties.Settings.Default.Height;
            if (mH < 1)
            {
                mH = 1080;
            }

            mFr.DataContext = this;
            mImageHolder.SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateUI();
        }

        private void UpdateImage()
        {
            mImg.Source = PixelFactory.CreateBmp(mSrcYuvData, mYuvType, mW, mH);

            UpdateUI();
        }

        private void UpdateUI()
        {
            double r = (double)mW / mH;
            double frW = mImageHolder.ActualWidth;
            double frH = mImageHolder.ActualHeight;
            double x = 0;
            double y = 0;
            double w = 0;
            double h = 0;
            if (r > 0)
            {
                w = r * frH;
                h = frH;
                if (r * frH > frW)
                {
                    w = frW;
                    h = frW / r;
                }
                x = Math.Round((frW - w) / 2);
                y = Math.Round((frH - h) / 2);
            }
            mImg.Width = Math.Round(w);
            mImg.Height = Math.Round(h);
            mImg.SetValue(Canvas.LeftProperty, x);
            mImg.SetValue(Canvas.TopProperty, y);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private PixelFormat mYuvType;
        public PixelFormat _YuvType
        {
            get
            {
                return mYuvType;
            }
            set
            {
                mYuvType = value;
                UpdateImage();
            }
        }

        private int mW;
        public int _ImageWidth
        {
            get
            {
                return mW;
            }
            set
            {
                mW = value;
                if (mW < 1)
                {
                    mW = 1;
                }
                else if (mW > 4096)
                {
                    mW = 4096;
                }
                Notify();
                UpdateImage();
            }
        }

        private int mH;
        public int _ImageHeight
        {
            get
            {
                return mH;
            }
            set
            {
                mH = value;
                if (mH < 1)
                {
                    mH = 1;
                }
                else if (mH > 4096)
                {
                    mH = 4096;
                }
                Notify();
                UpdateImage();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.YuvType = mYuvType.ToString();
            Properties.Settings.Default.Width = mW;
            Properties.Settings.Default.Height = mH;
            Properties.Settings.Default.Save();
        }

        private byte[] mSrcYuvData;

        private void mOpenBtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = ".yuv";
            ofd.Filter = "yuv file|*.yuv|all file|*";
            if ((bool)ofd.ShowDialog())
            {
                //获取名称上的分辨率信息(宽x高)
                string name = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName).ToLower();
                if (name.Contains("x"))
                {
                    string[] ss = name.Split('_');
                    foreach (string temp in ss)
                    {
                        if (temp.Contains("x"))
                        {
                            string[] ss2 = temp.Split('x');
                            if (ss2.Length > 1)
                            {
                                int w;
                                int h;
                                if (int.TryParse(ss2[0], out w) && int.TryParse(ss2[1], out h))
                                {
                                    if (w > 0 && h > 0)
                                    {
                                        _ImageWidth = w;
                                        _ImageHeight = h;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                //读取yuv数据
                mSrcYuvData = File.ReadAllBytes(ofd.FileName);

                UpdateImage();
            }
        }

        private void mSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.DefaultExt = ".yuv";
            sfd.Filter = "yuv file|*.yuv";
            sfd.FileName = 1 + ".json";
            if ((bool)sfd.ShowDialog())
            {
                //Console.WriteLine(sfd.FileName);
                string parentPath = Directory.GetParent(sfd.FileName).FullName;
                Directory.CreateDirectory(parentPath);
                //File.WriteAllText(sfd.FileName, "", new System.Text.UTF8Encoding(false));
                //fixme 未完成保存
                //打开文件夹
                System.Diagnostics.Process.Start("Explorer.exe", "/e," + parentPath);
            }
        }
    }
}
