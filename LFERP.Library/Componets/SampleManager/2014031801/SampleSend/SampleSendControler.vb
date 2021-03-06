Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Data.Sql
Namespace LFERP.Library.SampleManager.SampleSend
    Public Class SampleSendControler
        Public Function SampleSend_Add(ByVal objinfo As SampleSendInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleSend_Add")

                db.AddInParameter(dbComm, "SP_ID", DbType.String, objinfo.SP_ID)
                db.AddInParameter(dbComm, "SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "SS_Edition", DbType.String, objinfo.SS_Edition)
                db.AddInParameter(dbComm, "SP_Qty", DbType.Int32, objinfo.SP_Qty)
                db.AddInParameter(dbComm, "SP_CusterID", DbType.String, objinfo.SP_CusterID)
                db.AddInParameter(dbComm, "SP_SendDate", DbType.DateTime, CDate(objinfo.SP_SendDate))
                db.AddInParameter(dbComm, "CO_ID", DbType.String, objinfo.CO_ID)
                db.AddInParameter(dbComm, "SP_AddUserID", DbType.String, objinfo.SP_AddUserID)
                db.AddInParameter(dbComm, "SP_AddDate", DbType.DateTime, CDate(objinfo.SP_AddDate))
                db.AddInParameter(dbComm, "SP_Remark", DbType.String, objinfo.SP_Remark)
                'db.AddInParameter(dbComm, "SP_ModifyUserID", DbType.String, objinfo.SP_ModifyUserID)
                'db.AddInParameter(dbComm, "SP_ModifyDate", DbType.DateTime, objinfo.SP_ModifyDate)
                db.AddInParameter(dbComm, "PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "M_Code", DbType.String, objinfo.M_Code)
                db.AddInParameter(dbComm, "SP_ExpCompany", DbType.String, objinfo.SP_ExpCompany)
                db.AddInParameter(dbComm, "SP_ExpDeliveryID", DbType.String, objinfo.SP_ExpDeliveryID)
                db.AddInParameter(dbComm, "PK_Code_ID", DbType.String, objinfo.PK_Code_ID)


                db.ExecuteNonQuery(dbComm)
                SampleSend_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleSend_Add = False
            End Try
        End Function

        Public Function SampleSend_Update(ByVal objinfo As SampleSendInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleSend_Update")

                db.AddInParameter(dbComm, "SP_ID", DbType.String, objinfo.SP_ID)
                db.AddInParameter(dbComm, "SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "SS_Edition", DbType.String, objinfo.SS_Edition)
                db.AddInParameter(dbComm, "SP_Qty", DbType.Int32, objinfo.SP_Qty)
                db.AddInParameter(dbComm, "SP_CusterID", DbType.String, objinfo.SP_CusterID)
                db.AddInParameter(dbComm, "SP_SendDate", DbType.DateTime, CDate(objinfo.SP_SendDate))
                db.AddInParameter(dbComm, "CO_ID", DbType.String, objinfo.CO_ID)
                db.AddInParameter(dbComm, "SP_ModifyUserID", DbType.String, objinfo.SP_ModifyUserID)
                db.AddInParameter(dbComm, "SP_ModifyDate", DbType.DateTime, objinfo.SP_ModifyDate)
                db.AddInParameter(dbComm, "PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "M_Code", DbType.String, objinfo.M_Code)
                db.AddInParameter(dbComm, "AutoID", DbType.String, objinfo.AutoID)
                db.AddInParameter(dbComm, "SP_Remark", DbType.String, objinfo.SP_Remark)
                db.AddInParameter(dbComm, "SP_ExpCompany", DbType.String, objinfo.SP_ExpCompany)
                db.AddInParameter(dbComm, "SP_ExpDeliveryID", DbType.String, objinfo.SP_ExpDeliveryID)
                db.AddInParameter(dbComm, "PK_Code_ID", DbType.String, objinfo.PK_Code_ID)
                db.ExecuteNonQuery(dbComm)
                SampleSend_Update = True

            Catch ex As Exception
                MsgBox(ex.Message)
                SampleSend_Update = False
            End Try
        End Function


        Public Function SampleSend_Delete(ByVal SP_ID As String) As Boolean

            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleSend_Delete")
                db.AddInParameter(dbComm, "@SP_ID", DbType.String, SP_ID)
                db.ExecuteNonQuery(dbComm)
                SampleSend_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleSend_Delete = False
            End Try
        End Function

        Public Function SampleSend_DeleteAutoID(ByVal AutoID As String) As Boolean

            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleSend_DeleteAutoID")
                db.AddInParameter(dbComm, "@AutoID", DbType.Decimal, AutoID)
                db.ExecuteNonQuery(dbComm)
                SampleSend_DeleteAutoID = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleSend_DeleteAutoID = False
            End Try
        End Function

        Public Function SampleSend_UpdateNoSendQty(ByVal objinfo As SampleSendInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleSend_UpdateNoSendQty")

                db.AddInParameter(dbComm, "@SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "@SS_Edition", DbType.String, objinfo.SS_Edition)
                db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "@SC_ConfirmationQty", DbType.Int32, objinfo.SP_Qty)
                db.ExecuteNonQuery(dbComm)
                SampleSend_UpdateNoSendQty = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleSend_UpdateNoSendQty = False
            End Try
        End Function
        Public Function SampleSend_UpdateInCheck(ByVal objinfo As SampleSendInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleSend_UpdateInCheck")

                db.AddInParameter(dbComm, "SP_ID", DbType.String, objinfo.SP_ID)
                db.AddInParameter(dbComm, "SP_InCheck", DbType.Boolean, objinfo.SP_InCheck)
                db.AddInParameter(dbComm, "SP_InCheckDate", DbType.Date, objinfo.SP_InCheckDate)
                db.AddInParameter(dbComm, "SP_InCheckUserID", DbType.String, objinfo.SP_InCheckUserID)
                db.AddInParameter(dbComm, "SP_InCheckRemark", DbType.String, objinfo.SP_InCheckRemark)
                db.ExecuteNonQuery(dbComm)
                SampleSend_UpdateInCheck = True

            Catch ex As Exception
                MsgBox(ex.Message)
                SampleSend_UpdateInCheck = False
            End Try
        End Function

        Public Function SampleSend_UpdateCheck(ByVal objinfo As SampleSendInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleSend_UpdateCheck")

                db.AddInParameter(dbComm, "SP_ID", DbType.String, objinfo.SP_ID)
                db.AddInParameter(dbComm, "SP_Check", DbType.Boolean, objinfo.SP_Check)
                db.AddInParameter(dbComm, "SP_CheckDate", DbType.Date, objinfo.SP_CheckDate)
                db.AddInParameter(dbComm, "SP_CheckUserID", DbType.String, objinfo.SP_CheckUserID)
                db.AddInParameter(dbComm, "SP_CheckRemark", DbType.String, objinfo.SP_CheckRemark)
                db.ExecuteNonQuery(dbComm)
                SampleSend_UpdateCheck = True

            Catch ex As Exception
                MsgBox(ex.Message)
                SampleSend_UpdateCheck = False
            End Try
        End Function


        Public Function SampleSend_Getlist(ByVal SP_ID As String, ByVal SO_ID As String, ByVal SS_Edition As String, ByVal M_Code As String, ByVal SP_CusterID As String, ByVal PM_M_Code As String, ByVal SP_Check As String, ByVal StartDate As String, ByVal EndDate As String, ByVal SP_AddStartDate As String, ByVal SP_AddEndDate As String, ByVal SO_IDCheck As Boolean, ByVal SP_AddUserID As String) As List(Of SampleSendInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleSend_Getlist")

            db.AddInParameter(dbComm, "@SP_ID", DbType.String, SP_ID)
            db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
            db.AddInParameter(dbComm, "@SS_Edition", DbType.String, SS_Edition)
            db.AddInParameter(dbComm, "@M_Code", DbType.String, M_Code)
            db.AddInParameter(dbComm, "@SP_CusterID", DbType.String, SP_CusterID)
            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
            db.AddInParameter(dbComm, "@SP_Check", DbType.Boolean, SP_Check)
            db.AddInParameter(dbComm, "@StartDate", DbType.String, StartDate)
            db.AddInParameter(dbComm, "@EndDate", DbType.String, EndDate)
            db.AddInParameter(dbComm, "@SP_AddStartDate", DbType.String, SP_AddStartDate)
            db.AddInParameter(dbComm, "@SP_AddEndDate", DbType.String, SP_AddEndDate)
            db.AddInParameter(dbComm, "@SO_IDCheck", DbType.Boolean, SO_IDCheck)
            db.AddInParameter(dbComm, "@SP_AddUserID", DbType.String, SP_AddUserID)


            Dim FeatureList As New List(Of SampleSendInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillSampleSendType(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function SampleSendSP_GetList(ByVal SP_ID As String, ByVal AutoID As String, ByVal Type As String) As List(Of SampleSendInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleSendSP_GetList")

            db.AddInParameter(dbComm, "@SP_ID", DbType.String, SP_ID)
            db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)
            db.AddInParameter(dbComm, "@Type", DbType.String, Type)
            Dim FeatureList As New List(Of SampleSendInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillSampleSendType(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function SampleSend_Get(ByVal SP_ID As String) As SampleSendInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleSend_Get")
            db.AddInParameter(dbComm, "@SP_ID", DbType.String, SP_ID)
            Dim FeatureList As New SampleSendInfo
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.SP_ID = reader("SP_ID").ToString
                End While
                Return FeatureList
            End Using
        End Function

        Public Function SampleSend_GetQty(ByVal SO_ID As String, ByVal SS_Edition As String) As Integer
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleSend_GetQty")

            db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
            db.AddInParameter(dbComm, "@SS_Edition", DbType.String, SS_Edition)

            Dim StrCode_ID As Integer = 0
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    StrCode_ID = CInt(reader("Qty").ToString)
                End While
                SampleSend_GetQty = StrCode_ID
            End Using
        End Function
        Friend Function FillSampleSendType(ByVal reader As IDataReader) As SampleSendInfo
            '对应取得的数据
            On Error Resume Next
            Dim objInfo As New SampleSendInfo

            objInfo.SP_ID = reader("SP_ID").ToString
            objInfo.SO_ID = reader("SO_ID").ToString
            objInfo.SS_Edition = reader("SS_Edition").ToString
            objInfo.PS_NO = reader("PS_NO").ToString
            objInfo.SP_Qty = CInt(reader("SP_Qty").ToString)
            objInfo.SP_SendDate = CDate(reader("SP_SendDate").ToString)
            objInfo.SP_CusterID = reader("SP_CusterID").ToString
            objInfo.PS_NO = reader("PS_NO").ToString
            objInfo.PM_M_Code = reader("PM_M_Code").ToString
            objInfo.M_Code = reader("M_Code").ToString
            objInfo.M_Name = reader("M_Name").ToString
            objInfo.PM_M_Name = reader("PM_M_Name").ToString
            objInfo.SP_AddUserName = reader("SP_AddUserName").ToString
            objInfo.SP_AddDate = CDate(reader("SP_AddDate").ToString)
            objInfo.SP_AddUserID = reader("SP_AddUserID").ToString
            objInfo.SP_ModifyUserID = reader("SP_ModifyUserID").ToString
            objInfo.SP_ModifyDate = CDate(reader("SP_ModifyDate").ToString)

            objInfo.SP_Check = CBool(reader("SP_Check").ToString)
            objInfo.SP_CheckDate = CDate(reader("SP_CheckDate").ToString)
            objInfo.SP_CheckRemark = reader("SP_CheckRemark").ToString
            objInfo.SP_CheckUserID = reader("SP_CheckUserID").ToString
            objInfo.SP_CheckUserName = reader("SP_CheckUserName").ToString

            objInfo.SP_InCheck = CBool(reader("SP_InCheck").ToString)
            objInfo.SP_InCheckDate = CDate(reader("SP_InCheckDate").ToString)
            objInfo.SP_InCheckRemark = reader("SP_InCheckRemark").ToString
            objInfo.SP_InCheckUserID = reader("SP_InCheckUserID").ToString
            objInfo.SP_InCheckUserName = reader("SP_InCheckUserName").ToString

            objInfo.SP_Remark = reader("SP_Remark").ToString
            objInfo.C_ChsName = reader("C_ChsName").ToString
            objInfo.AutoID = CDbl(reader("AutoID").ToString)
            objInfo.SP_ExpDeliveryID = reader("SP_ExpDeliveryID").ToString
            objInfo.SP_ExpCompany = reader("SP_ExpCompany").ToString
            objInfo.SO_SampleID = reader("SO_SampleID").ToString
            objInfo.PK_Code_ID = reader("PK_Code_ID").ToString

            Return objInfo
        End Function
    End Class
End Namespace

