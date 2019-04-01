Imports LFERP.Library.Material
Imports LFERP.Library.MaterialParam
Imports LFERP.Library
Imports LFERP.Library.WareHouse
Imports LFERP.DataSetting



Public Class frmWareOutTotalReport

    Dim mtc As New Material.MaterialTypeController
    Dim mc As New MaterialController

    Public strDPTID, strFacID As String  '�����A�t�O  �եγ����C��
    Dim strWHID As String '�ܮw�N�� �եέܮw��


    Private Sub frmWareOutTotalReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mtc.LoadNodes(tv1, ErpUser.MaterialType)

        CheckEdit7.Checked = True
        DateEdit1.EditValue = Format(Now, "yyyy/MM/dd")
        DateEdit2.EditValue = Format(Now, "yyyy/MM/dd")
        Label1.Text = tempValue4
        tempValue4 = ""
        BEWHID.Select()
    End Sub

    '�ܮw��ܡA�q�{��Nothing
    Private Sub BEWHID_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEWHID.ButtonClick
        If Label1.Text = "�X�w���B���`" Then
            tempValue3 = "50080801"
        Else
            tempValue3 = "50080901"
        End If
        frmWareHouseSelect.SelectWareID = ""
        frmWareHouseSelect.StartPosition = FormStartPosition.Manual
        frmWareHouseSelect.Left = Me.Left + BEWHID.Left + 2
        frmWareHouseSelect.Top = Me.Top + BEWHID.Top + BEWHID.Height + 21
        frmWareHouseSelect.ShowDialog()
        If frmWareHouseSelect.SelectWareID = "" Then
        Else
            BEWHID.Text = frmWareHouseSelect.SelectWareName
            strWHID = frmWareHouseSelect.SelectWareID

        End If
    End Sub

    Private Sub BEfactory_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEfactory.ButtonClick
        tempValue = "�X�w���`-�t�O"

        frmDepartmentSelect.FacID = ""
        frmDepartmentSelect.FacName = ""

        frmDepartmentSelect.ShowDialog()

        If frmDepartmentSelect.FacID = "" Then

        Else
            BEfactory.Text = frmDepartmentSelect.FacName
            strFacID = frmDepartmentSelect.FacID

        End If
    End Sub

    Private Sub BEDepartment_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEDepartment.ButtonClick
        tempValue = "�X�w���`-����"

        frmDepartmentSelect.DptID = ""
        frmDepartmentSelect.DptName = ""

        frmDepartmentSelect.ShowDialog()

        If frmDepartmentSelect.DptID = "" Then

        Else
            BEDepartment.Text = frmDepartmentSelect.DptName
            strDPTID = frmDepartmentSelect.DptID

        End If

    End Sub
    '�������O
    Private Sub CheckEdit1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit1.CheckedChanged
        If CheckEdit1.Checked = True Then
            txtCode.Enabled = False
            PopupContainerEdit1.Enabled = True
            CheckEdit4.Checked = False
            txtCode.Text = Nothing
            PopupContainerEdit1.Focus()
            PopupContainerEdit1.SelectAll()
        End If
    End Sub
    '�t�O���
    Private Sub CheckEdit2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit2.CheckedChanged
        If CheckEdit2.Checked = True Then
            BEfactory.Enabled = True
            BEDepartment.Enabled = False
            CheckEdit3.Checked = False
            BEDepartment.EditValue = Nothing
            BEfactory.Focus()
            BEfactory.SelectAll()
        End If
    End Sub
    '�������
    Private Sub CheckEdit3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit3.CheckedChanged
        If CheckEdit3.Checked = True Then
            BEfactory.Enabled = False
            BEDepartment.Enabled = True
            CheckEdit2.Checked = False
            BEfactory.EditValue = Nothing
            BEDepartment.Focus()
            BEDepartment.SelectAll()
        End If
    End Sub
    '���ƽs�X
    Private Sub CheckEdit4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit4.CheckedChanged
        If CheckEdit4.Checked = True Then
            txtCode.Enabled = True
            PopupContainerEdit1.Enabled = False
            CheckEdit1.Checked = False
            PopupContainerEdit1.EditValue = Nothing
            txtCode.Focus()
            txtCode.SelectAll()
        End If
    End Sub

    '�����ܪ��������O
    Private Sub tv1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tv1.DoubleClick
        PopupContainerEdit1.EditValue = tv1.SelectedNode.Tag
        PopupContainerControl1.OwnerEdit.ClosePopup()
    End Sub

    '�C�L�H��
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim ds As New DataSet
        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ltc3 As New CollectionToDataSet
        Dim ltc4 As New CollectionToDataSet
        Dim ltc5 As New CollectionToDataSet

        Dim strCode As String '���ƽs�X
        Dim strName As String '���ƦW��
        Dim strDPT As String  '���γ��� Ū����
        Dim strWH As String  '���έܮw  Ū����
        Dim strDate1 As String '�X�w���
        Dim strDate2 As String '�X�w���

        '*-----------------------------------------------------------------*
        If CheckEdit1.Checked = True Then
            strCode = PopupContainerEdit1.EditValue
        ElseIf CheckEdit4.Checked = True Then
            strCode = txtCode.Text
        Else
            strCode = Nothing
        End If
        If CheckEdit2.Checked = True Then
            strDPT = strFacID
        ElseIf CheckEdit3.Checked = True Then
            strDPT = strDPTID
        Else
            strDPT = Nothing
        End If
        'If CheckEdit3.Checked = True Then
        '    strDPT = strDPTID
        'Else
        '    strDPT = Nothing
        'End If
        'If CheckEdit4.Checked = True Then
        '    strCode = txtCode.Text
        'Else
        '    strCode = Nothing
        'End If
        If CheckEdit5.Checked = True Then
            strName = txtName.Text
        Else
            strName = Nothing
        End If
        If CheckEdit6.Checked = True Then
            strDate1 = DateEdit1.EditValue
            strDate2 = DateEdit2.EditValue
        Else
            strDate1 = Nothing
            strDate2 = Nothing
        End If
        If Len(BEWHID.EditValue) = 0 Then
            MsgBox("�п�ܻݭn���`���ܮw�I")
            Exit Sub
        End If
        If CheckEdit7.Checked = True Then
            strWH = strWHID
        Else
            strWH = Nothing
        End If
        '*-----------------------------------------------------------------*
        Dim uc As New MaterialController
        Dim wh As New WareHouseController
        Dim wo As New WareOut.WareOutController
        Dim uc2 As New DepartmentControler

        ds.Tables.Clear()

        If wo.WareOut_GetList1(Nothing, strWH, strCode, strDPT, strName, strDate1, strDate2, Nothing, True).Count = 0 Then
            MsgBox("�L�ŦX���󪺰O��!")
            Exit Sub
        End If
        ltc.CollToDataSet(ds, "Department", uc2.Department_GetList(Nothing, Nothing, Nothing))
        ltc1.CollToDataSet(ds, "MaterialCode", uc.MaterialCode_GetList(Nothing))
        ltc3.CollToDataSet(ds, "WareHouse", wh.WareHouse_GetList(Nothing))
        ltc4.CollToDataSet(ds, "WareOut", wo.WareOut_GetList1(Nothing, strWH, strCode, strDPT, strName, strDate1, strDate2, Nothing, True))

        If Label1.Text = "�X�w���B���`" Then
            PreviewRPT(ds, "rptWareOutTotalMoney", "�X�w���B���`", True, True)
        Else
            PreviewRPT(ds, "rptWareOutTotalNoMoney", "�X�w�O�����`", True, True)
        End If
        ltc = Nothing
        ltc1 = Nothing
        ltc3 = Nothing
        ltc4 = Nothing
        ltc5 = Nothing

        Me.Close()

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub CheckEdit7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit7.CheckedChanged
        If CheckEdit7.Checked = True Then
            BEWHID.Focus()
            BEWHID.SelectAll()
        End If
    End Sub

    Private Sub CheckEdit5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit5.CheckedChanged
        If CheckEdit5.Checked = True Then
            txtName.Focus()
            txtName.SelectAll()
        End If
    End Sub

    Private Sub CheckEdit6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit6.CheckedChanged
        If CheckEdit6.Checked = True Then
            DateEdit1.Focus()
        End If
    End Sub
End Class