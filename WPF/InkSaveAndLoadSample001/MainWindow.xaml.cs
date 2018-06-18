using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InkSaveAndLoadSample001
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            inkCanvas.UseCustomCursor = true;
            inkCanvas.Cursor = Cursors.Pen;
            SetDrawingAttributes();
        }

        private void SetDrawingAttributes()
        {
            inkCanvas.DefaultDrawingAttributes.Color = Colors.DarkBlue;
            inkCanvas.DefaultDrawingAttributes.FitToCurve = true;
            inkCanvas.DefaultDrawingAttributes.IgnorePressure = false;
            inkCanvas.DefaultDrawingAttributes.IsHighlighter = false;
            inkCanvas.DefaultDrawingAttributes.StylusTip = StylusTip.Ellipse;
            inkCanvas.DefaultDrawingAttributes.Width = 18;
            inkCanvas.DefaultDrawingAttributes.Height = 18;
        }

        private void SaveButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (inkCanvas.Strokes.Count <= 0) return;
            var dialog = new SaveFileDialog
            {
                Filter = "ISF files (*.isf)|*.isf",
            };

            if (dialog.ShowDialog() == true)
            {
                using (var stream = new FileStream(dialog.FileName, FileMode.Create))
                {
                    inkCanvas.Strokes.Save(stream);
                }
            }
        }

        private void LoadButton_Clicked(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "ISF files (*.isf)|*.isf",
            };

            if (dialog.ShowDialog() == true)
            {
                using (var stream = new FileStream(dialog.FileName, FileMode.Open))
                {
                    inkCanvas.Strokes = new StrokeCollection(stream);
                }
            }
        }

        private void ClearButton_Clicked(object sender, RoutedEventArgs e)
        {
            inkCanvas.Strokes.Clear();
        }
    }
}
