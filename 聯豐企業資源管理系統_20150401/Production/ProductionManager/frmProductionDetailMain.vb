Imports LFERP.DataSetting
Imports LFERP.SystemManager
Imports LFERP.Library.ProductionField
Imports LFERP.Library.Production.ProductionFieldDaySummary
Imports LFERP.Library.ProductionSchedule
Imports LFERP.Library.ProductionKaiLiao

Public Class frmProductionDetailMain

    Dim ds As New DataSet

    Dim strType As String
    Dim strDate As String
    Dim strDate1 As String
    Dim strDate2 As String
    Dim IntDay As Integer
    Dim strProcess As String

    Sub LoadFacName()
        Dim fc As New FacControler
        Dim fi As List(Of FacInfo)

        Dim row1 As DataRow
        row1 = ds.Tables("Fac").NewRow
        row1("FacID") = "*"
        row1("FacName") = "����"
        ds.Tables("Fac").Rows.Add(row1)

        fi = fc.GetFacListB(Nothing, Nothing)
        If fi.Count = 0 Then
        Else
            Dim i As Integer
            For i = 0 To fi.Count - 1
                Dim row As DataRow
                row = ds.Tables("Fac").NewRow
                row("FacID") = fi(i).FacID
                row("FacName") = fi(i).FacName
                ds.Tables("Fac").Rows.Add(row)
            Next
        End If

        'GluDep.Properties.DataSource = fc.GetFacList(Nothing, "�Ͳ���")
        'GluDep.Properties.DisplayMember = "FacName"
        'GluDep.Properties.ValueMember = "FacID"

    End Sub

    Private Sub frmProductionDetailMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '�q�{7�ѰO��
        strType = "����d��"
        CreateTable()

        LoadFacName()

        Dim ui As List(Of UserPowerInfo)
        Dim uc As New UserPowerControl
        ui = uc.UserPower_GetList(InUserID, Nothing, Nothing, Nothing)
        If ui.Count = 0 Then
            Exit Sub
        Else
            GluDep.EditValue = Mid(ui(0).DepID, 1, 1)  '�o��Ͳ���
        End If

        DateEdit1.Text = DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd")))
        DateEdit2.Text = CDate(Format(Now, "yyyy/MM/dd"))

        strDate1 = DateEdit1.Text
        strDate2 = DateEdit2.Text



        If strType = "���w�Ѽ�" Then
            If GluDep.EditValue = "*" Then
                LoadDataAll("�Ͳ��[�u", DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate)
                Grid1.Visible = False
                Grid4.Visible = True
            Else
                LoadData("�Ͳ��[�u", GluDep.EditValue, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate)
                Grid1.Visible = True
                Grid4.Visible = False
            End If

        ElseIf strType = "����d��" Then

            If GluDep.EditValue = "*" Then
                LoadDataAll("�Ͳ��[�u", strDate1, strDate2)
                Grid1.Visible = False
                Grid4.Visible = True
            Else
                LoadData("�Ͳ��[�u", GluDep.EditValue, strDate1, strDate2)
                Grid1.Visible = True
                Grid4.Visible = False
            End If
        Else

            DateEdit1.Text = DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd")))
            DateEdit2.Text = CDate(Format(Now, "yyyy/MM/dd"))

            If GluDep.EditValue = "*" Then
                Grid1.Visible = False
                Grid4.Visible = True
                LoadDataAll("�Ͳ��[�u", DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))
            Else
                LoadData("�Ͳ��[�u", GluDep.EditValue, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))
                Grid1.Visible = True
                Grid4.Visible = False
            End If

        End If

        strProcess = "�Ͳ��[�u"
        Grid2.DataSource = Nothing

    End Sub

    Sub CreateTable()
        ds.Tables.Clear()

        With ds.Tables.Add("ProductionDetail")

            .Columns.Add("Pro_Type", GetType(String))
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("PM_Type", GetType(String))
            .Columns.Add("PS_ActualNumber", GetType(Integer))
            .Columns.Add("PS_Dep", GetType(String))
            .Columns.Add("FacName", GetType(String))

        End With

        Grid1.DataSource = ds.Tables("ProductionDetail")

        With ds.Tables.Add("Fac")
            .Columns.Add("FacID", GetType(String))
            .Columns.Add("FacName", GetType(String))

        End With

        GluDep.Properties.DataSource = ds.Tables("Fac")
        GluDep.Properties.DisplayMember = "FacName"
        GluDep.Properties.ValueMember = "FacID"


        With ds.Tables.Add("ProductionDetailAll")

            .Columns.Add("Pro_Type", GetType(String))
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("PM_Type", GetType(String))
            .Columns.Add("PS_ActualNumber", GetType(Integer))

        End With

        Grid4.DataSource = ds.Tables("ProductionDetailAll")

    End Sub

    Sub LoadData(ByVal Pro_Type As String, ByVal strFacID As String, ByVal startDate As String, ByVal endDate As String)
        Dim psc As New ProductionScheduleControl
        Dim psi As List(Of ProductionScheduleInfo)

        psi = psc.ProductionSchedule_GetList2(Nothing, Pro_Type, Nothing, Nothing, strFacID, startDate, endDate)

        ds.Tables("ProductionDetail").Clear()

        If psi.Count = 0 Then
            Exit Sub
        Else
            Dim i As Integer
            For i = 0 To psi.Count - 1

                Dim row As DataRow

                row = ds.Tables("ProductionDetail").NewRow

                row("Pro_Type") = psi(i).Pro_Type
                row("PM_M_Code") = psi(i).PM_M_Code
                row("PM_Type") = psi(i).PM_Type
                row("PS_ActualNumber") = psi(i).PS_ActualNumber
                row("PS_Dep") = psi(i).PS_Dep
                row("FacName") = psi(i).FacName

                ds.Tables("ProductionDetail").Rows.Add(row)

            Next
        End If

    End Sub

    '�w��ɥX�����Ͳ��H��(�s�b���P�Ͳ����Ͳ��ۦP���~--�u�Ǫ��p)
    Sub LoadDataAll(ByVal Pro_Type As String, ByVal startDate As String, ByVal endDate As String)
        Dim psc As New ProductionScheduleControl
        Dim psi As List(Of ProductionScheduleInfo)

        psi = psc.ProductionSchedule_GetList3(Pro_Type, Nothing, Nothing, startDate, endDate)


        ds.Tables("ProductionDetailAll").Clear()

        If psi.Count = 0 Then
            Exit Sub
        Else
            Dim i As Integer
            For i = 0 To psi.Count - 1

                Dim row As DataRow

                row = ds.Tables("ProductionDetailAll").NewRow

                row("Pro_Type") = psi(i).Pro_Type
                row("PM_M_Code") = psi(i).PM_M_Code
                row("PM_Type") = psi(i).PM_Type
                row("PS_ActualNumber") = psi(i).PS_ActualNumber

                ds.Tables("ProductionDetailAll").Rows.Add(row)

            Next
        End If

    End Sub

    Private Sub cmdPeiBu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPeiBu.Click
        If GluDep.EditValue = "*" Then

            If strType = "���w�Ѽ�" Then

                LoadDataAll(strProcess, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate)

            ElseIf strType = "����d��" Then

                LoadDataAll(strProcess, strDate1, strDate2)
            Else

                LoadDataAll(strProcess, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))

            End If
            Grid1.Visible = False
            Grid4.Visible = True
            strProcess = "�F���[�u"
            Grid2.DataSource = Nothing
        Else
            If strType = "���w�Ѽ�" Then

                LoadData("�F���[�u", GluDep.EditValue, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate)
            ElseIf strType = "����d��" Then

                LoadData("�F���[�u", GluDep.EditValue, strDate1, strDate2)
            Else
                LoadData("�F���[�u", GluDep.EditValue, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))

            End If
            Grid1.Visible = True
            Grid4.Visible = False
            strProcess = "�F���[�u"
            Grid2.DataSource = Nothing
        End If
    End Sub

    Private Sub cmdShengChan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShengChan.Click
        If GluDep.EditValue = "*" Then

            If strType = "���w�Ѽ�" Then

                LoadDataAll(strProcess, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate)

            ElseIf strType = "����d��" Then

                LoadDataAll(strProcess, strDate1, strDate2)
            Else

                LoadDataAll(strProcess, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))

            End If
            Grid1.Visible = False
            Grid4.Visible = True
            strProcess = "�Ͳ��[�u"
            Grid2.DataSource = Nothing
        Else
            If strType = "���w�Ѽ�" Then

                LoadData("�Ͳ��[�u", GluDep.EditValue, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate)
            ElseIf strType = "����d��" Then

                LoadData("�Ͳ��[�u", GluDep.EditValue, strDate1, strDate2)
            Else
                LoadData("�Ͳ��[�u", GluDep.EditValue, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))

            End If
            Grid1.Visible = True
            Grid4.Visible = False
            strProcess = "�Ͳ��[�u"
            Grid2.DataSource = Nothing
        End If
    End Sub

    Private Sub cmdZhuangPei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdZhuangPei.Click
        If GluDep.EditValue = "*" Then

            If strType = "���w�Ѽ�" Then

                LoadDataAll(strProcess, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate)

            ElseIf strType = "����d��" Then

                LoadDataAll(strProcess, strDate1, strDate2)
            Else

                LoadDataAll(strProcess, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))

            End If
            Grid1.Visible = False
            Grid4.Visible = True
            strProcess = "�˰t�X�f"
            Grid2.DataSource = Nothing
        Else
            If strType = "���w�Ѽ�" Then

                LoadData("�˰t�X�f", GluDep.EditValue, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate)
            ElseIf strType = "����d��" Then

                LoadData("�˰t�X�f", GluDep.EditValue, strDate1, strDate2)
            Else
                LoadData("�˰t�X�f", GluDep.EditValue, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))

            End If
            Grid1.Visible = True
            Grid4.Visible = False
            strProcess = "�˰t�X�f"
            Grid2.DataSource = Nothing
        End If
    End Sub

    '�ܧ󳡪��H��
    Private Sub GluDep_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GluDep.EditValueChanged
        On Error Resume Next

        If GluDep.EditValue = "*" Then

            If strType = "���w�Ѽ�" Then

                LoadDataAll(strProcess, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate)

            ElseIf strType = "����d��" Then

                LoadDataAll(strProcess, strDate1, strDate2)
            Else

                LoadDataAll(strProcess, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))

            End If
            Grid1.Visible = False
            Grid4.Visible = True
        Else
            If strType = "���w�Ѽ�" Then

                LoadData(strProcess, GluDep.EditValue, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate)

            ElseIf strType = "����d��" Then

                LoadData(strProcess, GluDep.EditValue, strDate1, strDate2)
            Else

                LoadData(strProcess, GluDep.EditValue, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))

            End If
            Grid1.Visible = True
            Grid4.Visible = False
        End If
        
        Grid2.DataSource = Nothing
    End Sub

    '@ 2012/1/6 �K�[�P�_�A�����Ůɰ���
    Private Sub FieldMainSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FieldMainSelect.Click
        Dim frm As New frmProductionMainSelect
        frm.ShowDialog()

        If tempValue <> "" Or tempValue2 <> "" Or tempValue3 <> "" Then '�P�_�����Ůɰ���
            If frm.RadioButton1.Checked = True Then
                '���w�ѼƬd��(�Z���ѤѼ�)
                strType = tempValue3
                strDate = tempValue2
                IntDay = CInt(tempValue)
                DateEdit1.Text = DateAdd(DateInterval.Day, -IntDay, CDate(Format(CDate(strDate), "yyyy/MM/dd")))
                DateEdit2.Text = strDate

            ElseIf frm.RadioButton2.Checked = True Then
                strType = tempValue3
                strDate1 = tempValue
                strDate2 = tempValue2
                DateEdit1.Text = strDate1
                DateEdit2.Text = strDate2
            Else
                DateEdit1.Text = DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd")))
                DateEdit2.Text = CDate(Format(Now, "yyyy/MM/dd"))

                strDate1 = DateEdit1.Text
                strDate2 = DateEdit2.Text

                '�q�{7�ѰO��
                strType = "����d��"
            End If

            cmdShengChan_Click(Nothing, Nothing)  '�q�{��ܥͲ��[�u�u��

            tempValue = ""
            tempValue2 = ""
            tempValue3 = ""
        End If
    End Sub

    Private Sub FieldMainStatusPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FieldMainStatusPrint.Click

        If GridView1.RowCount = 0 Then Exit Sub

        Dim ds As New DataSet

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet
        Dim ltc3 As New CollectionToDataSet
        Dim ltc4 As New CollectionToDataSet
        Dim ltc5 As New CollectionToDataSet
        Dim ltc6 As New CollectionToDataSet

        Dim psc As New ProductionScheduleControl  '�Ͳ��p��
        Dim pkc As New ProductionKaiLiaoControl   '�}�ƫH��
        Dim pfc As New ProductionFieldControl  '���Ʀ��o�H��
        Dim pdc As New ProductionFieldDaySummaryControl  '�C�ѰO���H��
        Dim pmc As New LFERP.Library.ProductionMaterial.ProductionMaterialControl


        ds.Tables.Clear()
        Dim strA, strB, strC, strD, strF As String
        strA = GridView1.GetFocusedRowCellValue("Pro_Type").ToString
        strB = GridView1.GetFocusedRowCellValue("PM_M_Code").ToString
        strC = GridView1.GetFocusedRowCellValue("PM_Type").ToString
        strD = GridView1.GetFocusedRowCellValue("FacName").ToString
        strF = GridView1.GetFocusedRowCellValue("PS_Dep").ToString


        If strType = "���w�Ѽ�" Then
            '---------------------------------------------------------------------------------------�ɤJ�{�ɤ����
            Dim pdi As New ProductionFieldDaySummaryInfo


            pdi.Str1 = DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd")))
            pdi.Str2 = strDate

            pdc.Temp3_Add(pdi)

            If psc.ProductionSchedule_GetList2(Nothing, strA, strB, strC, strF, DateAdd(DateInterval.Day, -IntDay, CDate(strDate)), strDate).Count = 0 Then
                MsgBox("��e����d�򤺵L���Ͳ��p���H��!")
                Exit Sub
            End If
            'If pkc.ProductionKaiLiao_GetList(Nothing, strA, Nothing, Nothing, Nothing, strB, strC, Nothing, Nothing, True, Nothing).Count = 0 Then
            '    MsgBox("��e����d�򤺦��Ͳ��p���L�}�ƫH��!")
            '    Exit Sub
            'End If
            If pfc.ProductionField_GetList1(Nothing, strA, strB, strC, Nothing, Nothing, Nothing, Nothing, Nothing, True, True, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), Format(Now, "yyyy/MM/dd"), Nothing, Nothing).Count = 0 Then
                MsgBox("��e����d�򤺦��Ͳ��p�����L���Ʀ��o�O��!")
                Exit Sub
            End If

            If pdc.ProductionFieldDaySummary_GetList(strA, strB, strC, Nothing, Nothing, strD, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate).Count = 0 Then
                MsgBox("��e����d��,�L��Ͳ��O�����`�H��!")
                Exit Sub
            End If

            '---------------------------------------------------------------------------------------

            ltc.CollToDataSet(ds, "ProductionSchedule", psc.ProductionSchedule_GetList2(Nothing, strA, strB, strC, strF, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate))

            If pmc.ProductionMaterialDayDetail_GetList(strA, strB, strC, Nothing, strDate).Count = 0 Then
                ltc1.CollToDataSet(ds, "ProductionMaterialDayDetail", pmc.NothingNew)
            Else
                ltc1.CollToDataSet(ds, "ProductionMaterialDayDetail", pmc.ProductionMaterialDayDetail_GetList(strA, strB, strC, Nothing, strDate))
            End If

            If pkc.vwProductionKaiLiao_GetList(strA, strB, strC, Nothing, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate).Count <= 0 Then
                ltc4.CollToDataSet(ds, "vwProductionKaiLiao", pkc.NothingNew)
            Else
                ltc4.CollToDataSet(ds, "vwProductionKaiLiao", pkc.vwProductionKaiLiao_GetList(strA, strB, strC, Nothing, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate))
            End If

            ltc6.CollToDataSet(ds, "ProductionMaterialDayDetail1", pmc.NothingNew)

            'ltc1.CollToDataSet(ds, "ProductionKaiLiao", pkc.ProductionKaiLiao_GetList(Nothing, strA, Nothing, Nothing, Nothing, strB, strC, Nothing, Nothing, True, Nothing))
            ltc2.CollToDataSet(ds, "ProductionField", pfc.ProductionField_GetList1(Nothing, strA, strB, strC, Nothing, Nothing, Nothing, Nothing, Nothing, True, True, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate, Nothing, Nothing))
            ltc3.CollToDataSet(ds, "ProductionFieldDaySummary", pdc.ProductionFieldDaySummary_GetList(strA, strB, strC, Nothing, Nothing, strD, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate))

        ElseIf strType = "����d��" Then
            '---------------------------------------------------------------------------------------�ɤJ�{�ɤ����
            Dim pdi As New ProductionFieldDaySummaryInfo


            pdi.Str1 = strDate1
            pdi.Str2 = strDate2

            pdc.Temp3_Add(pdi)
            '---------------------------------------------------------------------------------------

            If psc.ProductionSchedule_GetList2(Nothing, strA, strB, strC, strF, strDate1, strDate2).Count = 0 Then
                MsgBox("��e����d�򤺵L���Ͳ��p���H��!")
                Exit Sub
            End If
            'If pkc.ProductionKaiLiao_GetList(Nothing, strA, Nothing, Nothing, Nothing, strB, strC, Nothing, Nothing, True, Nothing).Count = 0 Then
            '    MsgBox("��e����d�򤺦��Ͳ��p���L�}�ƫH��!")
            '    Exit Sub
            'End If
            If pfc.ProductionField_GetList1(Nothing, strA, strB, strC, Nothing, Nothing, Nothing, Nothing, Nothing, True, True, strDate1, strDate2, Nothing, Nothing).Count = 0 Then
                MsgBox("��e����d�򤺦��Ͳ��p�����L���Ʀ��o�O��!")
                Exit Sub
            End If
            If pdc.ProductionFieldDaySummary_GetList(strA, strB, strC, Nothing, Nothing, strD, strDate1, strDate2).Count = 0 Then
                MsgBox("��e����d��,�L��Ͳ��O�����`�H��!")
                Exit Sub
            End If

            ltc.CollToDataSet(ds, "ProductionSchedule", psc.ProductionSchedule_GetList2(Nothing, strA, strB, strC, strF, strDate1, strDate2))

            ''2012-6-13
            If pmc.ProductionMaterialDayDetail_GetList(strA, strB, strC, Nothing, strDate1).Count = 0 Then
                ltc1.CollToDataSet(ds, "ProductionMaterialDayDetail", pmc.NothingNew)
            Else
                ltc1.CollToDataSet(ds, "ProductionMaterialDayDetail", pmc.ProductionMaterialDayDetail_GetList(strA, strB, strC, Nothing, strDate1))
            End If

            If pmc.ProductionMaterialDayDetail_GetList(strA, strB, strC, Nothing, strDate2).Count = 0 Then
                ltc6.CollToDataSet(ds, "ProductionMaterialDayDetail1", pmc.NothingNew)
            Else
                ltc6.CollToDataSet(ds, "ProductionMaterialDayDetail1", pmc.ProductionMaterialDayDetail_GetList(strA, strB, strC, Nothing, strDate2))
            End If

            If pkc.vwProductionKaiLiao_GetList(strA, strB, strC, Nothing, strDate1, strDate2).Count <= 0 Then
                ltc4.CollToDataSet(ds, "vwProductionKaiLiao", pkc.NothingNew)
            Else
                ltc4.CollToDataSet(ds, "vwProductionKaiLiao", pkc.vwProductionKaiLiao_GetList(strA, strB, strC, Nothing, strDate1, strDate2))
            End If


            'ltc1.CollToDataSet(ds, "ProductionKaiLiao", pkc.ProductionKaiLiao_GetList(Nothing, strA, Nothing, Nothing, Nothing, strB, strC, Nothing, Nothing, True, Nothing))
            ltc2.CollToDataSet(ds, "ProductionField", pfc.ProductionField_GetList1(Nothing, strA, strB, strC, Nothing, Nothing, Nothing, Nothing, Nothing, True, True, strDate1, strDate2, Nothing, Nothing))
            ltc3.CollToDataSet(ds, "ProductionFieldDaySummary", pdc.ProductionFieldDaySummary_GetList(strA, strB, strC, Nothing, Nothing, strD, strDate1, strDate2))
        Else
            '---------------------------------------------------------------------------------------�ɤJ�{�ɤ����
            Dim pdi As New ProductionFieldDaySummaryInfo


            pdi.Str1 = DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd")))
            pdi.Str2 = Format(Now, "yyyy/MM/dd")

            pdc.Temp3_Add(pdi)
            '---------------------------------------------------------------------------------------
            If psc.ProductionSchedule_GetList2(Nothing, strA, strB, strC, strF, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd"))).Count = 0 Then
                MsgBox("��e����d�򤺵L���Ͳ��p���H��!")
                Exit Sub
            End If
            'If pkc.ProductionKaiLiao_GetList(Nothing, strA, Nothing, Nothing, Nothing, strB, strC, Nothing, Nothing, True, Nothing).Count = 0 Then
            '    MsgBox("��e����d�򤺦��Ͳ��p���L�}�ƫH��!")
            '    Exit Sub
            'End If
            If pfc.ProductionField_GetList1(Nothing, strA, strB, strC, Nothing, Nothing, Nothing, Nothing, Nothing, True, True, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), Format(Now, "yyyy/MM/dd"), Nothing, Nothing).Count = 0 Then
                MsgBox("��e����d�򤺦��Ͳ��p�����L���Ʀ��o�O��!")
                Exit Sub
            End If

            If pdc.ProductionFieldDaySummary_GetList(strA, strB, strC, Nothing, Nothing, strD, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd"))).Count = 0 Then
                MsgBox("��e����d��,�L��Ͳ��O�����`�H��!")
                Exit Sub
            End If

            ltc.CollToDataSet(ds, "ProductionSchedule", psc.ProductionSchedule_GetList2(Nothing, strA, strB, strC, strF, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd"))))


            If pmc.ProductionMaterialDayDetail_GetList(strA, strB, strC, Nothing, CDate(Format(Now, "yyyy/MM/dd"))).Count <= 0 Then
                ltc1.CollToDataSet(ds, "ProductionMaterialDayDetail", pmc.NothingNew)
            Else
                ltc1.CollToDataSet(ds, "ProductionMaterialDayDetail", pmc.ProductionMaterialDayDetail_GetList(strA, strB, strC, Nothing, CDate(Format(Now, "yyyy/MM/dd"))))
            End If

            If pkc.vwProductionKaiLiao_GetList(strA, strB, strC, Nothing, CDate(Format(Now, "yyyy/MM/dd")), CDate(Format(Now, "yyyy/MM/dd"))).Count <= 0 Then
                ltc4.CollToDataSet(ds, "vwProductionKaiLiao", pkc.NothingNew)
            Else
                ltc4.CollToDataSet(ds, "vwProductionKaiLiao", pkc.vwProductionKaiLiao_GetList(strA, strB, strC, Nothing, CDate(Format(Now, "yyyy/MM/dd")), CDate(Format(Now, "yyyy/MM/dd"))))
            End If


            'ltc1.CollToDataSet(ds, "ProductionKaiLiao", pkc.ProductionKaiLiao_GetList(Nothing, strA, Nothing, Nothing, Nothing, strB, strC, Nothing, Nothing, True, Nothing))
            ltc2.CollToDataSet(ds, "ProductionField", pfc.ProductionField_GetList1(Nothing, strA, strB, strC, Nothing, Nothing, Nothing, Nothing, Nothing, True, True, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), Format(Now, "yyyy/MM/dd"), Nothing, Nothing))
            ltc3.CollToDataSet(ds, "ProductionFieldDaySummary", pdc.ProductionFieldDaySummary_GetList(strA, strB, strC, Nothing, Nothing, strD, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd"))))

            ltc6.CollToDataSet(ds, "ProductionMaterialDayDetail1", pmc.NothingNew)

        End If


        ltc5.CollToDataSet(ds, "Temp3", pdc.Temp3_GetList(Nothing, Nothing))

        PreviewRPT(ds, "rptProductionProcess", "�u���Ͳ��y�{��", True, True)


        ltc = Nothing
        ltc1 = Nothing
        ltc2 = Nothing
        ltc3 = Nothing
        ltc4 = Nothing
        ltc5 = Nothing
        ltc6 = Nothing
    End Sub

    Private Sub cmsTransferInOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsTransferInOut.Click
        If GridView2.RowCount = 0 Then Exit Sub

        Dim ds As New DataSet

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet

        Dim pfc As New ProductionFieldControl  '���Ʀ��o�H��
        Dim pdc As New ProductionFieldDaySummaryControl  '�C�ѰO���H��

        ds.Tables.Clear()
        Dim strA, strB, strC, strD, strE As String

        If GluDep.EditValue = "*" Then

            strA = GridView4.GetFocusedRowCellValue("Pro_Type").ToString
            strB = GridView4.GetFocusedRowCellValue("PM_M_Code").ToString
            strC = GridView4.GetFocusedRowCellValue("PM_Type").ToString
        Else

            strA = GridView1.GetFocusedRowCellValue("Pro_Type").ToString
            strB = GridView1.GetFocusedRowCellValue("PM_M_Code").ToString
            strC = GridView1.GetFocusedRowCellValue("PM_Type").ToString
        End If

        strD = GridView2.GetFocusedRowCellValue("FP_OutDep").ToString
        strE = GridView2.GetFocusedRowCellValue("Pro_NO").ToString


        Dim strEndDate As String = ""
        Dim StrStartDate As String = ""



        If strType = "���w�Ѽ�" Then

            If pfc.ProductionField_GetList2(Nothing, strA, strB, strC, strE, Nothing, Nothing, strD, Nothing, Nothing, Nothing, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), Format(Now, "yyyy/MM/dd"), Nothing, Nothing, "True").Count = 0 Then
                MsgBox("��e����d�򤺵L���u�Ǫ����Ʀ��o�O��!")
                Exit Sub
            End If

            ltc.CollToDataSet(ds, "ProductionField", pfc.ProductionField_GetList2(Nothing, strA, strB, strC, strE, Nothing, Nothing, strD, Nothing, Nothing, Nothing, Format(Now.AddDays(-IntDay), "yyyy/MM/dd"), Format(Now, "yyyy/MM/dd"), Nothing, Nothing, "True"))
            ltc1.CollToDataSet(ds, "ProductionFieldDaySummary", pdc.ProductionFieldDaySummary_GetList(strA, strB, strC, strE, strD, Nothing, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd"))))

            StrStartDate = DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd")))
            strEndDate = CDate(Format(Now, "yyyy/MM/dd"))

        ElseIf strType = "����d��" Then
            If pfc.ProductionField_GetList2(Nothing, strA, strB, strC, strE, Nothing, Nothing, strD, Nothing, Nothing, Nothing, strDate1, strDate2, Nothing, Nothing, "True").Count = 0 Then
                MsgBox("��e����d�򤺵L���u�Ǫ����Ʀ��o�O��!")
                Exit Sub
            End If

            ltc.CollToDataSet(ds, "ProductionField", pfc.ProductionField_GetList2(Nothing, strA, strB, strC, strE, Nothing, Nothing, strD, Nothing, Nothing, Nothing, strDate1, strDate2, Nothing, Nothing, "True"))
            ltc1.CollToDataSet(ds, "ProductionFieldDaySummary", pdc.ProductionFieldDaySummary_GetList(strA, strB, strC, strE, strD, Nothing, strDate1, strDate2))

            StrStartDate = strDate1
            strEndDate = strDate2
        Else

            If pfc.ProductionField_GetList2(Nothing, strA, strB, strC, strE, Nothing, Nothing, strD, Nothing, Nothing, Nothing, Format(Now.AddDays(-7), "yyyy/MM/dd"), Format(Now, "yyyy/MM/dd"), Nothing, Nothing, "True").Count = 0 Then
                MsgBox("��e����d�򤺵L���u�Ǫ����Ʀ��o�O��!")
                Exit Sub
            End If

            ltc.CollToDataSet(ds, "ProductionField", pfc.ProductionField_GetList2(Nothing, strA, strB, strC, strE, Nothing, Nothing, strD, Nothing, Nothing, Nothing, Format(Now.AddDays(-7), "yyyy/MM/dd"), Format(Now, "yyyy/MM/dd"), Nothing, Nothing, "True"))
            ltc1.CollToDataSet(ds, "ProductionFieldDaySummary", pdc.ProductionFieldDaySummary_GetList(strA, strB, strC, strE, strD, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd"))))

            StrStartDate = Format(Now.AddDays(-7), "yyyy/MM/dd")
            strEndDate = Format(Now, "yyyy/MM/dd")
        End If

        PreviewRPT1(ds, "rptProductionFieldSR", "�����ԲӦ��o�O��", StrStartDate, strEndDate, True, True)

        ltc = Nothing
        ltc1 = Nothing
    End Sub

    Private Sub Grid1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Grid1.MouseUp
        If GridView1.RowCount = 0 Then Exit Sub

        Dim pc As New ProductionFieldDaySummaryControl

        Dim strA, strB, strC, strD As String

        strA = GridView1.GetFocusedRowCellValue("Pro_Type").ToString
        strB = GridView1.GetFocusedRowCellValue("PM_M_Code").ToString
        strC = GridView1.GetFocusedRowCellValue("PM_Type").ToString
        strD = GridView1.GetFocusedRowCellValue("FacName").ToString

        If strType = "���w�Ѽ�" Then

            Grid2.DataSource = pc.ProductionFieldDaySummary_GetList(strA, strB, strC, Nothing, Nothing, strD, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate)
        ElseIf strType = "����d��" Then
            Grid2.DataSource = pc.ProductionFieldDaySummary_GetList(strA, strB, strC, Nothing, Nothing, strD, strDate1, strDate2)
        Else
            Grid2.DataSource = pc.ProductionFieldDaySummary_GetList(strA, strB, strC, Nothing, Nothing, strD, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))
        End If
    End Sub

    Private Sub Grid4_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Grid4.MouseUp
        If GridView4.RowCount = 0 Then Exit Sub

        Dim pc As New ProductionFieldDaySummaryControl

        Dim strA, strB, strC As String

        strA = GridView4.GetFocusedRowCellValue("Pro_Type").ToString
        strB = GridView4.GetFocusedRowCellValue("PM_M_Code").ToString
        strC = GridView4.GetFocusedRowCellValue("PM_Type").ToString

        If strType = "���w�Ѽ�" Then

            Grid2.DataSource = pc.ProductionFieldDaySummary_GetList(strA, strB, strC, Nothing, Nothing, Nothing, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate)
        ElseIf strType = "����d��" Then
            Grid2.DataSource = pc.ProductionFieldDaySummary_GetList(strA, strB, strC, Nothing, Nothing, Nothing, strDate1, strDate2)
        Else
            Grid2.DataSource = pc.ProductionFieldDaySummary_GetList(strA, strB, strC, Nothing, Nothing, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))
        End If
    End Sub

    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
        strType = "����d��"
        strDate1 = DateEdit1.Text
        strDate2 = DateEdit2.Text
        If GluDep.EditValue = "*" Then

            LoadDataAll(strProcess, strDate1, strDate2)
            Grid1.Visible = False
            Grid4.Visible = True
        Else
            LoadData(strProcess, GluDep.EditValue, strDate1, strDate2)
            Grid1.Visible = True
            Grid4.Visible = False
        End If
    End Sub
End Class