Imports System
Imports LFERP.SystemManager
Imports LFERP.Library.SampleManager.SampleCollection
Imports LFERP.Library.SampleManager.SamplePace
Imports LFERP.Library.SampleManager.SampleSetting

Public Class frmSampleCollection
    Dim ds As New DataSet
    Dim scCon As New SampleCollectionControler

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmSampleCollection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmdRef_Click(Nothing, Nothing)
        PowerUser()
    End Sub
    '设置权限
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890701")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890702")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdDel.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890703")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdInventoryDrep.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890705")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdInv.Enabled = True
        End If
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Dim fr As New frmSampleBarcode
        fr = New frmSampleBarcode
        fr.Lbl_Title.Text = "条码採集"
        fr.EditItem = "SampleCollection"
        'fr.EditValue = GridView3.GetFocusedRowCellValue("PM_M_Code")
        'fr.SO_ID = GridView3.GetFocusedRowCellValue("SO_ID")
        'fr.SS_Edition = GridView3.GetFocusedRowCellValue("SS_Edition")
        'fr.SS_Qty = GridView3.GetFocusedRowCellValue("SP_Qty")
        'fr.SP_ID = GridView3.GetFocusedRowCellValue("SP_ID")
        fr.ShowDialog()
        frmSampleCollection_Load(Nothing, Nothing)
    End Sub

    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        If GridView3.RowCount = 0 Then Exit Sub
        Dim StrAutoID As String
        StrAutoID = GridView3.GetFocusedRowCellValue("AutoID").ToString()

        Dim spc As New SampleCollectionControler
        Dim spl As New List(Of SampleCollectionInfo)
        spl = spc.SampleCollection_Getlist(Nothing, Nothing, StrAutoID, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If spl.Count <= 0 Then
            MsgBox("數據已刪除！")
            Exit Sub
        End If
        If spl(0).StatusType <> String.Empty Then
            MsgBox("存在样办交易資料,無法刪除", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
        '----------------------------------------------------------
        If MsgBox("確定要刪除名称為： '" & GridView3.GetFocusedRowCellValue("Code_ID").ToString & "' 的條碼嗎?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If scCon.SampleCollection_Delete(Nothing, StrAutoID) = True Then
                MsgBox("刪除当前記錄信息成功！", 60, "提示")
                frmSampleCollection_Load(Nothing, Nothing)
            Else
                MsgBox("刪除当前选定記錄失敗，请檢查原因！", 60, "提示")
                Exit Sub
            End If
        End If
    End Sub

    Private Sub cmdRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRef.Click
        Dim msi As New List(Of SampleSettingInfo)
        Dim msc As New SampleSettingController

        Dim StrCheck As String = Nothing
        Dim StrUser As String = Nothing
        Dim StrD_ID As String = Nothing
        Dim StrType As String = Nothing
        Dim StrOrdersBeginDate As String = Nothing

        msi = msc.SampleSetting_GetList(InUserID)
        If msi.Count > 0 Then
            '1.審核類型
            Select Case msi(0).SampleCollectionCheck
                Case "0,1"
                    StrCheck = Nothing
                Case "1"
                    StrCheck = "True"
                Case "0"
                    StrCheck = "False"
            End Select

            '1.用戶選擇
            If msi(0).SampleCollectionCreateUserID = "All" Then
                StrUser = Nothing
            Else
                StrUser = msi(0).SampleCollectionCreateUserID
            End If
            '2.部门選擇
            If msi(0).SampleCollectionD_ID = "All" Then
                StrD_ID = Nothing
            Else
                StrD_ID = msi(0).SampleCollectionD_ID
            End If
            '2.状态選擇
            If msi(0).SampleCollectionStatusType = "All" Then
                StrType = Nothing
            Else
                StrType = msi(0).SampleCollectionStatusType
            End If
            gridSampleCollection.DataSource = scCon.SampleCollection_Getlist(StrType, Nothing, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, StrD_ID, msi(0).SampleCollectionBeginDate, Nothing, StrUser, Nothing)
        Else
            gridSampleCollection.DataSource = scCon.SampleCollection_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        End If
        Me.GridView3.ActiveFilterString = " [StatusType]<>'T' "
    End Sub

    Private Sub GridView3_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView3.FocusedRowChanged
        If GridView3.RowCount > 0 Then
            Try
                Dim SPQC As New SamplePaceQueryController
                Dim strCode_ID As String = GridView3.GetFocusedRowCellValue("Code_ID").ToString
                ' Me.GridBarCode.DataSource = SPQC.SamplePaceQuery_Getlist(Nothing, Nothing, strCode_ID, Nothing, Nothing, Nothing, True)
                Me.GridBarCode.DataSource = SPQC.SamplePaceQueryA_Getlist(strCode_ID)
            Catch
            End Try
        End If
    End Sub

    Private Sub cmdQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuery.Click
        Dim myfrm As New frmSampleCollectionQ
        Dim spc As New SampleCollectionControler
        myfrm.ShowDialog()

        If Mid(tempValue2, 1, 3) = "[A]" Then
            gridSampleCollection.DataSource = spc.SampleCollection_Getlist(Nothing, tempValue, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        End If
        If Mid(tempValue2, 1, 3) = "[B]" Then
            gridSampleCollection.DataSource = spc.SampleCollection_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, tempValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        End If
        If Mid(tempValue2, 1, 3) = "[C]" Then
            gridSampleCollection.DataSource = spc.SampleCollection_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, tempValue, Nothing, Nothing, Nothing, Nothing)
        End If
        If Mid(tempValue2, 1, 3) = "[D]" Then
            gridSampleCollection.DataSource = spc.SampleCollection_Getlist(tempValue, Nothing, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        End If

        tempValue = ""
        tempValue2 = ""
    End Sub

    Private Sub cmdExcelA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcelA.Click
        If GridView3.RowCount = 0 Then Exit Sub
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Title = "導出Excel"
        saveFileDialog.Filter = "Excel2003文件(*.xls)|*.xls"
        Dim FiledialogResult As DialogResult = saveFileDialog.ShowDialog(Me)
        If FiledialogResult = Windows.Forms.DialogResult.OK Then
            If ExportToExcelOld(gridSampleCollection, saveFileDialog.FileName) Then
                MsgBox("已成功導出到：" + saveFileDialog.FileName, MsgBoxStyle.Information, "提示")
            End If
        End If
    End Sub

    Private Sub cmdExcelB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcelB.Click
        If GridView2.RowCount = 0 Then Exit Sub
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Title = "導出Excel"
        saveFileDialog.Filter = "Excel2003文件(*.xls)|*.xls"
        Dim FiledialogResult As DialogResult = saveFileDialog.ShowDialog(Me)
        If FiledialogResult = Windows.Forms.DialogResult.OK Then
            If ExportToExcelOld(GridBarCode, saveFileDialog.FileName) Then
                MsgBox("已成功導出到：" + saveFileDialog.FileName, MsgBoxStyle.Information, "提示")
            End If
        End If
    End Sub

    Private Sub cmdInventoryDrep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInventoryDrep.Click
        On Error Resume Next
        Dim fr As frmSampInventoryCheckLoginMain
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampInventoryCheckLoginMain Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampInventoryCheckLoginMain
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmdInv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInv.Click
        On Error Resume Next
        Dim fr As frmSampInventoryMain
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampInventoryMain Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampInventoryMain
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub gridSampleCollection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gridSampleCollection.Click

    End Sub
End Class