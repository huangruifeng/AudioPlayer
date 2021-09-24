using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace AudioPlayer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        string audioDirect = "audiofile";
        List<String> AudioFiles = new List<string>();

        MediaPlayer player = new MediaPlayer();//实例化绘图媒体
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            body.Opacity = 0.3;
            body.MouseLeftButtonDown += MainWindow_MouseLeftButtonDown;
            MouseMove += MainWindow_MouseMove;
            MouseLeftButtonUp += MainWindow_MouseLeftButtonUp;

            DirectoryInfo folder = new DirectoryInfo(audioDirect);
            GetFIles(folder,"*.mp3");
            GetFIles(folder, "*.wav");
        }

        private void GetFIles(DirectoryInfo folder,String filter)
        {
            var files = folder.GetFiles(filter);
            foreach (var file in files)
            {
                AudioFiles.Add(file.Name);
            }
        }

        private string GetFileForStrategy()
        {
            //todo read strategy from file;
            var size = AudioFiles.Count;

            if (size <= 0)
                return "";

            Random rd = new Random(Guid.NewGuid().GetHashCode());
            int s = rd.Next(0, size - 1);
            //random
            return audioDirect +"/" + AudioFiles[s];
        }

        private void Play(string file)
        {
            if (File.Exists(file))
            {
                player.Stop();
                player.Open(new Uri(file, UriKind.RelativeOrAbsolute));
                player.Play();
            }
        }
        private void MainWindow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            body.Opacity = 0.7;
            if (!isMouseMove){

                var file = GetFileForStrategy();
                if (!string.IsNullOrEmpty(file))
                    Play(file);
            }
            isMouseMove = false;
        }

        bool isMouseMove = false;
        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                isMouseMove = true;
                this.DragMove();
            }
        }

        private void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            body.Opacity = 1;
        }
    }
}
