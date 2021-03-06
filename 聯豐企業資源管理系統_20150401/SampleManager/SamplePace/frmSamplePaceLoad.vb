Imports LFERP.Library.PieceProcess
Imports LFERP.Library.SampleManager.SampleOrdersMain


Public Class frmSamplePaceLoad
    Dim pncon As New PersonnelControl
    Private Sub frmSamplePaceLoad_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim pmlist As New List(Of PersonnelInfo) '部門分享
        pmlist = pncon.FacBriSearch_GetList(Nothing, Nothing, Nothing, Nothing)
        gluD_ID.Properties.DisplayMember = "DepName"
        gluD_ID.Properties.ValueMember = "DepID"
        gluD_ID.Properties.DataSource = pmlist
        '載入訂單編號
        Dim mtd As New SampleOrdersMainControler
        gluSO_ID.Properties.DisplayMember = "SO_ID"
        gluSO_ID.Properties.ValueMember = "SO_ID"
        gluSO_ID.Properties.DataSource = mtd.SampleOrdersMain_GetListItem(Nothing, Nothing, Nothing, True)
    End Sub

    Private Sub gluSO_ID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluSO_ID.EditValueChanged
        If gluSO_ID.EditValue <> String.Empty Then
            Dim strM As String = gluSO_ID.EditValue

            Dim mc As New SampleOrdersMainControler
            gluPM_M_Code.Properties.DisplayMember = "PM_M_Code"
            gluPM_M_Code.Properties.ValueMember = "PM_M_Code"
            gluPM_M_Code.Properties.DataSource = mc.SampleOrdersMain_GetList(strM, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            txtSS_Edition.Text = GridView7.GetFocusedRowCellValue("SS_Edition")
            gluPM_M_Code.EditValue = GridView7.GetFocusedRowCellValue("PM_M_Code")
        End If
    End Sub

    Private Sub ButtonOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOK.Click
        If gluD_ID.EditValue = String.Empty Then
            tempValue = Nothing
        Else
            tempValue = gluD_ID.EditValue
        End If

        If gluSO_ID.EditValue = String.Empty Then
            tempValue2 = Nothing
            tempValue3 = Nothing
            tempValue4 = Nothing
        Else
            tempValue2 = gluSO_ID.EditValue
            tempValue3 = txtSS_Edition.Text
            tempValue4 = gluPM_M_Code.EditValue
        End If

  
        Me.Close()
    End Sub
End Class