using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;

namespace DoubleClickCell {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            new DevExpress.XtraGrid.Design.XViewsPrinting(gridControl1);
            radioGroup1_SelectedIndexChanged(radioGroup1, EventArgs.Empty);
        }

        private static void DoRowDoubleClick(GridView view, Point pt) {
            GridHitInfo info = view.CalcHitInfo(pt);
            if(info.InRow || info.InRowCell) {
                string colCaption = info.Column == null ? "N/A" : info.Column.GetCaption();
                MessageBox.Show(string.Format("DoubleClick on row: {0}, column: {1}.", info.RowHandle, colCaption));
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e) {
            gridView1.DoubleClick -= gridView1_DoubleClick;
            gridView1.ShownEditor -= gridView1_ShownEditor;
            gridView1.HiddenEditor -= gridView1_HiddenEditor;

            switch(radioGroup1.SelectedIndex) {
                case 0:
                    // Requires a single event handler. Works always.
                    gridView1.OptionsBehavior.Editable = false;
                    gridView1.DoubleClick += gridView1_DoubleClick;
                    break;

                case 1:
                    // Requires a single event handler. Doesn't work when double-clicking an open inplace editor.
                    gridView1.OptionsBehavior.Editable = true;
                    gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
                    gridView1.DoubleClick += gridView1_DoubleClick;
                    break;

                case 2:
                    // Requires 3 event handlers. Works always.
                    gridView1.OptionsBehavior.Editable = true;
                    gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Default;
                    gridView1.DoubleClick += gridView1_DoubleClick;
                    gridView1.ShownEditor += gridView1_ShownEditor;
                    gridView1.HiddenEditor += gridView1_HiddenEditor;
                    break;
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e) {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            DoRowDoubleClick(view, pt);
        }

        BaseEdit inplaceEditor;
        private void gridView1_ShownEditor(object sender, EventArgs e) {
            inplaceEditor = ((GridView)sender).ActiveEditor;
            inplaceEditor.DoubleClick += inplaceEditor_DoubleClick;
        }        

        private void gridView1_HiddenEditor(object sender, EventArgs e) {
            if(inplaceEditor != null) {
                inplaceEditor.DoubleClick -= inplaceEditor_DoubleClick;
                inplaceEditor = null;
            }
        }

        void inplaceEditor_DoubleClick(object sender, EventArgs e) {
            BaseEdit editor = (BaseEdit)sender;
            GridControl grid = (GridControl)editor.Parent;
            Point pt = grid.PointToClient(Control.MousePosition);
            GridView view = (GridView)grid.FocusedView;
            DoRowDoubleClick(view, pt);
        }
    }
}