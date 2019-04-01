Imports LFERP.Library.WareHouse
Imports LFERP.Library.WareHouse.WareOut
Imports LFERP.Library.Material
Imports LFERP.SystemManager.SystemUser
Imports LFERP.DataSetting
Imports LFERP.Library.WareHouseDetail
Imports Microsoft.Office.Core
Imports Microsoft.Office.Interop

Public Class frmWareInventoryHaltRpt
    Dim strWH As String

    Private Sub frmWareInventoryHaltRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        strWH = tempValue3
        Me.Text = tempValue4
        tempValue3 = Nothing
        tempValue4 = Nothing

        If strWH = "50081001" Then
            RadioButton2.Visible = True
            RadioButton1.Visible = True
            RadioButton1.Checked = True
        ElseIf strWH = "500208" Then
            RadioButton2.Visible = False
            RadioButton1.Visible = False
            cmdSave.Text = "�ɥX(&L)"

            Me.DateEdit1.EditValue = Format(Now, "yyyy-MM") & "-01"
            Me.DateEdit2.EditValue = Format(Now, "yyyy-MM-dd")
        Else
            RadioButton2.Visible = False
            RadioButton1.Visible = False
        End If


    End Sub
    'Private Sub ButtonEdit1_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ButtonEdit1.ButtonClick

    '    frmWareHouseSelect.SelectWareID = ""

    '    frmWareHouseSelect.ShowDialog()
    '    If frmWareHouseSelect.SelectWareID = "" Then
    '    Else
    '        ButtonEdit1.Text = frmWareHouseSelect.SelectWareID
    '    End If

    'End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If strWH = "50081001" Then      '�b�����Ƥ@����
            If Me.ButtonEdit1.Text = "" Then
                MsgBox("�ܮw������")
                Exit Sub
            End If
            If Me.DateEdit1.Text = "" Or Me.DateEdit2.Text = "" Then
                MsgBox("�ɶ�������")
                Exit Sub
            End If

            ''���L����
            If RadioButton1.Checked = True Then
                Dim ds As New DataSet
                Dim ltc As New CollectionToDataSet
                Dim ltc1 As New CollectionToDataSet
                Dim ltc2 As New CollectionToDataSet


                'Dim wh As New WareHouseController
                'Dim wic As New WareInventory.WareInventoryMTController

                'If wic.WareInventory_GetHalt(ButtonEdit1.EditValue, DateEdit1.Text, DateEdit2.Text).Count = 0 Then
                '    MsgBox("�b�����Ƭ���,�Э��s�]�m�d�߱���!")
                '    Exit Sub
                'End If

                'ds.Tables.Clear()

                'ltc.CollToDataSet(ds, "WareHouse", wh.WareHouse_GetList(Nothing))
                'ltc2.CollToDataSet(ds, "WareInventory", wic.WareInventory_GetHalt(ButtonEdit1.EditValue, DateEdit1.Text, DateEdit2.Text))

                'PreviewRPT(ds, "rptWareInventoryGetHalt", "�b�����Ƥ@����", True, True)
                'ltc = Nothing
                'ltc2 = Nothing


                Dim mcCompany As New LFERP.DataSetting.CompanyControler
                Dim strCompany As String
                strCompany = Mid(strInDPT_ID, 1, 4)   '��o�n���̩��ݤ��qID,�H��^���q�W�١ALOGO
                Dim wic As New WareInventory.WareInventoryMTController

                If wic.WareInventory_NOUserGetList(ButtonEdit1.EditValue, DateEdit1.Text, DateEdit2.Text).Count = 0 Then
                    MsgBox("�b�����Ƭ���,�Э��s�]�m�d�߱���!")
                    Exit Sub
                End If

                ds.Tables.Clear()

                ltc1.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))
                ltc2.CollToDataSet(ds, "WareInventory", wic.WareInventory_NOUserGetList(ButtonEdit1.EditValue, DateEdit1.Text, DateEdit2.Text))
                PreviewRPT1(ds, "rptWareInventory_NOUser", "�b�����Ƥ@����", DateEdit1.Text, DateEdit2.Text, True, True)

                ltc = Nothing
                ltc2 = Nothing

                Me.Close()
            End If

            ''�ɥXexcel  2012/8/9
            If RadioButton2.Checked = True Then
                ExpotExcel()
            End If

            '@ 2012/5/2 �K�[ �t�ƥX�J�w�O����
        ElseIf strWH = "500811" Then        '�t�ƥX�J�w�O����
            Dim ds As New DataSet
            Dim ltc As New CollectionToDataSet

            Dim wh As New WareHouseDetailControl

            If DateEdit1.Text = "" Then
                tempValue3 = Nothing
            Else
                tempValue3 = DateEdit1.Text
            End If
            If DateEdit2.Text = "" Then
                tempValue4 = Nothing
            Else
                tempValue4 = DateEdit2.Text
            End If

            If wh.WareINandOutQty_V_GetList(Nothing, ButtonEdit1.Text, tempValue3, tempValue4).Count = 0 Then
                MsgBox("�d�ߤ��e����,�Э��s�]�m�d�߱���!")
                Exit Sub
            End If

            ds.Tables.Clear()


            ltc.CollToDataSet(ds, "WareInventorySearch", wh.WareINandOutQty_V_GetList(Nothing, ButtonEdit1.Text, tempValue3, tempValue4))

            PreviewRPT(ds, "rptWareInAndOutQty", "�t�ƥX�J�w�O����", True, True)
            ltc = Nothing

            Me.Close()
        ElseIf strWH = "500208" Then
            ''-------------------------------------------------------------  �X�w����`�ƾ�

            WareOutExpotExcel()

        End If

    End Sub

    Private Sub ButtonEdit1_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ButtonEdit1.ButtonClick
        frmWareHouseSelect.SelectWareID = ""
        tempValue3 = "500401"
        frmWareHouseSelect.SelectWareID = ""
        frmWareHouseSelect.StartPosition = FormStartPosition.Manual
        frmWareHouseSelect.Left = Me.Left + ButtonEdit1.Left + 2
        frmWareHouseSelect.Top = Me.Top + ButtonEdit1.Top + ButtonEdit1.Height + 21
        frmWareHouseSelect.ShowDialog()
        If frmWareHouseSelect.SelectWareID = "" Then
        Else
            ButtonEdit1.Text = frmWareHouseSelect.SelectWareID
        End If

    End Sub



    Sub ExpotExcel()
        Dim strCompany, CompanyName As String
        strCompany = Mid(strInDPT_ID, 1, 4)   '��o�n���̩��ݤ��qID,�H��^���q�W�١ALOGO
        ''-----------------------------------------------------------------
        Dim mcCompany As New LFERP.DataSetting.CompanyControler
        Dim mLCompany As New List(Of LFERP.DataSetting.CompanyInfo)
        mLCompany = mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing)
        CompanyName = mLCompany(0).CO_ChsName
        ''----------------------------------------------------------------------------------

        Dim wic As New WareInventory.WareInventoryMTController
        Dim wl As New List(Of WareInventory.WareInventoryInfo)
        Dim CountJS As Integer
        wl = wic.WareInventory_NOUserGetList(ButtonEdit1.EditValue, DateEdit1.Text, DateEdit2.Text)
        CountJS = wl.Count

        If CountJS <= 0 Then
            MsgBox("�b�����Ƭ���,�Э��s�]�m�d�߱���!")
            Exit Sub
        End If

        Dim i As Integer
        Dim exapp As New Excel.Application   '�w�q�@��excel��H
        Dim exbook As Excel.Workbook     '�w�q�@��excel����
        Dim exsheet As Excel.Worksheet   '�w�q�@��excel�u�@��

        exapp = CreateObject("Excel.Application")   '�ͦ��@��excel��H
        exbook = exapp.Workbooks.Open(Application.StartupPath & "\ModuleFile\MaterialNOUse.xls")
        exsheet = exbook.Worksheets(1)

        exsheet.Cells(1, 1) = CompanyName
        exsheet.Cells(2, 1) = "�b�����Ƥ@����" & DateEdit1.EditValue & "~" & DateEdit2.EditValue

        'exsheet.Cells(3, 1) = "�ܮw�W��:" & wl(0).WH_Name & "  " & wl(0).WH_SName
        'exsheet.Cells(3, 6) = "�ܮw�s��:" & wl(0).WH_ID
        ProgressBar1.Visible = True
        ProgressBar1.Maximum = CountJS

        For i = 0 To CountJS - 1
            exsheet.Cells(5 + i, 1) = wl(i).Type3Name
            exsheet.Cells(5 + i, 2) = wl(i).M_Code
            exsheet.Cells(5 + i, 3) = wl(i).M_Name
            exsheet.Cells(5 + i, 4) = wl(i).M_Gauge
            exsheet.Cells(5 + i, 5) = wl(i).WI_Qty
            exsheet.Cells(5 + i, 6) = wl(i).M_Unit
            exsheet.Cells(5 + i, 7) = wl(i).DateIn
            exsheet.Cells(5 + i, 8) = wl(i).DateOut

            exbook.ActiveSheet.range(exsheet.Cells(5 + i, 1), exsheet.Cells(5 + i, 8)).borders(1).linestyle = 1
            exbook.ActiveSheet.range(exsheet.Cells(5 + i, 1), exsheet.Cells(5 + i, 8)).borders(2).linestyle = 1
            exbook.ActiveSheet.range(exsheet.Cells(5 + i, 1), exsheet.Cells(5 + i, 8)).borders(3).linestyle = 1
            exbook.ActiveSheet.range(exsheet.Cells(5 + i, 1), exsheet.Cells(5 + i, 8)).borders(4).linestyle = 1

            ProgressBar1.Value = i
        Next


        exsheet.Cells(5 + CountJS + 1, 1) = "�`:   1.�J�w�]�A�J�w�@�~.�ռ��J�w.����J�w.��X�J�w"
        exsheet.Cells(5 + CountJS + 2, 1) = "      2.�X�w�]�A�X�w�@�~.�ռ��X�w.����X�w.��X�X�w"

        ProgressBar1.Visible = False

        Dim tempName As String

        SaveFileDialog1.InitialDirectory = "c:\"
        SaveFileDialog1.Filter = "txt files (*.xls)|*.xls"
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            tempName = SaveFileDialog1.FileName
        Else
            Exit Sub
        End If

        exsheet.SaveAs(tempName)

        exapp.Quit()
        exsheet = Nothing
        exbook = Nothing
        exapp = Nothing

        MsgBox("�ɥX���\!")

        Me.Close()
    End Sub

    Sub WareOutExpotExcel()

        If Me.ButtonEdit1.EditValue = "" Then
            MsgBox("�ܮw������")
            Exit Sub
        End If

        If Me.DateEdit1.EditValue Is Nothing Or Me.DateEdit2.EditValue Is Nothing Then
            MsgBox("�ɶ�������")
            Exit Sub
        End If

        Dim wic As New WareOut.WareOutController
        Dim wl As New List(Of WareOut.WareOutInfo)
        Dim CountJS As Integer
        wl = wic.WareOut_Getlist4(ButtonEdit1.EditValue, DateEdit1.EditValue, DateEdit2.EditValue)

        CountJS = wl.Count

        If CountJS <= 0 Then
            MsgBox("�L�X�w�O���s�b,���ˬd!")
            Exit Sub
        End If

        Dim i As Integer
        Dim exapp As New Excel.Application   '�w�q�@��excel��H
        Dim exbook As Excel.Workbook     '�w�q�@��excel����
        Dim exsheet As Excel.Worksheet   '�w�q�@��excel�u�@��

        exapp = CreateObject("Excel.Application")   '�ͦ��@��excel��H
        exbook = exapp.Workbooks.Open(Application.StartupPath & "\ModuleFile\module.xls")
        exsheet = exbook.Worksheets(1)

        ProgressBar1.Visible = True
        ProgressBar1.Maximum = CountJS

        exsheet.Cells(1, 1) = "��Ƽt�O"
        exsheet.Cells(1, 2) = "��Ƴ���"
        exsheet.Cells(1, 3) = "���ƽs�X"
        exsheet.Cells(1, 4) = "���ƦW��"
        exsheet.Cells(1, 5) = "�W��"
        exsheet.Cells(1, 6) = "�X�w�`��"
        exsheet.Cells(1, 7) = "���O"
        exsheet.Cells(1, 8) = "���"
        exsheet.Cells(1, 9) = "�`���B"
        exsheet.Cells(1, 10) = "�`���B(�H����)"

        For i = 0 To CountJS - 1
            exsheet.Cells(2 + i, 1) = wl(i).DPT_PName
            exsheet.Cells(2 + i, 2) = wl(i).DPT_Name
            exsheet.Cells(2 + i, 3) = wl(i).M_Code
            exsheet.Cells(2 + i, 4) = wl(i).M_Name
            exsheet.Cells(2 + i, 5) = wl(i).M_Gauge
            exsheet.Cells(2 + i, 6) = wl(i).WO_Qty
            exsheet.Cells(2 + i, 7) = wl(i).M_Currency
            exsheet.Cells(2 + i, 8) = CStr(FormatNumber(wl(i).M_Price, 4, TriState.True))
            exsheet.Cells(2 + i, 9) = CStr(FormatNumber(Val(wl(i).M_Price) * Val(wl(i).WO_Qty), 4, TriState.True))
            exsheet.Cells(2 + i, 10) = Format(wl(i).SumHKD, "0.0000")
            ProgressBar1.Value = i
        Next

        exbook.ActiveSheet.range(exsheet.Cells(1, 1), exsheet.Cells(CountJS + 1, 10)).borders(1).linestyle = 1
        exbook.ActiveSheet.range(exsheet.Cells(1, 1), exsheet.Cells(CountJS + 1, 10)).borders(2).linestyle = 1
        exbook.ActiveSheet.range(exsheet.Cells(1, 1), exsheet.Cells(CountJS + 1, 10)).borders(3).linestyle = 1
        exbook.ActiveSheet.range(exsheet.Cells(1, 1), exsheet.Cells(CountJS + 1, 10)).borders(4).linestyle = 1

        ProgressBar1.Visible = False

        Dim tempName As String

        SaveFileDialog1.InitialDirectory = "c:\"
        SaveFileDialog1.Filter = "txt files (*.xls)|*.xls"
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            tempName = SaveFileDialog1.FileName
            exsheet.SaveAs(tempName)
        Else
            Exit Sub
        End If


        exapp.Quit()
        exsheet = Nothing
        exbook = Nothing
        exapp = Nothing

        MsgBox("�ɥX���\!")
    End Sub

End Class