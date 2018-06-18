using Microsoft.Ink;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HandWritingSample001
{
    public partial class Form1 : Form
    {
        private ObservableCollection<string> Candidates { get; set; }
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
            Candidates = new ObservableCollection<string>();
            comboBox1.DataSource = Candidates;
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
            Candidates.Clear();
            if (InkOverlay.Ink.Strokes.Count <= 0) return;
            using (var context = new RecognizerContext())
            {
                context.Strokes = InkOverlay.Ink.Strokes;
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
            comboBox1.DataSource = null;
            comboBox1.DataSource = Candidates;
            comboBox1.SelectedIndex = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InkOverlay.Ink.DeleteStrokes(InkOverlay.Ink.Strokes);
            panel2.Refresh();
            comboBox1.DataSource = null;
        }
    }
}
