Imports LFERP.Library.Positive
Imports LFERP.SystemManager

Public Class frmPositiveDeliverMain


#Region "�w�s�Ѽ�"

    Private m_DoCtrl As New PositiveDeliverController
    Private m_DsListInfo As New List(Of PositiveDeliverInfo)

    Private m_PoCtrl As New PositiveOrdersController
    Private m_PsInfo As New PositiveOrdersInfo
    Private m_PsListInfo As New List(Of PositiveOrdersInfo)



#Region "�����e�f�Ѽ�"
    ''' <summary>
    ''' �e�f�渹
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_StrD_NO As String

    Public Shared Property StrD_NO() As String
        Get
            Return m_StrD_NO
        End Get
        Set(ByVal value As String)
            m_StrD_NO = value
        End Set
    End Property

    ''' <summary>
    ''' ���~�s��
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_StrPOMP_M_Code As String

    Public Shared Property StrPOMP_M_Code() As String
        Get
            Return m_StrPOMP_M_Code
        End Get
        Set(ByVal value As String)
            m_StrPOMP_M_Code = value
        End Set
    End Property

    ''' <summary>
    ''' �U��_�l�ɶ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_StrPositiveDeliverStart As String

    Public Shared Property StrPositiveDeliverStart() As String
        Get
            Return m_StrPositiveDeliverStart
        End Get
        Set(ByVal value As String)
            m_StrPositiveDeliverStart = value
        End Set
    End Property


    ''' <summary>
    ''' �U��I�ܮɶ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_StrPositiveDeliverEnd As String

    Public Shared Property StrPositiveDeliverEnd() As String
        Get
            Return m_StrPositiveDeliverEnd
        End Get
        Set(ByVal value As String)
            m_StrPositiveDeliverEnd = value
        End Set
    End Property

#End Region

#Region "�M�Žw�s�Ѽ�"
    Private Sub ClearParam()
        m_StrD_NO = Nothing
        m_StrPOMP_M_Code = Nothing
        m_StrPositiveDeliverStart = Nothing
        m_StrPositiveDeliverEnd = Nothing
    End Sub
#End Region

#End Region

#Region "��s"
    ''' <summary>
    ''' ��s
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RefreshPositiveDeliverData()
        dgPositiveDeliver.DataSource = m_DoCtrl.PositiveDeliver_GetListTopFiveHundred(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub
#End Region

#Region "�k����ƥ�"
    Private Sub popPositiveDeliverMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPositiveDeliverAdd.Click, popPositiveDeliverMainView.Click, PopPositiveDeliverMainRefresh.Click, popPositiveDeliverMainEdit.Click, popPositiveDeliverMainDel.Click, popPositiveDeliverMainCheck.Click
        On Error Resume Next

        Dim frmObject As frmPositiveDeliver

        For Each frmObject In MDIMain.MdiChildren
            If TypeOf frmObject Is frmPositiveDeliver Then
                frmObject.Activate()
                Exit Sub
            End If
        Next

        frmObject = New frmPositiveDeliver

        m_DsListInfo.Clear()


        Select Case sender.Name

            Case "popPositiveDeliverAdd"

                frmObject.EditItemDescrip = "Add"

                frmObject.DOMID = dgvPositiveDeliver.GetFocusedRowCellValue("D_NO").ToString()

            Case "PopPositiveDeliverMainRefresh"

                RefreshPositiveDeliverData()
                Exit Sub

            Case Else



                '  m_DsListInfo = m_DoCtrl.PositiveDeliver_GetList(dgvPositiveDeliver.GetFocusedRowCellValue("D_NO").ToString(), Nothing, Nothing, Nothing, Nothing, Nothing)

                If m_DsListInfo Is Nothing Then
                    Exit Sub
                End If

                If m_DsListInfo.Count <= 0 Then
                    Exit Sub
                End If


                Select Case sender.Name

                    Case "popPositiveDeliverMainEdit"

                        If m_DsListInfo(0).D_Check Then
                            MsgBox("�����e�f�q��w�g�f��!�����\�ק�", 64, "����")
                            Exit Sub
                        End If

                        frmObject.EditItemDescrip = "Modify"

                    Case "popPositiveDeliverMainView"

                        frmObject.EditItemDescrip = "View"

                    Case "popPositiveDeliverMainDel"

                        If m_DsListInfo(0).D_Check Then
                            MsgBox("�����e�f�q��w�g�f��!�����\�R��", 64, "����")
                            Exit Sub
                        End If

                        If MsgBox("�T�w�n�R���s����" & dgvPositiveDeliver.GetFocusedRowCellValue("D_NO").ToString & "���q��ܡH", MsgBoxStyle.YesNo, "�R������") = MsgBoxResult.Yes Then

                            If m_DoCtrl.PositiveDeliver_DeleteByDOMNum(dgvPositiveDeliver.GetFocusedRowCellValue("D_NO").ToString()) Then
                                MsgBox("�R����e�e�f�q�榨�\!")
                                RefreshPositiveDeliverData()
                            Else
                                MsgBox("�R����e�e�f�q�楢��,���ˬd��]!")
                            End If

                        End If

                        Exit Sub

                    Case "popPositiveDeliverMainCheck"

                        'If m_DsListInfo(0).D_Check Then
                        '    MsgBox("�����e�f�q��w�g�f��!�����\�f��", 64, "����")
                        '    Exit Sub
                        'End If
                        frmObject.BolFalg = m_DsListInfo(0).D_Check
                        frmObject.EditItemDescrip = "Check"

                End Select

                frmObject.DOMID = dgvPositiveDeliver.GetFocusedRowCellValue("D_NO").ToString()

        End Select

        frmObject.MdiParent = MDIMain
        frmObject.WindowState = FormWindowState.Maximized
        frmObject.Show()

    End Sub
#End Region

#Region "�[���v��"
    Private Sub LoadPowerUser()

        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "9402101")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPositiveDeliverAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "9402102")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPositiveDeliverMainEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "9402103")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPositiveDeliverMainDel.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "9402105")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPositiveDeliverMainView.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "9402106")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then PopPositiveDeliverMainRefresh.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "9402104")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPositiveDeliverMainCheck.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "9402107")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPositiveDeliverMainQuery.Enabled = True
        End If

    End Sub
#End Region

#Region "��l�ƥ[���ƾ�"
    Private Sub dgPositiveDeliver_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgPositiveDeliver.Load

        RefreshPositiveDeliverData()

        LoadPowerUser()

    End Sub
#End Region

#Region "�C�L�q�f��"
    Private Sub popPositiveDeliverMainPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPositiveDeliverMainPrint.Click

        Dim ltc As New CollectionToDataSet
        Dim dsPositiveDeliver As New DataSet
        m_DsListInfo.Clear()

        '  m_DsListInfo = m_DoCtrl.PositiveDeliver_GetList(dgvPositiveDeliver.GetFocusedRowCellValue("D_NO").ToString(), Nothing, Nothing, Nothing, Nothing, Nothing)

        If m_DsListInfo.Count <= 0 Then
            MsgBox("�S���ŦX�n�D���ƾڡI", 64, "����")
            Exit Sub
        End If

        ltc.CollToDataSet(dsPositiveDeliver, "PositiveDeliver", m_DsListInfo)
        PreviewRPTDialog(dsPositiveDeliver, "rptPositiveDeliver", "�����e�f�q��", True, True)
    End Sub
#End Region

#Region "�e�f��d��"
    ''' <summary>
    ''' �e�f��d��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub popPositiveDeliverMainQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPositiveDeliverMainQuery.Click
        ClearParam()
        Dim frmQuery As New frmPositiveOrdersQuery
        frmQuery.StrQuery = "PositiveDeliver"
        frmQuery.ShowDialog()

        If Not frmPositiveOrdersQuery.BolFlag Then
            dgPositiveDeliver.DataSource = m_DoCtrl.PositiveDeliver_GetListTopFiveHundred(m_StrD_NO, Nothing, m_StrPOMP_M_Code, m_StrPositiveDeliverStart, m_StrPositiveDeliverEnd, Nothing)
        End If
    End Sub
#End Region

End Class