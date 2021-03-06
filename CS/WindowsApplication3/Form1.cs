using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;


namespace DXSample {
    public partial class Form1: XtraForm {
        public Form1() {
            InitializeComponent();
        }
        public void InitData() {
            for(int i = 0;i < 3000;i++) {
                dataSet11.Tables[0].Rows.Add(new object[] { i, string.Format("FirstName {0}", i), string.Format("LastName {0}", i), DateTime.Today.AddDays(i), true });
            }
        }

        private void OnFormLoad(object sender, EventArgs e) {
            InitData();
            exportGridControl.ForceInitialize();
            exportProgressBar.Properties.ShowTitle = true;
            exportProgressBar.Properties.PercentView = true;
            exportProgressBar.Properties.Maximum = exportGridView.RowCount * exportProgressBar.Properties.Step;
        }

        string filePath = @"..\\..Excel.xlsx";
        private void OnExportGrid(object sender, EventArgs e) {
            exportGridView.ExportToXlsx(filePath);
            using(Process pr = new Process()) {
                pr.StartInfo = new ProcessStartInfo(filePath);
                pr.Start();
            }
            exportProgressBar.Position = 0;
        }
        
        private void OnAfterPrintRow(object sender, DevExpress.XtraGrid.Views.Printing.PrintRowEventArgs e) {
            Application.DoEvents();
            exportProgressBar.PerformStep();
            exportProgressBar.Update();
        }
    }
}
