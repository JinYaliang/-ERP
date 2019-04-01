Imports LFERP.Library.ProductProcess
Imports LFERP.Library.Product
Imports LFERP.Library.ProductionDPTWareInventory
Imports LFERP.Library.Production.Datasetting

Public Class frmProductionSelect

    Dim ds As New DataSet
    Dim pc As New ProcessMainControl
    Dim strCode As String

    Sub LoadProductNo()
        'Dim mc As New ProductController
        'PM_M_Code.Properties.DisplayMember = "PM_M_Code"
        'PM_M_Code.Properties.ValueMember = "PM_M_Code"
        'PM_M_Code.Properties.DataSource = mc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        Dim pc As New ProcessMainControl
        Dim pdsi As List(Of ProductionDataSettingInfo)
        Dim pdsc As New ProductionDataSettingControl

        PM_M_Code.Properties.DisplayMember = "PM_M_Code"
        PM_M_Code.Properties.ValueMember = "PM_M_Code"
        'PM_M_Code.Properties.DataSource = pc.ProcessMain_GetList1(Nothing, Nothing, Nothing, Nothing)
        PM_M_Code.Properties.DataSource = pc.ProcessMain_GetList3(Nothing, Nothing)
        ' ProcessMain_GetList3


        If strInUserRank = "�έp" Then
            pdsi = pdsc.ProductionUser_GetList(strInDepIDFull, Nothing)
            If pdsi.Count > 0 Then
                PM_M_Code.Properties.DataSource = pdsc.ProductionUser_GetList(strInDepIDFull, Nothing)
            End If
        End If


        'PM_M_Code.Properties.DataSource = pc.ProcessMain_GetList1(Nothing, Nothing, Nothing, Nothing)

    End Sub

    Private Sub frmProductionSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cbType.EditValue = "�Ͳ��[�u"
        PM_M_Code.EditValue = ""
        gluType.EditValue = ""

        CreateTable()
        LoadProductNo()
        Label5.Text = tempValue2  '���J�����H��--��ܤ@���Φh���u��
        tempValue2 = ""
        Label6.Text = tempValue3
        tempValue3 = ""
        Label7.Text = tempValue4 '���J���� �ɤJ����������e�u�Ǽƶq
        tempValue4 = ""

        If Label5.Text = "�~�o��޲z" Then
            If Label6.Text = "�o��" Then
                GridColumn6.Visible = False
                WI_ReQty.Visible = True
                WI_Qty.Visible = True
            Else
                GridColumn6.Visible = False
                WI_Qty.Visible = False
                WI_ReQty.Visible = False
            End If

        ElseIf Label5.Text = "�u�ǲզX�޲z" Then
            GridColumn6.Visible = False
            WI_Qty.Visible = True
        Else

            GridColumn6.Visible = False
            WI_Qty.Visible = False
        End If

        PM_M_Code.Focus()
        PM_M_Code.Select()
    End Sub

    Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("Material")
            .Columns.Add("M_Code", GetType(String))  '�u�ǽs�X--���ƽs�X
            .Columns.Add("M_Name", GetType(String))  '�u�ǦW��--���ƦW��

            .Columns.Add("DPT_ID", GetType(String)) '����ID--�~�o����
            .Columns.Add("WI_Qty", GetType(Double)) '��e�~�o�����u�Ǽƶq
            .Columns.Add("WI_ReQty", GetType(Double)) '��e�~�o�����u�Ǽƶq


            .Columns.Add("GoIn", GetType(Boolean))
        End With

        Grid.DataSource = ds.Tables("Material")

        With ds.Tables.Add("ProductType")
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("PM_Type", GetType(String))
        End With
        gluType.Properties.DisplayMember = "PM_Type"
        gluType.Properties.ValueMember = "PM_Type"
        gluType.Properties.DataSource = ds.Tables("ProductType")

    End Sub

    '�d�߾ާ@
    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click

        If cbType.EditValue = "" Then
            MsgBox("�u���������ର��!")
            Exit Sub
        End If
        If PM_M_Code.EditValue = "" Then
            MsgBox("���~�s�����ର��!")
            Exit Sub
        End If
        If gluType.EditValue = "" Then
            MsgBox("�������ର��!")
            Exit Sub
        End If

        Dim pc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)
        pci = pc.ProcessMain_GetList(Nothing, PM_M_Code.EditValue, cbType.EditValue, gluType.EditValue, Nothing, "True")

        Try
            If pci.Count = 0 Then Exit Sub
            ds.Tables("Material").Clear()
            Dim i As Integer
            For i = 0 To pci.Count - 1
                Dim row As DataRow
                row = ds.Tables("Material").NewRow

                row("M_Code") = pci(i).PS_NO
                row("M_Name") = pci(i).PS_Name

                If Label5.Text = "�~�o��޲z" Then
                    If Label6.Text = "�o��" Then
                        Dim pdi As List(Of ProductionDPTWareInventoryInfo)
                        Dim pdc As New ProductionDPTWareInventoryControl
                        pdi = pdc.ProductionDPTWareInventory_GetList("F101", pci(i).PS_NO, Nothing)

                        If pdi.Count = 0 Then
                            row("DPT_ID") = "F101"
                            row("WI_Qty") = 0
                            row("WI_ReQty") = 0
                        Else
                            row("WI_ReQty") = pdi(0).WI_ReQty
                            row("DPT_ID") = "F101"
                            row("WI_Qty") = pdi(0).WI_Qty
                        End If
                    End If
                ElseIf Label5.Text = "�u�ǲզX�޲z" Then
                    Dim pdi As List(Of ProductionDPTWareInventoryInfo)
                    Dim pdc As New ProductionDPTWareInventoryControl
                    pdi = pdc.ProductionDPTWareInventory_GetList(Label7.Text, pci(i).PS_NO, Nothing)

                    If pdi.Count = 0 Then
                        row("DPT_ID") = Label7.Text
                        row("WI_Qty") = 0
                    Else
                        row("DPT_ID") = Label7.Text
                        row("WI_Qty") = pdi(0).WI_Qty
                    End If
                End If

                row("GoIn") = False

                ds.Tables("Material").Rows.Add(row)
            Next
        Catch ex As Exception

        End Try

    End Sub

    '���J�줽�γ���
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If Label5.Text = "�~�o��޲z" Then
            GridView1.Columns("GoIn").OptionsColumn.AllowEdit = True

            tempValue = ""
            Dim i, n As Integer
            n = 0

            If Label6.Text = "����" Then

                For i = 0 To ds.Tables("Material").Rows.Count - 1

                    If ds.Tables("Material").Rows(i)("GoIn") = True Then

                        If n = 0 Then
                            tempValue = ds.Tables("Material").Rows(i)("M_Code")
                            n = n + 1
                        Else
                            tempValue = tempValue & "," & ds.Tables("Material").Rows(i)("M_Code")
                            n = n + 1
                        End If
                    End If
                Next
                tempCode = tempValue
                tempValue = ""
            Else
                For i = 0 To ds.Tables("Material").Rows.Count - 1

                    If ds.Tables("Material").Rows(i)("GoIn") = True Then
                        'If ds.Tables("Material").Rows(i)("WI_Qty") <> 0 Then   '2012-7-27
                        If n = 0 Then
                            tempValue = ds.Tables("Material").Rows(i)("M_Code")
                            n = n + 1
                        Else
                            tempValue = tempValue & "," & ds.Tables("Material").Rows(i)("M_Code")
                            n = n + 1
                        End If
                        'Else
                        '    MsgBox("��e�u�Ǧb�~�o���w�s��0,�����\�i��~�o�ާ@!")
                        '    Exit Sub
                        'End If

                    End If
                Next
                tempCode = tempValue
                tempValue = ""
            End If

        ElseIf Label5.Text = "����޲z" Then
            GridView1.Columns("GoIn").OptionsColumn.AllowEdit = True

            tempValue = ""
            Dim i, n As Integer
            n = 0

            For i = 0 To ds.Tables("Material").Rows.Count - 1

                If ds.Tables("Material").Rows(i)("GoIn") = True Then

                    If n = 0 Then
                        tempValue = ds.Tables("Material").Rows(i)("M_Code")
                        n = n + 1
                    Else
                        tempValue = tempValue & "," & ds.Tables("Material").Rows(i)("M_Code")
                        n = n + 1
                    End If
                End If
            Next
            tempCode = tempValue
            tempValue = ""
        ElseIf Label5.Text = "�u�ǲզX�޲z" Then
            GridView1.Columns("GoIn").OptionsColumn.AllowEdit = True

            tempValue = ""
            Dim i, n As Integer
            n = 0

            For i = 0 To ds.Tables("Material").Rows.Count - 1

                If ds.Tables("Material").Rows(i)("GoIn") = True Then
                    If ds.Tables("Material").Rows(i)("WI_Qty") <> 0 Then
                        If n = 0 Then
                            tempValue = ds.Tables("Material").Rows(i)("M_Code")
                            n = n + 1
                        Else
                            tempValue = tempValue & "," & ds.Tables("Material").Rows(i)("M_Code")
                            n = n + 1
                        End If
                    Else
                        MsgBox("��e�u�Ǧb��w�������w�s��0,�����\�i��զX�ާ@!")
                        Exit Sub
                    End If

                End If
            Next
            tempCode = tempValue
            tempValue = ""
        ElseIf Label5.Text = "�u�ǩ���޲z" Then
            GridView1.Columns("GoIn").OptionsColumn.AllowEdit = True

            tempValue = ""
            Dim i, n As Integer
            n = 0

            For i = 0 To ds.Tables("Material").Rows.Count - 1

                If ds.Tables("Material").Rows(i)("GoIn") = True Then

                    If n = 0 Then
                        tempValue = ds.Tables("Material").Rows(i)("M_Code")
                        n = n + 1
                    Else
                        tempValue = tempValue & "," & ds.Tables("Material").Rows(i)("M_Code")
                        n = n + 1
                    End If

                End If
            Next
            tempCode = tempValue
            tempValue = ""
        Else
            GridView1.Columns("GoIn").OptionsColumn.AllowEdit = False

            strCode = GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "M_Code")
            tempCode = strCode  'tempCode�����@���ܶq
            tempValue9 = GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "M_Name")  '@2012/7/27 �K�[

        End If

        Me.Close()

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub PM_M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PM_M_Code.EditValueChanged
        On Error Resume Next

        Dim pcc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)

        ds.Tables("ProductType").Clear()

        pci = pcc.ProcessMain_GetList1(Nothing, PM_M_Code.EditValue, cbType.EditValue, Nothing)
        If pci.Count = 0 Then Exit Sub

        Dim i As Integer
        For i = 0 To pci.Count - 1
            Dim row As DataRow
            row = ds.Tables("ProductType").NewRow
            row("M_Code") = pci(i).M_Code
            row("PM_Type") = pci(i).Type3ID

            ds.Tables("ProductType").Rows.Add(row)
        Next
    End Sub

    '@2012/4/13 �K�[ ���Ů�����ܤU�Ե��
    Private Sub gluType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gluType.KeyDown, PM_M_Code.KeyDown
        If e.KeyCode = Keys.Space Then
            sender.showpopup()
        End If
    End Sub

    Private Sub gluType_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluType.EditValueChanged
        If cbType.EditValue = "" Then
            Exit Sub
        End If
        If PM_M_Code.EditValue = "" Then
            Exit Sub
        End If

        cmdSelect_Click(Nothing, Nothing)
    End Sub

    '@ 2012/1/7 �K�[�A���󤺮e�o�ͧ��ܡA�BPM_M_Code���󤺮e�����ŮɡA�[�����������e��gluType����
    '���L�{�եΥH�U�L�{�G
    'PM_M_Code_EditValueChanged()
    Private Sub cbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbType.SelectedIndexChanged
        If PM_M_Code.Text <> "" Then
            PM_M_Code_EditValueChanged(Nothing, Nothing)
        End If
    End Sub

    '@ 2012/7/27 �K�[
    Private Sub RepositoryItemCheckEdit1_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RepositoryItemCheckEdit1.EditValueChanged
        If Label5.Text = "���ƥX�w��u��" Then
            If sender.checked = True Then
                Dim i%
                For i = 0 To GridView1.RowCount - 1
                    If i <> GridView1.FocusedRowHandle Then
                        If GridView1.GetRowCellValue(i, "GoIn") = True Then
                            MsgBox("�u���ܤ@�Ӥu�ǡI", 64, "����")
                            sender.checked = False
                            Exit Sub
                        End If
                    End If
                Next
            End If
        End If
    End Sub
End Class