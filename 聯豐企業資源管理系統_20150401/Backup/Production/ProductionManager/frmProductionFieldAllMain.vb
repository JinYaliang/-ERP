Imports LFERP.Library.Production.ProductionFieldDaySummary

Public Class frmProductionFieldAllMain

    Dim pfc As New ProductionFieldDaySummaryControl


    Dim strType As String   '�Ϥ����w����άO���w�Ѽ�
    Dim strDate As String   '�{�ɤ��
    Dim strDate1 As String  '�{�ɤ��
    Dim strDate2 As String  '�{�ɤ��
    Dim IntDay As Integer   '�Ѽ�
    Dim strProcess As String  '�[�u����

    '�q�{���p�U���Ͳ��[�u/�B����d��7��
    Private Sub frmProductionFieldAllMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If strType = "���w�Ѽ�" Then
            Grid1.DataSource = pfc.ProductionFieldDaySummary_ComGetList("�Ͳ��[�u", Nothing, Nothing, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate)
        ElseIf strType = "����d��" Then
            Grid1.DataSource = pfc.ProductionFieldDaySummary_ComGetList("�Ͳ��[�u", Nothing, Nothing, strDate1, strDate2)
        Else
            DateEdit1.Text = DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd")))
            DateEdit2.Text = CDate(Format(Now, "yyyy/MM/dd"))
            Grid1.DataSource = pfc.ProductionFieldDaySummary_ComGetList("�Ͳ��[�u", Nothing, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))
        End If
        strProcess = "�Ͳ��[�u"
        Grid2.DataSource = ""
    End Sub
    '�F���[�u�u���Ͳ��������p
    Private Sub cmdPeiBu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPeiBu.Click
        If strType = "���w�Ѽ�" Then
            Grid1.DataSource = pfc.ProductionFieldDaySummary_ComGetList("�F���[�u", Nothing, Nothing, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate)
        ElseIf strType = "����d��" Then
            Grid1.DataSource = pfc.ProductionFieldDaySummary_ComGetList("�F���[�u", Nothing, Nothing, strDate1, strDate2)
        Else
            Grid1.DataSource = pfc.ProductionFieldDaySummary_ComGetList("�F���[�u", Nothing, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))
        End If
        strProcess = "�F���[�u"
        Grid2.DataSource = ""
    End Sub
    '�Ͳ��[�u�u���Ͳ��������p
    Private Sub cmdShengChan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShengChan.Click
        If strType = "���w�Ѽ�" Then
            Grid1.DataSource = pfc.ProductionFieldDaySummary_ComGetList("�Ͳ��[�u", Nothing, Nothing, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate)
        ElseIf strType = "����d��" Then
            Grid1.DataSource = pfc.ProductionFieldDaySummary_ComGetList("�Ͳ��[�u", Nothing, Nothing, strDate1, strDate2)
        Else
            Grid1.DataSource = pfc.ProductionFieldDaySummary_ComGetList("�Ͳ��[�u", Nothing, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))
        End If
        strProcess = "�Ͳ��[�u"
        Grid2.DataSource = ""
    End Sub
    '�˰t�[�u�u���Ͳ��������p
    Private Sub cmdZhuangPei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdZhuangPei.Click
        If strType = "���w�Ѽ�" Then
            Grid1.DataSource = pfc.ProductionFieldDaySummary_ComGetList("�˰t�X�f", Nothing, Nothing, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate)
        ElseIf strType = "����d��" Then
            Grid1.DataSource = pfc.ProductionFieldDaySummary_ComGetList("�˰t�X�f", Nothing, Nothing, strDate1, strDate2)
        Else
            Grid1.DataSource = pfc.ProductionFieldDaySummary_ComGetList("�˰t�X�f", Nothing, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))
        End If
        strProcess = "�˰t�X�f"
        Grid2.DataSource = ""
    End Sub

    '�d��---�w��d�ߤ���d��w�q
    '@ 2012/1/6 �K�[�P�_�A�����Ůɰ���
    Private Sub FieldMainStripSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FieldMainStripSelect.Click
        Dim frm As New frmProductionMainSelect
        frm.ShowDialog()
        If tempValue <> "" And tempValue2 <> "" And tempValue3 <> "" Then '�����Ůɰ���
            If frm.RadioButton1.Checked = True Then
                '���w�ѼƬd��(�Z���ѤѼ�)
                strType = tempValue3
                strDate = tempValue2
                IntDay = CInt(tempValue)

            ElseIf frm.RadioButton2.Checked = True Then
                strType = tempValue3
                strDate1 = tempValue
                strDate2 = tempValue2
            End If

            cmdShengChan_Click(Nothing, Nothing)  '�q�{��ܥͲ��[�u�u��

            tempValue = ""
            tempValue2 = ""
            tempValue3 = ""
        End If
    End Sub

    '�Ͳ����ԲӥͲ����p
    Private Sub Grid1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Grid1.MouseUp


        If GridView1.RowCount = 0 Then Exit Sub

        Dim strA, strB, strC As String

        strA = GridView1.GetFocusedRowCellValue("Pro_Type").ToString
        strB = GridView1.GetFocusedRowCellValue("PM_M_Code").ToString
        strC = GridView1.GetFocusedRowCellValue("PM_Type").ToString

        If strType = "���w�Ѽ�" Then

            Grid2.DataSource = pfc.ProductionFieldDaySummary_FacGetList(strA, strB, strC, Nothing, Nothing, DateAdd(DateInterval.Day, -IntDay, CDate(Format(Now, "yyyy/MM/dd"))), strDate)
        ElseIf strType = "����d��" Then
            Grid2.DataSource = pfc.ProductionFieldDaySummary_FacGetList(strA, strB, strC, Nothing, Nothing, strDate1, strDate2)
        Else
            Grid2.DataSource = pfc.ProductionFieldDaySummary_FacGetList(strA, strB, strC, Nothing, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))
        End If


    End Sub

    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
        strType = "����d��"
        strDate1 = DateEdit1.Text
        strDate2 = DateEdit2.Text
        Grid1.DataSource = pfc.ProductionFieldDaySummary_ComGetList(strProcess, Nothing, Nothing, strDate1, strDate2)
    End Sub

End Class