using log4net;
using log4net.Config;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using TourPlanner.DAL;

namespace TourPlanner.MVVM.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainWindow));

        ITourRepository repo;

        public MainWindow()
        {
            InitializeComponent();

            XmlConfigurator.Configure(new FileInfo("log4net.cfg"));

            log.Info("Logging configured.");

            var api = new APITourRepository();
            api.Connect(new Uri("http://localhost:5023/"));
            repo = api;
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            /*var sb = new StringBuilder();

            foreach (var t in repo.GetTours())
            {
                sb.AppendLine(t.ToString());
            }

            MessageBox.Show(sb.ToString());*/
        }
    }
}
