using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApplication1
{
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }
        int DCOUNT = 1;
        Button[,] btn;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mygrid.RowDefinitions.Clear();
            mygrid.ColumnDefinitions.Clear();
            DCOUNT = int.Parse(tbl.Text);
            btn = new Button[DCOUNT, DCOUNT];
            for (int i = 0; i < DCOUNT; i++)
            {
                RowDefinition row=new RowDefinition();
                ColumnDefinition col=new ColumnDefinition();
                mygrid.RowDefinitions.Add(row);
                mygrid.ColumnDefinitions.Add(col);
            }
            for (int i = 0; i < DCOUNT; i++)
            {
                for (int j = 0; j < DCOUNT; j++)
                {
                    btn[i, j]=new Button();
                    btn[i, j].Background = new ImageBrush(new BitmapImage(new Uri("Images1/field.jpg", UriKind.Relative)));
                    btn[i, j].Style = (Style)this.FindResource("MyButtonStyle");
                    btn[i, j].Tag="地洞";
                    btn[i, j].Click+=Window2_Click;
                    Grid.SetColumn(btn[i, j], j);
                    Grid.SetRow(btn[i, j], i);
                    mygrid.Children.Add(btn[i, j]);
                }
            }
            btnInit.IsEnabled = false;
            btnStart.IsEnabled = true;
        }

        void Window2_Click(object sender, RoutedEventArgs e)
        {
            Button bn = (Button)sender;
            if (bn.Tag.ToString() == "地鼠")
            {
                int x = int.Parse(la2.Content.ToString());
                x++;
                la2.Content = x + "";
                if (x < 5) { }
                else if (x > 5 && x < 10)
                    timer.Interval = TimeSpan.FromSeconds(0.9);
                else if(x>=10&&x<20)
                    timer.Interval = TimeSpan.FromSeconds(0.6);
                else
                    timer.Interval = TimeSpan.FromSeconds(0.4);
            }
            int y=int.Parse(la3.Content.ToString());
            y++;
            la3.Content = y + "";
            la1.Content = la2.Content;
        }

       

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            tbl.Focus();
        }

        private void mygrid_MouseEnter(object sender, MouseEventArgs e)
        {
            string path = System.IO.Path.GetFullPath("imgs/111.cur");
            this.Cursor = new Cursor(path);
        }
        Random rd = new Random();
        int row = 0, col = 0;//row,col代表地鼠出现的行列值

        //引入WPF中的计时器，计时器有三个知识点：（1）IsEnabled (2) Interval (3)Tick
        DispatcherTimer timer = new DispatcherTimer();

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            timer.IsEnabled = true;
            timer.Interval = TimeSpan.FromSeconds(1); //(2)TimeSpan.XXX()  (2)new TimeSpane(xxx)
            timer.Tick += timer_Tick;
            btnStop.IsEnabled = true;
            btnStart.IsEnabled = false;
        }

        void timer_Tick(object sender, EventArgs e)//按两次Tab自动生成
        {
            btn[row, col].Background = new ImageBrush(new BitmapImage(new Uri("Images1/field.jpg", UriKind.Relative)));
            btn[row, col].Tag = "地洞";
            row = rd.Next(DCOUNT);
            col = rd.Next(DCOUNT);
            
            btn[row, col].Background = new ImageBrush(new BitmapImage(new Uri("Images1/mouse.jpg", UriKind.Relative)));
            btn[row, col].Tag = "地鼠";
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            timer.IsEnabled = false;
            btn[row, col].Background = new ImageBrush(new BitmapImage(new Uri("Images1/field.jpg", UriKind.Relative)));
            btn[row, col].Tag = "";
            btnStart.IsEnabled = false;
            btnStop.IsEnabled = false;
            btnInit.IsEnabled = true;
            tbl.Text = "";
            tbl.Focus();
        }


    }
}
