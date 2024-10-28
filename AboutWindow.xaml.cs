using System.Windows;

namespace Stopwatch
{
    /// <summary>
    /// Логика взаимодействия для AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void buttonDonate_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.donationalerts.com/r/junglebird");
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.junglebird.su/");
        }
    }
}
