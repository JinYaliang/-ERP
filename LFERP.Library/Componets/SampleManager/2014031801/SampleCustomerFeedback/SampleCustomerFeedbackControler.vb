
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Data.Sql


Namespace LFERP.Library.SampleManager.SampleCustomerFeedback



    Public Class SampleCustomerFeedbackControler
        Friend Function FillCustomerFeedbackInfo(ByVal reader As IDataReader) As SampleCustomerFeedbackinfo
            '对应取得的数据
            On Error Resume Next
            Dim objInfo As New SampleCustomerFeedbackinfo
            objInfo.AutoID = reader("AutoID").ToString
            objInfo.SO_ID = reader("SO_ID").ToString
            objInfo.SC_Edition = reader("SS_Edition").ToString
            objInfo.SC_customerid = reader("SC_CustomerID").ToString
            objInfo.SC_Description = reader("SC_Description").ToString

            objInfo.SC_Picture = reader("SC_Picture").ToString
            objInfo.SC_Process = reader("SC_Process").ToString
            objInfo.SC_Confirmation = reader("SC_Confirmation").ToString
            objInfo.SC_Confirmationdate = reader("SC_ConfirmationDate").ToString
            objInfo.SC_ConfirmationQty = reader("SC_ConfirmationQty").ToString

            objInfo.SC_AdduserID = reader("SC_AdduserID").ToString
            objInfo.SC_Adddate = reader("SC_Adddate").ToString
            objInfo.SC_ModifyuserID = reader("SC_ModifyUserID").ToString
            objInfo.SC_Modifydate = reader("SC_ModifyDate").ToString
            objInfo.PM_M_Code = reader("PM_M_Code").ToString

            objInfo.M_Code = reader("M_Code").ToString
            objInfo.SO_Gauge = reader("SO_Gauge").ToString
            objInfo.SO_CusterID = reader("SO_CusterID").ToString
            objInfo.SO_CusterPO = reader("SO_CusterPO").ToString

            Return objInfo
        End Function

        Friend Function FillCustomerFeedbackphoto(ByVal reader As IDataReader) As SampleCustomerFeedbackinfo
            '对应取得的图片数据
            On Error Resume Next
            Dim objInfo As New SampleCustomerFeedbackinfo

            objInfo.SO_ID = reader("SO_ID").ToString
            objInfo.SC_Edition = reader("SS_Edition").ToString
            objInfo.PM_M_Code = reader("PM_M_Code").ToString
            objInfo.M_Code = reader("M_Code").ToString
            objInfo.M_Name = reader("M_Name").ToString

            objInfo.SC_FileName = reader("SC_FileName").ToString
            objInfo.SC_FileID = reader("SC_FileID").ToString
            objInfo.SC_Path = reader("SC_Path").ToString
            objInfo.AutoID = reader("AutoID").ToString
            objInfo.SC_SendNo = reader("SP_ID").ToString
            Return objInfo
        End Function
        ''' <summary>
        ''' 獲取客戶回饋资料
        ''' </summary>
        Public Function SampleCustomerFeedback_getlist(ByVal SO_ID As String, ByVal SC_Edition As String, ByVal SC_CustomerID As String, ByVal SC_Confirmation As String, _
                                                       ByVal SC_ConfirmationDate As Date, ByVal SC_fromdate As Date, ByVal SC_todate As Date, ByVal PM_M_Code As String, _
                                                       ByVal M_Code As String, ByVal Autoid As String, ByVal SO_IDCheck As Boolean) As List(Of SampleCustomerFeedbackinfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCustomerFeedback_getlist")
            db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
            db.AddInParameter(dbComm, "@SC_Edition", DbType.String, SC_Edition)
            db.AddInParameter(dbComm, "@SC_CustomerID", DbType.String, SC_CustomerID)
            db.AddInParameter(dbComm, "@SC_Confirmation", DbType.String, SC_Confirmation)

            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
            db.AddInParameter(dbComm, "@M_Code", DbType.String, M_Code)
            db.AddInParameter(dbComm, "@Autoid", DbType.String, Autoid)
            db.AddInParameter(dbComm, "@SO_IDCheck", DbType.Boolean, SO_IDCheck)

            Dim FeatureList As New List(Of SampleCustomerFeedbackinfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillSampleCustomerFeedbackType(reader))
                End While
                Return FeatureList
            End Using
        End Function
        ''' <summary>
        ''' 獲取样版订单信息
        ''' </summary>
        Public Function getSampleOrdersinfo(ByVal SO_ID As String) As List(Of SampleCustomerFeedbackinfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleOrdersMain_GetID")
            Dim FeatureList As New List(Of SampleCustomerFeedbackinfo)
            db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)

            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillCustomerFeedbackInfo(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function SampleSend_GetList(ByVal sapc As String) As List(Of SampleCustomerFeedbackinfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleSend_GetList1")
            Dim FeatureList As New List(Of SampleCustomerFeedbackinfo)
            db.AddInParameter(dbComm, "@SP_CusterID", DbType.String, sapc)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillCustomerFeedbackphoto(reader))
                End While
                Return FeatureList
            End Using
        End Function

        ''' <summary>
        ''' 刪除記錄
        ''' </summary>
        Public Function SampleCustomerFeedback_Delete(ByVal autoid As String) As Boolean
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCustomerFeedback_Delete")
            db.AddInParameter(dbComm, "@AutoID", DbType.String, autoid)


            Return db.ExecuteNonQuery(dbComm)
        End Function
        ''' <summary>
        ''' 刪除图片
        ''' </summary>
        Public Function SampleCustomerFeedbackPhoto_del(ByVal soid As Integer) As Boolean
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCustomerFeedbackPhoto_del")
            db.AddInParameter(dbComm, "@AutoID", DbType.String, soid)
            Return db.ExecuteNonQuery(dbComm)
        End Function
        ''' <summary>
        ''' 增加記錄
        ''' </summary>
        Public Function SampleCustomerFeedback_Add(ByVal objinfo As SampleCustomerFeedbackinfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCustomerFeedback_Add")

                db.AddInParameter(dbComm, "@SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "@SC_Edition", DbType.String, objinfo.SC_Edition)
                db.AddInParameter(dbComm, "@SC_CustomerID", DbType.String, objinfo.SO_CusterID)
                db.AddInParameter(dbComm, "@SC_Description", DbType.String, objinfo.SC_Description)
                db.AddInParameter(dbComm, "@SC_Confirmation", DbType.String, objinfo.SC_Confirmation)

                db.AddInParameter(dbComm, "@SC_ConfirmationDate", DbType.Date, objinfo.SC_Confirmationdate)
                db.AddInParameter(dbComm, "@SC_Picture", DbType.String, objinfo.SC_Picture)
                db.AddInParameter(dbComm, "@SC_Process", DbType.String, objinfo.SC_Process)
                db.AddInParameter(dbComm, "@SC_AdduserID", DbType.String, objinfo.SC_AdduserID)
                db.AddInParameter(dbComm, "@SC_Adddate", DbType.Date, objinfo.SC_Adddate)

                db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "@M_Code", DbType.String, objinfo.M_Code)
                db.AddInParameter(dbComm, "@SC_SendNo", DbType.String, objinfo.SC_SendNo)


                If db.ExecuteNonQuery(dbComm) = -1 Then
                    MsgBox("寄送单: " + objinfo.SC_SendNo + "  已经存在")
                End If
                SampleCustomerFeedback_Add = True

            Catch ex As Exception
                MsgBox(ex.Message)
                SampleCustomerFeedback_Add = False
            End Try
        End Function
        ''' <summary>
        ''' 添加图片
        ''' </summary>
        Public Function SampleCustomerFeedbackPhoto_Add(ByVal objinfo As SampleCustomerFeedbackinfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCustomerFeedbackPhoto_Add")

                db.AddInParameter(dbComm, "@SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "@SC_Edition", DbType.String, objinfo.SC_Edition)
                db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "@SC_AdduserID", DbType.String, objinfo.SC_AdduserID)
                db.AddInParameter(dbComm, "@SC_Adddate", DbType.String, objinfo.SC_Adduserdate)

                db.AddInParameter(dbComm, "@SC_FileID", DbType.String, objinfo.SC_FileID)
                db.AddInParameter(dbComm, "@SC_FileName", DbType.String, objinfo.SC_FileName)
                db.AddInParameter(dbComm, "@SC_Path", DbType.String, objinfo.SC_Path)

                db.ExecuteNonQuery(dbComm)
                SampleCustomerFeedbackPhoto_Add = True

            Catch ex As Exception
                MsgBox(ex.Message)
                SampleCustomerFeedbackPhoto_Add = False
            End Try
        End Function
        ''' <summary>
        ''' 更新記錄
        ''' </summary>
        Public Function SampleCustomerFeedback_Update(ByVal objinfo As SampleCustomerFeedbackinfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand
                dbComm = db.GetStoredProcCommand("SampleCustomerFeedback_Update")
                db.AddInParameter(dbComm, "@SC_ModifyUserID", DbType.String, objinfo.SC_ModifyuserID)
                db.AddInParameter(dbComm, "@SC_ModifyDate", DbType.Date, Convert.ToDateTime(objinfo.SC_Modifydate))

                db.AddInParameter(dbComm, "@AutoID", DbType.String, objinfo.AutoID)
                db.AddInParameter(dbComm, "@SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "@SC_Edition", DbType.String, objinfo.SC_Edition)
                db.AddInParameter(dbComm, "@SC_CustomerID", DbType.String, objinfo.SC_customerid)
                db.AddInParameter(dbComm, "@SC_Description", DbType.String, objinfo.SC_Description)

                db.AddInParameter(dbComm, "@SC_Picture", DbType.String, objinfo.SC_Picture)
                db.AddInParameter(dbComm, "@SC_Process", DbType.String, objinfo.SC_Process)
                db.AddInParameter(dbComm, "@SC_SendNo", DbType.String, objinfo.SC_SendNo)
                db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "@SC_ConfirmationQty", DbType.String, objinfo.SC_ConfirmationQty)

                db.ExecuteNonQuery(dbComm)
                SampleCustomerFeedback_Update = True

            Catch ex As Exception
                MsgBox(ex.Message)
                SampleCustomerFeedback_Update = False
            End Try
        End Function
        ''' <summary>
        ''' 更新記錄
        ''' </summary>
        Public Function SampleCustomerFeedback_Update_Comfarmation(ByVal objinfo As SampleCustomerFeedbackinfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand
                dbComm = db.GetStoredProcCommand("SampleCustomerFeedback_Update_Comfarmation")
                db.AddInParameter(dbComm, "@SC_Confirmation", DbType.String, objinfo.SC_Confirmation)
                db.AddInParameter(dbComm, "@SC_ConfirmationDate", DbType.Date, Convert.ToDateTime(objinfo.SC_Confirmationdate))

                db.AddInParameter(dbComm, "@AutoID", DbType.String, objinfo.AutoID)
                db.AddInParameter(dbComm, "@SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "@SC_Edition", DbType.String, objinfo.SC_Edition)
                db.AddInParameter(dbComm, "@SC_CustomerID", DbType.String, objinfo.SC_customerid)
                db.AddInParameter(dbComm, "@SC_Description", DbType.String, objinfo.SC_Description)

                db.AddInParameter(dbComm, "@SC_Picture", DbType.String, objinfo.SC_Picture)
                db.AddInParameter(dbComm, "@SC_Process", DbType.String, objinfo.SC_Process)
                db.AddInParameter(dbComm, "@SC_SendNo", DbType.String, objinfo.SC_SendNo)
                db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "@SC_ConfirmationQty", DbType.String, objinfo.SC_ConfirmationQty)

                db.ExecuteNonQuery(dbComm)
                SampleCustomerFeedback_Update_Comfarmation = True

            Catch ex As Exception
                MsgBox(ex.Message)
                SampleCustomerFeedback_Update_Comfarmation = False
            End Try
        End Function
        ''' <summary>
        ''' 更新图片
        ''' </summary>
        Public Function SampleCustomerFeedbackPhoto_update(ByVal objinfo As SampleCustomerFeedbackinfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCustomerFeedbackPhoto_update")

                db.AddInParameter(dbComm, "@SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "@SC_Edition", DbType.String, objinfo.SC_Edition)
                db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "@SC_ModifyUserID", DbType.String, objinfo.SC_AdduserID)
                db.AddInParameter(dbComm, "@SC_ModifyDate", DbType.String, objinfo.SC_Adduserdate)

                db.AddInParameter(dbComm, "@SC_FileID", DbType.String, objinfo.SC_FileID)
                db.AddInParameter(dbComm, "@SC_FileName", DbType.String, objinfo.SC_FileName)
                db.AddInParameter(dbComm, "@SC_Path", DbType.String, objinfo.SC_Path)
                db.AddInParameter(dbComm, "AutoID", DbType.String, objinfo.AutoID)

                db.ExecuteNonQuery(dbComm)
                SampleCustomerFeedbackPhoto_update = True

            Catch ex As Exception
                MsgBox(ex.Message)
                SampleCustomerFeedbackPhoto_update = False
            End Try
        End Function
        ''' <summary>
        ''' 增加新版本
        ''' </summary>
        Public Function SampleOrdersSub_Addedition(ByVal SO_ID As String, ByVal PM_M_Code As String, ByVal SS_Remark As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleOrdersSub_Addedition")

                db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
                db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
                db.AddInParameter(dbComm, "@SS_Remark", DbType.String, SS_Remark)
                db.ExecuteNonQuery(dbComm)
                SampleOrdersSub_Addedition = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleOrdersSub_Addedition = False
            End Try
        End Function

        Friend Function FillSampleCustomerFeedbackType(ByVal reader As IDataReader) As SampleCustomerFeedbackinfo
            '对应取得的数据
            On Error Resume Next
            Dim objInfo As New SampleCustomerFeedbackinfo

            objInfo.SO_ID = reader("SO_ID").ToString
            objInfo.SC_Edition = reader("SC_Edition").ToString
            objInfo.SC_customerid = reader("SC_CustomerID").ToString
            objInfo.SC_Description = reader("SC_Description").ToString
            objInfo.SC_Confirmation = reader("SC_Confirmation").ToString

            objInfo.SC_Confirmationdate = reader("SC_Confirmationdate").ToString
            objInfo.SC_AdduserID = reader("SC_Adduser").ToString
            objInfo.SC_Adddate = Format(reader("SC_Adddate"), "yyyy-MM-dd")
            objInfo.SC_ModifyuserID = reader("SC_Modifyuser").ToString
            objInfo.SC_Modifydate = reader("SC_Modyfydate").ToString

            objInfo.AutoID = reader("AutoID")
            objInfo.SC_Process = reader("SC_Process").ToString
            objInfo.SC_Picture = reader("SC_Picture").ToString
            objInfo.PM_M_Code = reader("PM_M_Code").ToString
            objInfo.M_Code = reader("M_Code").ToString
            objInfo.M_Name = reader("M_Name").ToString

            objInfo.SO_CusterID = reader("SO_CusterID").ToString
            objInfo.SO_CusterNo = reader("SO_CusterNo").ToString
            objInfo.SO_CusterPO = reader("SO_CusterPO").ToString
            objInfo.SC_SendNo = reader("SC_SendNo").ToString
            objInfo.SP_Qty = reader("SP_Qty")
            objInfo.SO_OrderQty = reader("SO_OrderQty")

            Return objInfo
        End Function


    End Class
End Namespace