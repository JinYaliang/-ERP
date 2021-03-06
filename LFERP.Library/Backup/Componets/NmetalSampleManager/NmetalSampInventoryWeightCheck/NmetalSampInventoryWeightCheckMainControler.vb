Namespace LFERP.Library.NmetalSampleManager.NmetalSampInventoryWeightCheck

    Public Class NmetalSampInventoryWeightCheckMainControler
        ''' <summary>
        ''' 新增
        ''' </summary>
        ''' <param name="objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampInventoryWeightCheckMain_Add(ByVal objinfo As NmetalSampInventoryWeightCheckMainInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampInventoryWeightCheckMain_Add")

                db.AddInParameter(dbComm, "@CH_NO", DbType.String, objinfo.CH_NO)
                db.AddInParameter(dbComm, "@DepID", DbType.String, objinfo.DepID)
                db.AddInParameter(dbComm, "@CH_Date", DbType.Date, objinfo.CH_Date)
                db.AddInParameter(dbComm, "@CH_Action", DbType.String, objinfo.CH_Action)
                db.AddInParameter(dbComm, "@CH_Remark", DbType.String, objinfo.CH_Remark)

                db.AddInParameter(dbComm, "@AddAction", DbType.String, objinfo.AddAction)
                db.AddInParameter(dbComm, "@AddDate", DbType.Date, objinfo.AddDate)

                db.AddInParameter(dbComm, "@CheckDwonRate", DbType.Decimal, objinfo.CheckDwonRate)
                db.AddInParameter(dbComm, "@CheckUpRate", DbType.Decimal, objinfo.CheckUpRate)



                db.ExecuteNonQuery(dbComm)
                NmetalSampInventoryWeightCheckMain_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampInventoryWeightCheckMain_Add = False
            End Try
        End Function
        ''' <summary>
        ''' 修改
        ''' </summary>
        ''' <remarks></remarks>
        Public Function NmetalSampInventoryWeightCheckMain_Update(ByVal objinfo As NmetalSampInventoryWeightCheckMainInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampInventoryWeightCheckMain_Update")


                db.AddInParameter(dbComm, "@AutoID", DbType.String, objinfo.AutoID)
                db.AddInParameter(dbComm, "@CH_NO", DbType.String, objinfo.CH_NO)
                db.AddInParameter(dbComm, "@DepID", DbType.String, objinfo.DepID)
                db.AddInParameter(dbComm, "@CH_Date", DbType.Date, objinfo.CH_Date)
                db.AddInParameter(dbComm, "@CH_Action", DbType.String, objinfo.CH_Action)

                db.AddInParameter(dbComm, "@CH_Remark", DbType.String, objinfo.CH_Remark)
                db.AddInParameter(dbComm, "@ModifyUserID", DbType.String, objinfo.ModifyUserID)
                db.AddInParameter(dbComm, "@ModifyDate", DbType.Date, objinfo.ModifyDate)

                db.AddInParameter(dbComm, "@CheckDwonRate", DbType.Decimal, objinfo.CheckDwonRate)
                db.AddInParameter(dbComm, "@CheckUpRate", DbType.Decimal, objinfo.CheckUpRate)

                db.ExecuteNonQuery(dbComm)
                NmetalSampInventoryWeightCheckMain_Update = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampInventoryWeightCheckMain_Update = False
            End Try
        End Function
        ''' <summary>
        ''' 刪除
        ''' </summary>
        ''' <param name="CH_NO"></param>
        ''' <param name="AutoID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampInventoryWeightCheckMain_Delete(ByVal AutoID As String, ByVal CH_NO As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampInventoryWeightCheckMain_Delete")

                db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)
                db.AddInParameter(dbComm, "@CH_NO", DbType.String, CH_NO)

                db.ExecuteNonQuery(dbComm)
                NmetalSampInventoryWeightCheckMain_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampInventoryWeightCheckMain_Delete = False
            End Try
        End Function
        ''' <summary>
        ''' 審核
        ''' </summary>
        ''' <param name="objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampInventoryWeightCheckMain_Check(ByVal objinfo As NmetalSampInventoryWeightCheckMainInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampInventoryWeightCheckMain_Check")

                db.AddInParameter(dbComm, "@CH_NO", DbType.String, objinfo.CH_NO)
                db.AddInParameter(dbComm, "@CheckStatus", DbType.String, objinfo.CheckStatus)
                db.AddInParameter(dbComm, "@CheckAction", DbType.String, objinfo.CheckAction)
                db.AddInParameter(dbComm, "@CheckDate", DbType.Date, objinfo.CheckDate)
                db.AddInParameter(dbComm, "@CheckRemark", DbType.String, objinfo.CheckRemark)
                db.AddInParameter(dbComm, "@CheckWastWeight", DbType.String, objinfo.CheckWastWeight)
                db.AddInParameter(dbComm, "@CheckType", DbType.String, objinfo.CheckType)

                db.ExecuteNonQuery(dbComm)
                NmetalSampInventoryWeightCheckMain_Check = True

            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampInventoryWeightCheckMain_Check = False
            End Try
        End Function

        ''' <summary>
        ''' 收料重量更新
        ''' </summary>
        ''' <param name="CH_NO"></param>
        ''' <param name="DepID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampInventoryWeightCheckMain_CheckQty(ByVal CH_NO As String, ByVal DepID As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampInventoryWeightCheckMain_CheckQty")

                db.AddInParameter(dbComm, "@CH_NO", DbType.String, CH_NO)
                db.AddInParameter(dbComm, "@DepID", DbType.String, DepID)

                db.ExecuteNonQuery(dbComm)
                NmetalSampInventoryWeightCheckMain_CheckQty = True

            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampInventoryWeightCheckMain_CheckQty = False
            End Try
        End Function


        ''' <summary>
        '''獲取編號
        ''' </summary>
        ''' <param name="CH_NO"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampInventoryWeightCheckMain_GetID(ByVal CH_NO As String) As NmetalSampInventoryWeightCheckMainInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampInventoryWeightCheckMain_GetID")

            db.AddInParameter(dbComm, "@CH_NO", DbType.String, CH_NO)

            Dim objinfo As New NmetalSampInventoryWeightCheckMainInfo
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    objinfo.CH_NO = reader("CH_NO").ToString
                End While
                Return objinfo
            End Using
        End Function
        ''' <summary>
        ''' 查询
        ''' </summary>
        ''' <param name="AutoID"></param>
        ''' <param name="CH_NO"></param>
        ''' <param name="DepID"></param>
        ''' <param name="CH_Action"></param>
        ''' <param name="StartDate"></param>
        ''' <param name="EndDate"></param>
        ''' <param name="CheckStatus"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampInventoryWeightCheckMain_GetList(ByVal AutoID As String, ByVal CH_NO As String, ByVal DepID As String, ByVal CH_Action As String, ByVal StartDate As String, ByVal EndDate As String, ByVal CheckStatus As String) As List(Of NmetalSampInventoryWeightCheckMainInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampInventoryWeightCheckMain_GetList")

            db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)
            db.AddInParameter(dbComm, "@CH_NO", DbType.String, CH_NO)
            db.AddInParameter(dbComm, "@DepID", DbType.String, DepID)
            db.AddInParameter(dbComm, "@CH_Action", DbType.String, CH_Action)
            db.AddInParameter(dbComm, "@StartDate", DbType.String, StartDate)

            db.AddInParameter(dbComm, "@EndDate", DbType.String, EndDate)
            db.AddInParameter(dbComm, "@CheckStatus", DbType.Boolean, CheckStatus)

            Dim FeatureList As New List(Of NmetalSampInventoryWeightCheckMainInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillSampInventoryWeightCheckMain(reader))
                End While
                Return FeatureList
            End Using
        End Function

        ''' <summary>
        ''' 判断审核 条件
        ''' </summary>
        ''' <param name="CH_NO"></param>
        ''' <param name="DepID"></param>
        ''' <param name="CheckType"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampInventoryWeightCheckMain_CheckList(ByVal CH_NO As String, ByVal DepID As String, ByVal CheckType As String) As List(Of NmetalSampInventoryWeightCheckMainInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampInventoryWeightCheckMain_CheckList")

            db.AddInParameter(dbComm, "@CH_NO", DbType.String, CH_NO)
            db.AddInParameter(dbComm, "@DepID", DbType.String, DepID)
            db.AddInParameter(dbComm, "@CheckType", DbType.String, CheckType)

            Dim FeatureList As New List(Of NmetalSampInventoryWeightCheckMainInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillSampInventoryWeightCheckMain(reader))
                End While
                Return FeatureList
            End Using
        End Function


        ''' <summary>
        ''' 綁定數據
        ''' </summary>
        ''' <param name="reader"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Function FillSampInventoryWeightCheckMain(ByVal reader As IDataReader) As NmetalSampInventoryWeightCheckMainInfo
            On Error Resume Next

            Dim objInfo As New NmetalSampInventoryWeightCheckMainInfo

            objInfo.AutoID = reader("AutoID")
            objInfo.CH_NO = reader("CH_NO").ToString
            objInfo.DepID = reader("DepID").ToString
            objInfo.CH_Date = CDate(reader("CH_Date").ToString)
            objInfo.CH_Action = reader("CH_Action").ToString

            objInfo.CH_Remark = reader("CH_Remark").ToString
            objInfo.AddAction = reader("AddAction").ToString
            objInfo.AddDate = CDate(reader("AddDate").ToString)
            objInfo.ModifyUserID = reader("ModifyUserID").ToString
            objInfo.ModifyDate = CDate(reader("ModifyDate").ToString)

            objInfo.CheckStatus = reader("CheckStatus")
            objInfo.CheckAction = reader("CheckAction").ToString
            objInfo.CheckDate = CDate(reader("CheckDate").ToString)
            objInfo.CheckRemark = reader("CheckRemark").ToString

            objInfo.DepName = reader("DepName").ToString
            objInfo.CHAction_Name = reader("CHAction_Name").ToString
            objInfo.Add_Name = reader("Add_Name").ToString
            objInfo.Modify_Name = reader("Modify_Name").ToString
            objInfo.Check_Name = reader("Check_Name").ToString

            objInfo.CheckWastWeight = reader("CheckWastWeight")
            objInfo.CheckType = reader("CheckType").ToString

            objInfo.CheckDwonRate = reader("CheckDwonRate")
            objInfo.CheckUpRate = reader("CheckUpRate")

            objInfo.Code_ID = reader("Code_ID").ToString


            Return objInfo
        End Function
    End Class
End Namespace