Imports System.Windows.Forms
Imports System
Imports System.Data
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.SqlClient
Imports System.Data.Common

Namespace LFERP.SystemManager.SystemUser
    Public Class SystemUserController
#Region "用戶資料管理"
        Public Function SystemUser_Add(ByVal objInfo As SystemUserInfo) As Boolean
            '檢查用戶名稱是否已存在
            Dim objSystemUserInfo As SystemUserInfo = SystemUser_Get(objInfo.U_ID)
            If objSystemUserInfo Is Nothing Then
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SystemUser_Add")
                db.AddInParameter(dbComm, "@U_ID", DbType.String, objInfo.U_ID)
                db.AddInParameter(dbComm, "@U_Name", DbType.String, objInfo.U_Name)
                db.AddInParameter(dbComm, "@U_PassWord", DbType.String, objInfo.U_PassWord)
                db.AddInParameter(dbComm, "@DPT_ID", DbType.String, objInfo.DPT_ID)
                db.AddInParameter(dbComm, "@U_KeyImage", DbType.Binary, objInfo.U_KeyImage)
                db.AddInParameter(dbComm, "@JobNo", DbType.String, objInfo.JobNo)
                db.AddInParameter(dbComm, "@CO_ID", DbType.String, objInfo.CO_ID)
                db.ExecuteNonQuery(dbComm)
                SystemUser_Add = True
            Else
                SystemUser_Add = False
            End If
        End Function
        Public Function SystemUser_Get(ByVal U_ID As String) As SystemUserInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SystemUser_Get")
            db.AddInParameter(dbComm, "@U_ID", DbType.String, U_ID)

            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    Return FillSystemUser(reader)
                End While
                Return Nothing
            End Using
        End Function
        Public Function SystemUser_GetList(ByVal U_ID As String, ByVal U_Name As String, ByVal DPT_ID As String) As List(Of SystemUserInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SystemUser_Get")
            db.AddInParameter(dbComm, "@U_ID", DbType.String, U_ID)
            db.AddInParameter(dbComm, "@U_Name", DbType.String, U_Name)
            db.AddInParameter(dbComm, "@DPT_ID", DbType.String, DPT_ID)
            Dim FeatureList As New List(Of SystemUserInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillSystemUser(reader))
                End While
                Return FeatureList
            End Using
        End Function
        Friend Function FillSystemUser(ByVal reader As IDataReader) As SystemUserInfo
            '對應取得的數據
            On Error Resume Next
            Dim objInfo As New SystemUserInfo    'SystemUser 表資料
            objInfo.U_ID = reader("U_ID").ToString
            objInfo.U_Name = reader("U_Name").ToString
            objInfo.U_PassWord = reader("U_PassWord").ToString
            objInfo.DPT_ID = reader("DPT_ID").ToString
            objInfo.U_DPT_Name = reader("U_DPT_Name").ToString
            If reader("U_KeyImage") Is DBNull.Value Then
                objInfo.U_KeyImage = Nothing
            Else
                objInfo.U_KeyImage = reader("U_KeyImage")
            End If
            objInfo.JobNo = reader("JobNo")
            objInfo.UserID = reader("UserID").ToString  'UserPower 表資料
            objInfo.UserName = reader("UserName").ToString
            objInfo.UserRank = reader("UserRank").ToString
            objInfo.DepID = reader("DepID").ToString
            objInfo.DepName = reader("DepName").ToString
            objInfo.UserType = reader("UserType").ToString
            objInfo.U_Enabled = reader("U_Enabled")

            '------2014.4.21 鄭少釗新增-----------
            If reader("U_Online") Is DBNull.Value Or CBool(reader("U_Online")) = False Then
                objInfo.U_Online = "離綫"
            Else
                objInfo.U_Online = "在綫"
            End If

            If reader("U_LoginDate") Is DBNull.Value Then
                objInfo.U_LoginDate = String.Empty
            Else
                objInfo.U_LoginDate = Format(CDate(reader("U_LoginDate").ToString), "yyyy/MM/dd HH:mm:ss")
            End If

            If reader("U_LogoutDate") Is DBNull.Value Then
                objInfo.U_LogoutDate = String.Empty
            Else
                objInfo.U_LogoutDate = Format(CDate(reader("U_LogoutDate").ToString), "yyyy/MM/dd HH:mm:ss")
            End If

            If reader("U_OnlineUserNum") Is DBNull.Value Then
                objInfo.U_OnlineUserNum = 0
            Else
                objInfo.U_OnlineUserNum = reader("U_OnlineUserNum")
            End If
            '-----------------------------------------------------
            objInfo.CO_ID = reader("CO_ID")
            objInfo.CO_ChsName = reader("CO_ChsName")
            Return objInfo
        End Function

        Public Function UserPower_Add(ByVal objInfo As SystemUserInfo) As Boolean
            '生產部用戶新增
            Dim objSystemUserInfo As SystemUserInfo = SystemUser_Get(objInfo.U_ID)
            If objSystemUserInfo Is Nothing Then
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("UserPower_Add")
                db.AddInParameter(dbComm, "@UserID", DbType.String, objInfo.UserID)
                db.AddInParameter(dbComm, "@UserName", DbType.String, objInfo.UserName)
                db.AddInParameter(dbComm, "@UserRank", DbType.String, objInfo.UserRank)
                db.AddInParameter(dbComm, "@DepID", DbType.String, objInfo.DepID)
                db.AddInParameter(dbComm, "@UserType", DbType.String, objInfo.UserType)
                db.ExecuteNonQuery(dbComm)
                UserPower_Add = True
            Else
                UserPower_Add = False
            End If
        End Function
        Public Function UserPower_Update(ByVal objInfo As SystemUserInfo) As Boolean
            '生產部用戶修改
            Dim objSystemUserInfo As SystemUserInfo = SystemUser_Get(objInfo.U_ID)
            If objSystemUserInfo Is Nothing Then
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("UserPower_Update")
                db.AddInParameter(dbComm, "@UserID", DbType.String, objInfo.UserID)
                db.AddInParameter(dbComm, "@UserName", DbType.String, objInfo.UserName)
                db.AddInParameter(dbComm, "@UserRank", DbType.String, objInfo.UserRank)
                db.AddInParameter(dbComm, "@DepID", DbType.String, objInfo.DepID)
                db.AddInParameter(dbComm, "@UserType", DbType.String, objInfo.UserType)

                db.ExecuteNonQuery(dbComm)
                UserPower_Update = True
            Else
                UserPower_Update = False
            End If
        End Function

        '----2014.4.21 鄭少釗新增---------
        Public Function SystemUser_UpdateState(ByVal objInfo As SystemUserInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SystemUser_UpdateState")
                db.AddInParameter(dbComm, "@U_ID", DbType.String, objInfo.U_ID)
                db.AddInParameter(dbComm, "@U_Online", DbType.String, objInfo.U_Online)
                db.AddInParameter(dbComm, "@U_LoginDate", DbType.String, objInfo.U_LoginDate)
                db.AddInParameter(dbComm, "@U_LogoutDate", DbType.String, objInfo.U_LogoutDate)
                db.AddInParameter(dbComm, "@U_OnlineUserNum", DbType.Int32, objInfo.U_OnlineUserNum)
                db.ExecuteNonQuery(dbComm)
                SystemUser_UpdateState = True
            Catch ex As Exception
                SystemUser_UpdateState = False
            End Try

        End Function
        '-----------------------------------
        Public Function UserPower_Del(ByVal UserID As String, ByVal DepID As String) As Boolean
            '生產部用戶刪除
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("UserPower_Del")
                db.AddInParameter(dbComm, "@UserID", DbType.String, UserID)
                db.AddInParameter(dbComm, "@DepID", DbType.String, DepID)
                db.ExecuteNonQuery(dbComm)
                UserPower_Del = True
            Catch ex As Exception
                UserPower_Del = False
            End Try
        End Function
        Public Function UserPower_GetList(ByVal UserID As String, ByVal UserName As String, ByVal UserRank As String, ByVal DepID As String) As List(Of SystemUserInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("UserPower_GetList")
            db.AddInParameter(dbComm, "@UserID", DbType.String, UserID)
            db.AddInParameter(dbComm, "@UserName", DbType.String, UserName)
            db.AddInParameter(dbComm, "@UserRank", DbType.String, UserRank)
            db.AddInParameter(dbComm, "@DepID", DbType.String, DepID)
            Dim FeatureList As New List(Of SystemUserInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillSystemUser(reader))
                End While
                Return FeatureList
            End Using
        End Function
        Public Function SystemUser_Update(ByVal objInfo As SystemUserInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SystemUser_Update")
                db.AddInParameter(dbComm, "@U_ID", DbType.String, objInfo.U_ID)
                db.AddInParameter(dbComm, "@U_Name", DbType.String, objInfo.U_Name)
                db.AddInParameter(dbComm, "@U_PassWord", DbType.String, objInfo.U_PassWord)
                db.AddInParameter(dbComm, "@DPT_ID", DbType.String, objInfo.DPT_ID)
                db.AddInParameter(dbComm, "@U_KeyImage", DbType.Binary, objInfo.U_KeyImage)
                db.AddInParameter(dbComm, "@JobNo", DbType.String, objInfo.JobNo)
                db.AddInParameter(dbComm, "@CO_ID", DbType.String, objInfo.CO_ID)
                db.ExecuteNonQuery(dbComm)
                SystemUser_Update = True
            Catch ex As Exception
                SystemUser_Update = False
            End Try

        End Function


        Public Function SystemUser_UpdateName(ByVal objInfo As SystemUserInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SystemUser_UpdateName")
                db.AddInParameter(dbComm, "@U_ID", DbType.String, objInfo.U_ID)
                db.AddInParameter(dbComm, "@U_Name", DbType.String, objInfo.U_Name)

                db.ExecuteNonQuery(dbComm)
                SystemUser_UpdateName = True
            Catch ex As Exception
                SystemUser_UpdateName = False
            End Try

        End Function

        Public Function SystemUser_UpdatePsw(ByVal objInfo As SystemUserInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SystemUser_UpdatePsw")
                db.AddInParameter(dbComm, "@U_ID", DbType.String, objInfo.U_ID)
                db.AddInParameter(dbComm, "@U_PassWord", DbType.String, objInfo.U_PassWord)

                db.ExecuteNonQuery(dbComm)
                SystemUser_UpdatePsw = True
            Catch ex As Exception
                SystemUser_UpdatePsw = False
            End Try

        End Function


        Public Function SystemUser_Delete(ByVal U_ID As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SystemUser_Delete")
                db.AddInParameter(dbComm, "@U_ID", DbType.String, U_ID)
                db.ExecuteNonQuery(dbComm)
                SystemUser_Delete = True
            Catch ex As Exception
                SystemUser_Delete = False
            End Try
        End Function

      
#End Region
    End Class
End Namespace



