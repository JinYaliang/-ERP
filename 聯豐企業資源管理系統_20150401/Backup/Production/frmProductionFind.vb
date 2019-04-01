Imports DevExpress.XtraEditors.Repository
Imports LFERP.Library.Product
Imports System.Data.SqlClient
Imports LFERP.Library.ProductProcess

Public Class frmProductionFind
    Private gridEditor As New GridEditorCollection1
    Private repositoryItemTextEdit As New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents repositoryItemButtonEdit As New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents repositoryItemComboBox, repositoryItemComboBox1, repositoryItemComboBox2 As New DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents repositoryItemCheckEdit As New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Private repositoryItemMemoEdit As New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Private repositoryItemDateEdit As New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents repositoryItemLookUpEdit As New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents RepositoryItemGridLookUpEdit, RepositoryItemGridLookUpEdit1, RepositoryItemGridLookUpEdit2 As New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents RepositoryItemGridLookUpEditView, RepositoryItemGridLookUpEditView1, RepositoryItemGridLookUpEditView2 As New DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents repositoryItemRadioGroup As New DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup
    Private dataColumn1 As New System.Data.DataColumn
    Private dataColumn2 As New System.Data.DataColumn
    Private dataColumn3 As New System.Data.DataColumn
    Private colName, colName1, colName2, colName3 As New DevExpress.XtraGrid.Columns.GridColumn
    Dim ds As New DataSet
    Dim dt As New DataTable
    Dim mc As New ProductController
    Public isClickbtnFind As Boolean


    Private Sub frmProductionFind_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitInplaceEditors()
        Me.GridControl1.DataSource = gridEditor
    End Sub

    Private Sub gridView1_CustomRowCellEdit(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs) Handles GridView1.CustomRowCellEdit
        If e.Column Is Me.gridEditorValue Then
            Dim item As GridEditorItem1 = Nothing
            If TypeOf GridView1.GetRow(e.RowHandle) Is GridEditorItem1 Then item = CType(GridView1.GetRow(e.RowHandle), GridEditorItem1)
            If Not item Is Nothing Then
                e.RepositoryItem = item.RepositoryItem
            End If
        End If
    End Sub

    Sub CreateTable()
        ds.Tables.Clear()

        With ds.Tables("Production")
            .Columns.Add("PM_M_Code", GetType(String))
        End With
        RepositoryItemGridLookUpEdit.DisplayMember = "PM_M_Code"
        RepositoryItemGridLookUpEdit.ValueMember = "PM_M_Code"
        RepositoryItemGridLookUpEdit.DataSource = ds.Tables("Production")

    End Sub

    Sub LoadData()
        'ds.Tables("Production").Clear()
        'Dim ada = New SqlDataAdapter(mc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        'ada.fill(ds.Tables("Production"))
    End Sub

    Private Sub InitInplaceEditors()

        Me.gridEditor.Add(Me.repositoryItemComboBox, "�u������", "�Ͳ��[�u")
        Me.gridEditor.Add(Me.RepositoryItemGridLookUpEdit, "���~�s��", "")
        Me.gridEditor.Add(Me.RepositoryItemGridLookUpEdit1, "���@�@��", "")
        Me.gridEditor.Add(Me.RepositoryItemGridLookUpEdit2, "�u�ǦW��", "")
        Me.gridEditor.Add(Me.repositoryItemComboBox1, "���o����", tempValue11)
        Me.gridEditor.Add(Me.repositoryItemDateEdit, "�_�l���", Format(Now, "yyyy/MM/dd"))
        Me.gridEditor.Add(Me.repositoryItemDateEdit, "�������", Format(Now, "yyyy/MM/dd"))
        Me.gridEditor.Add(Me.repositoryItemRadioGroup, "�f�֪��A", 0)
        Me.gridEditor.Add(Me.repositoryItemTextEdit, "��d�H��", "")
        Me.gridEditor.Add(Me.repositoryItemComboBox2, "�~�o����", "")

        Me.repositoryItemComboBox.Items.AddRange(New Object() {"�F���[�u", "�Ͳ��[�u", "�˰t�X�f"})
        Me.repositoryItemComboBox1.Items.AddRange(New Object() {"����", "�o��"})
        Me.repositoryItemRadioGroup.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(0, "����"), New DevExpress.XtraEditors.Controls.RadioGroupItem(1, "�w�f��"), New DevExpress.XtraEditors.Controls.RadioGroupItem(2, "���f��")})
        Me.repositoryItemComboBox2.Items.AddRange(New Object() {"�~�o�[�u", "���Τ����[�u"})

        RepositoryItemGridLookUpEdit.PopupSizeable = True
        RepositoryItemGridLookUpEdit.PopupFormWidth = gridEditorValue.Width - 15
        RepositoryItemGridLookUpEditView.OptionsView.ShowIndicator = False
        RepositoryItemGridLookUpEditView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {colName})
        Me.colName.Caption = "���~�s��"
        Me.colName.FieldName = "PM_M_Code"
        Me.colName.Name = "PM_M_Code"
        Me.colName.Visible = True
        Me.colName.VisibleIndex = 0
        RepositoryItemGridLookUpEdit.View = RepositoryItemGridLookUpEditView
        RepositoryItemGridLookUpEdit.DisplayMember = "PM_M_Code"
        RepositoryItemGridLookUpEdit.ValueMember = "PM_M_Code"
        RepositoryItemGridLookUpEdit.DataSource = mc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        RepositoryItemGridLookUpEdit1.NullText = ""
        RepositoryItemGridLookUpEdit2.NullText = ""
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub RepositoryItemGridLookUpEdit_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RepositoryItemGridLookUpEdit.EditValueChanged
        If sender.text = "" Then Exit Sub

        Dim ppc As New ProcessMainControl
        RepositoryItemGridLookUpEditView1.Columns.Clear()
        RepositoryItemGridLookUpEditView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {colName1})
        Me.colName1.Caption = "����"
        Me.colName1.FieldName = "Type3ID"
        Me.colName1.Name = "Type3ID"
        Me.colName1.Visible = True
        Me.colName1.VisibleIndex = 1
        RepositoryItemGridLookUpEdit1.View = RepositoryItemGridLookUpEditView1
        RepositoryItemGridLookUpEdit1.DisplayMember = "Type3ID"
        RepositoryItemGridLookUpEdit1.ValueMember = "Type3ID"
        RepositoryItemGridLookUpEdit1.DataSource = ppc.ProcessMain_GetList2(GridView1.GetRowCellValue(0, "Values").ToString, sender.EditValue)

    End Sub
    Private Sub RepositoryItemGridLookUpEdit1_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RepositoryItemGridLookUpEdit1.EditValueChanged
        If sender.text = "" Then Exit Sub

        Dim pc As New ProcessMainControl

        RepositoryItemGridLookUpEditView2.Columns.Clear()
        RepositoryItemGridLookUpEditView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {colName2})
        Me.colName2.Caption = "�u�ǦW��"
        Me.colName2.FieldName = "PS_Name"
        Me.colName2.Name = "PS_Name"
        Me.colName2.Visible = True
        Me.colName2.VisibleIndex = 0
        Me.colName3.Caption = "�u�ǽs��"
        Me.colName3.FieldName = "PS_NO"
        Me.colName3.Name = "PS_NO"
        Me.colName3.Visible = False
        Me.colName3.VisibleIndex = 1
        RepositoryItemGridLookUpEdit2.View = RepositoryItemGridLookUpEditView2
        RepositoryItemGridLookUpEdit2.DisplayMember = "PS_Name"
        RepositoryItemGridLookUpEdit2.ValueMember = "PS_NO"
        RepositoryItemGridLookUpEdit2.DataSource = pc.ProcessMain_GetList(Nothing, GridView1.GetRowCellValue(1, "Values").ToString, GridView1.GetRowCellValue(0, "Values").ToString, sender.EditValue, Nothing, True)

    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        isClickbtnFind = True
        tempValue = GridView1.GetRowCellValue(0, "Values").ToString      '�u������
        If GridView1.GetRowCellValue(1, "Values").ToString = "" Then
            tempValue2 = Nothing
        Else
            tempValue2 = GridView1.GetRowCellValue(1, "Values").ToString    '���~�s��
        End If
        If GridView1.GetRowCellValue(2, "Values").ToString = "" Then
            tempValue3 = Nothing
        Else
            tempValue3 = GridView1.GetRowCellValue(2, "Values").ToString    '���@�@��
        End If
        If GridView1.GetRowCellValue(3, "Values").ToString = "" Then
            tempValue4 = Nothing
        Else
            tempValue4 = GridView1.GetRowCellValue(3, "Values").ToString    '�u�ǦW��
        End If
        If GridView1.GetRowCellValue(4, "Values").ToString = "" Then
            tempValue5 = Nothing
        Else
            tempValue5 = GridView1.GetRowCellValue(4, "Values").ToString    '���o����
        End If
        If GridView1.GetRowCellValue(5, "Values").ToString = "" Then
            tempValue6 = Nothing
        Else
            tempValue6 = GridView1.GetRowCellValue(5, "Values")                   '�_�l���
        End If
        If GridView1.GetRowCellValue(6, "Values").ToString = "" Then
            tempValue7 = Nothing
        Else
            tempValue7 = GridView1.GetRowCellValue(6, "Values")                   '�������
        End If
        If GridView1.GetRowCellValue(7, "Values") = 0 Then
            tempValue8 = Nothing                                                                    '�f�֪��A
        ElseIf GridView1.GetRowCellValue(7, "Values") = 1 Then
            tempValue8 = True
        ElseIf GridView1.GetRowCellValue(7, "Values") = 2 Then
            tempValue8 = False
        End If
        If GridView1.GetRowCellValue(8, "Values").ToString = "" Then
            tempValue9 = Nothing
        Else
            tempValue9 = GridView1.GetRowCellValue(8, "Values").ToString    '��d�H��
        End If
        If GridView1.GetRowCellValue(9, "Values").ToString = "" Then
            tempValue10 = Nothing
        Else
            tempValue10 = GridView1.GetRowCellValue(9, "Values").ToString  '�~�o����
        End If

        Me.Close()

    End Sub
End Class


Public Class GridEditorItem1
    Private frName As String
    Private frValue As Object
    Private frRepositoryItem As RepositoryItem

    Public Sub New(ByVal frRepositoryItem As RepositoryItem, ByVal frName As String, ByVal frValue As Object)
        Me.frRepositoryItem = frRepositoryItem
        Me.frName = frName
        Me.frValue = frValue
    End Sub

    Public ReadOnly Property Names() As String
        Get
            Return Me.frName
        End Get
    End Property

    Public Property Values() As Object
        Get
            Return Me.frValue
        End Get
        Set(ByVal Values As Object)

            Me.frValue = Values
        End Set
    End Property

    Public ReadOnly Property RepositoryItem() As RepositoryItem
        Get
            Return Me.frRepositoryItem
        End Get
    End Property

End Class

Friend Class GridEditorCollection1

    Inherits ArrayList

    Public Sub New()
    End Sub

    Default Public Shadows ReadOnly Property Item(ByVal index As Integer) As GridEditorItem1
        Get
            Return CType(MyBase.Item(index), GridEditorItem1)
        End Get
    End Property

    Public Overloads Sub Add(ByVal frRepositoryItem As RepositoryItem, ByVal frName As String, ByVal frValue As Object)
        MyBase.Add(New GridEditorItem1(frRepositoryItem, frName, frValue))
    End Sub

End Class