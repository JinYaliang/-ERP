Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Data.Sql
Namespace LFERP.Library.BroadCastManager.BroadCastMessage


    Public Class BroadCastMessageControler
        Public Function BroadCastMessage_Add(ByVal objinfo As BroadCastMessageInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("BroadCastMessage_Add")

                db.AddInParameter(dbComm, "M_Message", DbType.String, objinfo.M_Message)
                db.AddInParameter(dbComm, "M_Out", DbType.String, objinfo.M_Out)
                db.AddInParameter(dbComm, "M_In", DbType.String, objinfo.M_In)
                db.AddInParameter(dbComm, "M_Type", DbType.String, objinfo.M_Type)
                db.AddInParameter(dbComm, "M_Affirm", DbType.String, objinfo.M_Affirm)

                db.AddInParameter(dbComm, "M_AdduserID", DbType.String, objinfo.M_AdduserID)
                db.AddInParameter(dbComm, "M_Status", DbType.String, objinfo.M_Status)
                db.AddInParameter(dbComm, "M_Adddate", DbType.Date, CDate(objinfo.M_Adddate))
                'db.AddInParameter(dbComm, "M_ModifyUserID", DbType.String, objinfo.M_ModifyUserID)
                'db.AddInParameter(dbComm, "M_ModifyDate", DbType.Date, CDate(objinfo.M_ModifyDate))

                db.ExecuteNonQuery(dbComm)
                BroadCastMessage_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                BroadCastMessage_Add = False
            End Try
        End Function

        Public Function BroadCastMessage_Update(ByVal objinfo As BroadCastMessageInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("BroadCastMessage_Update")
                db.AddInParameter(dbComm, "AutoID", DbType.String, objinfo.AutoID)
                db.AddInParameter(dbComm, "M_Message", DbType.String, objinfo.M_Message)
                db.AddInParameter(dbComm, "M_Out", DbType.String, objinfo.M_Out)
                db.AddInParameter(dbComm, "M_In", DbType.String, objinfo.M_In)
                db.AddInParameter(dbComm, "M_Type", DbType.String, objinfo.M_Type)
                db.AddInParameter(dbComm, "M_Affirm", DbType.Boolean, objinfo.M_Affirm)

                'db.AddInParameter(dbComm, "M_AdduserID", DbType.Int32, objinfo.M_AdduserID)
                'db.AddInParameter(dbComm, "M_Adddate", DbType.String, CDate(objinfo.M_Adddate))
                db.AddInParameter(dbComm, "M_ModifyUserID", DbType.String, objinfo.M_ModifyUserID)
                db.AddInParameter(dbComm, "M_ModifyDate", DbType.String, objinfo.M_ModifyDate)
                db.AddInParameter(dbComm, "M_Status", DbType.String, objinfo.M_Status)

                db.ExecuteNonQuery(dbComm)
                BroadCastMessage_Update = True

            Catch ex As Exception
                MsgBox(ex.Message)
                BroadCastMessage_Update = False
            End Try
        End Function

        Public Function BroadCastMessage_Delete(ByVal AutoID As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("BroadCastMessage_Delete")
                db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)
                db.ExecuteNonQuery(dbComm)
                BroadCastMessage_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                BroadCastMessage_Delete = False
            End Try
        End Function

        Public Function BroadCastMessage_Getlist(ByVal M_Out As String, ByVal M_In As String, ByVal M_message As String, ByVal M_affirm As Boolean, ByVal AutoID As String) As List(Of BroadCastMessageInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("BroadCastMessage_GetList")

            db.AddInParameter(dbComm, "@M_Out", DbType.String, M_Out)
            db.AddInParameter(dbComm, "@M_In", DbType.String, M_In)
            db.AddInParameter(dbComm, "@M_message", DbType.String, M_message)
            db.AddInParameter(dbComm, "@M_affirm", DbType.String, M_affirm)
            db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)

            Dim FeatureList As New List(Of BroadCastMessageInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillBroadCastMessageType(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function BroadCastMessage_Getlist1(ByVal M_Out As String, ByVal M_In As String, ByVal M_affirm As Boolean, ByVal M_Date As String) As List(Of BroadCastMessageInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("BroadCastMessage_GetList1")

            db.AddInParameter(dbComm, "@M_Out", DbType.String, M_Out)
            db.AddInParameter(dbComm, "@M_In", DbType.String, M_In)
            db.AddInParameter(dbComm, "@M_affirm", DbType.String, M_affirm)
            db.AddInParameter(dbComm, "@M_Date", DbType.String, M_Date)

            Dim FeatureList As New List(Of BroadCastMessageInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillBroadCastMessageType(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Friend Function FillBroadCastMessageType(ByVal reader As IDataReader) As BroadCastMessageInfo
            '对应取得的数据
            On Error Resume Next
            Dim objInfo As New BroadCastMessageInfo

            objInfo.AutoID = reader("AutoID").ToString
            objInfo.M_Adddate = CDate(reader("M_Adddate").ToString)
            objInfo.M_AdduserID = reader("M_AdduserID").ToString
            objInfo.M_ModifyDate = CDate(reader("M_ModifyDate").ToString)
            objInfo.M_ModifyUserID = reader("M_ModifyUserID").ToString
            objInfo.M_Date = CDate(reader("M_Date").ToString)
            objInfo.M_Time = Format(CDate(reader("M_Time").ToString), "HH:mm:ss")
            objInfo.M_Message = reader("M_Message").ToString
            objInfo.M_Out = reader("M_Out").ToString
            objInfo.M_In = reader("M_In").ToString
            objInfo.M_Type = reader("M_Type").ToString
            objInfo.M_Affirm = reader("M_Affirm").ToString
            objInfo.M_Status = reader("M_Status").ToString
            Return objInfo
        End Function
    End Class
End Namespace


