using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
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

namespace GenerateMACKey
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            txtValidationKey.Text = getRandomKey(64);
            txtDecryptionKey.Text = getRandomKey(32);
            CanEnableWebConfigButton();
        }

        private void CanEnableWebConfigButton()
        {
            if (txtValidationKey.Text.Length > 0 && txtDecryptionKey.Text.Length > 0)
            {
                btnCreateWebConfig.IsEnabled = true;
            }
        }

        public string getRandomKey(int bytelength)
        {
            byte[] buff = new byte[bytelength];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buff);
            StringBuilder sb = new StringBuilder(bytelength * 2);
            for (int i = 0; i < buff.Length; i++)
                sb.Append(string.Format("{0:X2}", buff[i]));
            return sb.ToString();
        }

        private void btnCreateWebConfig_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder aspnet20machinekey = new StringBuilder();
            string key64byte = getRandomKey(64);
            string key32byte = getRandomKey(32);
            aspnet20machinekey.Append("<machineKey \n");
            aspnet20machinekey.Append("validationKey=\"" + txtValidationKey.Text + "\"\n");
            aspnet20machinekey.Append("decryptionKey=\"" + txtDecryptionKey.Text + "\"\n");
            aspnet20machinekey.Append("validation=\"SHA1\" decryption=\"AES\" />\n");
            txtWebConfig.Text = aspnet20machinekey.ToString();
        }
    }
}
