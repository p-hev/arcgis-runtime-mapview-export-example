using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.UI.Controls;

namespace HighResExport.Extensions
{
    internal static class MapExtensions
    {
        /// <summary>
        ///  Extension method to generate a larger image for printing.
        /// </summary>
        /// <param name="mapView"></param>
        /// <param name="outputDpi"></param>
        /// <param name="renderOverlaySeparate">This is for map notes so their location is accurate, though at screen resolution</param>
        /// <param name="updateProgress"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<RuntimeImage> ExportHighResolutionImageAsync(this MapView mapView,
            int outputDpi, CancellationToken cancellationToken)
        {
            var source = PresentationSource.FromVisual(mapView);

            var originalScale = mapView.MapScale;
            var originalCenter = mapView.VisibleArea.Extent.GetCenter();

            var xDeviceTransform = source.CompositionTarget.TransformToDevice.M11;

            var printToScreenRatio = outputDpi / 96.0d;

            var imagePixelHeight = (int)Math.Round(mapView.ActualHeight * printToScreenRatio);
            var imagePixelWidth = (int)Math.Round(mapView.ActualWidth * printToScreenRatio);
            var calculatedScale = mapView.MapScale / printToScreenRatio;


            mapView.Height = imagePixelHeight;
            mapView.Width = imagePixelWidth;

            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            var viewpoint = new Viewpoint(originalCenter, calculatedScale);

            await mapView.SetViewpointAsync(viewpoint, TimeSpan.FromMilliseconds(10));

            await mapView.WaitForDrawingToComplete(cancellationToken);
            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);

            var result = await mapView.ExportImageAsync();


            var originalViewpoint = new Viewpoint(originalCenter, originalScale);
            await mapView.SetViewpointAsync(originalViewpoint, TimeSpan.FromMilliseconds(10));
            mapView.Height = Double.NaN;
            mapView.Width = Double.NaN;

            return result;
        }

        public static async Task WaitForDrawingToComplete(this MapView mapView, CancellationToken cancellationToken)
        {

            // Exit early if were not actually drawing anything right now.
            if (mapView.DrawStatus != DrawStatus.InProgress)
            {
                return;
            }

            if (mapView.Map?.Basemap == null || mapView.Map?.Basemap.BaseLayers.Any() == false)
            {
                return;
            }

            var tcs = new TaskCompletionSource<bool>();

            using var registration = cancellationToken.Register(() => { tcs.TrySetCanceled(); });

            void DrawStatusChanged(object sender, DrawStatusChangedEventArgs args)
            {
                if (args.Status == DrawStatus.Completed)
                {
                    tcs.TrySetResult(true);
                }
            }

            try
            {
                mapView.DrawStatusChanged += DrawStatusChanged;

                // Check the drawing status again in case it managed to change 
                // before the event handler was set up.
                if (mapView.DrawStatus != DrawStatus.InProgress)
                {
                    return;
                }

                await tcs.Task;
            }
            finally
            {
                mapView.DrawStatusChanged -= DrawStatusChanged;
            }
        }
    }
}