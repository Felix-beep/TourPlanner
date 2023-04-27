using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Core;

namespace TourPlanner.MVVM.ViewModel
{
    public class ImportToursViewModel : ObservableObject
    {
        private String _filename = "Your file name";

        public String FileName
        {
            get { return _filename; }
            set { 
                _filename = value;
                OnPropertyChanged();
            }
        }

        private string _path;

        public ImportToursViewModel()
        {
            ExecuteCommandOpenFileDialog = new RelayCommand(param => OpenFileDialogClicked?.Invoke(this, _filename));
            ExecuteCommandSubmitImport = new RelayCommand(param => SubmitClicked?.Invoke(this, _filename));

            OpenFileDialogClicked += (_, _filename) => btnOpenFile_Click();
        }

        public ICommand ExecuteCommandOpenFileDialog { get; }

        public event EventHandler<string> OpenFileDialogClicked;

        public ICommand ExecuteCommandSubmitImport { get; }

        public event EventHandler<string> SubmitClicked;

        private void btnOpenFile_Click()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true) { 
                _path = openFileDialog.FileName;
                FileName = Path.GetFileName(openFileDialog.FileName);
            }
        }

    }
}
