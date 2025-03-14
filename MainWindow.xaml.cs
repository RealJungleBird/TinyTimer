using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Forms;

namespace Stopwatch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   
        DispatcherTimer timer = new DispatcherTimer();
        string currentTime = string.Empty;

        private bool isOptionsOpened = true;
        private byte bgOpacity;

        private System.Windows.Media.Color bgColor = Color.FromRgb(255, 255, 255);
        private System.Windows.Media.Color textColor = Color.FromRgb(255, 255, 255);
        private System.Windows.Media.Color glowColor = Color.FromRgb(255, 255, 255);

        private ColorDialog bgColorDialog = new System.Windows.Forms.ColorDialog();
        private ColorDialog textColorDialog = new System.Windows.Forms.ColorDialog();
        private ColorDialog glowColorDialog = new System.Windows.Forms.ColorDialog();

        private FontDialog textFontDialog = new FontDialog();

        private int seconds = 0;
        private int minutes = 0;
        private int hours = 0;

        public MainWindow()
        {
            InitializeComponent();
            FileSystem.ReadJSON();

            seconds = FileSystem.styledata.currentSec;
            minutes = FileSystem.styledata.currentMin;
            hours = FileSystem.styledata.currentHour;
            TimeText.Text = string.Format("{0}:{1}:{2}", hours.ToString("D2"), minutes.ToString("D2"), seconds.ToString("D2"));

            // Get BG color UserData
            bgColor = FileSystem.styledata.bgColor;
            bgColorDialog.Color = System.Drawing.Color.FromArgb(bgColor.A, bgColor.R, bgColor.G, bgColor.B);
            colorPickBackground.Background = new SolidColorBrush(Color.FromArgb(255, bgColor.R, bgColor.G, bgColor.B));
            // Get text color UserData
            textColor = FileSystem.styledata.textColor;
            textColorDialog.Color = System.Drawing.Color.FromArgb(textColor.A, textColor.R, textColor.G, textColor.B);
            colorPickText.Background = new SolidColorBrush(textColor);
            // Get glow color UserData
            glowColor = FileSystem.styledata.glowColor;
            glowColorDialog.Color = System.Drawing.Color.FromArgb(glowColor.A, glowColor.R, glowColor.G, glowColor.B);
            colorPickGlow.Background = new SolidColorBrush(glowColor);

            // Get + set slider parameters
            OpacitySlider.Value = FileSystem.styledata.opacity;
            textSizeSlider.Value = FileSystem.styledata.textSize;
            sliderGlowOpacity.Value = FileSystem.styledata.glowOpacity;
            TimeText.FontSize = textSizeSlider.Value;

            // Set color parameters
            TimeText.Foreground = new SolidColorBrush(textColor);
            //this.Background = new SolidColorBrush(bgColor);
            this.Background = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0));
            TimerBackground.Background = new SolidColorBrush(bgColor);
            TextGlow.Opacity = FileSystem.styledata.glowOpacity;
            TextGlow.Color = glowColor;

            fontDialog.FontFamily = new FontFamily(FileSystem.styledata.textFamily);
            TimeText.FontFamily = new FontFamily(FileSystem.styledata.textFamily);

            // SETTING UP DIALOGS
            bgColorDialog.FullOpen = true;
            textColorDialog.FullOpen = true;
            glowColorDialog.FullOpen = true;

            textFontDialog.ScriptsOnly = true;
            textFontDialog.ShowEffects = false;
            textFontDialog.ShowColor = false;

            // Setting up timer
            timer.Tick += new EventHandler(dt_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            seconds++;
            if (seconds == 60)
            {
                seconds = 0;
                minutes++;
            }
            if (minutes == 60)
            {
                minutes = 0;
                hours++;
            }

            TimeText.Text = string.Format("{0}:{1}:{2}", hours.ToString("D2"), minutes.ToString("D2"), seconds.ToString("D2"));
        }

        #region BUTTONS

        // Start/stop
        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            if(timer.IsEnabled)
            {
                timer.Stop();
                buttonStart.Content = "Start";
                return;
            }
            timer.Start();
            buttonStart.Content = "Stop";
        }
        // Reset
        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            seconds = 0;
            minutes = 0;
            hours = 0;
            TimeText.Text = string.Format("{0}:{1}:{2}", hours.ToString("D2"), minutes.ToString("D2"), seconds.ToString("D2"));
        }
        // About
        private void buttonAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow window = new AboutWindow();
            window.Owner = this;
            window.ShowDialog();
            //System.Windows.MessageBox.Show("meow", "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        // Close
        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        #region STYLE_SETTINGS

        // Pick text color
        private void colorPickText_Click(object sender, RoutedEventArgs e)
        {
            var dialog = textColorDialog;
            if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                colorPickText.Background = new SolidColorBrush(Color.FromArgb(dialog.Color.A, dialog.Color.R, dialog.Color.G, dialog.Color.B));
                textColor = Color.FromArgb(dialog.Color.A, dialog.Color.R, dialog.Color.G, dialog.Color.B);
                TimeText.Foreground = new SolidColorBrush(textColor);
                FileSystem.styledata.textColor = textColor;
            }
        }
        // Pick bg color
        private void colorPickBackground_Click(object sender, RoutedEventArgs e)
        {
            var dialog = bgColorDialog;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bgColor = Color.FromArgb(bgOpacity, dialog.Color.R, dialog.Color.G, dialog.Color.B);
                colorPickBackground.Background = new SolidColorBrush(Color.FromArgb(dialog.Color.A, dialog.Color.R, dialog.Color.G, dialog.Color.B));
                //this.Background = new SolidColorBrush(bgColor);
                TimerBackground.Background = new SolidColorBrush(bgColor);
                FileSystem.styledata.bgColor = bgColor;
            }
        }
        // Pick glow color
        private void colorPickGlow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = glowColorDialog;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                glowColor = Color.FromArgb(dialog.Color.A, dialog.Color.R, dialog.Color.G, dialog.Color.B);
                colorPickGlow.Background = new SolidColorBrush(glowColor);
                TextGlow.Color = glowColor;
                FileSystem.styledata.glowColor = glowColor;
            }
        }
        // Changing bg opacity
        private void OpacitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            bgOpacity = (byte)OpacitySlider.Value;
            bgColor.A = bgOpacity;
            //this.Background = new SolidColorBrush(bgColor);
            TimerBackground.Background = new SolidColorBrush(bgColor);
            FileSystem.styledata.opacity = bgOpacity;
        }

        // Changing text size
        private void textSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeText.FontSize = (int)textSizeSlider.Value;
            FileSystem.styledata.textSize = (int)textSizeSlider.Value;
        }

        // Changing glow opacity
        private void GlowOpacitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            float glowOpacity = (float)sliderGlowOpacity.Value;
            TextGlow.Opacity = glowOpacity;
            FileSystem.styledata.glowOpacity = glowOpacity;
        }

        // Changing font
        private void fontDialog_Click(object sender, RoutedEventArgs e)
        {
            var dialog = textFontDialog;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TimeText.FontFamily = new FontFamily(dialog.Font.FontFamily.Name);
                FileSystem.styledata.textFamily = dialog.Font.FontFamily.Name;
            }
        }

        // Changing corner radius
        private void CornerRadiusSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int cornerRadius = (int)CornerRadiusSlider.Value;
            TimerBackground.CornerRadius = new CornerRadius(cornerRadius);
            FileSystem.styledata.borderRadius = cornerRadius;
        }

        #endregion

        #region COMMON_EVENTS

        // Drag&move the window
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        // EXPAND & MAXIMIZE
        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isOptionsOpened)
            {
                FileSystem.WriteJSON();
                this.Width = TimeText.FontSize * 5.6;
                this.Height = this.Width / 4;
                TimerGrid.Width = this.Width;
                TimerGrid.Height = this.Height;
            }
            else
            {
                this.Width = 400;
                this.Height = 400;
                TimerGrid.Width = this.Width;
                TimerGrid.Height = 100;
            }
            isOptionsOpened = !isOptionsOpened;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FileSystem.styledata.currentSec = seconds;
            FileSystem.styledata.currentMin = minutes;
            FileSystem.styledata.currentHour = hours;
            FileSystem.WriteJSON();
        }

        #endregion

    }
}
