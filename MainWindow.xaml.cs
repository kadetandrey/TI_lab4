using System.Windows;
using Microsoft.Win32;

namespace TI_Lab_4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            bEncrypt.IsEnabled = false;
            bDecrypt.IsEnabled = false;
            bFile.IsEnabled = false;
        }

        private void bKey_Click(object sender, RoutedEventArgs e)
        {
            if (tbKey.Text != "")
            {
                LFSR.SetKey(ulong.Parse(tbKey.Text));
                bFile.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Enter your key", "Info", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }

        private void bFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                LFSR.SetFilePath(openFileDialog.FileName);
                lFile.Content = openFileDialog.FileName;
            }
            bEncrypt.IsEnabled = true;
            bDecrypt.IsEnabled = true;
        }

        private void bEncrypt_Click(object sender, RoutedEventArgs e)
        {
            LFSR.Encrypt();
        }

        private void bDecrypt_Click(object sender, RoutedEventArgs e)
        {
            LFSR.Decrypt();
        }
    }
}
