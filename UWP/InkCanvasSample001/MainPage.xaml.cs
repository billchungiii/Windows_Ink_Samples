using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白頁項目範本已記錄在 https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x404

namespace InkCanvasSample001
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetInputDeviceTypes();
            SetDrawingAttributes();
        }

        /// <summary>
        /// 設定 InkCanvas 的輸入介面
        /// </summary>
        private void SetInputDeviceTypes()
        {
            inkCanvas.InkPresenter.InputDeviceTypes =
                CoreInputDeviceTypes.Mouse | CoreInputDeviceTypes.Pen | CoreInputDeviceTypes.Touch;
        }

        /// <summary>
        /// 設定其他的屬性
        /// </summary>
        private void SetDrawingAttributes()
        {
            var attributes = new InkDrawingAttributes
            {
                Color = Colors.GreenYellow,
                Size = new Size(18, 18),
                DrawAsHighlighter = true,
                FitToCurve = true,
                IgnorePressure = false,
                IgnoreTilt = false,
                PenTip = PenTipShape.Rectangle,
            };
            inkCanvas.InkPresenter.UpdateDefaultDrawingAttributes(attributes);
            
        }
    }
}
