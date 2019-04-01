Imports LFERP.Library.ProductionField
Imports LFERP.Library.Product
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.Production.ProductionFieldDaySummary
Imports LFERP.DataSetting
Imports LFERP.Library.ProductionSchedule
Imports LFERP.Library.ProductionKaiLiao

Public Class frmProductionFieldSelect

    Dim ds As New DataSet
    Public isClickcmdSave As Boolean

    Sub LoadProductNo()  '���~�s��

        Dim mc As New ProcessMainControl
        PM_M_Code.Properties.DisplayMember = "PM_M_Code"
        PM_M_Code.Properties.ValueMember = "PM_M_Code"
        PM_M_Code.Properties.DataSource = mc.ProcessMain_GetList3(Nothing, Nothing)

    End Sub

    Private Sub frmProductionFieldSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()

        LoadProductNo()
        ComboBoxEdit1.EditValue = "�Ͳ��[�u"
        DateEdit1.Text = Format(Now, "yyyy/MM/dd")
        DateEdit2.Text = Format(Now, "yyyy/MM/dd")
        'ComboBoxEdit2.EditValue = "����" 'mao2012/3/5
        ComboBoxEdit3.EditValue = "����"

        isClickcmdSave = False

        Me.ComboBoxEdit1.EditValue = tempValue13

    End Sub

    Sub CreateTable()
        ds.Tables.Clear()

        With ds.Tables.Add("ProductType")
            .Columns.Add("PM_Type", GetType(String))
        End With
        GluType.Properties.ValueMember = "PM_Type"
        GluType.Properties.DisplayMember = "PM_Type"
        GluType.Properties.DataSource = ds.Tables("ProductType")


        With ds.Tables.Add("Process")
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("PS_Name", GetType(String))
        End With

        GluEdit1.Properties.ValueMember = "PS_NO"
        GluEdit1.Properties.DisplayMember = "PS_Name"
        GluEdit1.Properties.DataSource = ds.Tables("Process")

    End Sub

    Private Sub PM_M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PM_M_Code.EditValueChanged

        On Error Resume Next

        Dim ppc As New ProcessMainControl
        Dim ppi As List(Of ProcessMainInfo)

        ds.Tables("ProductType").Clear()
        ds.Tables("Process").Clear()

        If PM_M_Code.Text.Trim = "" Then
            Exit Sub
        End If

        'If ComboBoxEdit1.EditValue = "" Then
        '    MsgBox("�u���������ର��!")
        '    Exit Sub
        'End If
        ppi = ppc.ProcessMain_GetList2(ComboBoxEdit1.EditValue, PM_M_Code.EditValue)
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

        GluType.Text = ""
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        '@ 2012/8/28 �K�[
        If txtFP_NO.Text.Trim = "" Then       '���o�渹
            tempValue = Nothing
        Else
            tempValue = txtFP_NO.Text.Trim
        End If


        If ComboBoxEdit1.EditValue = "" Then       '�u������
            tempValue2 = Nothing
        Else
            tempValue2 = ComboBoxEdit1.EditValue
        End If

        If PM_M_Code.EditValue = "" Then         '���~�s��
            tempValue3 = Nothing
        Else
            tempValue3 = PM_M_Code.EditValue
        End If

        If GluType.EditValue = "" Then                '���@�@��
            tempValue4 = Nothing
        Else
            tempValue4 = GluType.EditValue
        End If

        If GluEdit1.EditValue = "" Then           '�u�ǦW��
            tempValue12 = Nothing
        Else
            tempValue12 = GluEdit1.EditValue
        End If

        If ComboBoxEdit2.EditValue = "" Then              '���o����
            tempValue7 = Nothing
        Else
            tempValue7 = ComboBoxEdit2.EditValue
        End If

        If DateEdit1.Text = "" Then                     '�_�l���
            tempValue5 = Nothing
        Else
            tempValue5 = DateEdit1.Text
        End If

        If DateEdit2.Text = "" Then                     '�������
            tempValue6 = Nothing
        Else
            tempValue6 = DateEdit2.Text
        End If

        If ComboBoxEdit3.EditValue = "����" Then              '�f�֪��A
            tempValue8 = Nothing
        ElseIf ComboBoxEdit3.EditValue = "�w�f��" Then
            tempValue8 = True
        Else
            tempValue8 = False
        End If

        If txtCardID.Text = "" Then                  '��d�H��
            tempValue10 = Nothing
        Else
            tempValue10 = txtCardID.Text.Trim
        End If

        If cboFP_OutType.Text = "" Then       '�~�o����
            tempValue11 = Nothing
        Else
            tempValue11 = cboFP_OutType.Text.Trim
        End If

        isClickcmdSave = True
        Me.Close()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
    '���Ʀ��o�O��--��e����d��(�����w�h�q�{���@�ӬP��)
    '@ 2012/1/6 �ק�P�_�����T�ɡA����������o�J�I
    Private Sub cmdInOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInOut.Click
        If ComboBoxEdit1.EditValue = "" Then
            MsgBox("�u���������ର��", 64, "����")
            ComboBoxEdit1.Select()
            Exit Sub
        End If
        If PM_M_Code.EditValue = "" Then
            MsgBox("���~�s�����ର��!", 64, "����")
            PM_M_Code.Select()
            Exit Sub
        End If
        If GluType.EditValue = "" Then
            MsgBox("���~�������ର��!", 64, "����")
            GluType.Select()
            Exit Sub
        End If
        If GluEdit1.EditValue = "" Then
            MsgBox("�u�ǦW�٤��ର��!", 64, "����")
            GluEdit1.Select()
            Exit Sub
        End If
        If ComboBoxEdit2.EditValue = "" Then
            MsgBox("���o�������ର��!", 64, "����")
            ComboBoxEdit2.Select()
            Exit Sub
        End If

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet

        Dim pfc As New ProductionFieldControl  '���Ʀ��o�H��
        Dim pdc As New ProductionFieldDaySummaryControl  '�C�ѰO���H��


        ds.Tables.Clear()
        Dim strA, strB, strC, strD, strE, strF, strG, strH, strCheck As String

        If txtCardID.Text = "" Then '2012-5-25
            strG = Nothing
        Else
            strG = txtCardID.Text
        End If

        If cboFP_OutType.Text = "" Then '2012-6-11
            strH = Nothing
        Else
            strH = cboFP_OutType.Text
        End If

        strA = ComboBoxEdit1.EditValue
        strB = PM_M_Code.EditValue
        strC = GluType.EditValue

        Dim upi As List(Of UserPowerInfo)
        Dim upc As New UserPowerControl
        upi = upc.UserPower_GetList(InUserID, Nothing, Nothing, Nothing)

        Dim strDep As String


        If upi.Count = 0 Then
            Exit Sub
        Else

            If upi(0).UserRank = "�޲z" Then
                strDep = Nothing
            ElseIf upi(0).UserRank = "�Ͳ���" Then

                Dim pc As New LFERP.Library.PieceProcess.PersonnelControl
                Dim pci As List(Of LFERP.Library.PieceProcess.PersonnelInfo)

                pci = pc.FacBriSearch_GetList(Nothing, Nothing, upi(0).DepID, Nothing)
                If pci.Count = 0 Then
                    MsgBox("���Τ���ݳ������ݩ�p����!")
                    Exit Sub
                Else
                    strDep = pci(0).FacID
                End If
            Else
                strDep = upi(0).DepID

            End If

        End If
        strD = strDep
        strE = GluEdit1.EditValue
        strF = ComboBoxEdit2.EditValue

        If ComboBoxEdit3.EditValue = "����" Then              '�f�֪��A
            strCheck = Nothing
        ElseIf ComboBoxEdit3.EditValue = "�w�f��" Then
            strCheck = True
        Else
            strCheck = False
        End If


        If DateEdit1.Text = "" Or DateEdit1.Text = "" Then
            '�q�{�@�ӬP��

            If pfc.ProductionField_GetList2(Nothing, strA, strB, strC, strE, strF, Nothing, strD, Nothing, Nothing, strCheck, Format(Now.AddDays(-7), "yyyy/MM/dd"), Format(Now, "yyyy/MM/dd"), Nothing, strH, "True").Count = 0 Then
                MsgBox("��e����d�򤺵L���u�Ǫ����Ʀ��o�O��!")
                Exit Sub
            End If

            ltc.CollToDataSet(ds, "ProductionField", pfc.ProductionField_GetList2(Nothing, strA, strB, strC, strE, strF, Nothing, strD, Nothing, Nothing, strCheck, Format(Now.AddDays(-7), "yyyy/MM/dd"), Format(Now, "yyyy/MM/dd"), strG, strH, "True"))
            ltc1.CollToDataSet(ds, "ProductionFieldDaySummary", pdc.ProductionFieldDaySummary_GetList(strA, strB, strC, strE, strD, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), Format(Now, "yyyy/MM/dd")))

            PreviewRPT(ds, "rptProductionFieldSR", "�����ԲӦ��o�O��", True, True)

            ltc = Nothing
            ltc1 = Nothing

        Else
            '���w����d��
            Dim strDate1, strDate2 As Date

            strDate1 = DateTime.Parse(DateEdit1.Text)
            strDate2 = DateTime.Parse(DateEdit2.Text)

            Dim ts As System.TimeSpan
            ts = strDate2 - strDate1

            If pfc.ProductionField_GetList2(Nothing, strA, strB, strC, strE, strF, Nothing, strD, Nothing, Nothing, strCheck, DateEdit1.Text, DateEdit2.Text, Nothing, strH, "True").Count = 0 Then
                MsgBox("��e����d�򤺵L���u�Ǫ����Ʀ��o�O��!")
                Exit Sub
            End If

            ltc.CollToDataSet(ds, "ProductionField", pfc.ProductionField_GetList2(Nothing, strA, strB, strC, strE, strF, Nothing, strD, Nothing, Nothing, strCheck, DateEdit1.Text, DateEdit2.Text, strG, strH, "True"))
            ltc1.CollToDataSet(ds, "ProductionFieldDaySummary", pdc.ProductionFieldDaySummary_GetList(strA, strB, strC, strE, strD, Nothing, DateEdit1.Text, DateEdit2.Text))

            PreviewRPT(ds, "rptProductionFieldSR", "�����ԲӦ��o�O��", True, True)

            ltc = Nothing
            ltc1 = Nothing

        End If
        Me.Close()
    End Sub
    '�Ͳ��y�{��--��e����d��(�����w�h�q�{���@�ӬP��)
    '@ 2012/1/6 �ק�P�_�����T�ɡA����������o�J�I
    Private Sub cmdProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProcess.Click
        If ComboBoxEdit1.EditValue = "" Then
            MsgBox("�u���������ର��", 64, "����")
            ComboBoxEdit1.Select()
            Exit Sub
        End If
        If PM_M_Code.EditValue = "" Then
            MsgBox("���~�s�����ର��!", 64, "����")
            PM_M_Code.Select()
            Exit Sub
        End If
        If GluType.EditValue = "" Then
            MsgBox("���~�������ର��!", 64, "����")
            GluType.Select()
            Exit Sub
        End If
        If ComboBoxEdit3.Text <> "�w�f��" Then
            ComboBoxEdit3.Text = "�w�f��"
            MsgBox("�Ͳ��y�{��u����`�w�f�֫H��!", 64, "����")
        End If


        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet
        Dim ltc3 As New CollectionToDataSet
        Dim ltc4 As New CollectionToDataSet
        Dim ltc5 As New CollectionToDataSet

        Dim psc As New ProductionScheduleControl  '�Ͳ��p��
        Dim pkc As New ProductionKaiLiaoControl   '�}�ƫH��
        Dim pfc As New ProductionFieldControl  '���Ʀ��o�H��
        Dim pdc As New ProductionFieldDaySummaryControl  '�C�ѰO���H��
        Dim pmc As New LFERP.Library.ProductionMaterial.ProductionMaterialControl


        ds.Tables.Clear()
        Dim strA, strB, strC, strE As String
        Dim strBeginDate, strEndDate, strFacName, strFacID As String     '@ 2012/8/28 �K�[

        strFacName = Nothing
        strFacID = Nothing



        strA = ComboBoxEdit1.EditValue
        strB = PM_M_Code.EditValue
        strC = GluType.EditValue

        If cboFP_OutType.Text = "" Then '2012-6-11
            strE = Nothing
        Else
            strE = cboFP_OutType.Text
        End If

        Dim upi As List(Of UserPowerInfo)
        Dim upc As New UserPowerControl
        upi = upc.UserPower_GetList(InUserID, Nothing, Nothing, Nothing)

        If upi.Count = 0 Then
            Exit Sub
        Else
            Dim pc As New LFERP.Library.PieceProcess.PersonnelControl
            Dim pci As List(Of LFERP.Library.PieceProcess.PersonnelInfo)

            pci = pc.FacBriSearch_GetList(Nothing, Nothing, upi(0).DepID, Nothing)        '@ 2012/8/28 �ק�
            If pci.Count > 0 Then
                strFacName = pci(0).FacName
                strFacID = pci(0).FacID
            End If
        End If

        '@ 2012/8/28 �ק�
        If DateEdit1.Text = "" Or DateEdit1.Text = "" Then   '�q�{�@�ӬP��
            strBeginDate = DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd")))
            strEndDate = Format(Now, "yyyy/MM/dd")
        Else
            strBeginDate = DateEdit1.Text
            strEndDate = DateEdit2.Text
        End If

        '---------------------------------------------------------------------------------------�ɤJ�{�ɤ����
        Dim pdi As New ProductionFieldDaySummaryInfo

        pdi.Str1 = strBeginDate
        pdi.Str2 = strEndDate

        pdc.Temp3_Add(pdi)
        '---------------------------------------------------------------------------------------

        If psc.ProductionSchedule_GetList2(Nothing, strA, strB, strC, strFacID, CDate(strBeginDate), CDate(strEndDate)).Count = 0 Then
            MsgBox("��e����d�򤺵L���Ͳ��p���H��!")
            Exit Sub
        End If
        If pfc.ProductionField_GetList1(Nothing, strA, strB, strC, Nothing, Nothing, Nothing, Nothing, Nothing, True, True, CDate(strBeginDate), CDate(strEndDate), Nothing, strE).Count = 0 Then
            MsgBox("��e����d�򤺦��Ͳ��p�����L���Ʀ��o�O��!")
            Exit Sub
        End If

        '@ 2012/8/28 �K�[
        If pdc.ProductionFieldDaySummary_GetList(strA, strB, strC, Nothing, Nothing, strFacName, CDate(strBeginDate), CDate(strEndDate)).Count = 0 Then
            MsgBox("��e����d��,�L��Ͳ��O�����`�H��!")
            Exit Sub
        End If

        ltc.CollToDataSet(ds, "ProductionSchedule", psc.ProductionSchedule_GetList2(Nothing, strA, strB, strC, strFacID, CDate(strBeginDate), CDate(strEndDate)))

        '@ 2012/8/28 �K�[
        If pmc.ProductionMaterialDayDetail_GetList(strA, strB, strC, Nothing, CDate(strBeginDate)).Count = 0 Then
            ltc1.CollToDataSet(ds, "ProductionMaterialDayDetail", pmc.NothingNew)
        Else
            ltc1.CollToDataSet(ds, "ProductionMaterialDayDetail", pmc.ProductionMaterialDayDetail_GetList(strA, strB, strC, Nothing, CDate(strBeginDate)))
        End If

        '@ 2012/8/28 �K�[
        If pkc.vwProductionKaiLiao_GetList(strA, strB, strC, Nothing, CDate(strBeginDate), CDate(strEndDate)).Count <= 0 Then
            ltc5.CollToDataSet(ds, "vwProductionKaiLiao", pkc.NothingNew)
        Else
            ltc5.CollToDataSet(ds, "vwProductionKaiLiao", pkc.vwProductionKaiLiao_GetList(strA, strB, strC, Nothing, CDate(strBeginDate), CDate(strEndDate)))
        End If

        ltc2.CollToDataSet(ds, "ProductionField", pfc.ProductionField_GetList1(Nothing, strA, strB, strC, Nothing, Nothing, Nothing, Nothing, Nothing, True, True, CDate(strBeginDate), CDate(strEndDate), Nothing, strE))

        ltc3.CollToDataSet(ds, "ProductionFieldDaySummary", pdc.ProductionFieldDaySummary_GetList(strA, strB, strC, Nothing, Nothing, strFacName, CDate(strBeginDate), CDate(strEndDate)))
        ltc4.CollToDataSet(ds, "Temp3", pdc.Temp3_GetList(Nothing, Nothing))

        PreviewRPT(ds, "rptProductionProcess", "�u���Ͳ��y�{��", True, True)

        ltc = Nothing
        ltc1 = Nothing
        ltc2 = Nothing
        ltc3 = Nothing
        ltc4 = Nothing
        ltc5 = Nothing

        Me.Close()
    End Sub
    '�Ͳ��i�ת�--��e����d��(�����w�h�q�{���@�ӬP��)
    Private Sub cmdComplete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdComplete.Click

    End Sub

    Private Sub GluType_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GluType.EditValueChanged
        On Error Resume Next

        Dim pc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)

        ds.Tables("Process").Clear()

        If GluType.Text.Trim = "" Then
            Exit Sub
        End If

        pci = pc.ProcessMain_GetList(Nothing, PM_M_Code.EditValue, ComboBoxEdit1.EditValue, GluType.EditValue, Nothing, True)

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

    Private Sub PM_M_Code_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles PM_M_Code.KeyDown, ComboBoxEdit1.KeyDown, GluType.KeyDown, ComboBoxEdit2.KeyDown, GluEdit1.KeyDown, ComboBoxEdit3.KeyDown, cboFP_OutType.KeyDown
        If e.KeyCode = Keys.Space Then
            sender.showpopup()
        End If
    End Sub

    '@ 2012/8/11 �K�[
    Private Sub ComboBoxEdit1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxEdit1.SelectedIndexChanged
        PM_M_Code_EditValueChanged(Nothing, Nothing)
    End Sub

    Private Sub txtCardID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCardID.EditValueChanged

    End Sub
End Class