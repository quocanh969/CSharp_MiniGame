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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _1612018_CS_MiniGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void clickStart(object sender, RoutedEventArgs e)
        {
            // Chuyển sang màn hình chơi game sau khi click nút Start
            var screen = new GameWindow();
            this.Close();
            screen.Show();
        }

        private void clickExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
