﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPieceTimePersonnel
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請不要使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Label1 = New System.Windows.Forms.Label
        Me.Grid = New DevExpress.XtraGrid.GridControl
        Me.MenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmdAdd = New System.Windows.Forms.ToolStripMenuItem
        Me.cmdEdit = New System.Windows.Forms.ToolStripMenuItem
        Me.cmdDel = New System.Windows.Forms.ToolStripMenuItem
        Me.cmdView = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdQuery = New System.Windows.Forms.ToolStripMenuItem
        Me.cmdRef = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip.SuspendLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("標楷體", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(6, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 24)
        Me.Label1.TabIndex = 46
        Me.Label1.Text = "個人計時"
        '
        'Grid
        '
        Me.Grid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grid.ContextMenuStrip = Me.MenuStrip
        Me.Grid.EmbeddedNavigator.Name = ""
        Me.Grid.Location = New System.Drawing.Point(1, 36)
        Me.Grid.MainView = Me.GridView1
        Me.Grid.Name = "Grid"
        Me.Grid.Size = New System.Drawing.Size(658, 493)
        Me.Grid.TabIndex = 47
        Me.Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdAdd, Me.cmdEdit, Me.cmdDel, Me.cmdView, Me.ToolStripSeparator4, Me.cmdQuery, Me.cmdRef, Me.ToolStripSeparator5})
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(95, 148)
        '
        'cmdAdd
        '
        Me.cmdAdd.Enabled = False
        Me.cmdAdd.Image = Global.LFERP.My.Resources.Resources.QueryAppend
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(94, 22)
        Me.cmdAdd.Text = "新增"
        '
        'cmdEdit
        '
        Me.cmdEdit.Enabled = False
        Me.cmdEdit.Image = Global.LFERP.My.Resources.Resources.ReviseContents
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(94, 22)
        Me.cmdEdit.Text = "修改"
        '
        'cmdDel
        '
        Me.cmdDel.Enabled = False
        Me.cmdDel.Image = Global.LFERP.My.Resources.Resources.SharingRequestDeny
        Me.cmdDel.Name = "cmdDel"
        Me.cmdDel.Size = New System.Drawing.Size(94, 22)
        Me.cmdDel.Text = "刪除"
        '
        'cmdView
        '
        Me.cmdView.Name = "cmdView"
        Me.cmdView.Size = New System.Drawing.Size(94, 22)
        Me.cmdView.Text = "查看"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(91, 6)
        '
        'cmdQuery
        '
        Me.cmdQuery.Image = Global.LFERP.My.Resources.Resources.ZoomClassic
        Me.cmdQuery.Name = "cmdQuery"
        Me.cmdQuery.Size = New System.Drawing.Size(94, 22)
        Me.cmdQuery.Text = "查詢"
        '
        'cmdRef
        '
        Me.cmdRef.Image = Global.LFERP.My.Resources.Resources.Repeat
        Me.cmdRef.Name = "cmdRef"
        Me.cmdRef.Size = New System.Drawing.Size(94, 22)
        Me.cmdRef.Text = "刷新"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(91, 6)
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn4, Me.GridColumn3, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9, Me.GridColumn10, Me.GridColumn11, Me.GridColumn12})
        Me.GridView1.GridControl = Me.Grid
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.AutoSelectAllInEditor = False
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsCustomization.AllowColumnMoving = False
        Me.GridView1.OptionsCustomization.AllowFilter = False
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.RowAutoHeight = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "廠證編號"
        Me.GridColumn1.FieldName = "Per_NO"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        Me.GridColumn1.Width = 71
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "部門代號"
        Me.GridColumn2.FieldName = "DepID"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "部門名稱"
        Me.GridColumn4.FieldName = "DepName"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 72
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "廠別名稱"
        Me.GridColumn3.FieldName = "FacName"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 4
        Me.GridColumn3.Width = 76
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "開始時間"
        Me.GridColumn5.FieldName = "PT_BeginDate"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 5
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "建立日期"
        Me.GridColumn6.FieldName = "PT_AddDate"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 8
        Me.GridColumn6.Width = 64
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "操作人"
        Me.GridColumn7.FieldName = "PT_ActionName"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 10
        Me.GridColumn7.Width = 70
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "修改日期"
        Me.GridColumn8.FieldName = "PT_EditDate"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 9
        Me.GridColumn8.Width = 62
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "計時編號"
        Me.GridColumn9.FieldName = "PT_NO"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 0
        Me.GridColumn9.Width = 72
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "結束時間"
        Me.GridColumn10.FieldName = "PT_EndDate"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 6
        Me.GridColumn10.Width = 60
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "開始時間"
        Me.GridColumn11.FieldName = "PT_BeginDate"
        Me.GridColumn11.Name = "GridColumn11"
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "合計時間"
        Me.GridColumn12.FieldName = "PT_QtyDate"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 7
        Me.GridColumn12.Width = 72
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Location = New System.Drawing.Point(1, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(658, 36)
        Me.PictureBox1.TabIndex = 45
        Me.PictureBox1.TabStop = False
        '
        'FrmPieceTimePersonnel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(661, 530)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Grid)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "FrmPieceTimePersonnel"
        Me.Text = "個人計時"
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip.ResumeLayout(False)
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Grid As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents MenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmdAdd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdDel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdView As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdQuery As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdRef As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
End Class
