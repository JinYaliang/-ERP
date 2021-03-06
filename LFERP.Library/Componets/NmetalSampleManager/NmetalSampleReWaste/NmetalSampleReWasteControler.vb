Namespace LFERP.Library.NmetalSampleManager.NmetalSampleReWaste

    Public Class NmetalSampleReWasteControler
        ''' <summary>
        ''' 新增
        ''' </summary>
        ''' <param name="objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleReWaste_Add(ByVal objinfo As NmetalSampleReWasteInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleReWaste_Add")

                db.AddInParameter(dbComm, "@ReNO", DbType.String, objinfo.ReNO)
                db.AddInParameter(dbComm, "@ReDepID", DbType.String, objinfo.ReDepID)
                db.AddInParameter(dbComm, "@OutDepID", DbType.String, objinfo.OutDepID)
                db.AddInParameter(dbComm, "@ReDate", DbType.Date, objinfo.ReDate)
                db.AddInParameter(dbComm, "@ReType", DbType.String, objinfo.ReType)

                db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "@PM_Type", DbType.String, objinfo.PM_Type)
                db.AddInParameter(dbComm, "@PS_NO", DbType.String, objinfo.PS_NO)
                db.AddInParameter(dbComm, "@ReWeight", DbType.Decimal, objinfo.ReWeight)
                db.AddInParameter(dbComm, "@Remark", DbType.String, objinfo.Remark)

                db.AddInParameter(dbComm, "@RealWeight", DbType.Decimal, objinfo.RealWeight)

                db.AddInParameter(dbComm, "@AddUserID", DbType.String, objinfo.AddUserID)
                db.AddInParameter(dbComm, "@AddDate", DbType.Date, objinfo.AddDate)

                db.AddInParameter(dbComm, "@PS_NOIn", DbType.String, objinfo.PS_NOIn)
                db.AddInParameter(dbComm, "@OutCardID", DbType.String, objinfo.OutCardID)


                db.ExecuteNonQuery(dbComm)
                NmetalSampleReWaste_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleReWaste_Add = False
            End Try
        End Function

        ''' <summary>
        ''' 修改
        ''' </summary>
        ''' <remarks></remarks>
        Public Function NmetalSampleReWaste_Update(ByVal objinfo As NmetalSampleReWasteInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleReWaste_Update")
                db.AddInParameter(dbComm, "@AutoID", DbType.String, objinfo.AutoID)
                db.AddInParameter(dbComm, "@ReNO", DbType.String, objinfo.ReNO)
                db.AddInParameter(dbComm, "@ReDepID", DbType.String, objinfo.ReDepID)
                db.AddInParameter(dbComm, "@OutDepID", DbType.String, objinfo.OutDepID)
                db.AddInParameter(dbComm, "@ReDate", DbType.Date, objinfo.ReDate)

                db.AddInParameter(dbComm, "@ReType", DbType.String, objinfo.ReType)
                db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "@PM_Type", DbType.String, objinfo.PM_Type)
                db.AddInParameter(dbComm, "@PS_NO", DbType.String, objinfo.PS_NO)
                db.AddInParameter(dbComm, "@ReWeight", DbType.Decimal, objinfo.ReWeight)

                db.AddInParameter(dbComm, "@RealWeight", DbType.Decimal, objinfo.RealWeight)

                db.AddInParameter(dbComm, "@Remark", DbType.String, objinfo.Remark)
                db.AddInParameter(dbComm, "@ModifyUserID", DbType.String, objinfo.ModifyUserID)
                db.AddInParameter(dbComm, "@ModifyDate", DbType.Date, objinfo.ModifyDate)

                db.AddInParameter(dbComm, "@PS_NOIn", DbType.String, objinfo.PS_NOIn)
                db.AddInParameter(dbComm, "@OutCardID", DbType.String, objinfo.OutCardID)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleReWaste_Update = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleReWaste_Update = False
            End Try
        End Function

        ''' <summary>
        ''' 刪除
        ''' </summary>
        ''' <param name="ReNO"></param>
        ''' <param name="AutoID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleReWaste_Delete(ByVal AutoID As String, ByVal ReNO As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleReWaste_Delete")

                db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)
                db.AddInParameter(dbComm, "@ReNO", DbType.String, ReNO)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleReWaste_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleReWaste_Delete = False
            End Try
        End Function

        ''' <summary>
        ''' 審核
        ''' </summary>
        ''' <param name="objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleReWaste_Check(ByVal objinfo As NmetalSampleReWasteInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleReWaste_Check")

                db.AddInParameter(dbComm, "@ReNO", DbType.String, objinfo.ReNO)
                db.AddInParameter(dbComm, "@ReCheck", DbType.String, objinfo.ReCheck)
                db.AddInParameter(dbComm, "@CheckDate", DbType.Date, objinfo.CheckDate)
                db.AddInParameter(dbComm, "@CheckUserID", DbType.String, objinfo.CheckUserID)
                db.AddInParameter(dbComm, "@CheckRemark", DbType.String, objinfo.CheckRemark)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleReWaste_Check = True

            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleReWaste_Check = False
            End Try
        End Function

        ''' <summary>
        '''獲取編號
        ''' </summary>
        ''' <param name="ReNO"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleReWaste_GetID(ByVal ReNO As String) As NmetalSampleReWasteInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleReWaste_GetID")

            db.AddInParameter(dbComm, "@ReNO", DbType.String, ReNO)

            Dim FeatureList As New NmetalSampleReWasteInfo
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.ReNO = reader("ReNO").ToString
                End While
                Return FeatureList
            End Using
        End Function


        Public Function NmetalSampleReWaste_GetList(ByVal AutoID As String, ByVal ReNO As String, ByVal ReDepID As String, ByVal OutDepID As String, ByVal Incheck As String, ByVal ReCheck As String, ByVal StartDate As String, ByVal EndDate As String) As List(Of NmetalSampleReWasteInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleReWaste_GetList")

            db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)
            db.AddInParameter(dbComm, "@ReNO", DbType.String, ReNO)
            db.AddInParameter(dbComm, "@StartDate", DbType.String, StartDate)
            db.AddInParameter(dbComm, "@EndDate", DbType.String, EndDate)
            db.AddInParameter(dbComm, "@Incheck", DbType.String, Incheck)
            db.AddInParameter(dbComm, "@ReCheck", DbType.Boolean, ReCheck)

            db.AddInParameter(dbComm, "@ReDepID", DbType.String, ReDepID)
            db.AddInParameter(dbComm, "@OutDepID", DbType.String, OutDepID)

            Dim FeatureList As New List(Of NmetalSampleReWasteInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleReWaste(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function NmetalSampleReWaste_InCheck(ByVal objinfo As NmetalSampleReWasteInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleReWaste_InCheck")

                db.AddInParameter(dbComm, "@ReNO", DbType.String, objinfo.ReNO)
                db.AddInParameter(dbComm, "@Incheck", DbType.Boolean, objinfo.Incheck)
                db.AddInParameter(dbComm, "@InCheckDate", DbType.String, objinfo.InCheckDate)
                db.AddInParameter(dbComm, "@InCheckUserID", DbType.String, objinfo.InCheckUserID)
                db.AddInParameter(dbComm, "@InCheckWeight", DbType.Decimal, objinfo.InCheckWeight)
                db.AddInParameter(dbComm, "@InCheckRemark", DbType.String, objinfo.InCheckRemark)

                db.AddInParameter(dbComm, "@InCardID", DbType.String, objinfo.InCardID)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleReWaste_InCheck = True

            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleReWaste_InCheck = False
            End Try
        End Function


        ''' <summary>
        ''' 綁定數據
        ''' </summary>
        ''' <param name="reader"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Function FillNmetalSampleReWaste(ByVal reader As IDataReader) As NmetalSampleReWasteInfo
            On Error Resume Next

            Dim objInfo As New NmetalSampleReWasteInfo

            objInfo.AutoID = reader("AutoID")
            objInfo.ReNO = reader("ReNO").ToString
            objInfo.ReDepID = reader("ReDepID").ToString
            objInfo.OutDepID = reader("OutDepID").ToString
            objInfo.ReDate = CDate(reader("ReDate").ToString)

            objInfo.ReType = reader("ReType").ToString
            objInfo.PM_M_Code = reader("PM_M_Code").ToString
            objInfo.PM_Type = reader("PM_Type").ToString
            objInfo.PS_NO = reader("PS_NO").ToString
            objInfo.ReWeight = reader("ReWeight")

            objInfo.Remark = reader("Remark").ToString
            objInfo.AddUserID = reader("AddUserID").ToString
            objInfo.AddDate = CDate(reader("AddDate").ToString)
            objInfo.ModifyUserID = reader("ModifyUserID").ToString
            objInfo.ModifyDate = CDate(reader("ModifyDate").ToString)

            objInfo.ReCheck = reader("ReCheck").ToString
            objInfo.CheckDate = CDate(reader("CheckDate").ToString)
            objInfo.CheckUserID = reader("CheckUserID").ToString
            objInfo.CheckRemark = reader("CheckRemark").ToString

            objInfo.ReDepName = reader("ReDepName").ToString
            objInfo.OutDepName = reader("OutDepName").ToString
            objInfo.PS_Name = reader("PS_Name").ToString
            objInfo.Add_Name = reader("Add_Name").ToString
            objInfo.Modify_Name = reader("Modify_Name").ToString
            objInfo.Check_Name = reader("Check_Name").ToString

            objInfo.Incheck = reader("Incheck")
            objInfo.InCheckDate = reader("InCheckDate")
            objInfo.InCheckUserID = reader("InCheckUserID").ToString
            objInfo.InCheckWeight = reader("InCheckWeight")
            objInfo.InCheckRemark = reader("InCheckRemark").ToString

            objInfo.InCheckName = reader("InCheckName").ToString

            objInfo.RealWeight = reader("RealWeight")

            objInfo.ReTypeName = reader("ReTypeName").ToString
            objInfo.ReTypeID = reader("ReTypeID").ToString
            objInfo.IsBarCode = reader("IsBarCode")
            objInfo.IsSameDep = reader("IsSameDep")
            objInfo.IsSamePS_NO = reader("IsSamePS_NO")

            objInfo.PS_NOIn = reader("PS_NOIn")
            objInfo.PS_NameIn = reader("PS_NameIn")
            objInfo.IsDeductOut = reader("IsDeductOut")
            objInfo.IsDeductIn = reader("IsDeductIn")

            objInfo.OutCardID = reader("OutCardID")
            objInfo.InCardID = reader("InCardID")


            Return objInfo
        End Function


        ''' <summary>
        ''' 溶废料处理类型
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleReWasteType_GetList(ByVal ReTypeID As String) As List(Of NmetalSampleReWasteInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleReWasteType_GetList")

            db.AddInParameter(dbComm, "@ReTypeID", DbType.String, ReTypeID)

            Dim FeatureList As New List(Of NmetalSampleReWasteInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleReWaste(reader))
                End While
                Return FeatureList
            End Using
        End Function


    End Class
End Namespace
