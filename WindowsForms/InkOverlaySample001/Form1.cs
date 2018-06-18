using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Ink;

namespace InkOverlaySample001
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
            //inkOverlay.Ink.Save(PersistenceFormat.Gif);
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


    }
}
