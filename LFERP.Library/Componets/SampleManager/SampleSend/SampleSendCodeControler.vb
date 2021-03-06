Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Data.Sql

Namespace LFERP.Library.SampleManager.SampleSend
    Public Class SampleSendCodeControler
        Public Function SampleSendCode_Add(ByVal objinfo As SampleSendCodeInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleSendCode_Add")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@SP_ID", DbType.String, objinfo.SP_ID)
                db.AddInParameter(dbComm, "@SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "@SS_Edition", DbType.String, objinfo.SS_Edition)
                db.AddInParameter(dbComm, "@Code_Qty", DbType.Int32, objinfo.Code_Qty)
                db.AddInParameter(dbComm, "@Code_ID", DbType.String, objinfo.Code_ID)

                db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "@AddUserID", DbType.String, objinfo.AddUserID)
                db.AddInParameter(dbComm, "@AddDate", DbType.DateTime, CDate(objinfo.AddDate))
                db.AddInParameter(dbComm, "@CodeType", DbType.String, objinfo.CodeType)

                db.ExecuteNonQuery(dbComm)
                SampleSendCode_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleSendCode_Add = False
            End Try
        End Function

        Public Function SampleSendCode_Update(ByVal objinfo As SampleSendCodeInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleSendCode_Update")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@SP_ID", DbType.String, objinfo.SP_ID)
                db.AddInParameter(dbComm, "@SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "@SS_Edition", DbType.String, objinfo.SS_Edition)
                db.AddInParameter(dbComm, "@Code_Qty", DbType.Int32, objinfo.Code_Qty)
                db.AddInParameter(dbComm, "@Code_ID", DbType.String, objinfo.Code_ID)
                db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "@ModifyUserID", DbType.String, objinfo.ModifyUserID)
                db.AddInParameter(dbComm, "@ModifyDate", DbType.DateTime, objinfo.ModifyDate)
                db.AddInParameter(dbComm, "@AutoID", DbType.String, objinfo.AutoID)
                db.AddInParameter(dbComm, "@CodeType", DbType.String, objinfo.CodeType)

                db.ExecuteNonQuery(dbComm)
                SampleSendCode_Update = True

            Catch ex As Exception
                MsgBox(ex.Message)
                SampleSendCode_Update = False
            End Try
        End Function


        Public Function SampleSendCode_Delete(ByVal SP_ID As String, ByVal AutoID As String) As Boolean

            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleSendCode_Delete")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@SP_ID", DbType.String, SP_ID)
                db.AddInParameter(dbComm, "@AutoID ", DbType.String, AutoID)
                db.ExecuteNonQuery(dbComm)
                SampleSendCode_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleSendCode_Delete = False
            End Try
        End Function


        Public Function SampleSendCode_Getlist(ByVal SP_ID As String, ByVal SO_ID As String, ByVal SS_Edition As String, ByVal PM_M_Code As String, ByVal M_Code As String, ByVal AutoID As String) As List(Of SampleSendCodeInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleSendCode_Getlist")
            dbComm.CommandTimeout = 0
            db.AddInParameter(dbComm, "@SP_ID", DbType.String, SP_ID)
            db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
            db.AddInParameter(dbComm, "@SS_Edition", DbType.String, SS_Edition)
            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
            db.AddInParameter(dbComm, "@Code_ID", DbType.String, M_Code)
            db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)


            Dim FeatureList As New List(Of SampleSendCodeInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillSampleSendCodeType(reader))
                End While
                Return FeatureList
            End Using
        End Function
        Public Function SampleSendCode_GetCount(ByVal SO_ID As String, ByVal SS_Edition As String) As Integer
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleSendCode_GetCount")
            dbComm.CommandTimeout = 0
            db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
            db.AddInParameter(dbComm, "@SS_Edition", DbType.String, SS_Edition)

            Dim StrCode_ID As Integer = 0
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    StrCode_ID = CInt(reader("NCount").ToString)
                End While
                SampleSendCode_GetCount = StrCode_ID
            End Using
        End Function
        Public Function SampleSendCode_GetID(ByVal Code_ID As String) As Boolean
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleSendCode_GetID")
            dbComm.CommandTimeout = 0
            db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)

            Dim StrCode_ID As String = String.Empty
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    StrCode_ID = reader("Code_ID").ToString
                End While
                If StrCode_ID = String.Empty Then
                    Return False
                Else
                    Return True
                End If
            End Using
        End Function
        Friend Function FillSampleSendCodeType(ByVal reader As IDataReader) As SampleSendCodeInfo
            '对应取得的数据
            On Error Resume Next
            Dim objInfo As New SampleSendCodeInfo

            objInfo.SP_ID = reader("SP_ID").ToString
            objInfo.SO_ID = reader("SO_ID").ToString
            objInfo.SS_Edition = reader("SS_Edition").ToString
            objInfo.Code_ID = reader("Code_ID").ToString
            objInfo.ClientBarcode = reader("ClientBarcode").ToString
            objInfo.Code_Qty = CInt(reader("Code_Qty").ToString)
            objInfo.PM_M_Code = reader("PM_M_Code").ToString

            objInfo.AddDate = Format(CDate(reader("AddDate").ToString), "yyyy-MM-dd HH:mm:ss")
            objInfo.AddUserID = reader("AddUserID").ToString
            objInfo.ModifyUserID = reader("ModifyUserID").ToString
            objInfo.ModifyDate = CDate(reader("ModifyDate").ToString)
            objInfo.AutoID = CDbl(reader("AutoID").ToString)
            objInfo.CodeType = reader("CodeType").ToString
            Return objInfo
        End Function
    End Class
End Namespace

