using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.UI.Controls;
using HighResExport.Extensions;
using HighResExport.Views;

namespace HighResExport.ViewModels
{
    public class ShellViewModel : ViewAware
    {
        private MapView _mapView;
        private int _outputDpi = 300;
        private string _outputFile;
        private int _deviceResolution = 96;
        private Visibility _progressBarVisibility = Visibility.Hidden;



        public ShellViewModel()
        {
            UpdateOutputFilePath();
        }

        private void UpdateOutputFilePath()
        {
            OutputFile = System.IO.Path.Combine(GetTempPath(), GetTempFileName());
        }

        private static string GetTempPath() => System.IO.Path.GetTempPath();

        private string GetTempFileName() => $"MapView{_outputDpi}.png";


        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            if (view is ShellView shellView)
            {
                _mapView = shellView.MapView;
                _mapView.SetViewpointCenterAsync(new MapPoint(-105.0275148018931, 40.56930328177301, SpatialReferences.Wgs84), 6000);
            }
        }

        public void ExportMap()
        {
            ExportAndWriteToDisk().ContinueWith((t) =>
            {
                if (t.IsFaulted)
                {
                    MessageBox.Show($"Error exporting mapview to image");
                }
            });
        }

        private async Task ExportAndWriteToDisk()
        {
            ProgressBarVisibility = Visibility.Visible;
            var runtimeImage = await _mapView.ExportHighResolutionImageAsync(_outputDpi, CancellationToken.None);
            var stream = await runtimeImage.GetEncodedBufferAsync();
            var image = Image.FromStream(stream);
            image.Save(_outputFile);

            var openFileResult = MessageBox.Show("Export Complete, open image?", "Open Image", MessageBoxButton.YesNo);
            if (openFileResult == MessageBoxResult.Yes)
            {
                Process.Start(_outputFile);
            }

            ProgressBarVisibility = Visibility.Hidden;
            OutputDpi += 300;
        }

        public int OutputDpi
        {
            get => _outputDpi;
            set
            {
                if (Set(ref _outputDpi, value))
                {
                    NotifyOfPropertyChange(nameof(OutputWidth));
                    NotifyOfPropertyChange(nameof(OutputHeight));
                    UpdateOutputFilePath();
                }
            }
        }

        public string OutputFile
        {
            get => _outputFile;
            set => Set(ref _outputFile, value);
        }

        public Visibility ProgressBarVisibility
        {
            get => _progressBarVisibility;
            set => Set(ref _progressBarVisibility, value);
        }

        public string OutputHeight => $"H {_mapView.ActualHeight * (_outputDpi / 96.0d):N} ";
        public string OutputWidth => $"W {_mapView.ActualWidth * (_outputDpi / 96.0d):N}";

    }
}