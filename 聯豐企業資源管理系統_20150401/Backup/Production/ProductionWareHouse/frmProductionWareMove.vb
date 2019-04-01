Imports LFERP.Library.ProductionWareMove
Imports LFERP.Library.ProductionWareInventory
Imports LFERP.Library.Product
Imports LFERP.Library.ProductProcess


Public Class frmProductionWareMove
    Dim pic As New ProductInventoryController
    Dim ds As New DataSet
    Dim strWHInID As String
    Dim strWHOutID As String
    Dim TorF As Boolean

    Private Sub frmProductionWareMove_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label4.Text = MTypeName
        Label9.Text = tempValue

        tempValue = ""
        MTypeName = ""
        CreateTable()
        txtQuoID.Text = Label9.Text
        txtQuoID.Enabled = False

        Select Case Label4.Text

            Case "ProductionWareMove"

                If Edit = False Then

                    strWHOutID = tempValue2
                    If tempValue2 = "" Then
                        MsgBox("�Х���ܬ������ܧO")
                        Exit Sub
                    End If
                    OutWH.Text = tempValue3
                    DateEdit1.DateTime = Format(Now, "yyyy/MM/dd HH:mm:ss")
                    'DateEdit1.Enabled = False
                    tempValue2 = ""
                    tempValue3 = ""
                Else
                    LoadData(Label9.Text)
                End If
                'XtraTabControl1.SelectedTabPage = XtraTabPage1
                XtraTabPage2.PageVisible = False
                GridView2.Columns.Item("PWM_Qty").OptionsColumn.ReadOnly = False
                GridView2.Columns.Item("PWM_Property").OptionsColumn.ReadOnly = False
                GridView2.Columns.Item("PWM_Remark").OptionsColumn.ReadOnly = False
            Case "PreView"
                LoadData(Label9.Text)
                cmdSave.Visible = False

                XtraTabControl1.SelectedTabPage = XtraTabPage1
            Case "InCheck"
                LoadData(Label9.Text)
                CheckEdit3.Enabled = True
                'XtraTabControl1.SelectedTabPage = XtraTabPage1
                XtraTabPage2.PageVisible = False
                cmdSave.Enabled = False

                GroupBox1.Enabled = False
                GridControl1.ContextMenuStrip.Enabled = False
                GridView2.OptionsBehavior.Editable = False

            Case "Check"
                LoadData(Label9.Text)

                XtraTabControl1.SelectedTabPage = XtraTabPage2
                cmdSave.Enabled = False

                GroupBox1.Enabled = False
                GridControl1.ContextMenuStrip.Enabled = False
                GridView2.OptionsBehavior.Editable = False
        End Select

    End Sub

    Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("WareMove")

            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("PWM_Type", GetType(String)) 'PWM_Type
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("M_Unit", GetType(String))
            .Columns.Add("PWM_Qty", GetType(Integer))
            .Columns.Add("PI_Qty", GetType(Integer))
            .Columns.Add("PWM_Property", GetType(String))
            .Columns.Add("PWM_Remark", GetType(String))
        End With
        GridControl1.DataSource = ds.Tables("WareMove")

        With ds.Tables.Add("DelWareMove")
            .Columns.Add("AutoID", GetType(String))
        End With
    End Sub

    Function LoadData(ByVal PWM_NO As String) As Boolean
        LoadData = True
        Dim pc As New ProductionWareMoveControl
        Dim pi As List(Of ProductionWareMoveInfo)

        Try
            pi = pc.ProductionWareMove_GetList(PWM_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            If pi.Count = 0 Then
                LoadData = False
                Exit Function
            Else
                Dim i As Integer
                txtQuoID.Text = pi(0).PWM_NO
                txtWH.Text = pi(0).PH_InName
                strWHInID = pi(0).PH_InID
                OutWH.Text = pi(0).PH_OutName
                strWHOutID = pi(0).PH_OutID
                DateEdit1.EditValue = Format(pi(0).PWM_AddDate, "yyyy/MM/dd")

                ds.Tables("WareMove").Clear()
                Dim row As DataRow
                For i = 0 To pi.Count - 1

                    row = ds.Tables("WareMove").NewRow

                    row("AutoID") = pi(i).AutoID
                    row("PM_M_Code") = pi(i).PM_M_Code
                    row("M_Code") = pi(i).M_Code
                    row("M_Name") = pi(i).M_Name
                    row("M_Gauge") = pi(i).M_Gauge
                    row("M_Unit") = pi(i).M_Unit
                    row("PWM_Qty") = pi(i).PWM_Qty
                    row("PWM_Property") = pi(i).PWM_Property
                    row("PWM_Remark") = pi(i).PWM_Remark

                    row("PWM_Type") = pi(i).PWM_Type

                    Dim pii As List(Of ProductInventoryInfo)

                    pii = pic.ProductInventory_GetList(pi(i).PM_M_Code, pi(i).M_Code, strWHOutID, Nothing)
                    If pii.Count > 0 Then
                        row("PI_Qty") = pii(0).PI_Qty
                    Else
                        row("PI_Qty") = 0
                    End If

                    ds.Tables("WareMove").Rows.Add(row)
                Next
                CheckEdit3.Checked = pi(0).PWM_InCheck
                CheckEdit2.Checked = pi(0).PWM_Check
                Label5.Text = pi(0).CheckActionName
                Label8.Text = Format(pi(0).PWM_CheckDate, "yyyy/MM/dd")
                txtcheckremark.Text = pi(0).PWM_CheckRemark

            End If

        Catch ex As Exception
            LoadData = False
            MsgBox(ex.Message)
        End Try

    End Function

    Function GetNO() As String
        '�ͦ��s���ռ��渹
        Dim str1 As String
        Dim ac As New ProductionWareMoveControl
        Dim ai As ProductionWareMoveInfo
        str1 = Format(Now, "yyMM")
     
        ai = ac.ProductionWareMove_GetNo(str1)

        If ai Is Nothing Then
            GetNO = "PWM" & str1 & "00001"
        Else
            GetNO = "PWM" & str1 & Mid((CInt(Mid(ai.PWM_NO, 8)) + 100001), 2)
        End If

    End Function

    'Function GetNum() As String
    '    Dim str1 As String
    '    Dim ac As New ProductionWareMoveControl
    '    Dim ai As New ProductionWareMoveInfo
    '    str1 = Format(Now, "yyMM")

    '    ai = ac.ProductionWareMove_GetNo(str1)

    '    If ai Is Nothing Then
    '        GetNum = str1 & "00001"
    '    Else
    '        GetNum = str1 & Mid((CInt(Mid(ai.PWM_Num, 5)) + 100001), 2)
    '    End If
    'End Function

    Sub DataNew()

        Dim ac As New ProductionWareMoveControl
        Dim ai As New ProductionWareMoveInfo

        txtQuoID.Text = GetNO()
        ai.PWM_NO = txtQuoID.Text
        ai.PH_InID = strWHInID   '�ܧ�ܮw
        ai.PH_OutID = strWHOutID  '��l�o�X�ܮw
        ai.PWM_AddDate = DateEdit1.EditValue
        ai.PWM_OutAction = InUserID
        ai.PWM_OutCheck = True



        Dim i As Integer
        For i = 0 To ds.Tables("WareMove").Rows.Count - 1
            ai.PM_M_Code = ds.Tables("WareMove").Rows(i)("PM_M_Code")
            ai.M_Code = ds.Tables("WareMove").Rows(i)("M_Code")
            ai.PWM_Qty = CInt(ds.Tables("WareMove").Rows(i)("PWM_Qty"))
            ai.PWM_Property = ds.Tables("WareMove").Rows(i)("PWM_Property")
            ai.PWM_Remark = ds.Tables("WareMove").Rows(i)("PWM_Remark")
            ai.PWM_Type = ds.Tables("WareMove").Rows(i)("PWM_Type")

            ac.ProductionWareMove_Add(ai)

        Next
        MsgBox("�w�O�s,�渹: " & txtQuoID.Text & " ")
        Me.Close()


    End Sub

    Sub DataEdit()

        Dim i, j As Integer
        Dim ac As New ProductionWareMoveControl
        Dim ai As New ProductionWareMoveInfo

        '��s�R�����O��
        If ds.Tables("DelWareMove").Rows.Count > 0 Then
            For j = 0 To ds.Tables("DelWareMove").Rows.Count - 1
                ac.ProductionWareMove_Delete(Nothing, ds.Tables("DelWareMove").Rows(j)("AutoID"))
            Next j
        End If

        ai.PWM_NO = txtQuoID.Text
        ai.PWM_AddDate = DateEdit1.EditValue
        ai.PWM_OutAction = InUserID
        ai.PH_InID = strWHInID
        ai.PH_OutID = strWHOutID

        For i = 0 To ds.Tables("WareMove").Rows.Count - 1

            If Not IsDBNull(ds.Tables("WareMove").Rows(i)("AutoID")) Then '�u�O�ק�

                ai.AutoID = ds.Tables("WareMove").Rows(i)("AutoID")
                ai.PWM_Qty = CInt(ds.Tables("WareMove").Rows(i)("PWM_Qty"))
                ai.PWM_ModifyDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
                ai.PWM_ModifyUserID = ds.Tables("WareMove").Rows(i)("PWM_ModifyUserID").ToString
                ai.PWM_Property = ds.Tables("WareMove").Rows(i)("PWM_Property")

                ai.PWM_Remark = ds.Tables("WareMove").Rows(i)("PWM_Remark")
                ai.PWM_Type = ds.Tables("WareMove").Rows(i)("PWM_Type")
                ac.ProductionWareMove_Update(ai)

            ElseIf IsDBNull(ds.Tables("WareMove").Rows(i)("PWM_Num")) Then '�ק襤�s�W

                ai.PM_M_Code = ds.Tables("WareMove").Rows(i)("PM_M_Code")
                ai.M_Code = ds.Tables("WareMove").Rows(i)("M_Code")
                ai.PWM_Qty = CInt(ds.Tables("WareMove").Rows(i)("PWM_Qty"))
                ai.PWM_Property = ds.Tables("WareMove").Rows(i)("PWM_Property")
                ai.PWM_Remark = ds.Tables("WareMove").Rows(i)("PWM_Remark")
                ai.PWM_Type = ds.Tables("WareMove").Rows(i)("PWM_Type")

                ac.ProductionWareMove_Add(ai)

            End If
        Next
        MsgBox("�w�O�s,�渹: " & txtQuoID.Text & " ")
        Me.Close()

    End Sub

    Sub InCheck()
        Try
            Dim ac As New ProductionWareMoveControl
            Dim ai As New ProductionWareMoveInfo
            Dim i%
            For i = 0 To ds.Tables("WareMove").Rows.Count - 1
                ai.AutoID = ds.Tables("WareMove").Rows(i)("AutoID").ToString
                ai.PWM_InCheck = CheckEdit3.Checked
                ai.PWM_InAction = InUserID
                ai.PWM_InDate = Format(Now, "yyyy/MM/dd HH:mm:ss")

                If ac.ProductionWareMove_UpdateInCheck(ai) = False Then
                    MsgBox("�O���G" & ds.Tables("DelWareMove").Rows(i)("PM_M_Code").ToString & " /  " & ds.Tables("DelWareMove").Rows(i)("M_Code").ToString & "�T�{����,���ˬd��]!", 64, "����")
                End If
            Next
            MsgBox("���ƽT�{���A�w����", 64, "����")
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Sub UpdateCheck()
        Dim ac As New ProductionWareMoveControl
        Dim ai As New ProductionWareMoveInfo
        ai.PWM_NO = txtQuoID.Text
        ai.PWM_Check = CheckEdit2.Checked
        ai.PWM_CheckAction = InUserID
        ai.PWM_CheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        ai.PWM_CheckRemark = txtcheckremark.Text

        If ac.ProductionWareMove_UpdateCheck(ai) = True Then
            MsgBox("���Ƽf�֪��A�w����")
            Me.Close()
        Else
            MsgBox("�f�֥���,���ˬd��]!")
        End If

        '-------------------------------------------------���ƺ޲z

        'Dim i As Integer
        'For i = 0 To ds.Tables("WareMove").Rows.Count - 1

        '    Dim wic As New ProductionWareInventoryControl
        '    Dim wiiIn As New ProductionWareInventoryInfo
        '    Dim wiiOut As New ProductionWareInventoryInfo

        '    wiiIn.M_Code = ds.Tables("WareMove").Rows(i)("M_Code").ToString
        '    wiiIn.PH_ID = strWHInID


        '    If TorF = True And CheckEdit2.Checked = False Then
        '        wiiIn.PI_Qty = "-" & ds.Tables("WareMove").Rows(i)("PWM_Qty").ToString
        '        wiiOut.PI_Qty = ds.Tables("WareMove").Rows(i)("PWM_Qty").ToString
        '    End If

        '    wiiOut.M_Code = ds.Tables("WareMove").Rows(i)("M_Code").ToString
        '    wiiOut.PH_ID = strWHOutID
        '    If TorF = False And CheckEdit2.Checked = True Then
        '        wiiIn.PI_Qty = ds.Tables("WareMove").Rows(i)("PWM_Qty").ToString
        '        wiiOut.PI_Qty = "-" & ds.Tables("WareMove").Rows(i)("PWM_Qty").ToString
        '    End If

        '    wic.UpdateProductionWareInventory_PIQty(wiiIn) '����
        '    wic.UpdateProductionWareInventory_PIQty(wiiOut)  '�o��

        'Next
        '-------------------------------------------------

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Select Case Label4.Text
            Case "ProductionWareMove"
                If Edit = False Then
                    If CheckSave() = True Then
                        DataNew()
                    End If

                Else
                    If CheckSave() = True Then
                        DataEdit()
                    End If
                End If
            Case "InCheck"
                InCheck()
            Case "Check"
                UpdateCheck()
        End Select

    End Sub
    Function CheckSave() As Boolean
        CheckSave = True

        If ds.Tables("WareMove").Rows.Count = 0 Then
            MsgBox("�S���ƾ�,�L�k�O�s!")
            CheckSave = False
            Exit Function
        End If

        If txtWH.Text = "" Then
            MsgBox("�����ܮw���ର��!", MsgBoxStyle.OkOnly)
            CheckSave = False
            Exit Function
        End If

        If OutWH.Text = "" Then
            MsgBox("�o�ƭܮw���ର��!", MsgBoxStyle.OkOnly)
            CheckSave = False
            Exit Function
        End If

        If strWHInID = strWHOutID Then
            MsgBox("�����M�o�X����O�P�@�ӭܧO!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            CheckSave = False
            Exit Function
        End If

        If DateEdit1.Text = "" Then
            MsgBox("������ର��!", MsgBoxStyle.OkOnly)
            CheckSave = False
            Exit Function
        End If


        Dim i As Integer
        For i = 0 To ds.Tables("WareMove").Rows.Count - 1

            If CInt(ds.Tables("WareMove").Rows(i)("PWM_Qty").ToString) = 0 Then
                MsgBox("�ռ��ƶq���ର0�I", MsgBoxStyle.OkOnly)
                CheckSave = False
                Exit Function
            End If

            If CInt(ds.Tables("WareMove").Rows(i)("PI_Qty")) < CInt(ds.Tables("WareMove").Rows(i)("PWM_Qty")) Then
                MsgBox("�o�X�ܮw�s����!")
                CheckSave = False
                Exit Function
            End If

            If ds.Tables("WareMove").Rows(i)("PWM_Property").ToString = "" Then
                MsgBox("�ж�g�ռ������I", MsgBoxStyle.OkOnly)
                CheckSave = False
                Exit Function
            End If

        Next
    End Function
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub txtWH_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtWH.ButtonClick

        tempCode = "�Ͳ��ܮw"
        frmWareHouseSelect.SelectWareID = ""
        tempValue3 = "880606"
        frmWareHouseSelect.StartPosition = FormStartPosition.Manual
        frmWareHouseSelect.Left = MDIMain.Left + MDIMain.tvModule.Width + Me.Left + txtWH.Left + 17
        frmWareHouseSelect.Top = MDIMain.Top + Me.Top + txtWH.Top + txtWH.Height + 140
        frmWareHouseSelect.ShowDialog()

        If frmWareHouseSelect.SelectWareID = "" Then
        Else
            strWHInID = frmWareHouseSelect.SelectWareID
            txtWH.Text = frmWareHouseSelect.SelectWareName
        End If
    End Sub

    Private Sub popWareMoveOutAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMoveOutAdd.Click
        tempCode = ""
        arlM_Code.Clear()
        tempValue6 = "�Ͳ��ܮw�ռ�"
        frmBOMSelect.ShowDialog()
        '�W�[�O��
        ' If frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 0 Then
        If arlM_Code.Count > 0 Then
            Dim i%
            For i = 0 To arlM_Code.Count - 1
                AddRow(arlM_Code(i))
            Next
        End If
        'End If
        tempCode = ""
        tempValue6 = ""
        arlM_Code.Clear()
    End Sub
    Sub AddRow(ByVal strCode As String)
        If strCode = "" Then
        Else

            Dim i As Integer

            For i = 0 To ds.Tables("WareMove").Rows.Count - 1
                If strCode = ds.Tables("WareMove").Rows(i)("M_Code") And tempCode = ds.Tables("WareMove").Rows(i)("PM_M_Code") Then
                    MsgBox("�@�i�椣���\�����_����....")
                    Exit Sub
                End If
            Next
            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(strCode)
            Dim row As DataRow
            row = ds.Tables("WareMove").NewRow
            row("PM_M_Code") = tempCode
            row("M_Code") = objInfo.M_Code
            row("M_Name") = objInfo.M_Name
            row("M_Unit") = objInfo.M_Unit
            row("M_Gauge") = objInfo.M_Gauge
            row("PWM_Qty") = 0
            row("PWM_Property") = ""
            row("PWM_Remark") = ""

            ''--------------2012-10-8------------------------------------------------------------------------------------
            row("PWM_Type") = LoadProductionType("�Ͳ��[�u", tempCode, objInfo.M_Code)
            ''--------------------------------------------------------------------------------------------------

            Dim pii As List(Of ProductInventoryInfo)

            pii = pic.ProductInventory_GetList(tempCode, objInfo.M_Code, strWHOutID, Nothing)
            If pii.Count > 0 Then
                row("PI_Qty") = pii(0).PI_Qty
            Else
                row("PI_Qty") = 0
            End If

            ds.Tables("WareMove").Rows.Add(row)

            GridView2.MoveLast()
        End If
    End Sub
    Private Sub popWareMoveOutDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMoveOutDel.Click
        If GridView2.RowCount = 0 Then Exit Sub

        '�b�R�����W�[�Q�R�����O��
        Dim row As DataRow = ds.Tables("DelWareMove").NewRow
        row("AutoID") = GridView2.GetFocusedRowCellValue(AutoID)
        ds.Tables("DelWareMove").Rows.Add(row)

        ds.Tables("WareMove").Rows.RemoveAt(CInt(ArrayToString(GridView2.GetSelectedRows())))
    End Sub

    Private Sub CheckEdit3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit3.CheckedChanged
        cmdSave.Enabled = Not cmdSave.Enabled
    End Sub

    Private Sub CheckEdit2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit2.CheckedChanged
        cmdSave.Enabled = Not cmdSave.Enabled
    End Sub


    ''2012-10-8-------

    Function LoadProductionType(ByVal _Pro_Type As String, ByVal _PM_M_Code As String, ByVal _M_Code As String) As String
        LoadProductionType = ""

        Dim ppc As New ProcessMainControl
        Dim ppi As New List(Of ProcessMainInfo)

        RepositoryItemComboBox2.Items.Clear()

        ppi = ppc.ProcessMain_GetList1(Nothing, _PM_M_Code, _Pro_Type, _M_Code)
        If ppi.Count > 0 Then
            Dim k As Integer
            For k = 0 To ppi.Count - 1
                RepositoryItemComboBox2.Items.Add(ppi(k).Type3ID)
            Next

            LoadProductionType = ppi(0).Type3ID

        End If
    End Function

    Private Sub RepositoryItemComboBox2_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles RepositoryItemComboBox2.Enter
        Dim Get_LoadP_YN As Boolean

        If GridView2.RowCount > 0 Then
        Else
            Exit Sub
        End If

        Dim strA, strB, strC As String

        strA = ds.Tables("WareMove").Rows((GridView2.FocusedRowHandle)).Item("PM_M_Code")
        strB = "�Ͳ��[�u"
        strC = ds.Tables("WareMove").Rows((GridView2.FocusedRowHandle)).Item("M_Code")

        If LabelID.Text <> strA Or LabelName.Text <> strB Or LabM_Code.Text <> strC Then
            LabelID.Text = strA
            LabelName.Text = strB
            LabM_Code.Text = strC
            Get_LoadP_YN = True
        Else
            Get_LoadP_YN = False
        End If

        If Get_LoadP_YN = True Then
            LoadProductionType(strB, strA, strC)
        End If
    End Sub
End Class