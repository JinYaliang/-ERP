Imports System.Collections.Specialized
Imports System.Math
Imports Microsoft.VisualBasic
Imports LFERP.Library.ProductionPieceFormula


Public Class frmProductionFormula
    Dim 日薪, 上班天數, 平日加班, 假日加班, 計時工資 As Single
    ''' <summary>
    ''' 拖動數據
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TVwGS_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles TVwFiled.ItemDrag
        On Error Resume Next
        If TVwFiled.SelectedNode.Level = 1 Then
            TVwFiled.DoDragDrop(TVwFiled.SelectedNode.Text, DragDropEffects.All)
        End If
    End Sub

    Private Sub TVwCalr_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles TVwCalr.ItemDrag
        On Error Resume Next
        If TVwCalr.SelectedNode.Level = 1 Then
            TVwCalr.DoDragDrop(TVwCalr.SelectedNode.Text, DragDropEffects.All)
        End If
    End Sub
    ''' <summary>
    ''' 雙擊加入字段
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TVwFiled_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TVwFiled.DoubleClick
        On Error Resume Next
        If TVwFiled.SelectedNode.Level = 1 Then
            InsertStrToText(RichTextBoxFormula, TVwFiled.SelectedNode.Text, False)
        End If
    End Sub

    Private Sub TVwCalr_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TVwCalr.DoubleClick
        On Error Resume Next
        If TVwCalr.SelectedNode.Level = 1 Then
            InsertStrToText(RichTextBoxFormula, TVwCalr.SelectedNode.Text, False)
        End If
    End Sub

    ''' <summary>
    ''' 插入字段到光標停留的位置
    ''' </summary>
    ''' <param name="aobjTxt"></param>
    ''' <param name="astrValue"></param>
    ''' <param name="ablnLf"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertStrToText(ByVal aobjTxt As RichTextBox, ByVal astrValue As String, Optional ByVal ablnLf As Boolean = False) As String
        InsertStrToText = ""

        Dim lngPos As Long
        lngPos = aobjTxt.SelectionStart
        If lngPos > 0 And ablnLf = False Then
            aobjTxt.Text = Microsoft.VisualBasic.Left(aobjTxt.Text.ToString, lngPos) & astrValue & Mid(aobjTxt.Text, lngPos + 1)
        ElseIf ablnLf = True Then
            aobjTxt.Text = astrValue & vbCrLf & aobjTxt.Text
        Else
            aobjTxt.Text = astrValue & aobjTxt.Text
        End If
        aobjTxt.Focus()
        If ablnLf = False Then
            aobjTxt.SelectionStart = lngPos + Len(astrValue)
        Else
            aobjTxt.SelectionStart = lngPos + Len(astrValue & vbCrLf)
        End If
    End Function
    ''' <summary>
    ''' 刷新
    ''' </summary>
    ''' <remarks></remarks>
    Sub Refresh_Formula()
        Dim fgc As New ProductionPieceFormulaControl
        Grid1.DataSource = fgc.ProductionPieceFormula_GetList(Nothing, Nothing, Nothing, Nothing)

        ComboFormulaName.Text = ""
        CheckAllow.Checked = False
    End Sub
    ''' <summary>
    ''' 載入工式
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.Click
        If GridView1.RowCount <= 0 Then
            Exit Sub
        End If
        ComboFormulaName.Text = GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "FormulaName")
        loadDate(ComboFormulaName.Text)
    End Sub
    ''' <summary>
    ''' 檢查工式是否有誤
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SimpButtonCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpButtonCheck.Click
        strMsg = ""

        Dim expression As String = RichTextBoxFormula.Text
        Dim parameters As NameValueCollection = New NameValueCollection()
        parameters.Add("日薪", "80")   '其下為測試數據
        parameters.Add("上班天數", "20")
        parameters.Add("平日加班", "10")
        parameters.Add("假日加班", "5")

        Dim results() As Decimal = Calculator.Eval(expression, parameters)

        If strMsg <> "" Then
        Else
            Labelxx.Text = results(0)
            MsgBox("工式無誤!")
        End If
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' 先檢查工式有無錯誤碼，再進行保存
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        strMsg = ""

        Dim expression As String = RichTextBoxFormula.Text
        Dim parameters As NameValueCollection = New NameValueCollection()
        parameters.Add("日薪", "80")
        parameters.Add("上班天數", "20")
        parameters.Add("平日加班", "10")
        parameters.Add("假日加班", "5")

        Dim results() As Decimal = Calculator.Eval(expression, parameters)

        If strMsg <> "" Then
        Else
            Save_Formula()
        End If


    End Sub
    ''' <summary>
    ''' 保存工式
    ''' </summary>
    ''' <remarks></remarks>
    Sub Save_Formula()
        Dim fi As New ProductionPieceFormulaInfo
        Dim fc As New ProductionPieceFormulaControl
        Dim fl As New List(Of ProductionPieceFormulaInfo)

        Dim Save_Update As String

        If ComboFormulaName.Text = "" Then
            MsgBox("公式編號不能為空！")
            Exit Sub
        End If

        fl = fc.ProductionPieceFormula_GetList(Nothing, Nothing, ComboFormulaName.Text, Nothing)

        If fl.Count <= 0 Then
            Save_Update = "Add"
        Else
            If MsgBox(ComboFormulaName.Text & "的記錄已存在，確定是否繼續?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Save_Update = "Update"
            Else
                Exit Sub
            End If
        End If

        fi.FormulaName = ComboFormulaName.Text
        fi.Formula = RichTextBoxFormula.Text

        If CheckAllow.Checked = True Then
            fi.InCheck = True
        Else
            fi.InCheck = False
        End If

        If Save_Update = "Add" Then
            If fc.ProductionPieceFormula_Add(fi) = True Then
                MsgBox("保存成功！")
                Refresh_Formula()
            Else
                MsgBox("保存失敗!")
            End If
        Else
            If fc.ProductionPieceFormula_Update(fi) = True Then
                MsgBox("更新成功！")
                ComboFormulaName.Text = ""
                fi.InCheck = False
                RichTextBoxFormula.Text = ""
            Else
                MsgBox("更新失敗！")
            End If
        End If

    End Sub

    ''' <summary>
    ''' 載入數據
    ''' </summary>
    ''' <param name="strFormula"></param>
    ''' <remarks></remarks>
    Sub loadDate(ByVal strFormula As String)

        Dim fl As New List(Of ProductionPieceFormulaInfo)
        Dim fc As New ProductionPieceFormulaControl

        fl = fc.ProductionPieceFormula_GetList(Nothing, Nothing, strFormula, Nothing)

        If fl.Count > 0 Then
            RichTextBoxFormula.Text = fl(0).Formula
            CheckAllow.Checked = fl(0).InCheck
        End If
    End Sub
    ''' <summary>
    ''' 刪除工式(工式名)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DelToolStrip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DelToolStrip.Click
        Dim strA As String

        If GridView1.RowCount <= 0 Then Exit Sub

        strA = GridView1.GetFocusedRowCellValue("FormulaName")

        If strA = "" Then Exit Sub

        If MsgBox("你確定刪除工式名為:  '" & strA & "'  的記錄嗎?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Dim fc As New ProductionPieceFormulaControl

            If fc.ProductionPieceFormula_Delete(Nothing, strA) = True Then
                MsgBox("刪除成功!")

                Refresh_Formula()
            Else
                MsgBox("刪除失敗!")
            End If
        End If

    End Sub
    ''' <summary>
    ''' 填定關鍵字顏色
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RichTextBoxFormula_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBoxFormula.TextChanged
        If RichTextBoxFormula.Text = "" Then Exit Sub

        Dim StrAarray As Array = Split("+,-,*,/,RoundEx", ",") '任意添加关键字。 

        Dim l As Long, T, TS As String, d As Long

        T = RichTextBoxFormula.Text
        d = RichTextBoxFormula.SelectionStart

        RichTextBoxFormula.SelectionStart = 0
        RichTextBoxFormula.SelectionLength = Len(T)
        RichTextBoxFormula.SelectionColor = Color.Black

        For i As Integer = 0 To UBound(StrAarray)
            TS = Trim(StrAarray(i))
            l = InStr(1, T, StrAarray(i), CompareMethod.Text)

            If l = 0 Then GoTo n
            RichTextBoxFormula.SelectionStart = l - 1
            RichTextBoxFormula.SelectionLength = Len(TS)
            RichTextBoxFormula.SelectionColor = Color.Blue
            RichTextBoxFormula.SelectionStart = l + Len(TS) - 1
            RichTextBoxFormula.SelectionColor = Color.Black
            Do Until l = 0
                l = InStr(l + 1, T, StrAarray(i), CompareMethod.Text)
                If l = 0 Then Exit Do
                RichTextBoxFormula.SelectionStart = l - 1
                RichTextBoxFormula.SelectionLength = Len(TS)
                RichTextBoxFormula.SelectionColor = Color.Blue
                RichTextBoxFormula.SelectionStart = l + Len(TS) - 1
                RichTextBoxFormula.SelectionColor = Color.Black
            Loop
n:      Next
        RichTextBoxFormula.SelectionStart = d
        RichTextBoxFormula.SelectionLength = 0

    End Sub

 
    Private Sub frmProductionFormula_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TVwFiled.ExpandAll()
        TVwCalr.ExpandAll()
        TVwCalr.Nodes(0).Nodes(4).ToolTipText = "四舍五入.如" & vbCrLf & "RoundEx(1.2345,0)=1" & vbCrLf & "RoundEx(1.2345,3)=1.235"

        Refresh_Formula()

        ComboFormulaName.Select()
        ComboFormulaName.Focus()
    End Sub


    Private Sub StripCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StripCopy.Click
        RichTextBoxFormula.Copy()
    End Sub

    Private Sub StripPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StripPaste.Click
        RichTextBoxFormula.Paste()
    End Sub

    Private Sub StripCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StripCut.Click
        RichTextBoxFormula.Cut()
    End Sub

    Private Sub TVwFiled_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TVwFiled.AfterSelect

    End Sub
End Class



