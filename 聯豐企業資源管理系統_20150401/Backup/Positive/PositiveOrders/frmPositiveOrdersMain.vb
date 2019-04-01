Imports LFERP.Library.Positive
Imports LFERP.SystemManager

Public Class frmPositiveOrdersMain

#Region "�w�s�Ѽ�"

    Private m_DoCtrl As New PositiveDeliverController
    Private m_DsListInfo As New List(Of PositiveDeliverInfo)

    Private m_PoCtrl As New PositiveOrdersController
    Private m_PsInfo As New PositiveOrdersInfo
    Private m_PsListInfo As New List(Of PositiveOrdersInfo)

#Region "�����q��Ѽ�"

    ''' <summary>
    ''' �q��s��
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_StrP_OM_ID As String

    Public Shared Property StrP_OM_ID() As String
        Get
            Return m_StrP_OM_ID
        End Get
        Set(ByVal value As String)
            m_StrP_OM_ID = value
        End Set
    End Property

    ''' <summary>
    ''' ���~�s��
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_StrP_M_Code As String

    Public Shared Property StrP_M_Code() As String
        Get
            Return m_StrP_M_Code
        End Get
        Set(ByVal value As String)
            m_StrP_M_Code = value
        End Set
    End Property

    ''' <summary>
    ''' �Ȥ�N��
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_StrP_OM_CusterNo As String

    Public Shared Property StrP_OM_CusterNo() As String
        Get
            Return m_StrP_OM_CusterNo
        End Get
        Set(ByVal value As String)
            m_StrP_OM_CusterNo = value
        End Set
    End Property


    ''' <summary>
    ''' �q��}�l���
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_StrP_OM_AddDateStart As String


    Public Shared Property StrP_OM_AddDateStart() As String
        Get
            Return m_StrP_OM_AddDateStart
        End Get
        Set(ByVal value As String)
            m_StrP_OM_AddDateStart = value
        End Set
    End Property

    ''' <summary>
    ''' �q��I�ܤ��
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_StrP_OM_AddDateEnd As String

    Public Shared Property StrP_OM_AddDateEnd() As String
        Get
            Return m_StrP_OM_AddDateEnd
        End Get
        Set(ByVal value As String)
            m_StrP_OM_AddDateEnd = value
        End Set
    End Property

    ''' <summary>
    ''' �U��}�l���
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_StrP_OMComplateDateStart As String

    Public Shared Property StrP_OMComplateDateStart() As String
        Get
            Return m_StrP_OMComplateDateStart
        End Get
        Set(ByVal value As String)
            m_StrP_OMComplateDateStart = value
        End Set
    End Property


    ''' <summary>
    ''' �U��I�ܤ��
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_StrP_OMComplateDateEnd As String

    Public Shared Property StrP_OMComplateDateEnd() As String
        Get
            Return m_StrP_OMComplateDateEnd
        End Get
        Set(ByVal value As String)
            m_StrP_OMComplateDateEnd = value
        End Set
    End Property

#End Region

#Region "�M�Žw�s�Ѽ�"
    Private Sub ClearParam()
        m_StrP_OM_ID = Nothing
        m_StrP_M_Code = Nothing
        m_StrP_OM_CusterNo = Nothing
        m_StrP_OM_AddDateStart = Nothing
        m_StrP_OM_AddDateEnd = Nothing
        m_StrP_OMComplateDateStart = Nothing
        m_StrP_OMComplateDateEnd = Nothing
    End Sub
#End Region

#End Region

#Region "��s"
    ''' <summary>
    ''' ��s
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RefreshOrdersData()

        dgPositiveOrdersMain.DataSource = m_PoCtrl.PositiveOrders_GetListTopFiveHundred(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

    End Sub
#End Region

#Region "�k����ƥ�"
    Private Sub popPositiveOrdersMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPositiveOrdersMainAdd.Click, PositiveOrdersMainCheck.Click, popPositiveOrdersMainView.Click, PopPositiveOrdersMainRefresh.Click, popPositiveOrdersMainEdit.Click, popPositiveOrdersMainDel.Click, PopPositiveOrdersMainPrice.Click
        On Error Resume Next

        Dim frmObject As frmPositiveOrders
        For Each frmObject In MDIMain.MdiChildren
            If TypeOf frmObject Is frmPositiveOrders Then
                frmObject.Activate()
                Exit Sub
            End If
        Next


        frmObject = New frmPositiveOrders

        m_PsListInfo.Clear()

        Select Case sender.Name

            Case "popPositiveOrdersMainAdd"

                frmObject.EditItemDescrip = "Add"

                frmObject.POMID = dgvPositiveOrdersMain.GetFocusedRowCellValue("P_OM_ID").ToString()

            Case "PopPositiveOrdersMainRefresh"

                RefreshOrdersData()
                Exit Sub

            Case Else

                ' m_PsListInfo = m_PoCtrl.PositiveOrders_GetList(dgvPositiveOrdersMain.GetFocusedRowCellValue("P_OM_ID").ToString(), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                If m_PsListInfo Is Nothing Then
                    Exit Sub
                End If

                If m_PsListInfo.Count <= 0 Then
                    Exit Sub
                End If


                Select Case sender.Name

                    Case "popPositiveOrdersMainEdit"


                        If m_PsListInfo(0).P_OM_Check Then
                            MsgBox("�����q��w�g�f��!�����\�ק�", 64, "����")
                            Exit Sub
                        End If

                        Dim nIndexCheck As Integer

                        For nIndexCheck = 0 To m_PsListInfo.Count - 1
                            'If m_DoCtrl.PositiveDeliver_GetList(Nothing, m_PsListInfo(nIndexCheck).P_OM_Num, Nothing, Nothing, Nothing, Nothing).Count > 0 Then
                            '    MsgBox("�ӭq��w���e�f�O��!�����\�ק�", 64, "����")
                            '    Exit Sub
                            'End If
                        Next

                        frmObject.EditItemDescrip = "Modify"
                    Case "PopPositiveOrdersMainPrice"

                        'If m_PsListInfo(0).P_OM_Check Then
                        '    MsgBox("�����q��w�g�f��!�����\�ק������J", 64, "����")
                        '    Exit Sub
                        'End If

                        frmObject.EditItemDescrip = "Price"

                    Case "popPositiveOrdersMainView"

                        frmObject.EditItemDescrip = "View"

                    Case "popPositiveOrdersMainDel"

                        If m_PsListInfo(0).P_OM_Check Then
                            MsgBox("�����q��w�g�f��!�����\�R��", 64, "����")
                            Exit Sub
                        End If


                        Dim nIndexCheck As Integer

                        For nIndexCheck = 0 To m_PsListInfo.Count - 1
                            'If m_DoCtrl.PositiveDeliver_GetList(Nothing, m_PsListInfo(nIndexCheck).P_OM_Num, Nothing, Nothing, Nothing, Nothing).Count > 0 Then
                            '    MsgBox("�ӭq��w���e�f�O��!�����\�R��", 64, "����")
                            '    Exit Sub
                            'End If
                        Next

                        If MsgBox("�T�w�n�R���s����" & dgvPositiveOrdersMain.GetFocusedRowCellValue("P_OM_ID").ToString & "���q��ܡH", MsgBoxStyle.YesNo, "�R������") = MsgBoxResult.Yes Then

                            If m_PoCtrl.PositiveOrders_Delete(dgvPositiveOrdersMain.GetFocusedRowCellValue("P_OM_ID").ToString()) Then
                                MsgBox("�R����e�����q�榨�\!")
                                RefreshOrdersData()
                            Else
                                MsgBox("�R�������q��,���ˬd��]!")
                            End If

                        End If

                        Exit Sub

                    Case "PositiveOrdersMainCheck"

                        Dim nIndexCheck As Integer

                        For nIndexCheck = 0 To m_PsListInfo.Count - 1
                            'If m_DoCtrl.PositiveDeliver_GetList(Nothing, m_PsListInfo(nIndexCheck).P_OM_Num, Nothing, Nothing, Nothing, Nothing).Count > 0 Then
                            '    MsgBox("�ӭq��w���e�f�O��!�����\�ק�f�֪��A", 64, "����")
                            '    Exit Sub
                            'End If
                        Next

                        frmObject.BolFalg = m_PsListInfo(0).P_OM_Check
                        frmObject.EditItemDescrip = "Check"

                End Select

                frmObject.POMID = dgvPositiveOrdersMain.GetFocusedRowCellValue("P_OM_ID").ToString()

        End Select

        frmObject.MdiParent = MDIMain
        frmObject.WindowState = FormWindowState.Maximized
        frmObject.Show()
    End Sub
#End Region

#Region "��l�ƥ[���ƾ�"

    Private Sub frmPositiveOrdersMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        RefreshOrdersData()

        LoadPowerUser()

    End Sub

#End Region

#Region "�[���v��"
    Private Sub LoadPowerUser()

        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "9401101")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPositiveOrdersMainAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "9401102")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPositiveOrdersMainEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "9401103")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPositiveOrdersMainDel.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "9401105")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPositiveOrdersMainView.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "9401106")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then PopPositiveOrdersMainRefresh.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "9401104")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then PositiveOrdersMainCheck.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "9401107")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then PopPositiveOrdersMainPrice.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "9401108")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then PositiveOrdersMainQuery.Enabled = True
        End If

    End Sub
#End Region

#Region "�C�L�q����`"
    ''' <summary>
    ''' �C�L�q����`
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PositiveOrdersMainPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PositiveOrdersMainPrint.Click
        Dim frmObject As New frmPositiveOrdersPrint
        frmObject.ShowDialog()
        frmObject.Dispose()
    End Sub
#End Region

#Region "�ھڭq��y�����[�����~�H��"
    ''' <summary>
    ''' �ھڭq��y�����[�����~�H��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvPositiveOrdersMain_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvPositiveOrdersMain.MouseUp
        If Me.dgvPositiveOrdersMain.RowCount <= 0 Then
            Exit Sub
        End If


        dgPositiveDeliver.DataSource = m_DoCtrl.PositiveDeliver_GetListTopFiveHundred(Nothing, dgvPositiveOrdersMain.GetFocusedRowCellValue("P_OM_Num").ToString(), Nothing, Nothing, Nothing, Nothing)

    End Sub

    ''' <summary>
    ''' ���ФW�U����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvPositiveOrdersMain_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvPositiveOrdersMain.KeyUp
        If e.KeyData = Keys.Down Or e.KeyData = Keys.Up Then
            If Me.dgvPositiveOrdersMain.RowCount <= 0 Then
                Exit Sub
            End If
            dgPositiveDeliver.DataSource = m_DoCtrl.PositiveDeliver_GetListTopFiveHundred(Nothing, dgvPositiveOrdersMain.GetFocusedRowCellValue("P_OM_Num").ToString(), Nothing, Nothing, Nothing, Nothing)
        End If
    End Sub

#End Region

#Region "�����q��d��"
    ''' <summary>
    ''' �����q��d��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PositiveOrdersMainQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PositiveOrdersMainQuery.Click
        ClearParam()

        Dim frmQuery As New frmPositiveOrdersQuery
        frmQuery.StrQuery = "PositiveOrders"
        frmQuery.ShowDialog()


        If Not frmPositiveOrdersQuery.BolFlag Then
            dgPositiveOrdersMain.DataSource = m_PoCtrl.PositiveOrders_GetListTopFiveHundred(m_StrP_OM_ID, m_StrP_OM_CusterNo, m_StrP_M_Code, Nothing, m_StrP_OM_AddDateStart, m_StrP_OM_AddDateEnd, Nothing, m_StrP_OMComplateDateStart, m_StrP_OMComplateDateEnd)
        End If

        If dgvPositiveOrdersMain.DataRowCount <= 0 Then
            dgPositiveDeliver.DataSource = Nothing
        End If

    End Sub
#End Region

End Class