Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Data.Sql
Namespace LFERP.Library.NmetalSampleManager.NmetalSampleOrders
    Public Class NmetalSampleOrdersCodeControler
        Public Function NmetalSampleOrdersCode_Add(ByVal objinfo As NmetalSampleOrdersCodeInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersCode_Add")

                db.AddInParameter(dbComm, "SP_ID", DbType.String, objinfo.SP_ID)
                db.AddInParameter(dbComm, "SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "SS_Edition", DbType.String, objinfo.SS_Edition)
                db.AddInParameter(dbComm, "Code_Qty", DbType.Int32, objinfo.Code_Qty)
                db.AddInParameter(dbComm, "Code_ID", DbType.String, objinfo.Code_ID)

                db.AddInParameter(dbComm, "PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "AddUserID", DbType.String, objinfo.AddUserID)
                db.AddInParameter(dbComm, "AddDate", DbType.DateTime, CDate(objinfo.AddDate))

                db.ExecuteNonQuery(dbComm)
                NmetalSampleOrdersCode_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleOrdersCode_Add = False
            End Try
        End Function

        Public Function NmetalSampleOrdersCode_Update(ByVal objinfo As NmetalSampleOrdersCodeInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersCode_Update")

                db.AddInParameter(dbComm, "SP_ID", DbType.String, objinfo.SP_ID)
                db.AddInParameter(dbComm, "SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "SS_Edition", DbType.String, objinfo.SS_Edition)
                db.AddInParameter(dbComm, "Code_Qty", DbType.Int32, objinfo.Code_Qty)
                db.AddInParameter(dbComm, "Code_ID", DbType.String, objinfo.Code_ID)
                db.AddInParameter(dbComm, "PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "ModifyUserID", DbType.String, objinfo.ModifyUserID)
                db.AddInParameter(dbComm, "ModifyDate", DbType.DateTime, objinfo.ModifyDate)
                db.AddInParameter(dbComm, "AutoID", DbType.String, objinfo.AutoID)


                db.ExecuteNonQuery(dbComm)
                NmetalSampleOrdersCode_Update = True

            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleOrdersCode_Update = False
            End Try
        End Function
        Public Function NmetalSampleOrdersCode_UpdateA(ByVal objinfo As NmetalSampleOrdersCodeInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersCode_UpdateA")

                db.AddInParameter(dbComm, "SP_ID", DbType.String, objinfo.SP_ID)
                db.AddInParameter(dbComm, "Code_ID", DbType.String, objinfo.Code_ID)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleOrdersCode_UpdateA = True

            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleOrdersCode_UpdateA = False
            End Try
        End Function
        Public Function NmetalSampleOrdersCode_UpdateB(ByVal SO_ID As String, ByVal SS_Edition As String, ByVal SWI_Qty As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersCode_UpdateB")

                db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
                db.AddInParameter(dbComm, "@SS_Edition", DbType.String, SS_Edition)
                db.AddInParameter(dbComm, "@SWI_Qty", DbType.Int32, SWI_Qty)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleOrdersCode_UpdateB = True

            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleOrdersCode_UpdateB = False
            End Try
        End Function


        Public Function NmetalSampleOrdersCode_Delete(ByVal SO_ID As String, ByVal SS_Edition As String, ByVal AutoID As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersCode_Delete")
                db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
                db.AddInParameter(dbComm, "@SS_Edition", DbType.String, SS_Edition)
                db.AddInParameter(dbComm, "@AutoID ", DbType.String, AutoID)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleOrdersCode_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleOrdersCode_Delete = False
            End Try
        End Function


        Public Function NmetalSampleOrdersCode_Getlist(ByVal SP_ID As String, ByVal SO_ID As String, ByVal SS_Edition As String, ByVal PM_M_Code As String, ByVal M_Code As String, ByVal AutoID As String) As List(Of NmetalSampleOrdersCodeInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersCode_Getlist")

            db.AddInParameter(dbComm, "@SP_ID", DbType.String, SP_ID)
            db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
            db.AddInParameter(dbComm, "@SS_Edition", DbType.String, SS_Edition)
            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
            db.AddInParameter(dbComm, "@Code_ID", DbType.String, M_Code)
            db.AddInParameter(dbComm, "AutoID", DbType.String, AutoID)


            Dim FeatureList As New List(Of NmetalSampleOrdersCodeInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleOrdersCodeType(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function NmetalSampleOrdersCode_GetID(ByVal Code_ID As String) As Boolean
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersCode_GetID")

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

        Public Function NmetalSampleOrdersCode_GetCount(ByVal SO_ID As String, ByVal SS_Edition As String) As Integer
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersCode_GetCount")

            db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
            db.AddInParameter(dbComm, "@SS_Edition", DbType.String, SS_Edition)

            Dim StrCode_ID As Integer = 0
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    StrCode_ID = CInt(reader("NCount").ToString)
                End While
                NmetalSampleOrdersCode_GetCount = StrCode_ID
            End Using
        End Function

        Friend Function FillNmetalSampleOrdersCodeType(ByVal reader As IDataReader) As NmetalSampleOrdersCodeInfo
            '对应取得的数据
            On Error Resume Next
            Dim objInfo As New NmetalSampleOrdersCodeInfo

            objInfo.SP_ID = reader("SP_ID").ToString
            objInfo.SO_ID = reader("SO_ID").ToString
            objInfo.SS_Edition = reader("SS_Edition").ToString
            objInfo.Code_ID = reader("Code_ID").ToString
            objInfo.Code_Qty = CInt(reader("Code_Qty").ToString)
            objInfo.PM_M_Code = reader("PM_M_Code").ToString

            objInfo.AddDate = CDate(reader("AddDate").ToString)
            objInfo.AddUserID = reader("AddUserID").ToString
            objInfo.ModifyUserID = reader("ModifyUserID").ToString
            objInfo.ModifyDate = CDate(reader("ModifyDate").ToString)
            objInfo.AutoID = CDbl(reader("AutoID").ToString)
            Return objInfo
        End Function
    End Class
End Namespace

