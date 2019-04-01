Imports LFERP.Library.Product
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.Production.ProductionRatio

Public Class frmProductionCombinationFind1
    Dim ds As New DataSet
    Public isClickbtnFind As Boolean

    Private Sub frmProductionCombinationFind1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()       '�եγЫت�L�{

        '�[�����~�s��
        Dim mc As New ProcessMainControl
        gluPM_M_Code.Properties.DisplayMember = "PM_M_Code"
        gluPM_M_Code.Properties.ValueMember = "PM_M_Code"
        gluPM_M_Code.Properties.DataSource = mc.ProcessMain_GetList3(Nothing, Nothing)
        isClickbtnFind = False
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

        '�j�u�ǦW�٪�
        With ds.Tables.Add("Process")
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("PS_Name", GetType(String))
        End With
        gluPro_NO.Properties.ValueMember = "PS_NO"
        gluPro_NO.Properties.DisplayMember = "PS_Name"
        gluPro_NO.Properties.DataSource = ds.Tables("Process")

        '�p�u�ǦW�٪�
        With ds.Tables.Add("ProcessSub")
            .Columns.Add("Pro_NO1", GetType(String))
            .Columns.Add("PS_Name1", GetType(String))
        End With
        gluPro_NO1.Properties.ValueMember = "Pro_NO1"
        gluPro_NO1.Properties.DisplayMember = "PS_Name1"
        gluPro_NO1.Properties.DataSource = ds.Tables("ProcessSub")
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

        gluPM_Type.Text = ""
        ds.Tables("ProductType").Clear()
        ds.Tables("Process").Clear()
        ds.Tables("ProcessSub").Clear()

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
    ''' �t��W�٧��ܮɡA�[���������j�u�ǦW�٫H��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gluPM_Type_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluPM_Type.EditValueChanged
        On Error Resume Next

        Dim pc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)
        pci = pc.ProcessMain_GetList(Nothing, gluPM_M_Code.EditValue, cbType.EditValue, gluPM_Type.EditValue, Nothing, True)

        gluPro_NO.Text = ""
        ds.Tables("Process").Clear()
        ds.Tables("ProcessSub").Clear()

        If pci.Count = 0 Then Exit Sub

        Dim i As Integer
        For i = 0 To pci.Count - 1
            Dim row As DataRow
            row = ds.Tables("Process").NewRow

            row("PS_NO") = pci(i).PS_NO
            row("PS_Name") = pci(i).PS_Name

            ds.Tables("Process").Rows.Add(row)
        Next
    End Sub
    ''' <summary>
    ''' �j�u�ǦW�٧��ܮɡA�[���������p�u�ǦW�٫H��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gluPro_NO_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluPro_NO.EditValueChanged
        On Error Resume Next

        Dim prc As New ProductionRatioControl
        Dim pri As List(Of ProductionRatioInfo)
        pri = prc.ProductionRatio_GetList(Nothing, gluPro_NO.EditValue, Nothing, Nothing)

        gluPro_NO1.Text = ""
        ds.Tables("ProcessSub").Clear()

        If pri.Count = 0 Then Exit Sub

        Dim i As Integer
        For i = 0 To pri.Count - 1
            Dim row As DataRow
            row = ds.Tables("ProcessSub").NewRow

            row("Pro_NO1") = pri(i).Pro_NO1
            row("PS_Name1") = pri(i).PS_Name1

            ds.Tables("ProcessSub").Rows.Add(row)
        Next
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
        If gluPro_NO.Text.Trim = "" Then
            MsgBox("�п�J�j�u�ǦW��!", 64, "����")
            gluPro_NO.Focus()
            Exit Sub
        End If

        tempValue = gluPro_NO.EditValue
        If gluPro_NO1.Text.Trim = "" Then
            tempValue2 = Nothing
        Else
            tempValue2 = gluPro_NO1.EditValue
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

    '@ 2012/8/10 �K�[ ���Ů���u�X�U�Ե��
    Private Sub gluPM_M_Code_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gluPM_M_Code.KeyDown, gluPM_Type.KeyDown, cbType.KeyDown, gluPro_NO.KeyDown, gluPro_NO1.KeyDown
        If e.KeyCode = Keys.Space Then
            sender.ShowPopup()
        End If
    End Sub
End Class