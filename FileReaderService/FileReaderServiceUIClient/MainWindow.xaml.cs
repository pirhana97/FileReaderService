
using FileReaderService;
using FileReaderServiceProxy;
using FileReaderServiceUIClient;
using System;
using System.IO;
using System.ServiceModel;
using System.Windows;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace FileReaderWCFServiceUIClient
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

        private void OnBrowseClick(object sender, EventArgs e)
        {
            WriteFilePath filePath = new WriteFilePath();
            string filename = filePath.writeFilePath();
            FilePath.Text = filename;

        }

        private void OnGetContentsClick(object sender, EventArgs e)
        {

            IFileReaderServiceCallback callback = new FileReaderServiceCallback();
            InstanceContext context = new InstanceContext(callback);

            FileReaderServiceProxy.FileReaderServiceProxy proxy = new FileReaderServiceProxy.FileReaderServiceProxy(context);
            string filePath = FilePath.Text;

            string message = proxy.Echo(filePath);
            FileAttributes.Text = proxy.GetFileAttributes(filePath);


            MessageBoxResult result = MessageBox.Show(proxy.PerCall_FileReader()+ "\nSession ID :"+proxy.InnerChannel.SessionId);
            

        }
    }
}







// ChannelFactory<IFileReaderService> factory = new ChannelFactory<IFileReaderService>("");
// IFileReaderService proxy = factory.CreateChannel();

// FileAttributes.Text=proxy.GetFileAttributes(FilePath.Text);
// IFileReaderServiceCallback callback = new FileReaderServiceCallback();
// InstanceContext context = new InstanceContext(callback);
// FileReaderServiceProxy.FileReaderServiceProxy proxy1= new FileReaderServiceProxy.FileReaderServiceProxy(context);

////// FileReaderUIServiceProxy.FileReaderServiceProxy proxy = new FileReaderUIServiceProxy.FileReaderServiceProxy();
//string filePath = FilePath.Text;

//FileAttributes.Text = proxy1.GetFileAttributes(filePath);
//string message = proxy.Echo(filePath);
//MessageBox.Show(message);

//string fileAttributes = proxy.GetFileAttributes(filePath);
//MessageBox.Show(fileAttributes);