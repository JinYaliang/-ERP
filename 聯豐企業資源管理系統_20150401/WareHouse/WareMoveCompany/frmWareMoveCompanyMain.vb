Imports LFERP.Library.WareHouse
Imports LFERP.Library.WareHouse.WareMoveCompany
Imports LFERP.SystemManager

Public Class frmWareMoveCompanyMain
    Dim CompanyID As String  ''�o�X���q�N��

#Region "��t�ռ��Ѽ�"
    ''' <summary>
    ''' �渹
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
    ''' �s��
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_StrM_Code As String

    Public Shared Property StrM_Code() As String
        Get
            Return m_StrM_Code
        End Get
        Set(ByVal value As String)
            m_StrM_Code = value
        End Set
    End Property

    ''' <summary>
    ''' ���q
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_StrCompany As String

    Public Shared Property StrCompany() As String
        Get
            Return m_StrCompany
        End Get
        Set(ByVal value As String)
            m_StrCompany = value
        End Set
    End Property

    ''' <summary>
    ''' �ܮw
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_StrWareHourse As String

    Public Shared Property StrWareHourse() As String
        Get
            Return m_StrWareHourse
        End Get
        Set(ByVal value As String)
            m_StrWareHourse = value
        End Set
    End Property
    ''' <summary>
    ''' �ܮwID
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_StrWHID As String

    Public Shared Property StrWHID() As String
        Get
            Return m_StrWHID
        End Get
        Set(ByVal value As String)
            m_StrWHID = value
        End Set
    End Property


    ''' <summary>
    ''' ����
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_StrCType As String

    Public Shared Property StrCType() As String
        Get
            Return m_StrCType
        End Get
        Set(ByVal value As String)
            m_StrCType = value
        End Set
    End Property


    ''' <summary>
    ''' �_�l�ɶ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_StrWareMoveStart As String

    Public Shared Property StrWareMoveStart() As String
        Get
            Return m_StrWareMoveStart
        End Get
        Set(ByVal value As String)
            m_StrWareMoveStart = value
        End Set
    End Property


    ''' <summary>
    ''' �I�ܮɶ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_StrWareMoveEnd As String

    Public Shared Property StrWareMoveEnd() As String
        Get
            Return m_StrWareMoveEnd
        End Get
        Set(ByVal value As String)
            m_StrWareMoveEnd = value
        End Set
    End Property

#End Region

#Region "�M�Žw�s�Ѽ�"
    Private Sub ClearParam()
        m_StrD_NO = Nothing
        m_StrM_Code = Nothing
        m_StrWareMoveStart = Nothing
        m_StrWareMoveEnd = Nothing
        m_StrCType = Nothing
        m_StrWareHourse = Nothing
        m_StrCompany = Nothing
        m_StrWHID = Nothing
    End Sub
#End Region


    Private Sub frmWareMoveCompanyMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mt As New WareHouseController
        mt.WareHouse_LoadToTreeView(TreeView1, WareSelect(InUserID, "501102"))

        LoadUserPower()
        PingMidDataBase()

    End Sub

    Sub PingMidDataBase()

        Dim CC As New WareMoveCompanyController
        Dim CL As New List(Of WareMoveCompanyInfo)
        CL = CC.WareMoveCompanyCenterSetting_GetList()
        If CL.Count <= 0 Then
            Exit Sub
        End If

        If My.Computer.Network.Ping(CL(0).ServerIP) Then
            LabBZ.Text = "���ݳs�����`."
            LabBZ.BackColor = Color.FromArgb(224, 224, 224)
        Else
            LabBZ.Text = CL(0).ServerIP & "���ݳs������."
            LabBZ.BackColor = Color.Red
        End If


    End Sub


    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        tempValue2 = ""
        Dim wmc As New WareMoveCompanyController
        Dim wmi As New WareMoveCompanyInfo
        If e.Node.Level = 1 Then

            '' Grid1.DataSource = wmc.WareMoveCompany_GetList(Nothing, Nothing, Nothing, TreeView1.SelectedNode.Tag, Nothing, Nothing, Nothing, DateAdd(DateInterval.Day, -30, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")), Nothing, Nothing)
            tempValue2 = TreeView1.SelectedNode.Tag

            tv1.ExpandAll() 'TV1�i�}�Ҧ��ؿ�

            Dim a As New WareMoveCompanyController
            Dim b As New List(Of WareMoveCompanyInfo)
            Dim c As New List(Of WareMoveCompanyInfo)
            b = a.WareMoveCompany_GetList(Nothing, Nothing, Nothing, Nothing, CompanyID, tempValue2, Nothing, Nothing, Nothing, True, False)
            c = a.WareMoveCompany_GetList(Nothing, Nothing, CompanyID, tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False)
            If b.Count > 0 Then
                tv1.Nodes.Item(0).Nodes.Item(0).Text = "���f�� (" & b.Count & ")"   '���J
            Else
                tv1.Nodes.Item(0).Nodes.Item(0).Text = "���f��"
            End If

            If c.Count > 0 Then
                tv1.Nodes.Item(1).Nodes.Item(0).Text = "���f�� (" & c.Count & ")" '���o�X
            Else
                tv1.Nodes.Item(1).Nodes.Item(0).Text = "���f��"
            End If

        End If
    End Sub

    Private Sub tv1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv1.AfterSelect
        Dim wmc As New WareMoveCompanyController
        Dim wmi As New WareMoveCompanyInfo
        If e.Node.Level = 1 Then

            Select Case Mid(tv1.SelectedNode.Text, 1, 3)

                Case "���f��"
                    If tv1.SelectedNode.Parent.Text = "���J����" Then
                        Grid1.DataSource = wmc.WareMoveCompany_GetList(Nothing, Nothing, Nothing, Nothing, CompanyID, tempValue2, Nothing, Nothing, Nothing, True, False)

                    End If
                    If tv1.SelectedNode.Parent.Text = "�o�X����" Then
                        Grid1.DataSource = wmc.WareMoveCompany_GetList(Nothing, Nothing, CompanyID, tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False)
                    End If
                Case "�w�f��"
                    If tv1.SelectedNode.Parent.Text = "���J����" Then
                        Grid1.DataSource = wmc.WareMoveCompany_GetList(Nothing, Nothing, Nothing, Nothing, CompanyID, tempValue2, Nothing, DateAdd(DateInterval.Day, -30, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")), Nothing, True)
                    End If
                    If tv1.SelectedNode.Parent.Text = "�o�X����" Then
                        Grid1.DataSource = wmc.WareMoveCompany_GetList(Nothing, Nothing, CompanyID, tempValue2, Nothing, Nothing, Nothing, DateAdd(DateInterval.Day, -30, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")), Nothing, True)
                    End If
            End Select

        End If
    End Sub

    Sub LoadUserPower()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        'CompanyID
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "501101")
        If pmwiL.Count > 0 Then
            CompanyID = pmwiL.Item(0).PMWS_Value
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "501103")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareMoveOutAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "501104")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareMoveEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "501105")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareMoveDel.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "501106")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareMoveIn.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "501107")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareMoveCheck.Enabled = True
        End If

    End Sub

    ''�s�W
    Private Sub popWareMoveOutAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMoveOutAdd.Click
        On Error Resume Next
        Edit = False

        If TreeView1.SelectedNode.Level = 1 Then
            Dim fr As frmWareMoveCompany
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmWareMoveCompany Then
                    fr.Activate()
                    Exit Sub
                End If
            Next

            fr = New frmWareMoveCompany
            fr.EditItem = "Add"
            fr.WH_ID = TreeView1.SelectedNode.Tag
            fr.WH_Name = TreeView1.SelectedNode.Text
            fr.Company = CompanyID
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        Else
            MsgBox("�п�ܥ��T���ܮw!")
            Exit Sub
        End If
    End Sub
    ''�ק�   --�w�f  --�w���Ƥ����\�ק�
    ''���J���ؤ���ק�
    Private Sub popWareMoveEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMoveEdit.Click

        Dim TempStr As String
        If Me.GridView1.RowCount <= 0 Then
            Exit Sub
        End If

        If TreeView1.SelectedNode.Level = 1 Then

            TempStr = GridView1.GetFocusedRowCellValue("MC_NO").ToString
            '--------------------------------------------
            Dim mvc As New WareMoveCompanyController
            Dim mvl As New List(Of WareMoveCompanyInfo)
            mvl = mvc.WareMoveCompany_GetList(Nothing, TempStr, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If mvl.Count <= 0 Then
                MsgBox("�ƾڤ��s�b�I")
                Exit Sub
            End If

            '------------------------------------
            If mvl(0).MC_Check = True Then
                MsgBox("����w�f��,����ק�I")
                Exit Sub
            End If
            '------------------------------------
            If mvl(0).MC_InCheck = True Then
                MsgBox("����w���ƽT�{,����ק�I")
                Exit Sub
            End If
            '-----------------------------------
            If TreeView1.SelectedNode.Tag <> mvl(0).MC_OUT_WHID Then
                MsgBox("�D���ܵo�X�ƾڤ���ק�I")
                Exit Sub
            End If
            '---------------------------------------
            On Error Resume Next
            Dim fr As frmWareMoveCompany
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmWareMoveCompany Then
                    fr.Activate()
                    Exit Sub
                End If
            Next

            fr = New frmWareMoveCompany
            fr.EditItem = "Edit"
            fr.EditID = TempStr
            fr.Company = CompanyID

            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        Else
            MsgBox("�п�ܥ��T���ܮw!")
            Exit Sub
        End If
    End Sub

    Private Sub popWareMoveDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMoveDel.Click
        Dim TempStr As String
        If Me.GridView1.RowCount <= 0 Then
            Exit Sub
        End If

        If TreeView1.SelectedNode.Level = 1 Then
            TempStr = GridView1.GetFocusedRowCellValue("MC_NO").ToString
            '--------------------------------------------
            Dim mvc As New WareMoveCompanyController
            Dim mvl As New List(Of WareMoveCompanyInfo)
            mvl = mvc.WareMoveCompany_GetList(Nothing, TempStr, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If mvl.Count <= 0 Then
                MsgBox("�ƾڤ��s�b�I")
                Exit Sub
            End If

            '------------------------------------
            If mvl(0).MC_Check = True Then
                MsgBox("����w�f��,����R���I")
                Exit Sub
            End If
            '------------------------------------
            If mvl(0).MC_InCheck = True Then
                MsgBox("����w���ƽT�{,����R���I")
                Exit Sub
            End If
            '-----------------------------------
            If TreeView1.SelectedNode.Tag <> mvl(0).MC_OUT_WHID Then
                MsgBox("�D���ܵo�X�ƾڡA����R���I")
                Exit Sub
            End If
            '---------------------------------------
            If MsgBox("�T�w�n�R���s����:" & TempStr & "��ܡH", MsgBoxStyle.YesNo, "�R������") = MsgBoxResult.Yes Then
                If mvc.WareMoveCompany_Delete(Nothing, Nothing, TempStr, CompanyID) = True Then
                    MsgBox("�R�����\�I")
                End If
            End If

        Else
            MsgBox("�п�ܥ��T���ܮw!")
            Exit Sub
        End If
    End Sub

    Private Sub popWareMoveCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMoveCheck.Click
        Dim TempStr As String
        If Me.GridView1.RowCount <= 0 Then
            Exit Sub
        End If

        TempStr = GridView1.GetFocusedRowCellValue("MC_NO").ToString
        '--------------------------------------------
        Dim mvc As New WareMoveCompanyController
        Dim mvl As New List(Of WareMoveCompanyInfo)
        mvl = mvc.WareMoveCompany_GetList(Nothing, TempStr, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If mvl.Count <= 0 Then
            MsgBox("�ƾڤ��s�b�I")
            Exit Sub
        End If
        '------------------------------------
        If mvl(0).MC_Check = True Then
            MsgBox("����w�f��,����f�֡I")
            Exit Sub
        End If
        '------------------------------------
        If mvl(0).MC_InCheck = True Then
            MsgBox("����w���ƽT�{,����f�֡I")
            Exit Sub
        End If
        '-----------------------------------
        If TreeView1.SelectedNode.Tag <> mvl(0).MC_OUT_WHID Then
            MsgBox("�D���ܵo�X�ƾڡA����f�֡I")
            Exit Sub
        End If
        '---------------------------------------
        On Error Resume Next
        If TreeView1.SelectedNode.Level = 1 Then
            Dim fr As frmWareMoveCompany
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmWareMoveCompany Then
                    fr.Activate()
                    Exit Sub
                End If
            Next

            fr = New frmWareMoveCompany
            fr.EditItem = "Check"
            fr.EditID = TempStr
            fr.Company = CompanyID

            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        Else
            MsgBox("�п�ܥ��T���ܮw!")
            Exit Sub
        End If
    End Sub

    Private Sub popWareMoveIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMoveIn.Click
        Dim TempStr As String
        Dim TempstrCom As String
        If Me.GridView1.RowCount <= 0 Then
            Exit Sub
        End If

        TempStr = GridView1.GetFocusedRowCellValue("MC_NO").ToString
        TempstrCom = GridView1.GetFocusedRowCellValue("MC_OUT_Company").ToString

        If TreeView1.SelectedNode.Level = 1 Then
            '--------------------------------------------
            Dim mvc As New WareMoveCompanyController
            Dim mvl As New List(Of WareMoveCompanyInfo)
            mvl = mvc.WareMoveCompany_GetList(Nothing, TempStr, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If mvl.Count <= 0 Then
                MsgBox("�ƾڤ��s�b�I")
                Exit Sub
            End If
            '------------------------------------
            If TreeView1.SelectedNode.Tag = mvl(0).MC_OUT_WHID And TempstrCom = CompanyID Then
                MsgBox("���ܵo�X�ƾ�,����i�榬�ƽT�{�I")
                Exit Sub
            End If
            '---------------------------------------
            If mvl(0).MC_Check = False Then
                MsgBox("���楼�f��, ���ব�ƽT�{�I")
                Exit Sub
            End If
            '------------------------------------
            If mvl(0).MC_InCheck = True Then
                MsgBox("����w���ƽT�{,���ব�ơI")
                Exit Sub
            End If
            '-----------------------------------
           
            On Error Resume Next
            Dim fr As frmWareMoveCompany
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmWareMoveCompany Then
                    fr.Activate()
                    Exit Sub
                End If
            Next

            fr = New frmWareMoveCompany
            fr.EditItem = "InCheck"
            fr.EditID = TempStr
            fr.Company = CompanyID

            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        Else
            MsgBox("�п�ܥ��T���ܮw!")
            Exit Sub
        End If
    End Sub

    Private Sub popWareMoveView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMoveView.Click
        Dim TempStr As String
        If Me.GridView1.RowCount <= 0 Then
            Exit Sub
        End If

        TempStr = GridView1.GetFocusedRowCellValue("MC_NO").ToString

        If TreeView1.SelectedNode.Level = 1 Then
            '--------------------------------------------
            On Error Resume Next
            Dim fr As frmWareMoveCompany
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmWareMoveCompany Then
                    fr.Activate()
                    Exit Sub
                End If
            Next

            fr = New frmWareMoveCompany
            fr.EditItem = "View"
            fr.EditID = TempStr
            fr.Company = CompanyID

            fr.TypeInorOut = tv1.SelectedNode.Parent.Text

            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        Else
            MsgBox("�п�ܥ��T���ܮw!")
            Exit Sub
        End If
    End Sub
#Region "�d��"
    Private Sub popWareMoveSeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMoveSeek.Click
        If TreeView1.SelectedNode.Level <> 1 Then
            MsgBox("�п�ܥ��T���ܮw!")
            Exit Sub
        End If

        ClearParam()
        Dim frmQuery As New frmWareMoveCompanyQuery
        frmQuery.StrCompany = CompanyID
        frmQuery.StrWareHourse = TreeView1.SelectedNode.Text
        frmQuery.StrWHID = TreeView1.SelectedNode.Tag
        frmQuery.ShowDialog()

        Dim wmc As New WareMoveCompanyController
        Dim wmi As New WareMoveCompanyInfo

        If Not frmWareMoveCompanyQuery.BolFlag Then
            If m_StrCType = "���J" Then
                Grid1.DataSource = wmc.WareMoveCompany_GetList(Nothing, m_StrD_NO, Nothing, Nothing, m_StrCompany, m_StrWHID, m_StrM_Code, m_StrWareMoveStart, m_StrWareMoveEnd, Nothing, Nothing)
            Else
                Grid1.DataSource = wmc.WareMoveCompany_GetList(Nothing, m_StrD_NO, m_StrCompany, m_StrWHID, Nothing, Nothing, m_StrM_Code, m_StrWareMoveStart, m_StrWareMoveEnd, Nothing, Nothing)
            End If
        End If
    End Sub
#End Region

End Class