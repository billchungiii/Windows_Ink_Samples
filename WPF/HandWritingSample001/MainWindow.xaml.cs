using Microsoft.Ink;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HandWritingSample001
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> Candidates { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Candidates = new ObservableCollection<string>();
            DataContext = Candidates;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            inkCanvas.UseCustomCursor = true;
            inkCanvas.Cursor = System.Windows.Input.Cursors.Pen;
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

        private void ClearButton_Clicked(object sender, RoutedEventArgs e)
        {
            inkCanvas.Strokes.Clear();
            Candidates.Clear();
        }

        private void RecognizeButton_Clicked(object sender, RoutedEventArgs e)
        {
            Candidates.Clear();
            if (inkCanvas.Strokes.Count <= 0) return;
            using (var stream = new MemoryStream())
            {
                inkCanvas.Strokes.Save(stream);
                var ink = new Ink();
                ink.Load(stream.ToArray());
                using (var context = new RecognizerContext())
                {
                    context.Strokes = ink.Strokes;
                    var result = context.Recognize(out RecognitionStatus status);
                    if (status == RecognitionStatus.NoError)
                    {
                        var candidates = result.GetAlternatesFromSelection();
                        foreach (var candidate in candidates)
                        {
                            Candidates.Add(candidate.ToString());
                        }
                    }
                }
            }
        }

        private void Item_Clicked(object sender, RoutedEventArgs e)
        {
            var context = ((FrameworkElement)sender).DataContext;
            MessageBox.Show(context.ToString(), "你所選的字是：");
        }
    }
}
