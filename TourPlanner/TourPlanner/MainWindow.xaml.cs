using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Windows;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainWindow));

        public MainWindow()
        {
            InitializeComponent();

            XmlConfigurator.Configure(new FileInfo("log4net.cfg"));

            log.Info("Logging configured.");
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Clicked MenuItem File.New!");
        }
    }
}
