Imports LFERP.Library.Product
Imports LFERP.Library.ProductProcess

Public Class frmProductionCombinationFind2
    Dim fc As New LFERP.Library.PieceProcess.PersonnelControl
    Dim ds As New DataSet
    Public isClickbtnFind As Boolean

    Private Sub frmProductionCombinationFind2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()        '�եγЫت�L�{

        '�[���ާ@�����H��
        gluM_Dep.Properties.DataSource = fc.FacBriSearch_GetList(Nothing, Nothing, Nothing, Nothing)  '�ܧ󳡪�
        gluM_Dep.Properties.DisplayMember = "DepName"
        gluM_Dep.Properties.ValueMember = "DepID"

        '�[�����~�s��
        Dim mc As New ProcessMainControl
        gluPM_M_Code.Properties.DisplayMember = "PM_M_Code"
        gluPM_M_Code.Properties.ValueMember = "PM_M_Code"
        gluPM_M_Code.Properties.DataSource = mc.ProcessMain_GetList3(Nothing, Nothing)

    End Sub
    ''' <summary>
    ''' �Ыت�L�{
    ''' </summary>
    ''' ���L�{�Q�H�U�L�{�եΡG
    ''' frmProductionCombinationFind1_Load()
    Sub CreateTable()
        ds.Tables.Clear()

        '�t��W�٪�
        With ds.Tables.Add("ProductType")
            .Columns.Add("PM_Type", GetType(String))
        End With
        gluPM_Type.Properties.ValueMember = "PM_Type"
        gluPM_Type.Properties.DisplayMember = "PM_Type"
        gluPM_Type.Properties.DataSource = ds.Tables("ProductType")

        '�o�Ƥu�Ǫ�
        With ds.Tables.Add("Process")
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("PS_Name", GetType(String))
        End With
        gluPro_NO.Properties.ValueMember = "PS_NO"
        gluPro_NO.Properties.DisplayMember = "PS_Name"
        gluPro_NO.Properties.DataSource = ds.Tables("Process")

    End Sub
    ''' <summary>
    ''' ���~�s�����ܮɡA�[���������t��W�٫H��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' ���L�{�Q�H�U�L�{�եΡG
    ''' cbType_SelectedIndexChanged
    Private Sub gluPM_M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluPM_M_Code.EditValueChanged
        On Error Resume Next

        Dim ppc As New ProcessMainControl
        Dim ppi As List(Of ProcessMainInfo)
        ds.Tables("ProductType").Clear()
        ds.Tables("Process").Clear()
        ppi = ppc.ProcessMain_GetList2(cbType.EditValue, gluPM_M_Code.EditValue)
        If ppi.Count = 0 Then
        Else
            Dim i As Integer
            For i = 0 To ppi.Count - 1
                Dim row As DataRow
                row = ds.Tables("ProductType").NewRow
                row("PM_Type") = ppi(i).Type3ID
                ds.Tables("ProductType").Rows.Add(row)
            Next
            gluPro_NO.EditValue = Nothing
        End If
    End Sub
    ''' <summary>
    ''' �����������s�A��������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' �����d�߫��s
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        If txtM_ID.Text.Trim = "" Then
            tempValue = Nothing
        Else
            tempValue = txtM_ID.Text.Trim
        End If

        If cboM_Type.Text.Trim = "" Then
            tempValue2 = Nothing
        Else
            tempValue2 = cboM_Type.EditValue
        End If

        If gluM_Dep.Text.Trim = "" Then
            tempValue3 = Nothing
        Else
            tempValue3 = gluM_Dep.EditValue
        End If

        If cbType.Text.Trim = "" Then
            tempValue4 = Nothing
        Else
            tempValue4 = cbType.EditValue
        End If

        If gluPM_M_Code.Text.Trim = "" Then
            tempValue5 = Nothing
        Else
            tempValue5 = gluPM_M_Code.EditValue
        End If

        If gluPM_Type.Text.Trim = "" Then
            tempValue6 = Nothing
        Else
            tempValue6 = gluPM_Type.EditValue
        End If

        If gluPro_NO.Text.Trim = "" Then
            tempValue7 = Nothing
        Else
            tempValue7 = gluPro_NO.EditValue
        End If

        If dteM_DateBegin.Text.Trim = "" Then
            tempValue8 = Nothing
        Else
            tempValue8 = dteM_DateBegin.DateTime
        End If

        If dteM_DateEnd.Text.Trim = "" Then
            tempValue9 = Nothing
        Else
            tempValue9 = dteM_DateEnd.DateTime
        End If

        isClickbtnFind = True
        Me.Close()
    End Sub
    ''' <summary>
    ''' �u���������ܮɡA�եβ��~�s�����ܹL�{
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbType.SelectedIndexChanged
        gluPM_M_Code_EditValueChanged(Nothing, Nothing)     '�եβ��~�s�����ܹL�{
    End Sub
    ''' <summary>
    ''' �t��W�٧��ܮɡA�[���������o�Ƥu�ǫH��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gluPM_Type_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluPM_Type.EditValueChanged
        On Error Resume Next

        Dim pc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)
        pci = pc.ProcessMain_GetList(Nothing, gluPM_M_Code.EditValue, cbType.EditValue, gluPM_Type.EditValue, Nothing, True)

        If pci.Count = 0 Then Exit Sub
        ds.Tables("Process").Clear()

        Dim i As Integer
        For i = 0 To pci.Count - 1
            Dim row As DataRow
            row = ds.Tables("Process").NewRow

            row("PS_NO") = pci(i).PS_NO
            row("PS_Name") = pci(i).PS_Name

            ds.Tables("Process").Rows.Add(row)
        Next
    End Sub

    '@ 2012/8/10 �K�[ ���Ů���u�X�U�Ե��
    Private Sub cboM_Type_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboM_Type.KeyDown, gluM_Dep.KeyDown, gluPM_M_Code.KeyDown, gluPM_Type.KeyDown, gluPro_NO.KeyDown, cbType.KeyDown
        If e.KeyCode = Keys.Space Then
            sender.ShowPopup()
        End If
    End Sub
End Class