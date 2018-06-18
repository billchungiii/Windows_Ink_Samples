using System;
using System.Collections.Generic;
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

namespace InkCanvasSample001
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

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (inkCanvas == null) return;
            inkCanvas.EditingMode = InkCanvasEditingMode.Select;
            ((CheckBox)sender).Content = "Select Mode";
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (inkCanvas == null) return;
            inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            ((CheckBox) sender).Content = "Ink Mode";
        }
    }
}
