using Microsoft.Ink;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InkSaveAndLoadSample001
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
                Filter = "GIF files (*.gif)|*.gif",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures),
            };

            
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrEmpty(dialog.FileName))
            {
                using (var stream = new FileStream(dialog.FileName, FileMode.Create))
                {
                    var bytes = InkOverlay.Ink.Save(PersistenceFormat.Gif);
                    stream.Write(bytes, 0, bytes.Length);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "GIF files (*.gif)|*.gif",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures),
            };
        
            var result = dialog.ShowDialog();
            InkOverlay.Enabled = false;
            InkOverlay.Ink = new Ink();
            InkOverlay.Enabled = true;
            if (result == DialogResult.OK)
            {
                using (var stream = new FileStream(dialog.FileName, FileMode.Open))
                {
                    var bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    InkOverlay.Ink.Load(bytes);
                }
            }
            panel2.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InkOverlay.Ink.DeleteStrokes(InkOverlay.Ink.Strokes);
            panel2.Refresh();
        }
    }
}
