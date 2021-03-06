Imports LFERP.Library.NmetalSampleManager.NmetalSampInventoryCheck
Imports LFERP.SystemManager
Imports LFERP.Library.PieceProcess
Imports LFERP.Library.NmetalSampleManager.NmetalSampleCollection

Public Class frmNmetalSampInventoryMain
    Dim ds As New DataSet
    Dim pncon As New PersonnelControl
    Private Sub frmSampInventoryMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTables()
        Dim pmlist As New List(Of PersonnelInfo) '部門分享
        pmlist = pncon.FacBriSearch_GetList(Nothing, Nothing, Nothing, Nothing)
        gluStateD_ID.Properties.DisplayMember = "DepName"
        gluStateD_ID.Properties.ValueMember = "DepID"
        gluStateD_ID.Properties.DataSource = pmlist
    End Sub

    Sub CreateTables()
        ds.Tables.Clear()
        With ds.Tables.Add("SampInventory") '子配件表
            .Columns.Add("Code_ID", GetType(String))
            .Columns.Add("StatusType", GetType(String))
            .Columns.Add("StatusTypeName", GetType(String))
            .Columns.Add("Qty", GetType(String))
            .Columns.Add("SO_SampleID", GetType(String))
            .Columns.Add("D_Dep", GetType(String))
            .Columns.Add("BarCode", GetType(String))
        End With
        Grid1.DataSource = ds.Tables("SampInventory")
        With ds.Tables.Add("SampLogin") '子配件表
            .Columns.Add("Code_ID", GetType(String))
            .Columns.Add("YesNo", GetType(String))
            .Columns.Add("Qty", GetType(String))
        End With
        Grid2.DataSource = ds.Tables("SampLogin")
    End Sub

    Private Sub SimpleButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton3.Click
        If gluStateD_ID.EditValue = String.Empty Then
            MsgBox("部门不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
            gluStateD_ID.Focus()
            Exit Sub
        End If

        Dim i As Integer
        Dim strCode_ID As String = String.Empty
        Dim strStatusType As String = String.Empty
        For i = 0 To ds.Tables("SampInventory").Rows.Count - 1
            strCode_ID = ds.Tables("SampInventory").Rows(i)("BarCode").ToString
            If strCode_ID <> String.Empty Then
                MsgBox("存在扫描条码,不能查询！", MsgBoxStyle.Information, "溫馨提示")
                Exit Sub
            End If

        Next

        Dim strD_ID As String = gluStateD_ID.EditValue
        Dim spc As New NmetalSampleCollectionControler
        Dim spl As New List(Of NmetalSampleCollectionInfo)

        spl = spc.NmetalSampleCollection_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, strD_ID, Nothing, Nothing, Nothing, Nothing)
        If spl.Count = 0 Then
            Exit Sub
        Else
            Dim boolSt As Boolean = False
            ds.Tables("SampInventory").Clear()
            For i = 0 To spl.Count - 1
                Dim row As DataRow
                row = ds.Tables("SampInventory").NewRow
                strStatusType = spl(i).StatusType
                If strStatusType = "T" Then
                    boolSt = True
                End If

                row("Code_ID") = spl(i).Code_ID
                row("StatusTypeName") = spl(i).StatusTypeName
                row("StatusType") = spl(i).StatusType
                row("Qty") = spl(i).Qty
                row("SO_SampleID") = spl(i).SO_SampleID
                row("D_Dep") = spl(i).D_Dep
                row("BarCode") = ""
                ds.Tables("SampInventory").Rows.Add(row)
            Next
            If boolSt = True Then
                ds.Tables("SampInventory").Clear()
                MsgBox("此部门已经调整过!")
                Exit Sub
            End If
        End If

    End Sub

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub

    Private Sub txtM_Code_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtM_Code.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtM_Code.Text = String.Empty Then
                MsgBox("条码不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
                txtM_Code.Focus()
                Exit Sub
            End If
            '-----------------------------------------------------------------------
            Dim i As Integer
            Dim strCode_ID As String = String.Empty
            Dim boolKey As Boolean = False
            For i = 0 To ds.Tables("SampInventory").Rows.Count - 1
                strCode_ID = ds.Tables("SampInventory").Rows(i)("Code_ID").ToString
                If strCode_ID = txtM_Code.Text Then
                    boolKey = True
                    ds.Tables("SampInventory").Rows(i)("BarCode") = txtM_Code.Text
                End If
            Next

            If boolKey = False Then
                Dim strGetCode As String = String.Empty
                Dim boolGetKey As Boolean = False
                For i = 0 To ds.Tables("SampLogin").Rows.Count - 1
                    strGetCode = ds.Tables("SampLogin").Rows(i)("Code_ID").ToString
                    If strGetCode = txtM_Code.Text Then
                        boolGetKey = True
                        ds.Tables("SampLogin").Rows(i)("Qty") = CInt(ds.Tables("SampLogin").Rows(i)("Qty")) + 1
                    End If
                Next
                If boolGetKey = False Then
                    Dim row As DataRow
                    row = ds.Tables("SampLogin").NewRow
                    row("Code_ID") = txtM_Code.Text
                    row("YesNo") = "N"
                    row("Qty") = 1
                    ds.Tables("SampLogin").Rows.Add(row)
                End If
                MsgBox("条码本部门不存在,请上交部门负责人!")
                txtM_Code.Text = String.Empty
                Exit Sub
            Else
                Dim strGetCode As String = String.Empty
                Dim boolGetKey As Boolean = False
                Dim intQty As Integer = 0
                For i = 0 To ds.Tables("SampLogin").Rows.Count - 1
                    strGetCode = ds.Tables("SampLogin").Rows(i)("Code_ID").ToString
                    If strGetCode = txtM_Code.Text Then
                        boolGetKey = True
                        MsgBox("此条码重复,请上交部门负责人!")
                        ds.Tables("SampLogin").Rows(i)("Qty") = CInt(ds.Tables("SampLogin").Rows(i)("Qty")) + 1
                    End If
                Next

                If boolGetKey = False Then
                    Dim row As DataRow
                    row = ds.Tables("SampLogin").NewRow
                    row("Code_ID") = txtM_Code.Text
                    row("YesNo") = ""
                    row("Qty") = 1
                    ds.Tables("SampLogin").Rows.Add(row)
                End If

            End If
            '-----------------------------------------------------------
            txtM_Code.Text = String.Empty
        End If
        Me.GridView3.ActiveFilterString = "[BarCode] ='' "
    End Sub

    Private Sub SimpleButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton4.Click
        If GridView3.RowCount = 0 Then Exit Sub
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Title = "導出Excel"
        saveFileDialog.Filter = "Excel2003文件(*.xls)|*.xls"
        Dim FiledialogResult As DialogResult = saveFileDialog.ShowDialog(Me)
        If FiledialogResult = Windows.Forms.DialogResult.OK Then
            If ExportToExcelOld(Grid1, saveFileDialog.FileName) Then
                MsgBox("已成功導出到：" + saveFileDialog.FileName, MsgBoxStyle.Information, "提示")
            End If
        End If
    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        Dim spc As New NmetalSampleCollectionControler
        Dim scinfo As New NmetalSampleCollectionInfo
        If MsgBox("您确认要调整吗?每个部门只能修改一次", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Dim strBarCode As String = String.Empty
            Dim strCode_ID As String = String.Empty
            Dim i As Integer
            For i = 0 To ds.Tables("SampInventory").Rows.Count - 1
                strBarCode = ds.Tables("SampInventory").Rows(i)("BarCode").ToString
                strCode_ID = ds.Tables("SampInventory").Rows(i)("Code_ID").ToString

                scinfo.StatusType = ds.Tables("SampInventory").Rows(i)("StatusTypeName").ToString
                scinfo.Code_ID = ds.Tables("SampInventory").Rows(i)("Code_ID").ToString
                scinfo.Qty = 1
                scinfo.BarCode = ds.Tables("SampInventory").Rows(i)("BarCode").ToString
                scinfo.D_ID = ds.Tables("SampInventory").Rows(i)("D_Dep").ToString
                scinfo.AddUserID = InUserID
                If strBarCode = String.Empty Then
                    scinfo.BarCode = "条码禁用"
                    If spc.NmetalSampleCollection_UpdateA(strCode_ID, "T") = False Then
                        MsgBox(strCode_ID & ",修改状态错误!")
                        Exit Sub
                    End If
                End If

                If spc.NmetalSampleCollectionLogin_Add(scinfo) = False Then
                    MsgBox(strCode_ID & ",修改状态错误!")
                    Exit Sub
                End If
            Next
        End If
        MsgBox("调整完成")
    End Sub

    Private Sub cmdExcelA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcelA.Click
        If GridView2.RowCount = 0 Then Exit Sub
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Title = "導出Excel"
        saveFileDialog.Filter = "Excel2003文件(*.xls)|*.xls"
        Dim FiledialogResult As DialogResult = saveFileDialog.ShowDialog(Me)
        If FiledialogResult = Windows.Forms.DialogResult.OK Then
            If ExportToExcelOld(Grid2, saveFileDialog.FileName) Then
                MsgBox("已成功導出到：" + saveFileDialog.FileName, MsgBoxStyle.Information, "提示")
            End If
        End If

    End Sub
End Class