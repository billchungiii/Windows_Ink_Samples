using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Input.Inking;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白頁項目範本已記錄在 https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x404

namespace HandWritingSample001
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private ObservableCollection<string> Candidates { get; set; }
        private InkRecognizerContainer Container { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            Candidates = new ObservableCollection<string>();
            DataContext = Candidates;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Container = new InkRecognizerContainer();
            var recognizers = Container.GetRecognizers();
            if (recognizers != null && recognizers.Count > 0)
            {
                // 以下為取得目前的語系, 並設定使用目前語系的辨識引擎
                string recognizerName = InkRecognizerHelper.LanguageTagToRecognizerName(CultureInfo.CurrentCulture.Name);
                var recognizer = recognizers.FirstOrDefault((x) => x.Name == recognizerName);
                if (recognizer != null)
                {
                    Container.SetDefaultRecognizer(recognizer);
                }
            }
        }

        async private void RecognizeButton_Click(object sender, RoutedEventArgs e)
        {
            var strokes = inkCanvas.InkPresenter.StrokeContainer.GetStrokes();
            if (strokes.Count <= 0) return;
            var result =
                await Container.RecognizeAsync(inkCanvas.InkPresenter.StrokeContainer, InkRecognitionTarget.All);
            Candidates.Clear();
            foreach (var item in result)
            {
                foreach (var candidate in item.GetTextCandidates())
                {
                    Candidates.Add(candidate);
                }
            }
        }

        async private void gridview_ItemClick(object sender, ItemClickEventArgs e)
        {
            MessageDialog dialog = new MessageDialog((string)e.ClickedItem, "你所選的字：");
            await dialog.ShowAsync();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.InkPresenter.StrokeContainer.Clear();
            Candidates.Clear();
        }
    }
}
