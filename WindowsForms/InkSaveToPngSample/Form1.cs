using Microsoft.Ink;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InkSaveToPngSample
{
    public partial class Form1 : Form
    {
        private InkOverlay InkOverlay { get; set; }
        public Form1()
        {
            InitializeComponent();
            InkOverlay = new InkOverlay
            {
                Handle = panel2.Handle,
                Enabled = true
            };

            SetDrawingAttributes();
            ToEditMode();
        }

        private void SetDrawingAttributes()
        {
            var attriburtes = new DrawingAttributes()
            {
                Color = Color.DarkBlue,
                FitToCurve = true,
                Height = 128,
                Width = 128,
                IgnorePressure = false,
                PenTip = PenTip.Ball,
            };
            InkOverlay.DefaultDrawingAttributes = attriburtes;
            InkOverlay.EraserMode = InkOverlayEraserMode.StrokeErase;
        }

        private void ToEditMode()
        {
            InkOverlay.EditingMode = InkOverlayEditingMode.Ink;
            InkOverlay.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        private void ToEraseMode()
        {
            InkOverlay.EditingMode = InkOverlayEditingMode.Delete;
            InkOverlay.Cursor = System.Windows.Forms.Cursors.Cross;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ToEditMode();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ToEraseMode();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "PNG files (*.png)|*.png",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures),
            };
            
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrEmpty(dialog.FileName))
            {
                var bitmap = new Bitmap(panel2.Width, panel2.Height);
                InkOverlay.Renderer.Draw(bitmap, InkOverlay.Ink.Strokes);
                bitmap.Save(dialog.FileName, ImageFormat.Png);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            InkOverlay.Ink.DeleteStrokes(InkOverlay.Ink.Strokes);
            panel2.Refresh();
        }


    }
}
