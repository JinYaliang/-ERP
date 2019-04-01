Imports LFERP.Library.WareHouse
Imports LFERP.Library.KnifeWare
Imports LFERP.SystemManager

Public Class frmWhiteUser

#Region "�Ǧ^���ܮw�����X�M���"

    Private _KnifeWare As String

    Public Property KnifeWare() As String
        Get
            Return "�M���"
        End Get
        Set(ByVal value As String)
            _KnifeWare = value
        End Set
    End Property

#End Region

    Private Sub WhiteUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim mt As New WareHouseController
        mt.WareHouse_LoadToTreeView(TreeView1, WareSelect(InUserID, "510904"))

        LoadUserPower()

    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect

        Dim wuc As New KnifeWhiteUserController

        If e.Node.Level = 1 Then
            Grid1.DataSource = wuc.WhiteUser_GetList(Nothing, Nothing, TreeView1.SelectedNode.Tag, Nothing, Nothing, False)
        End If

    End Sub

    Private Sub AddpopMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddpopMenuItem.Click

        Try
            If TreeView1.SelectedNode.Level = 1 Then
                '2013-11-21
                If GetNightWareHouse(TreeView1.SelectedNode.Tag) = True Then
                    MsgBox("���ܬ��]�Z��,����]�m�զW��!")
                    Exit Sub
                End If

                Dim wuc As New KnifeWhiteUserController
                Dim fr As New frmWhiteUserSub
                fr.WHID = TreeView1.SelectedNode.Tag
                fr.WareType = TreeView1.SelectedNode.Text
                fr.EditType = "newAdd"
                fr.ShowDialog()
                If fr.isNew = True Then
                    LoadData(Nothing, TreeView1.SelectedNode.Tag)
                End If
            Else
                MsgBox("�п�ܭܮw�I", 64, "����")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox("�п�ܭܮw�I", 64, "����")
        End Try

    End Sub

    Private Sub EditpopMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditpopMenuItem.Click

        Dim fr As New frmWhiteUserSub
        Dim strW_UserID As String

        strW_UserID = GridView2.GetFocusedRowCellValue("W_UserID")
        If strW_UserID = Nothing Then
            MsgBox("�S���i�H�ק諸�Τ�I", 64, "����")
            Exit Sub
        Else
            Dim wuc As New KnifeWhiteUserController
            fr.AutoID = GridView2.GetFocusedRowCellValue("AutoID").ToString
            fr.EditType = "update"
            fr.ShowDialog()

            If fr.isEdit = True Then
                LoadData(strW_UserID, GridView2.GetFocusedRowCellValue("WH_ID"))
            End If
        End If

    End Sub

    Private Sub DeletepopMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeletepopMenuItem.Click

        If GridView2.RowCount <= 0 Then
            Exit Sub
        End If

        Dim wuc As New KnifeWhiteUserController
        Dim intAutoID As Integer

        intAutoID = GridView2.GetFocusedRowCellValue("AutoID")
        If intAutoID = Nothing Then
            MsgBox("�S���i�H�R�����Τ�I", 64, "����")
            Exit Sub
        Else
            If MsgBox("�T�w�R���u����" + GridView2.GetFocusedRowCellValue("W_UserID") + "���Τ�H", MsgBoxStyle.YesNo, "����") = MsgBoxResult.Yes Then
                If wuc.WhiteUser_Delete(intAutoID) = True Then
                    MsgBox("�R�����\�I", 64, "����")
                    RefreshpopMenuItem_Click(Nothing, Nothing)
                Else
                    MsgBox("�R�����ѡA���ˬd��]�I", 64, "����")
                End If
            Else
                Exit Sub
            End If
        End If

    End Sub

    Private Sub RefreshpopMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshpopMenuItem.Click

        Try
            Dim wuc As New KnifeWhiteUserController
            Grid1.DataSource = wuc.WhiteUser_GetList(Nothing, Nothing, TreeView1.SelectedNode.Tag, Nothing, Nothing, Nothing)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub AllUpdatepopMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AllUpdatepopMenuItem.Click

        Dim wuc As New KnifeWhiteUserController
        Dim winfo As List(Of KnifeWhiteUserInfo)
        Dim strUserID As String
        Dim i As Integer

        winfo = wuc.WhiteUser_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, False)
        If MsgBox("�O�_�T�w�H����q��s�H", MsgBoxStyle.YesNo, "����") = MsgBoxResult.Yes Then
            Dim winfo1 As List(Of KnifeWhiteUserInfo)
            For i = 0 To winfo.Count - 1
                strUserID = winfo(i).W_UserID
                winfo1 = wuc.WhiteUser_GetList(Nothing, strUserID, Nothing, Nothing, Nothing, False)

                Call GetNameDep(strUserID)

                If strKQdata = "haveData" Then
                    If winfo1(0).W_UserName.Equals(strKQName) And winfo1(0).FacName.Equals(strKQFacName) And winfo1(0).DepName.Equals(strKQDepName) Then
                        Continue For
                    Else
                        wuc.WhiteUser_AllUpdate(strUserID, strKQName, strKQFacName, strKQDepName, winfo1(0).AutoID)
                    End If
                Else
                    Continue For
                End If
            Next
            Grid1.DataSource = wuc.WhiteUser_GetList(Nothing, Nothing, TreeView1.SelectedNode.Tag.ToString, Nothing, Nothing, False)
        End If

    End Sub

    Private Sub FindpopMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindpopMenuItem.Click

        Dim wuc As New KnifeWhiteUserController
        Dim fr As New frmWhiteUserSelect

        fr.ShowDialog()
        If fr.strData = "havedata" Then
            Grid1.DataSource = fr.whiteSelectList
        Else
            Exit Sub
        End If

    End Sub

    Private Sub LoadData(ByVal userID As String, ByVal WHID As String)

        Dim wuc As New KnifeWhiteUserController
        Grid1.DataSource = wuc.WhiteUser_GetList(Nothing, userID, WHID, Nothing, Nothing, False)

    End Sub

    Private Sub WhiteUserpopMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WhiteUserpopMenuItem.Click

        Dim ds As New DataSet
        Dim ltc As New CollectionToDataSet
        Dim wuc As New KnifeWhiteUserController

        ltc.CollToDataSet(ds, "KnifeWareWhiteUser", wuc.WhiteUser_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, False))
        PreviewRPT(ds, "rptKnifeWareWhiteUser", "�M��ܥզW��", True, True)
        ltc = Nothing

    End Sub

    Private Sub ToolStripQuitBit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripQuitBit.Click
        ''��u�B�z
        If GridView2.RowCount <= 0 Then
            Exit Sub
        End If

        Dim wuc As New KnifeWhiteUserController
        Dim intAutoID As String

        intAutoID = GridView2.GetFocusedRowCellValue("AutoID")
        

        If MsgBox("�T�w�n��u����" + GridView2.GetFocusedRowCellValue("W_UserID") + "���Τ�,��u�B�z�ܡH", MsgBoxStyle.YesNo, "����") = MsgBoxResult.Yes Then
        Else
            Exit Sub
        End If

        Dim wc As New KnifeWhiteUserController
        If wc.WhiteUser_QuitBitUpdate(intAutoID, True) = True Then
            MsgBox("��u�B�z���\�I", 64, "����")
            RefreshpopMenuItem_Click(Nothing, Nothing)
        End If


    End Sub

    Private Sub ToolStripQuitBitNO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripQuitBitNO.Click
        ''��u�B�z
        If GridView2.RowCount <= 0 Then
            Exit Sub
        End If

        Dim wuc As New KnifeWhiteUserController
        Dim intAutoID As String

        intAutoID = GridView2.GetFocusedRowCellValue("AutoID")


        If MsgBox("�T�w�n��u����" + GridView2.GetFocusedRowCellValue("W_UserID") + "���Τ�,��u�٭�B�z�ܡH", MsgBoxStyle.YesNo, "����") = MsgBoxResult.Yes Then
        Else
            Exit Sub
        End If

        Dim wc As New KnifeWhiteUserController
        If wc.WhiteUser_QuitBitUpdate(intAutoID, False) = True Then
            MsgBox("��u�٭�B�z���\�I", 64, "����")
            RefreshpopMenuItem_Click(Nothing, Nothing)
        End If
    End Sub




    Sub LoadUserPower()

        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510905")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then AddpopMenuItem.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510906")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then EditpopMenuItem.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510907")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then FindpopMenuItem.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510908")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ToolStripQuitBit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510909")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ToolStripQuitBitNO.Enabled = True
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510910")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then DeletepopMenuItem.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510911")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then AllUpdatepopMenuItem.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510912")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then WhiteUserpopMenuItem.Enabled = True
        End If

    End Sub


End Class