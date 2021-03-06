Imports LFERP.Library.WareHouse.WareBorrowReturn
Imports LFERP.Library.Shared
Imports LFERP.Library.WareHouse.WareBorrowChange


Public Class frmWareBorrowChange

    Dim LoadType As String
    Dim ds As New DataSet



    Private Sub frmWareBorrowChange_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadType = tempValue
        txtChangeNo.Text = tempValue2
        tempValue = Nothing
        tempValue2 = Nothing

        CreateTables()

        Select Case LoadType
            Case "Add"
                Me.CaptionLabel.Text = "更改單-新增"
                Panel1.Visible = True
                DelayDate.Visible = True
                txtM_Code.Properties.PopupFormWidth = "500"

            Case "View"
                Me.CaptionLabel.Text = "更改單-查看"
                Panel1.Visible = False
                DelayDate.Visible = False
                cmdSave.Visible = False

                LoadData(txtChangeNo.Text)


            Case "Check"
                Me.CaptionLabel.Text = "更改單-審核"

                GridView2.OptionsBehavior.Editable = False
                XtbCheck.PageVisible = True
                XtraTabControl1.SelectedTabPage = XtbCheck
                GroupBox1.Enabled = False

                lblCheckAction.Text = InUserID
                lblCheckDate.Text = DateTime.Now.ToString("yyyy-MM-dd")
                LoadData(txtChangeNo.Text)

        End Select

        TxtPerID.Select()



    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Sub CreateTables()
        ds.Tables.Clear()
        With ds.Tables.Add("Change")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("WB_NUM", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))

            .Columns.Add("DelayDate", GetType(String))
            .Columns.Add("QtyS", GetType(Int32))
            .Columns.Add("BQty", GetType(Int32))
            .Columns.Add("QtyE", GetType(Int32))

            .Columns.Add("WB_PerID", GetType(String))
            .Columns.Add("Remark", GetType(String))
        End With

        Grid1.DataSource = ds.Tables("Change")


        With ds.Tables.Add("M_CodeA")
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("SumNO_ReQty", GetType(Int32))
        End With
        txtM_Code.Properties.DisplayMember = "M_Code"
        txtM_Code.Properties.ValueMember = "M_Code"
        txtM_Code.Properties.DataSource = ds.Tables("M_CodeA")

    End Sub

    Sub LoadData(ByVal ChangeNo As String)
        Dim CC As New WareBorrowChangeControl
        Dim CL As New List(Of WareBorrowChangeInfo)

        CL = CC.WareBorrowChange_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, txtChangeNo.Text)
        If CL.Count <= 0 Then
            MsgBox("無數據存在,請檢查！")
            Exit Sub
        End If

        LabChangeActionName.Text = "操作人:" & CL(0).ChangeActionName
        LabChageDate.Text = "操作日期:" & CL(0).ChageDate

        Dim L As Integer
        For L = 0 To CL.Count - 1

            Dim row As DataRow
            row = ds.Tables("Change").NewRow
            row("AutoID") = CL(L).AutoID
            row("WB_NUM") = CL(L).WB_NUM
            row("M_Code") = CL(L).M_Code
            row("M_Name") = CL(L).M_Name
            row("M_Gauge") = CL(L).M_Gauge

            row("WB_PerID") = CL(L).WB_PerID

            '---------------------------------------------------
            Dim wbt As New WareBorrowReturnControl
            Dim wbl As New List(Of WareBorrowReturnInfo)

            wbl = wbt.WareBorrowReturn_GetList(CL(L).WB_NUM, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            row("BQty") = wbl(0).Qty

            TxtPerID.Text = wbl(0).WB_PerID
            Label4.Text = wbl(0).WB_PerName
            '---------------------------------------------------

            row("Remark") = CL(L).Remark
            row("QtyS") = CL(L).QtyS
            row("QtyE") = CL(L).QtyE
            ds.Tables("Change").Rows.Add(row)
        Next
    End Sub

    Private Sub LoadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadButton.Click

        ds.Tables("Change").Clear()

        If txtM_Code.Text = "" Then
            txtM_Code.Select()
            MsgBox("請輸入刀具編碼!")
            Exit Sub
        End If

        If TxtPerID.Text = "" Then
            TxtPerID.Select()
            MsgBox("請輸入借刀人!")
            Exit Sub
        End If

        If Val(TxtQty.Text) <= 0 Then
            TxtQty.Select()
            MsgBox("更改數不能為0!")
            Exit Sub
        End If

        Dim strM_Code, Per_ID As String
        strM_Code = txtM_Code.Text
        Per_ID = TxtPerID.Text
        '----------------------------------------------------------------------------------------
        Dim rcc As New WareBorrowReturnControl
        Dim Rcl As New List(Of WareBorrowReturnInfo)
        Rcl = rcc.WareBorrowReturn_Sum("借刀", strM_Code, Per_ID, Nothing)

        If Rcl.Count <= 0 Then
            MsgBox("指定條件無借刀記錄!")
            txtM_Code.Select()
            Exit Sub
        End If

        If Val(TxtQty.Text) > Rcl(0).SumNO_ReQty Then
            MsgBox("不能更改,更改數大於未還刀數量!")
            TxtQty.Select()
            Exit Sub
        End If

        '----------------------------------------------------------------------------------------
        Dim wbt As New WareBorrowReturnControl
        Dim wbl As New List(Of WareBorrowReturnInfo)

        wbl = wbt.WareBorrowReturn_GetList(Nothing, Nothing, "借刀", strM_Code, 100, Nothing, Per_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If wbl.Count <= 0 Then
            MsgBox(Per_ID & "無借此刀信息！")
            Exit Sub
        End If

        Dim strWBR_NO As String = ""
        Dim i As Integer
        Dim TempDouble As Double '要扣除的總數
        TempDouble = Val(TxtQty.Text)

        For i = 0 To wbl.Count - 1
            Dim NODouble As Double '
            Dim WBR_NODouble As Double '扣除本單 數量

            If TempDouble > 0 Then
                If TempDouble > wbl(i).NO_ReQty Then
                    NODouble = 0
                    TempDouble = TempDouble - wbl(i).NO_ReQty
                    WBR_NODouble = wbl(i).NO_ReQty
                Else
                    NODouble = wbl(i).NO_ReQty - TempDouble  '本單中未交數大於 還刀總數
                    WBR_NODouble = TempDouble
                    TempDouble = 0
                End If

                strWBR_NO = wbl(i).WBR_NO
                '加載ds---------------------------------------------------------
                Dim row As DataRow
                row = ds.Tables("Change").NewRow
                row("WB_NUM") = wbl(i).WB_NUM
                row("M_Code") = wbl(i).M_Code
                row("M_Name") = wbl(i).M_Name
                row("M_Gauge") = wbl(i).M_Gauge

                row("DelayDate") = wbl(i).DelayDate
                row("BQty") = wbl(i).Qty
                row("QtyS") = wbl(i).NO_ReQty
                row("QtyE") = WBR_NODouble

                ds.Tables("Change").Rows.Add(row)
            End If
            ''------------------------------------------------
        Next



    End Sub


    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Select Case LoadType
            Case "Add"
                If CheckData() = True Then
                    SaveDate()
                End If
            Case "Check"
                If CheckData() = True Then
                    SaveCheck()
                End If
        End Select
    End Sub

    '2014-02-18 姚骏
    'Private Sub LoadPerInfo()
    '    Dim wbt As New WareBorrowReturnControl
    '    Dim wbl As New List(Of WareBorrowReturnInfo)

    '    Dim strWB_NUM As String
    '    strWB_NUM = ds.Tables("Change").Rows(0)("WB_NUM").ToString()

    '    wbl = wbt.WareBorrowReturn_GetList(strWB_NUM, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

    '    TxtPerID.Text = wbl(0).WB_PerID
    '    Label4.Text = wbl(0).WB_PerName
    'End Sub


    Function WareBorrowChange_GetNo() As String
        '生成新pm
        Dim pm As New WareBorrowChangeControl
        Dim pi As New WareBorrowChangeInfo
        Dim ndate As String
        ndate = "WC" + Format(Now(), "yyMM")
        pi = pm.WareBorrowChange_GetNo(ndate)
        If pi Is Nothing Then
            WareBorrowChange_GetNo = ndate + "00001"
        Else
            WareBorrowChange_GetNo = ndate + Mid((CInt(Mid(pi.ChangeNo, 7)) + 100001), 2)
        End If
    End Function
    Function CheckData() As Boolean
        CheckData = True
        If ds.Tables("Change").Rows.Count <= 0 Then
            CheckData = False
            MsgBox("無數據保存!")
            Exit Function
        End If
        '---------------------------------------------------------------------
        Dim k As Integer
        For k = 0 To ds.Tables("Change").Rows.Count - 1
            Dim strWB_NUM As String
            strWB_NUM = ds.Tables("Change").Rows(k)("WB_NUM").ToString()
            '----------------------
            Dim wbt As New WareBorrowReturnControl
            Dim wbl As New List(Of WareBorrowReturnInfo)

            wbl = wbt.WareBorrowReturn_GetList(strWB_NUM, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            If wbl(0).NO_ReQty = ds.Tables("Change").Rows(k)("QtyS") Then
            Else
                CheckData = False
                MsgBox("還刀數已變更,請檢查！")
                Exit Function
            End If

            If ds.Tables("Change").Rows(k)("QtyS") < ds.Tables("Change").Rows(k)("QtyE") Then
                CheckData = False
                MsgBox(strWB_NUM & "更改的未還刀數大於更改前未還數,請檢查！")
                Exit Function
            End If

        Next
        '---------------------------------------------------------------------

    End Function
    '2014-02-13     姚駿
    Sub SaveCheck()

        If Not chkCheck.Checked Then
            MsgBox("請確認審核狀態!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
            Exit Sub
        End If

        Dim j As New Integer
        For j = 0 To ds.Tables("Change").Rows.Count - 1

            Dim wc As New WareBorrowChangeControl
            If wc.WareBorrowChange_update(ds.Tables("Change").Rows(j)("AutoID"), chkCheck.Checked, lblCheckAction.Text, lblCheckDate.Text, txtCheckRemark.Text) Then
                Dim wt As New WareBorrowChangeControl
                Dim NoReturnQtyA As Int32


                NoReturnQtyA = ds.Tables("Change").Rows(j)("QtyE")
                If wt.WareBorrowReturn_UpdateNO_ReQtyChange(ds.Tables("Change").Rows(j)("WB_NUM").ToString, NoReturnQtyA) = True Then
                Else
                    MsgBox(ds.Tables("Change").Rows(j)("WB_NUM").ToString & "部分數據保存失敗,請檢查!")
                    Exit Sub

                End If
            Else
                MsgBox("審核失敗")
            End If
        Next

        MsgBox("審核狀態已改變!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
        Me.Close()
    End Sub
    '2014-02-13     姚駿
    Sub SaveDate()

        txtChangeNo.Text = WareBorrowChange_GetNo()

        If Len(txtChangeNo.EditValue) = 0 Then
            MsgBox("不能生成單號，無法保存！")
            Exit Sub
        End If

        Dim j As New Integer
        For j = 0 To ds.Tables("Change").Rows.Count - 1
            Dim wc As New WareBorrowChangeControl
            Dim wi As New WareBorrowChangeInfo

            wi.ChangeNo = txtChangeNo.Text
            wi.M_Code = ds.Tables("Change").Rows(j)("M_Code").ToString
            wi.WB_PerID = ds.Tables("Change").Rows(j)("WB_PerID").ToString
            wi.WB_NUM = ds.Tables("Change").Rows(j)("WB_NUM").ToString
            wi.QtyS = ds.Tables("Change").Rows(j)("QtyS")

            wi.QtyE = ds.Tables("Change").Rows(j)("QtyS") - ds.Tables("Change").Rows(j)("QtyE")
            wi.ChangeType = "更改還刀數"
            wi.ChageDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
            wi.ChangeAction = InUserID


            If IsDBNull(ds.Tables("Change").Rows(j)("Remark")) Then
            Else
                wi.Remark = ds.Tables("Change").Rows(j)("Remark").ToString
            End If

            wc.WareBorrowChange_Add(wi)
            'If wc.WareBorrowChange_Add(wi) = True Then
            '    ''更新此單數據-----------------------------------
            '    Dim wt As New WareBorrowChangeControl

            '    Dim NoReturnQtyA As Int32
            '    NoReturnQtyA = ds.Tables("Change").Rows(j)("QtyS") - ds.Tables("Change").Rows(j)("QtyE")
            '    If wt.WareBorrowReturn_UpdateNO_ReQtyChange(ds.Tables("Change").Rows(j)("WB_NUM").ToString, NoReturnQtyA) = True Then
            '    Else
            '        MsgBox(ds.Tables("Change").Rows(j)("WB_NUM").ToString & "部分數據保存失敗,請檢查!")
            '        Exit Sub
            '    End If
            'Else
            '    Exit Sub
            'End If
        Next

        MsgBox("更改成功！")
        Me.Close()

    End Sub


    Private Sub AButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AButton.Click

        ds.Tables("M_CodeA").Clear()

        Dim wulc As New WhiteUserListController
        Dim wuliL As New List(Of WhiteuserListInfo)
        wuliL = wulc.WhiteUserList_GetList(TxtPerID.Text, "W21")
        If wuliL.Count = 0 Then
            MsgBox("此員工在樣辦倉的白名單已刪除!")
            Exit Sub
        End If

        Label4.Text = wuliL.Item(0).W_UserName
        '-----------------------------------------------------
        Dim rcc As New WareBorrowReturnControl
        Dim Rcl As New List(Of WareBorrowReturnInfo)
        Rcl = rcc.WareBorrowReturn_Sum("借刀", Nothing, TxtPerID.Text, Nothing)

        If Rcl.Count <= 0 Then
            MsgBox("指定條件無借刀記錄!")
            Exit Sub
        End If

        Dim J As Integer
        For J = 0 To Rcl.Count - 1
            If Rcl(J).SumNO_ReQty > 0 Then
                Dim row As DataRow
                row = ds.Tables("M_CodeA").NewRow
                row("M_Code") = Rcl(J).M_Code
                row("M_Name") = Rcl(J).M_Name
                row("M_Gauge") = Rcl(J).M_Gauge
                row("SumNO_ReQty") = Rcl(J).SumNO_ReQty
                ds.Tables("M_CodeA").Rows.Add(row)
            End If
        Next

    End Sub
End Class