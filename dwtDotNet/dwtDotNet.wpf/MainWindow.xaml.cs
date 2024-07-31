using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WIA;

namespace dwtDotNet.wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Bitmap scannedBitmap;  // Declare the variable at the class level
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            var deviceManager = new DeviceManager();
            Device scanner = null;
            foreach (DeviceInfo info in deviceManager.DeviceInfos)
            {
                if (info.Type == WiaDeviceType.ScannerDeviceType)
                {
                    scanner = info.Connect();
                    break;
                }
            }

            if (scanner != null)
            {
                var item = scanner.Items[1];
                var imgFile = (ImageFile)item.Transfer("{B96B3CAF-0728-11D3-9D7B-0000F81EF32E}");               

                var imageBytes = (byte[])imgFile.FileData.get_BinaryData();
                using (var ms = new MemoryStream(imageBytes))
                {
                   scannedBitmap = new Bitmap(ms);
                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = ms;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                    ScannedImage.Source = bitmapImage;
                }
            }
        }

        private async void UploadButton_Click(object sender, RoutedEventArgs e)
        {
       
            if ( scannedBitmap != null)
            {
                var title = TitleTextBox.Text;

                using (var ms = new MemoryStream())
                {
                    scannedBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    var imageBytes = ms.ToArray();
                    var base64String = Convert.ToBase64String(imageBytes);

                    await UploadImage(title, base64String);
                }
            }
        }

        private async Task UploadImage(string title, string base64Image)
        {
            using (var client = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(title), "title");
                content.Add(new StringContent(base64Image), "imageData");

                var response = await client.PostAsync("https://localhost:7084/api/images", content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Image uploaded successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to upload image.");
                }
            }
        }
    }
}