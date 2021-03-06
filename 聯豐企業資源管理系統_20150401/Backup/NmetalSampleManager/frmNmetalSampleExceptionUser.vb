Imports LFERP.Library.NmetalSampleManager.NmetalSamplePaceTypeBriName
Public Class frmNmetalSampleExceptionUser
    Private ds As New DataSet
    Private nseu As New NmetalSamplePaceTypeBriNameControl
    Dim objinfo As New NmetalSamplePaceTypeBriNameInfo

    Private Sub ButtonLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLoad.Click
        If CheckDataEmpty() = False Then
            Exit Sub
        End If

        If txt_PE_User.Text <> String.Empty Then
            Dim row As DataRow = ds.Tables("NmetalSampleExceptionUser").NewRow
            row("PE_User") = Trim(StrConv(UCase(Me.txt_PE_User.Text), vbNarrow))
            row("PE_Name") = txt_PE_Name.Text

            ds.Tables("NmetalSampleExceptionUser").Rows.Add(row)
        End If
        txt_PE_User.Text = String.Empty
        txt_PE_Name.Text = String.Empty
        txt_PE_User.Focus()
    End Sub
    ''' <summary>
    ''' 判斷是否為空
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckDataEmpty() As Boolean
        CheckDataEmpty = True
        If txt_PE_User.Text = String.Empty Then
            CheckDataEmpty = False
            MsgBox("工号不能为空，请重新输入！", MsgBoxStyle.OkOnly, "提示")
            txt_PE_User.Focus()
            Exit Function
        End If
        If Not IsNumeric(txt_PE_User.Text) Then
            CheckDataEmpty = False
            MsgBox("工号是由数字组成的，请重新输入！", MsgBoxStyle.OkOnly, "提示")
            txt_PE_User.Focus()
            Exit Function
        End If
        If txt_PE_Name.Text = String.Empty Then
            CheckDataEmpty = False
            MsgBox("姓名不能为空，请重新输入！", MsgBoxStyle.OkOnly, "提示")
            txt_PE_Name.Focus()
            Exit Function
        End If

        Dim i As Integer
        '判断工号是否存在在表中
        'For i = 0 To ds.Tables("NmetalSampleExceptionUser").Rows.Count - 1
        '    If nseu.NmetalSampleExceptionUser_GetID(txt_PE_User.Text) = True Then
        '        MsgBox("工号‘" & txt_PE_User.Text & "’, 已存在数据表中!", MsgBoxStyle.OkOnly, "提示")
        '        CheckDataEmpty = False
        '        Exit Function
        '    End If
        'Next
        Dim m As Integer
        For m = 0 To ds.Tables("NmetalSampleExceptionUser").Rows.Count - 1
            If ds.Tables("NmetalSampleExceptionUser").Rows(m)("PE_User").ToString() = txt_PE_User.Text Then
                Grid1.Focus()
                GridView2.FocusedRowHandle = m
                txt_PE_User.Text = String.Empty
                txt_PE_User.Focus()
                MsgBox("工号'" & ds.Tables("NmetalSampleExceptionUser").Rows(m)("PE_User") & "'已存在表中，请重新输入!", MsgBoxStyle.OkOnly, "提示")
                CheckDataEmpty = False
                Exit Function
            End If
        Next
    End Function
    ''' <summary>
    ''' 创建临时表
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CreateTable()
        With ds.Tables.Add("NmetalSampleExceptionUser")
            .Columns.Add("AutoID", GetType(Decimal))
            .Columns.Add("PE_User", GetType(String))
            .Columns.Add("PE_Name", GetType(String))

            '2014-08-21     Mark
            .Columns.Add("PE_Type", GetType(String))
        End With
        Grid1.DataSource = ds.Tables("NmetalSampleExceptionUser")
        With ds.Tables.Add("NmetalSampleDepWeightCheckDel")
            .Columns.Add("AutoID", GetType(String))
        End With
    End Sub
    ''' <summary>
    ''' 确认按钮事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Savebutton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Savebutton.Click
        If ds.Tables("NmetalSampleExceptionUser").Rows.Count < 0 Then
            MsgBox("表中无数据，无法保存！", MsgBoxStyle.OkOnly, "提示")
            Exit Sub
        End If
        SaveData()

    End Sub
    ''' <summary>
    ''' 保存数据
    ''' </summary>
    ''' <remarks></remarks>
    Sub SaveData()

        Dim i As Integer
        For i = 0 To ds.Tables("NmetalSampleDepWeightCheckDel").Rows.Count - 1
            If nseu.NmetalSampleExceptionUser_Delete(ds.Tables("NmetalSampleDepWeightCheckDel").Rows(i)("AutoID").ToString) = False Then
                MsgBox("刪除当前选定记录失敗，请检查原因！", 60, "提示")
                Exit Sub
            End If
        Next
        For i = 0 To ds.Tables("NmetalSampleExceptionUser").Rows.Count - 1
            objinfo.PE_User = ds.Tables("NmetalSampleExceptionUser").Rows(i)("PE_User")
            objinfo.PE_Name = ds.Tables("NmetalSampleExceptionUser").Rows(i)("PE_Name")

            objinfo.PE_Type = "W"           '2014-08-21    Mark


            If IsDBNull(ds.Tables("NmetalSampleExceptionUser").Rows(i)("AutoID")) = True Then
                If nseu.NmetalSampleExceptionUser_Add(objinfo) = False Then
                    MsgBox(ds.Tables("NmetalSampleExceptionUser").Rows(i)("AutoID") & "，请检查原因！", 60, "提示")
                    Exit Sub
                End If
            Else
                objinfo.AutoID = ds.Tables("NmetalSampleExceptionUser").Rows(i)("AutoID")
                If nseu.NmetalSampleExceptionUser_Update(objinfo) = False Then
                    MsgBox(ds.Tables("NmetalSampleExceptionUser").Rows(i)("AutoID") & "，请检查原因！", 60, "提示")
                    Exit Sub
                End If
            End If
        Next

        MsgBox("保存成功!", MsgBoxStyle.OkOnly, "提示")
        Me.Close()
    End Sub
    ''' <summary>
    ''' 删除按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_Del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Del.Click
        If GridView2.RowCount > 0 Then
            Dim deltemp As String = ""
            deltemp = ds.Tables("NmetalSampleExceptionUser").Rows(GridView2.FocusedRowHandle)("AutoID").ToString
            ds.Tables("NmetalSampleExceptionUser").Rows.RemoveAt(GridView2.FocusedRowHandle)

            Dim row As DataRow
            If deltemp <> "" Then
                row = ds.Tables("NmetalSampleDepWeightCheckDel").NewRow
                row("AutoID") = deltemp
                ds.Tables("NmetalSampleDepWeightCheckDel").Rows.Add(row)
            End If
        End If
    End Sub
    ''' <summary>
    ''' 取消按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub

    Private Sub frmNmetalSampleExceptionUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()
        LoadData(Nothing)

    End Sub
    ''' <summary>
    ''' 加载数据
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadData(ByVal PE_User As String)
        Dim objinfo As New List(Of NmetalSamplePaceTypeBriNameInfo)
        objinfo = nseu.NmetalSampleExceptionUser_GetList(PE_User, Nothing, "W")     '2014-08-21     Mark
        If objinfo.Count <= 0 Then
            Exit Sub
        End If
        Dim i As Integer
        For i = 0 To objinfo.Count - 1
            Dim row As DataRow
            row = ds.Tables("NmetalSampleExceptionUser").NewRow

            row("AutoID") = objinfo(i).AutoID
            row("PE_User") = objinfo(i).PE_User
            row("PE_Name") = objinfo(i).PE_Name
            ds.Tables("NmetalSampleExceptionUser").Rows.Add(row)
        Next
    End Sub
End Class