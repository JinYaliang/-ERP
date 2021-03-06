Imports LFERP.Library.ProductProcess

Public Class frmPDProductTypeSet
#Region "属性"
    Dim ds As New DataSet
    Dim ptcon As New ProcessTypeControl
    Dim pmcon As New ProcessMainControl
#End Region

#Region "創建臨時表"
    Private Sub CreateTable()
        ds.Tables.Clear()

        With ds.Tables.Add("ProcessType") '
            .Columns.Add("NumberID", GetType(String))
            .Columns.Add("D_ID", GetType(String))
            .Columns.Add("D_Dep", GetType(String))
            .Columns.Add("D_ProcessName", GetType(String))
            .Columns.Add("D_IsSub", GetType(Boolean))
            .Columns.Add("Remarks", GetType(String))
        End With
        ds.Tables("ProcessType").DefaultView.Sort = "NumberID Asc"
        '綁定表格
        GridControl1.DataSource = ds.Tables("ProcessType")

    End Sub
#End Region

#Region "填充臨時表"
    Sub FillTable(ByVal ptiList As List(Of ProcessTypeInfo))
        Try
            Dim row As DataRow
            Dim i As Integer
            For i = 0 To ptiList.Count - 1
                row = ds.Tables("ProcessType").NewRow
                row("NumberID") = ptiList(i).NumberID
                row("D_ID") = ptiList(i).D_ID
                row("D_Dep") = ptiList(i).D_Dep
                row("D_ProcessName") = ptiList(i).D_ProcessName
                row("D_IsSub") = ptiList(i).D_IsSub
                row("Remarks") = ptiList(i).Remarks
                ds.Tables("ProcessType").Rows.Add(row)
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "FillTable方法出錯")
        End Try
    End Sub
#End Region

#Region "窗體載入"
    Private Sub frmPDProductTypeSet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()
        gridDept.DataSource = pmcon.ProDepartMent_GetList(Nothing, Nothing)
        gridType.DataSource = ptcon.ProcessTypeA_GetList(Nothing, Nothing)
    End Sub
#End Region

#Region "右擊菜單"
#Region "設置右擊菜單項是否可用"
    Private Sub GridControl1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridControl1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            SetRightClickMenuEnable()
        End If
    End Sub

    Private Sub GridView1_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        SetRightClickMenuEnable()
        Try
            Label2.Text = "ID:" + GridView1.GetFocusedRowCellValue(NumberID).ToString() + ",rowHandle:" + GridView1.FocusedRowHandle.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SetRightClickMenuEnable()
        Try
            Dim c As ToolStripItem
            If GridView1.FocusedRowHandle < 0 Then
                For Each c In cmsProductType.Items
                    If (c.Name = "cmsProductTypeAdd") Then
                        c.Enabled = True
                    Else
                        c.Enabled = False
                    End If
                Next
            ElseIf GridView1.RowCount = 1 Then
                For Each c In cmsProductType.Items
                    If (c.Name = "cmsProductTypeUp" Or c.Name = "cmsProductTypeDown") Then
                        c.Enabled = False
                    Else
                        c.Enabled = True
                    End If
                Next
            ElseIf GridView1.FocusedRowHandle = 0 Then
                For Each c In cmsProductType.Items
                    If (c.Name = "cmsProductTypeUp") Then
                        c.Enabled = False
                    Else
                        c.Enabled = True
                    End If
                Next
            ElseIf GridView1.FocusedRowHandle = GridView1.RowCount - 1 Then
                For Each c In cmsProductType.Items
                    If (c.Name = "cmsProductTypeDown") Then
                        c.Enabled = False
                    Else
                        c.Enabled = True
                    End If
                Next
            Else
                For Each c In cmsProductType.Items
                    c.Enabled = True
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "SetRightClickMenuEnable方法出錯")
        End Try
    End Sub
#End Region

    Private Sub cmsProductTypeAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductTypeAdd.Click
        Dim row As DataRow = ds.Tables("ProcessType").NewRow
        row("NumberID") = ds.Tables("ProcessType").Rows.Count + 1
        ds.Tables("ProcessType").Rows.Add(row)
    End Sub

    Private Sub cmsProductTypeInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductTypeInsert.Click
        Dim currentID As Integer = CInt(GridView1.GetFocusedRowCellValue(NumberID))
        Try
            For i As Integer = currentID - 1 To ds.Tables("ProcessType").Rows.Count - 1
                ds.Tables("ProcessType").Rows(i)("NumberID") += 1
            Next

            Dim row As DataRow = ds.Tables("ProcessType").NewRow
            row("NumberID") = currentID
            ds.Tables("ProcessType").Rows.InsertAt(row, currentID - 1)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmsProductTypeDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductTypeDel.Click
        Dim currentID As Integer = CInt(GridView1.GetFocusedRowCellValue(NumberID))
        Try
            For i As Integer = currentID - 1 To ds.Tables("ProcessType").Rows.Count - 1
                ds.Tables("ProcessType").Rows(i)("NumberID") -= 1
            Next
            ds.Tables("ProcessType").Rows.RemoveAt(currentID - 1)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmsProductTypeUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductTypeUp.Click
        Try
            With ds.Tables("ProcessType")
                Dim tempRow As DataRow = .NewRow
                tempRow.ItemArray = .Rows(GridView1.FocusedRowHandle).ItemArray
                tempRow("NumberID") = tempRow("NumberID") - 1
                .Rows(GridView1.FocusedRowHandle).ItemArray = .Rows(GridView1.FocusedRowHandle - 1).ItemArray
                .Rows(GridView1.FocusedRowHandle)("NumberID") += 1
                .Rows(GridView1.FocusedRowHandle - 1).ItemArray = tempRow.ItemArray
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmsProductTypeDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductTypeDown.Click
        Try
            With ds.Tables("ProcessType")
                Dim tempRow As DataRow = .NewRow
                tempRow.ItemArray = .Rows(GridView1.FocusedRowHandle).ItemArray
                tempRow("NumberID") += 1
                .Rows(GridView1.FocusedRowHandle).ItemArray = .Rows(GridView1.FocusedRowHandle + 1).ItemArray
                .Rows(GridView1.FocusedRowHandle)("NumberID") -= 1
                .Rows(GridView1.FocusedRowHandle + 1).ItemArray = tempRow.ItemArray
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

#Region "退出及保存按键"
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If MsgBox("請確認是否保存", MsgBoxStyle.OkCancel, "詢問") = MsgBoxResult.Cancel Then
            Exit Sub
        End If
        If SaveCheck() = False Then
            Exit Sub
        End If

        ptcon.ProcessType_Delete(pceType.Text)
        Dim pti As New ProcessTypeInfo
        Try
            With ds.Tables("ProcessType")
                For i As Integer = 0 To .Rows.Count - 1
                    pti.NumberID = .Rows(i)("NumberID")
                    pti.D_ID = .Rows(i)("D_ID")
                    pti.D_Type = pceType.Text
                    pti.D_Dep = .Rows(i)("D_Dep")
                    pti.D_ProcessName = .Rows(i)("D_ProcessName")
                    pti.D_IsSub = True
                    pti.Remarks = .Rows(i)("Remarks").ToString
                    If ptcon.ProcessType_Add(pti) = False Then
                        MsgBox("部分保存出錯", MsgBoxStyle.Critical, "提示")
                    End If
                Next
                MsgBox("保存成功", MsgBoxStyle.Information, "提示")
                Me.Close()
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        gridType.DataSource = ptcon.ProcessTypeA_GetList(Nothing, Nothing)
    End Sub

    Private Function SaveCheck() As Boolean
        SaveCheck = False
        If IsNothing(pceType.EditValue) Then
            MsgBox("工藝類型不能為空", MsgBoxStyle.Information, "提示")
            Exit Function
        End If
        If ds.Tables("ProcessType").Rows.Count < 1 Then
            MsgBox("工藝類型明細表不能為空", MsgBoxStyle.Information, "提示")
            Exit Function
        End If
        With ds.Tables("ProcessType")
            For i As Integer = 0 To .Rows.Count - 1
                If IsDBNull(.Rows(i)("D_ID")) Then
                    MsgBox("工藝部門不能為空", MsgBoxStyle.Information, "提示")
                    Exit Function
                ElseIf IsDBNull(.Rows(i)("D_ProcessName")) Then
                    MsgBox("工藝名稱不能為空", MsgBoxStyle.Information, "提示")
                    Exit Function
                End If
            Next
        End With
        SaveCheck = True
    End Function
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
#End Region

#Region "控件事件"
    Private Sub pceType_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pceType.EditValueChanged
        For i As Integer = 0 To ptcon.ProcessTypeA_GetList(Nothing, Nothing).Count - 1
            If pceType.EditValue = ptcon.ProcessTypeA_GetList(Nothing, Nothing)(i).D_Type Then
                Exit Sub
            End If
        Next
        ds.Tables("ProcessType").Rows.Clear()
    End Sub

    Private Sub viewType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles viewType.Click
        pceType.EditValue = viewType.GetFocusedRowCellValue("D_Type")
        pceType.Text = viewType.GetFocusedRowCellValue("D_Type")

        ds.Tables("ProcessType").Rows.Clear()
        FillTable(ptcon.ProcessType_GetList(Nothing, pceType.Text, Nothing))
        pceType.ClosePopup()
    End Sub

    Private Sub viewDept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles viewDept.Click
        ds.Tables("ProcessType").Rows(GridView1.FocusedRowHandle)("D_Dep") = viewDept.GetFocusedRowCellValue("D_Dep")
        ds.Tables("ProcessType").Rows(GridView1.FocusedRowHandle)("D_ID") = viewDept.GetFocusedRowCellValue("D_ID")
        PopupContainerControl2.OwnerEdit.ClosePopup()
        cmdSave.Focus()
    End Sub
#End Region

End Class