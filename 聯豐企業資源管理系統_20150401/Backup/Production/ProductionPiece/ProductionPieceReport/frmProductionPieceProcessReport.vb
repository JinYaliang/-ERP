Imports LFERP.Library.Product
Imports LFERP.Library.ProductionPieceProcess
Imports LFERP.Library.ProductProcess
Imports LFERP.DataSetting

Public Class frmProductionPieceProcessReport
    Dim mc As New ProductController
    Dim ppc As New ProductionPieceProcessControl
    Dim fc As New FacControler
    Dim ds As New DataSet
    Public isClickbtnFind As Boolean  '�P�_�O�_�����FbtnFind���s

    Private Sub frmProductionPieceProcessFind_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()   '�եγЫت�L�{

        '�[���t�O�W��
        gluFacID.Properties.DisplayMember = "FacName"
        gluFacID.Properties.ValueMember = "FacID"
        gluFacID.Properties.DataSource = fc.GetFacList(strInFacID, Nothing)

        gluPM_M_Code.Properties.DisplayMember = "PM_M_Code"
        gluPM_M_Code.Properties.ValueMember = "PM_M_Code"
        gluPM_M_Code.Properties.DataSource = mc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        ''--------------------�d�߮ɥΤ��v��------------------------
        If strInUserRank = "�Ͳ���" Then
            gluFacID.EditValue = strInFacIDFull
        ElseIf strInUserRank = "�޲z" Then
        ElseIf strInUserRank = "�έp" Then
            gluFacID.EditValue = strInFacIDFull
            gluDepID.EditValue = strInDepIDFull
        End If

    End Sub

    ''' <summary>
    ''' �Ыت�
    ''' </summary>
    ''' ���L�{�Q�H�U�L�{�ե�:
    ''' frmProductionPieceProcessFind_Load()
    Sub CreateTable()
        ds.Tables.Clear()

        With ds.Tables.Add("ProductType")       '�t��W�٪�
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("PM_Type", GetType(String))
        End With
        gluPM_Type.Properties.DisplayMember = "PM_Type"
        gluPM_Type.Properties.ValueMember = "PM_Type"
        gluPM_Type.Properties.DataSource = ds.Tables("ProductType")

    End Sub
    ''' <summary>
    ''' ���������������s
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' ���~�s���ȧ���,�bgluPM_Type���[���������t��W��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' ���L�{�Q�H�U�L�{�ե�:
    ''' cboPro_Type_SelectedIndexChanged()
    Private Sub gluPM_M_Code_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gluPM_M_Code.EditValueChanged
        On Error Resume Next

        Dim ppc As New ProcessMainControl
        Dim ppi As List(Of ProcessMainInfo)
        ds.Tables("ProductType").Clear()

        ppi = ppc.ProcessMain_GetList2(cboPro_Type.EditValue, gluPM_M_Code.EditValue)
        If ppi.Count = 0 Then
        Else

            Dim i As Integer
            For i = 0 To ppi.Count - 1
                Dim row As DataRow
                row = ds.Tables("ProductType").NewRow
                row("PM_Type") = ppi(i).Type3ID
                ds.Tables("ProductType").Rows.Add(row)
            Next
        End If
    End Sub
    ''' <summary>
    ''' �������d�ߡ����s
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim ds As New DataSet
        Dim strDPT_ID As String

        'If GridView1.RowCount = 0 Then Exit Sub

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet

        Dim mcCompany As New LFERP.DataSetting.CompanyControler
        Dim strCompany As String

        strCompany = Mid(strInDPT_ID, 1, 4)   '��o�n���̩��ݤ��qID,�H��^���q�W�١ALOGO

        ds.Tables.Clear()

        If gluDepID.EditValue Is Nothing Then
            If gluFacID.EditValue Is Nothing Then
                strDPT_ID = strInDepID
            Else
                strDPT_ID = gluFacID.EditValue
            End If
        Else
            strDPT_ID = gluDepID.EditValue
        End If

        If dtePP_ActionDateBegin.Text = "" Then
            tempValue2 = Nothing
        Else
            tempValue2 = dtePP_ActionDateBegin.Text
        End If
        If dtePP_ActionDateEnd.Text = "" Then
            tempValue3 = Nothing
        Else
            tempValue3 = dtePP_ActionDateEnd.Text
        End If
        If ppc.ProductionPieceProcess_GetList(Nothing, Nothing, cboPro_Type.Text.Trim, gluPM_M_Code.EditValue, gluPM_Type.EditValue, luePS_NO.EditValue, cboPP_N_Name.Text.Trim, strDPT_ID, Nothing, Nothing, tempValue2, tempValue3).Count <= 0 Then
            MsgBox("�����`����L�O��!", 64, "����")
            gluFacID.Focus()
            Exit Sub
        End If
        ltc.CollToDataSet(ds, "ProductionPieceProcess", ppc.ProductionPieceProcess_GetList(Nothing, Nothing, cboPro_Type.Text.Trim, gluPM_M_Code.EditValue, gluPM_Type.EditValue, luePS_NO.EditValue, cboPP_N_Name.Text.Trim, strDPT_ID, Nothing, Nothing, tempValue2, tempValue3))
        ltc1.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))

        ds.Tables("ProductionPieceProcess").Columns.Add("P_ID", GetType(Integer))
        Dim i As Long

        For i = 0 To ds.Tables("ProductionPieceProcess").Rows.Count - 1
            ds.Tables("ProductionPieceProcess").Rows(i)("P_ID") = i + 1
        Next

        PreviewRPT(ds, "rptProductionPieceProcessPrice", "�p��u�ǳ�����`��", True, True)

        ltc = Nothing
        ltc1 = Nothing
    End Sub
    ''' <summary>
    ''' ���Ů�����ܤU�ԦC��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gluPM_M_Code_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gluFacID.KeyDown, gluDepID.KeyDown, gluPM_M_Code.KeyDown, cboPro_Type.KeyDown, gluPM_Type.KeyDown
        If e.KeyCode = Keys.Space Then
            sender.ShowPopup()
        End If
    End Sub
    ''' <summary>
    ''' �u�������ȧ���,�bgluPM_Type���[���������t��W��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboPro_Type_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPro_Type.SelectedIndexChanged
        gluPM_M_Code_EditValueChanged(Nothing, Nothing) '�ե�gluPM_M_Code_EditValueChanged()�L�{
    End Sub
    ''' <summary>
    ''' �t�O�W�٧��ܮɡA���s�[����������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gluFacID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluFacID.EditValueChanged
        Dim dc As New DepartmentControler
        If gluFacID.EditValue Is Nothing Then Exit Sub

        gluDepID.Properties.DisplayMember = "DepName"
        gluDepID.Properties.ValueMember = "DepID"
        gluDepID.Properties.DataSource = dc.BriName_GetList(strInDepID, Nothing, gluFacID.EditValue)
    End Sub
    ''' <summary>
    ''' �t��W�٧��ܮɡA���s�[���j�u�ǦW�ٶ���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gluPM_Type_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluPM_Type.EditValueChanged
        Dim pc As New ProcessMainControl

        luePS_NO.Properties.DisplayMember = "PS_Name"
        luePS_NO.Properties.ValueMember = "PS_NO"
        luePS_NO.Properties.DataSource = pc.ProcessMain_GetList(Nothing, gluPM_M_Code.EditValue, cboPro_Type.EditValue, gluPM_Type.EditValue, Nothing, Nothing)
    End Sub
    ''' <summary>
    ''' �j�u�ǦW�٧��ܮɡA���s�[���p�u�ǦW�ٶ���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub luePS_NO_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles luePS_NO.EditValueChanged
        Dim ppi As List(Of ProductionPieceProcessInfo)
        Dim i, j%

        cboPP_N_Name.Properties.Items.Clear()
        cboPP_N_Name.Text = ""
        ppi = ppc.ProductionPieceProcess_GetList(Nothing, Nothing, cboPro_Type.Text.Trim, gluPM_M_Code.EditValue, gluPM_Type.EditValue, luePS_NO.EditValue, Nothing, strInDepID, Nothing, Nothing, Nothing, Nothing)
        If ppi.Count > 0 Then
            For i = 0 To ppi.Count - 1
                If cboPP_N_Name.Properties.Items.Count > 0 Then
                    For j = 0 To cboPP_N_Name.Properties.Items.Count - 1
                        If cboPP_N_Name.Properties.Items.Item(j) = ppi(i).PP_N_Name Then
                            GoTo Abc
                        End If
                    Next
                End If
                cboPP_N_Name.Properties.Items.Add(ppi(i).PP_N_Name)
Abc:
            Next
        End If
    End Sub
End Class