Imports LFERP.SystemManager
Imports LFERP.SystemManager.SystemUser
Imports System.IO

Imports System.Data.SqlClient
Imports system.data.oledb

Imports LFERPDB

Public Class Login
    Public uu As New SystemUserController
    Dim isEnter As Boolean

    Public IOPath As String = "C:\lferp\�p�ץ��~�귽�޲z�t��\UserLog.txt"
    Public XMLPath As String = "C:\lferp\�p�ץ��~�귽�޲z�t��\Ver.xml"



    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb2.Click
        Me.Close()
    End Sub

    Private Sub txtuserpsw_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtuserpsw.Enter
        If Trim(txtuserid.Text) <> "" Then
            Dim us As SystemUserInfo = uu.SystemUser_Get(txtuserid.Text.ToString)
            If us Is Nothing Then
                MsgBox("�Τᤣ�s�b�I", 64, "����")
                Label1.Text = "���Τᤣ�s�b"
                txtuserid.Focus()
                txtuserid.SelectAll()
                Exit Sub
            End If
            Label1.Text = us.U_Name
            InUserID = us.U_ID
        End If
    End Sub

    Private Sub txtuserpsw_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtuserpsw.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' check()
            sb1_Click(Nothing, Nothing)
        End If
    End Sub

    Sub check()
        If Trim(txtuserid.Text) = "" Then
            MsgBox("�п�J�t���I", 64, "����")
            txtuserid.Focus()
        Else
            Dim us As SystemUserInfo = uu.SystemUser_Get(txtuserid.Text.ToString)
            If us Is Nothing Then
                MsgBox("�Τᤣ�s�b�I", 64, "����")
                Label1.Text = "���Τᤣ�s�b"
                txtuserid.Focus()
                txtuserid.SelectAll()
                Exit Sub
            ElseIf us.U_PassWord.Equals(txtuserpsw.Text) Then
                If us.U_Enabled = True Then
                    strUserLoginTime = Format(Now, "yyyy/MM/dd HH:mm:ss")
                    UserName = Label1.Text  '�n���Τ�W
                    InUserID = us.U_ID      '�n���Τ�ID
                    InUser = Label1.Text    '�n���Τ�W
                    strInDPT_ID = us.DPT_ID     '�n���Τ���ݳ���ID
                    strInDepName = us.DepName   '�n���Τ���ݥͲ������W��

                    strInUserRank = us.UserRank '�n���Τ�Ҿ֦����Ͳ��v������
                    strInUserType = us.UserType '�u������

                    strInCompany = Mid(strInDPT_ID, 1, 4)         ''�n���Τ���ݤ��q�Ʀr�N��

                    If us.DepID Is Nothing Then
                        strInDepID = "���s�b"
                    Else
                        If strInUserRank = "�Ͳ���" Then
                            strInDepID = Mid(us.DepID, 1, 1)    '�n���Τ���ݥͲ�����ID
                            strInFacID = Mid(us.DepID, 1, 1)    '�n���Τ���ݼt�OID
                        ElseIf strInUserRank = "�޲z" Then
                            strInDepID = Nothing
                            strInFacID = Nothing
                        ElseIf strInUserRank = "�έp" Then
                            strInDepID = us.DepID
                            strInFacID = Mid(us.DepID, 1, 1)
                        End If

                        strInFacIDFull = Mid(us.DepID, 1, 1)
                        strInDepIDFull = us.DepID  ''�Τ���ݳ�������

                    End If

                    Dim pmws As New PermissionModuleWarrantSubController
                    Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

                    pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880408")
                    If pmwiL.Count > 0 Then
                        strRefCard = pmwiL.Item(0).PMWS_Value
                    End If

                    My.Settings.UserID = txtuserid.Text.Trim
                    My.Settings.Save()
                    Me.Hide()

                    MDIMain.Show()
                Else
                    MsgBox("�L���Τ�!", 64, "����")
                    txtuserpsw.Text = ""
                    txtuserid.Focus()
                    txtuserid.SelectAll()
                End If
            Else
                If txtuserpsw.Text = "" Then
                    MsgBox("�п�J�K�X�I", 64, "����")
                Else
                    MsgBox("�K�X���~,�Э��s��J�K�X�I", 64, "����")
                    txtuserpsw.Text = ""
                End If
                txtuserpsw.Focus()
            End If
        End If
    End Sub

    Private Sub sb1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb1.Click
        If WriteUserXmlUpdateYN(txtuserid.Text) = True Then
            Dim str As String
            str = "C:\lferp\�۰ʧ�s\AutoUpdateNew.exe"
            If Dir(str) = "" Then
            Else
                System.Diagnostics.Process.Start("C:\lferp\�۰ʧ�s\AutoUpdateNew.exe")
                Exit Sub
            End If
        End If


        check()
    End Sub

    Private Sub Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Dim str As String
        'str = "C:\lferp\�۰ʧ�s\AutoUpdate.exe"
        'If Dir(str) = "" Then
        'Else
        '    System.Diagnostics.Process.Start("C:\lferp\�۰ʧ�s\AutoUpdate.exe")
        'End If
        txtuserid.Text = My.Settings.UserID
        If txtuserid.Text.Trim = "" Then
            txtuserid.Select()
        Else
            txtuserpsw.Select()
        End If

        strVer = "2014-04-08 V2.0.7"  '�{�Ǫ������A��ʧ���
        CheckUpdate()

    End Sub




    Function WriteUserXmlUpdateYN(ByVal U_ID As String) As Boolean

        WriteUserXmlUpdateYN = False

        Dim dsA As New DataSet
        With dsA.Tables.Add("U_IDVer")
            .Columns.Add("U_ID", GetType(String))
            .Columns.Add("Ver", GetType(String))
        End With
        '-------------------------------------------------------------------------------
        Dim strVer As String
        Dim dsVer As New DataSet

        dsVer = DataSet("select U_ID,Ver from VerUser where U_ID= '" + U_ID + "'")
        If dsVer.Tables(0).Rows.Count <= 0 Then
            Exit Function
        End If
        strVer = dsVer.Tables(0).Rows(0)("Ver").ToString


        If Dir(IOPath, FileAttribute.Directory) <> "" Then
            Kill(IOPath)
        End If
        Using sw As StreamWriter = File.AppendText(IOPath)
            sw.WriteLine(U_ID & "," & strVer)
            sw.Close()
        End Using


        '-------------------------------------------------------------------------------
        If Dir(XmlPath, FileAttribute.Directory) = "" Then
            Dim row As DataRow
            row = dsA.Tables(0).NewRow
            row("U_ID") = U_ID
            row("Ver") = strVer
            dsA.Tables(0).Rows.Add(row)
            dsA.WriteXml(XmlPath)
            Exit Function
        End If
        '-------------------------------------------------------------------------------
        dsA.ReadXml(XmlPath)

        Dim Bz As String = ""
        Dim i As Integer
        If dsA.Tables(0).Rows.Count > 0 Then
            For i = 0 To dsA.Tables(0).Rows.Count - 1
                If dsA.Tables(0).Rows(i)("U_ID").ToString = U_ID Then
                    Bz = "Y"
                    If strVer = dsA.Tables(0).Rows(i)("Ver").ToString Then
                    Else
                        WriteUserXmlUpdateYN = True
                    End If
                End If
            Next
        End If
        '-------------------------------------------------------------------------------
        If Bz = "" Then
            Dim row As DataRow
            row = dsA.Tables(0).NewRow
            row("U_ID") = U_ID
            row("Ver") = strVer
            dsA.Tables(0).Rows.Add(row)
        End If

        dsA.WriteXml(XmlPath)

    End Function

    Function DataSet(ByVal sql As String) As DataSet

        Dim ai As New LFERPDataBase

        Dim ds As New DataSet
        Dim conn As SqlConnection
        Dim da As SqlDataAdapter
        conn = New SqlConnection
        conn.ConnectionString = ai.LoadConnStr
        conn.Open()
        da = New SqlDataAdapter(sql, conn)
        da.Fill(ds)

        da.Dispose()
        conn.Close()
        Return ds
    End Function
End Class
