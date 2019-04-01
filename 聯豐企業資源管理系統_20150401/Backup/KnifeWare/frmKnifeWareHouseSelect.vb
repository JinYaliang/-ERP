

Imports LFERP.Library.WareHouse
Imports LFERP.Library.Shared
Imports LFERP.DataSetting
Imports LFERP.Library.ProductionWareHouse

Public Class frmKnifeWareHouseSelect
    Public SelectWareID As String
    Public SelectWareName As String
    Public SelectWareUpName As String
    Public blnSelectParentNode As Boolean

    Private Sub frmWareHouseSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '2013-2-20 �[��ܦh�ܮw
        Labelxx.Visible = False
        Labelxx.Text = tempValue2

        tempValue2 = Nothing

        ''----------------------------------------------------------------------------
        Dim mt As New WareHouseController

        If tempValue3 = "" Then
            mt.WareHouse_LoadToTreeView(tw, ErpUser.WareHouseList)
        Else
            'exceed.lee�[
            If Labelxx.Text = "AAA" Then '�Ȯɤ���
                Dim a As New LFERP.SystemManager.PermissionModuleWarrantSubController
                Dim b As New List(Of LFERP.SystemManager.PermissionModuleWarrantSubInfo)
                b = a.PermissionModuleWarrantSub_GetList(InUserID, tempValue3)
                mt.WareHouse_LoadToTreeViewIn(tw, WareInSelect(InUserID, tempValue3), b.Item(0).PMWS_Value)

            Else
                mt.WareHouse_LoadToTreeView(tw, WareSelect(InUserID, tempValue3))
            End If

        End If

        tempValue3 = ""
        tw.ExpandAll()
        Me.StartPosition = FormStartPosition.CenterScreen

    End Sub


    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        ' 2013-8-20���W�s�W
        If tw.SelectedNode Is Nothing Then
            MsgBox("�п�ܥ��T���ܮw", , "����")
            Exit Sub
        End If

        If blnSelectParentNode = True Then
            SelectWareID = tw.SelectedNode.Tag
            SelectWareName = tw.SelectedNode.Text
            blnSelectParentNode = False
            Me.Close()
        Else
            SelectWareID = ""
            MsgBox("�п�ܥ��T���ܮw", , "����")
        End If


    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click


        SelectWareID = ""
        SelectWareName = ""
        SelectWareUpName = ""
        Me.Close()

    End Sub

    Private Sub tw_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tw.AfterSelect

    End Sub

    Private Sub tw_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tw.NodeMouseClick
        '2013-2-20 �[��ܦh�ܮw
        Dim i As Integer
        If Labelxx.Text = "500401" Or Labelxx.Text = "500408" Or Labelxx.Text = "500409" Then
            If e.Node.Level = 0 Then
                If e.Node.Checked = True Then
                    For i = 0 To e.Node.Nodes.Count - 1
                        e.Node.Nodes(i).Checked = True
                    Next
                Else
                    For i = 0 To e.Node.Nodes.Count - 1
                        e.Node.Nodes(i).Checked = False
                    Next
                End If
            End If
        End If
    End Sub
End Class
