Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid

Namespace DoubleClickCell
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			Dim TempXViewsPrinting As DevExpress.XtraGrid.Design.XViewsPrinting = New DevExpress.XtraGrid.Design.XViewsPrinting(gridControl1)
			radioGroup1_SelectedIndexChanged(radioGroup1, EventArgs.Empty)
		End Sub

		Private Shared Sub DoRowDoubleClick(ByVal view As GridView, ByVal pt As Point)
			Dim info As GridHitInfo = view.CalcHitInfo(pt)
			If info.InRow OrElse info.InRowCell Then
				Dim colCaption As String
				If info.Column Is Nothing Then
					colCaption = "N/A"
				Else
					colCaption = info.Column.GetCaption()
				End If
				MessageBox.Show(String.Format("DoubleClick on row: {0}, column: {1}.", info.RowHandle, colCaption))
			End If
		End Sub

		Private Sub radioGroup1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radioGroup1.SelectedIndexChanged
			RemoveHandler gridView1.DoubleClick, AddressOf gridView1_DoubleClick
			RemoveHandler gridView1.ShownEditor, AddressOf gridView1_ShownEditor
			RemoveHandler gridView1.HiddenEditor, AddressOf gridView1_HiddenEditor

			Select Case radioGroup1.SelectedIndex
				Case 0
					' Requires a single event handler. Works always.
					gridView1.OptionsBehavior.Editable = False
					AddHandler gridView1.DoubleClick, AddressOf gridView1_DoubleClick

				Case 1
					' Requires a single event handler. Doesn't work when double-clicking an open inplace editor.
					gridView1.OptionsBehavior.Editable = True
					gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click
					AddHandler gridView1.DoubleClick, AddressOf gridView1_DoubleClick

				Case 2
					' Requires 3 event handlers. Works always.
					gridView1.OptionsBehavior.Editable = True
					gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Default
					AddHandler gridView1.DoubleClick, AddressOf gridView1_DoubleClick
					AddHandler gridView1.ShownEditor, AddressOf gridView1_ShownEditor
					AddHandler gridView1.HiddenEditor, AddressOf gridView1_HiddenEditor
			End Select
		End Sub

		Private Sub gridView1_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles gridView1.DoubleClick
			Dim view As GridView = CType(sender, GridView)
			Dim pt As Point = view.GridControl.PointToClient(Control.MousePosition)
			DoRowDoubleClick(view, pt)
		End Sub

		Private inplaceEditor As BaseEdit
		Private Sub gridView1_ShownEditor(ByVal sender As Object, ByVal e As EventArgs) Handles gridView1.ShownEditor
			inplaceEditor = (CType(sender, GridView)).ActiveEditor
			AddHandler inplaceEditor.DoubleClick, AddressOf inplaceEditor_DoubleClick
		End Sub

		Private Sub gridView1_HiddenEditor(ByVal sender As Object, ByVal e As EventArgs) Handles gridView1.HiddenEditor
			If inplaceEditor IsNot Nothing Then
				RemoveHandler inplaceEditor.DoubleClick, AddressOf inplaceEditor_DoubleClick
				inplaceEditor = Nothing
			End If
		End Sub

		Private Sub inplaceEditor_DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
			Dim editor As BaseEdit = CType(sender, BaseEdit)
			Dim grid As GridControl = CType(editor.Parent, GridControl)
			Dim pt As Point = grid.PointToClient(Control.MousePosition)
			Dim view As GridView = CType(grid.FocusedView, GridView)
			DoRowDoubleClick(view, pt)
		End Sub
	End Class
End Namespace