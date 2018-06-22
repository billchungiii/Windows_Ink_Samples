using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白頁項目範本已記錄在 https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x404

namespace InkSaveAndLoadSample001
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

        async private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
            };
            picker.FileTypeFilter.Add(".gif");
            var file = await picker.PickSingleFileAsync();
            if (file == null) return;
            using (var stream = await file.OpenSequentialReadAsync())
            {
                await inkCanvas.InkPresenter.StrokeContainer.LoadAsync(stream);
            }
        }

        async private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var strokes = inkCanvas.InkPresenter.StrokeContainer.GetStrokes();
            if (strokes.Count <= 0) return;
            var picker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                DefaultFileExtension = ".gif",
                SuggestedFileName = "Ink",
            };
          
            picker.FileTypeChoices.Add("GIF with ISF", new List<string> { ".gif" });
            var file = await picker.PickSaveFileAsync();
            if (file == null) return;
            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                await inkCanvas.InkPresenter.StrokeContainer.SaveAsync(stream);
               // await inkCanvas.InkPresenter.StrokeContainer.SaveAsync(stream, InkPersistenceFormat.GifWithEmbeddedIsf);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.InkPresenter.StrokeContainer.Clear();
        }
    }
}
