Imports LFERP.FileManager

Module mblFileManager
    '�B�z���ե�
    ''' <summary>
    ''' �B�z���ե�
    ''' </summary>
    ''' <param name="FB_Type">�Ҷ����O,�p���~��� </param>
    ''' <param name="FB_TypeNo">���ݼҶ����O�s��,�p���~�s��</param>
    ''' <param name="blnOpen">�d���v��</param>
    ''' <param name="blnUpdate">�W���v��</param>
    ''' <param name="blnDown">�U���v��</param>
    ''' <param name="blnDel">�R���v��</param>
    ''' <param name="blnDetail">�Բ��v��</param>
    ''' <remarks></remarks>
    Public Sub FileShow(ByVal FB_Type As String, ByVal FB_TypeNo As String, ByVal blnOpen As Boolean, ByVal blnUpdate As Boolean _
        , ByVal blnDown As Boolean, ByVal blnEdit As Boolean, ByVal blnDel As Boolean, ByVal blnDetail As Boolean)
        Dim HeadName As String
        HeadName = "�d�ݪ��� - [ " & FB_Type & "-" & FB_TypeNo & " ]"
        Dim frTemp As New Form

        For Each frTemp In MDIMain.MdiChildren
            If TypeOf frTemp Is frmFileShow Then
                If frTemp.Text = HeadName Then
                    frTemp.Activate()
                    Exit Sub
                End If
            End If
        Next
        frTemp = Nothing


        Dim fr As New frmFileShow
        '�եε��f

        fr.popFileShowOpen.Enabled = blnOpen
        fr.popFileShowUpdate.Enabled = blnUpdate
        fr.popFileShowDown.Enabled = blnDown
        fr.popFileShowEdit.Enabled = blnEdit
        fr.popFileShowDel.Enabled = blnDel
        fr.popFileShowDetail.Enabled = blnDetail
        fr.Text = HeadName
        fr.Label2.Text = FB_Type
        fr.Label3.Text = FB_TypeNo
        ' GetFileList(fr.lvwFile, FB_Type, FB_TypeNo)
        Dim dt As New FileController
        fr.Grid.AutoGenerateColumns = False
        fr.Grid.DataSource = dt.FileBond_GetList(FB_Type, FB_TypeNo, Nothing)

        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    ''' �d�ݤ��
    ''' </summary>
    ''' <param name="FB_Type">�Ҷ����O,�p���~���</param>
    ''' <param name="FB_TypeNo">���ݼҶ����O�s��,�p���~�s��</param>
    ''' <param name="blnOpen">�d���v��</param>
    ''' <remarks></remarks>
    Public Sub FileShowDisplay(ByVal FB_Type As String, ByVal FB_TypeNo As String, ByVal blnOpen As Boolean)
        Dim HeadName As String
        HeadName = "�d�ݪ��� - [ " & FB_Type & "-" & FB_TypeNo & " ]"
        '�եε��f

        frmFileShowDisplay.popFileShowOpen.Enabled = blnOpen

        frmFileShowDisplay.Text = HeadName
        frmFileShowDisplay.Label2.Text = FB_Type
        frmFileShowDisplay.Label3.Text = FB_TypeNo

        Dim dt As New FileController
        frmFileShowDisplay.Grid.AutoGenerateColumns = False
        frmFileShowDisplay.Grid.DataSource = dt.FileBond_GetList(FB_Type, FB_TypeNo, Nothing)

        frmFileShowDisplay.MdiParent = MDIMain
        frmFileShowDisplay.WindowState = FormWindowState.Maximized
        frmFileShowDisplay.ShowDialog()

    End Sub

    Sub GetFileList(ByVal ListView As System.Windows.Forms.ListView, ByVal FB_Type As String, ByVal FB_TypeNo As String)
        Dim dt As New FileController
        Dim i As Long
        Dim obj As New List(Of FilesInfo)
        ListView.Items.Clear()
        obj = dt.FileBond_GetList(FB_Type, FB_TypeNo, Nothing)
        For i = 0 To obj.Count - 1
            Dim lst As New Windows.Forms.ListViewItem
            lst = ListView.Items.Add(obj(i).F_No, 0)
            lst.SubItems.Add(obj(i).F_OldName)
            lst.SubItems.Add(obj(i).F_Name)
            lst.SubItems.Add(obj(i).F_Detail)
            lst.SubItems.Add(obj(i).F_AddDate)
            lst.SubItems.Add(obj(i).F_Action)
        Next

    End Sub

End Module
