Imports LFERP.VerUser

Public Class frmProjectFile
    Dim ds As New DataSet
    Dim loadBZ As String = ""

    Dim uvc As New VerUserControl
    ''' <summary>
    ''' 界面加載
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmProjectFile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()
        GC_ProjectInfo.DataSource = uvc.VerUserPermissionModule_GetList(Nothing)
        loadBZ = "Y"
    End Sub
    Sub CreateTable() '建立表
        ds.Tables.Clear()
        With ds.Tables.Add("ProjectManagement")
            .Columns.Add("U_ID", GetType(String))
            .Columns.Add("U_Name", GetType(String))
            .Columns.Add("Verold", GetType(String))
            .Columns.Add("IsCheck", GetType(Boolean))
            .Columns.Add("Ver", GetType(String))
        End With
        GC_UserInfo.DataSource = ds.Tables("ProjectManagement")

    End Sub
    Sub LoadData(ByVal StrPMID As String) '加載數據

        ds.Tables("ProjectManagement").Clear()
        Dim uvl As New List(Of VerUserInfo)

        uvl = uvc.VerUserPermissionModuleUser_GetList(StrPMID, Nothing)
        If uvl.Count <= 0 Then
            Exit Sub
        End If

        Dim i As Integer
        For i = 0 To uvl.Count - 1
            Dim row As DataRow
            row = ds.Tables("ProjectManagement").NewRow
            row("U_ID") = uvl(i).U_ID
            row("U_Name") = uvl(i).U_Name
            row("Verold") = uvl(i).Ver
            row("IsCheck") = False
            row("Ver") = ""
            ds.Tables("ProjectManagement").Rows.Add(row)
        Next


    End Sub

    ''' <summary>
    ''' 項目動態瀏覽人員信息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridView1_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GVW_ProjectInfo.FocusedRowChanged

        If GVW_ProjectInfo.RowCount <= 0 Then
            Exit Sub
        End If

        Dim StrPMID As String
        StrPMID = GVW_ProjectInfo.GetFocusedRowCellValue("PM_ID").ToString
        LoadData(StrPMID)
        GVW_UserInfo.FocusedRowHandle = 0
        CBX_CheckAll.Checked = False
    End Sub
    ''' <summary>
    ''' 更新按鈕
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdReplace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReplace.Click

        If txt_NewNumber.Text = "" Then
            MsgBox("版本號不能為空!")
            Exit Sub
        End If

        Dim k As Integer
        Dim bz As String = ""
        For k = 0 To ds.Tables("ProjectManagement").Rows.Count - 1
            If ds.Tables("ProjectManagement").Rows(k)("IsCheck") = True Then
                bz = "Y"
            End If
        Next
       
        If bz = "" Then
            MsgBox("沒有選擇任何需更新用戶！")
            Exit Sub
        End If


        If MsgBox("是否確定更新？", vbOKCancel, "请选择") = vbOK Then
            Dim i As Integer
            Dim j As Boolean = False
            For i = 0 To ds.Tables("ProjectManagement").Rows.Count - 1
                If ds.Tables("ProjectManagement").Rows(i)("IsCheck") = True Then
                    Dim Veri As New VerUserInfo

                    Veri.U_ID = ds.Tables("ProjectManagement").Rows(i)("U_ID").ToString
                    Veri.Ver = ds.Tables("ProjectManagement").Rows(i)("Ver").ToString

                    If uvc.VerUser_Update(Veri) = False Then
                        MsgBox("部分保存失敗!")
                        Exit Sub
                    End If
                End If
            Next
            MsgBox("保存成功!")
        End If
    End Sub

    ''' <summary>
    ''' 全選CheckBox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CBX_CheckAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBX_CheckAll.Click
        Dim i As Integer
        For i = 0 To ds.Tables("ProjectManagement").Rows.Count - 1
            If CBX_CheckAll.Checked = True Then
                If txt_NewNumber.Text <> "" Then
                    ds.Tables("ProjectManagement").Rows(i)("IsCheck") = True
                    ds.Tables("ProjectManagement").Rows(i)("Ver") = txt_NewNumber.Text
                End If
            End If
            If CBX_CheckAll.Checked = False Then
                ds.Tables("ProjectManagement").Rows(i)("IsCheck") = False
                ds.Tables("ProjectManagement").Rows(i)("Ver") = ""
            End If
        Next
    End Sub
    ''' <summary>
    ''' 新版本號編輯文本框變化影響GridView
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_NewNumber_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_NewNumber.EditValueChanged
        If loadBZ = "" Then Exit Sub

        Dim i As Integer
        For i = 0 To ds.Tables("ProjectManagement").Rows.Count - 1
            If ds.Tables("ProjectManagement").Rows(i)("IsCheck") = True Then
                ds.Tables("ProjectManagement").Rows(i)("Ver") = Trim(txt_NewNumber.Text)
            Else
                ds.Tables("ProjectManagement").Rows(i)("Ver") = ""
            End If
        Next
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub txtPKID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPKID.EditValueChanged
        If loadBZ = "" Then Exit Sub

        Dim ppp As New List(Of VerUserInfo)
        ppp = uvc.VerUserPermissionModule_GetList(txtPKID.Text)
        If ppp.Count <= 0 Then Exit Sub

        For i As Integer = 0 To GVW_ProjectInfo.RowCount - 1
            If GVW_ProjectInfo.GetRowCellValue(i, "PM_ID") = ppp(0).PM_ID Then
                GVW_ProjectInfo.FocusedRowHandle = i
            End If
        Next
    End Sub

End Class