

Namespace LFERP.Library.NmetalSampleManager.NmetalSampleProcess

    Public Class NmetalSampleProcessControl
        Public Function MaterialCode2_Add(ByVal Objinfo As NmetalSampleProcessInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("MaterialCode2_Add")

                db.AddInParameter(dbComm, "@M_Code", DbType.String, Objinfo.M_Code)
                db.AddInParameter(dbComm, "@M_Name", DbType.String, Objinfo.M_Name)
                db.AddInParameter(dbComm, "@M_Unit", DbType.String, Objinfo.M_Unit)
                db.AddInParameter(dbComm, "@Type3ID", DbType.String, Objinfo.Type3ID)

                db.ExecuteNonQuery(dbComm)
                MaterialCode2_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                MaterialCode2_Add = False
            End Try
        End Function

        Public Function MaterialCode2_Update(ByVal Objinfo As NmetalSampleProcessInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("MaterialCode2_Update")

                db.AddInParameter(dbComm, "@M_Code", DbType.String, Objinfo.M_Code)
                db.AddInParameter(dbComm, "@M_Name", DbType.String, Objinfo.M_Name)
                db.AddInParameter(dbComm, "@Type3ID", DbType.String, Objinfo.Type3ID)

                db.ExecuteNonQuery(dbComm)
                MaterialCode2_Update = True
            Catch ex As Exception
                MsgBox(ex.Message)
                MaterialCode2_Update = False
            End Try

        End Function
        Public Function MaterialCode2_Del(ByVal M_Code As String) As Boolean

            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("MaterialCode2_Del")

                db.AddInParameter(dbComm, "@M_Code", DbType.String, M_Code)

                db.ExecuteNonQuery(dbComm)
                MaterialCode2_Del = True
            Catch ex As Exception
                MaterialCode2_Del = False
            End Try

        End Function
        ''' <summary>
        ''' 产品工艺主表新增
        ''' </summary>
        ''' <param name="Objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleProcessMain_Add(ByVal Objinfo As NmetalSampleProcessInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcessMain_Add")

                db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, Objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "@Pro_NO", DbType.String, Objinfo.Pro_NO)
                db.AddInParameter(dbComm, "@M_Code", DbType.String, Objinfo.M_Code)
                db.AddInParameter(dbComm, "@Pro_Type", DbType.String, Objinfo.Pro_Type)
                db.AddInParameter(dbComm, "@Type3ID", DbType.String, Objinfo.Type3ID)

                db.AddInParameter(dbComm, "@PM_Type", DbType.String, Objinfo.PM_Type)
                db.AddInParameter(dbComm, "@Pro_Weight", DbType.String, Objinfo.Pro_Weight)
                db.AddInParameter(dbComm, "@Pro_Remark", DbType.String, Objinfo.Pro_Remark)
                db.AddInParameter(dbComm, "@Pro_Describe", DbType.String, Objinfo.Pro_Describe)
                db.AddInParameter(dbComm, "@Pro_Photo", DbType.Binary, Objinfo.Pro_Photo)

                db.AddInParameter(dbComm, "@Pro_Action", DbType.String, Objinfo.Pro_Action)
                db.AddInParameter(dbComm, "@Pro_AddDate", DbType.Date, Objinfo.Pro_AddDate)
                db.AddInParameter(dbComm, "@Pro_EditDate", DbType.Date, Objinfo.Pro_EditDate)
                db.AddInParameter(dbComm, "@Pro_Rate", DbType.Double, Objinfo.Pro_Rate)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleProcessMain_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleProcessMain_Add = False
            End Try

        End Function
        ''' <summary>
        ''' 产品工艺主表修改
        ''' </summary>
        ''' <param name="objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleProcessMain_Update(ByVal objinfo As NmetalSampleProcessInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcessMain_Update")

                db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "@Pro_NO", DbType.String, objinfo.Pro_NO)
                db.AddInParameter(dbComm, "@M_Code", DbType.String, objinfo.M_Code)
                db.AddInParameter(dbComm, "@Pro_Type", DbType.String, objinfo.Pro_Type)
                db.AddInParameter(dbComm, "@Type3ID", DbType.String, objinfo.Type3ID)
                db.AddInParameter(dbComm, "@Pro_Weight", DbType.String, objinfo.Pro_Weight)
                db.AddInParameter(dbComm, "@PM_Type", DbType.String, objinfo.PM_Type)
                db.AddInParameter(dbComm, "@Pro_Remark", DbType.String, objinfo.Pro_Remark)
                db.AddInParameter(dbComm, "@Pro_Describe", DbType.String, objinfo.Pro_Describe)
                db.AddInParameter(dbComm, "@Pro_Photo", DbType.Binary, objinfo.Pro_Photo)
                db.AddInParameter(dbComm, "@Pro_Action", DbType.String, objinfo.Pro_Action)
                db.AddInParameter(dbComm, "@Pro_EditDate", DbType.Date, objinfo.Pro_EditDate)
                db.AddInParameter(dbComm, "@Pro_Rate", DbType.Double, objinfo.Pro_Rate)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleProcessMain_Update = True
            Catch ex As Exception
                NmetalSampleProcessMain_Update = False
            End Try
        End Function
        ''' <summary>
        ''' 产品工艺主表审核
        ''' </summary>
        ''' <param name="objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleProcessMain_UpdateCheck(ByVal objinfo As NmetalSampleProcessInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcessMain_UpdateCheck")

                db.AddInParameter(dbComm, "@Pro_NO", DbType.String, objinfo.Pro_NO)
                db.AddInParameter(dbComm, "@Pro_Check", DbType.Boolean, objinfo.Pro_Check)
                db.AddInParameter(dbComm, "@Pro_CheckDate", DbType.Date, objinfo.Pro_CheckDate)
                db.AddInParameter(dbComm, "@Pro_CheckRemark", DbType.String, objinfo.Pro_CheckRemark)
                db.AddInParameter(dbComm, "@Pro_CheckAction", DbType.String, objinfo.Pro_CheckAction)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleProcessMain_UpdateCheck = True

            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleProcessMain_UpdateCheck = False
            End Try
        End Function

        Public Function NmetalSampleProcessSub_ShowReport(ByVal PS_NO As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcessSub_ShowReport")
                db.AddInParameter(dbComm, "@PS_NO", DbType.String, PS_NO)
                db.ExecuteNonQuery(dbComm)
                NmetalSampleProcessSub_ShowReport = True

            Catch ex As Exception
                NmetalSampleProcessSub_ShowReport = False
            End Try
        End Function
        ''' <summary>
        ''' 产品工艺主表刪除
        ''' </summary>
        ''' <param name="Pro_NO"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleProcessMain_Delete(ByVal Pro_NO As String) As Boolean

            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcessMain_Delete")

                db.AddInParameter(dbComm, "@Pro_NO", DbType.String, Pro_NO)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleProcessMain_Delete = True
            Catch ex As Exception
                NmetalSampleProcessMain_Delete = False
            End Try

        End Function
        ''' <summary>
        ''' 产品工艺主表得到工艺流程单号最大一條記錄
        ''' </summary>
        ''' <param name="Ndate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleProcessMain_GetNO(ByVal Ndate As String) As NmetalSampleProcessInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcessMain_GetNO")
            db.AddInParameter(dbComm, "@NDate", DbType.String, Ndate)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    Return FillNmetalSampleProcess(reader)
                End While
                Return Nothing
            End Using
        End Function

        ''' <summary>
        ''' 查询得到主表子表所有記錄
        ''' </summary>
        ''' <param name="Pro_NO"></param>
        ''' <param name="PM_M_Code"></param>
        ''' <param name="Pro_Type"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleProcessMain_GetList(ByVal Pro_NO As String, ByVal PM_M_Code As String, ByVal Pro_Type As String, ByVal PM_Type As String, ByVal D_Name As String, ByVal PS_Enable As String, ByVal PS_NO As String) As List(Of NmetalSampleProcessInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcessMain_GetList")

            db.AddInParameter(dbComm, "@Pro_NO", DbType.String, Pro_NO)
            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
            db.AddInParameter(dbComm, "@Pro_Type", DbType.String, Pro_Type)
            db.AddInParameter(dbComm, "@PM_Type", DbType.String, PM_Type)
            db.AddInParameter(dbComm, "@D_Name", DbType.String, D_Name)
            db.AddInParameter(dbComm, "@PS_Enable", DbType.String, PS_Enable)
            db.AddInParameter(dbComm, "@PS_NO", DbType.String, PS_NO)

            Dim FeatureList As New List(Of NmetalSampleProcessInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleProcess(reader))
                End While
                Return FeatureList
            End Using
        End Function
        ''' <summary>
        ''' 查询得到子表部門信息
        ''' </summary>
        ''' <param name="D_ID"></param>
        ''' <param name="D_Type"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalProDepartMent_GetList(ByVal D_ID As String, ByVal D_Type As String) As List(Of NmetalSampleProcessInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalProDepartMent_GetList")

            db.AddInParameter(dbComm, "@D_ID", DbType.String, D_ID)
            db.AddInParameter(dbComm, "@D_Type", DbType.String, D_Type)

            Dim FeatureList As New List(Of NmetalSampleProcessInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleProcess(reader))
                End While
                Return FeatureList
            End Using
        End Function
        ''' <summary>
        ''' 得到当前主表記錄
        ''' </summary>
        ''' <param name="Pro_NO"></param>
        ''' <param name="PM_M_Code"></param>
        ''' <param name="Pro_Type"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleProcessMain_GetList1(ByVal Pro_NO As String, ByVal PM_M_Code As String, ByVal Pro_Type As String, ByVal M_Code As String, ByVal StartAddDate As String, ByVal StartEndDate As String, ByVal Pro_Action As String, ByVal Pro_Check As String) As List(Of NmetalSampleProcessInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcessMain_GetList1")

            db.AddInParameter(dbComm, "@Pro_NO", DbType.String, Pro_NO)
            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
            db.AddInParameter(dbComm, "@Pro_Type", DbType.String, Pro_Type)
            db.AddInParameter(dbComm, "@M_Code", DbType.String, M_Code)

            db.AddInParameter(dbComm, "@StartAddDate", DbType.String, StartAddDate)
            db.AddInParameter(dbComm, "@StartEndDate", DbType.String, StartEndDate)
            db.AddInParameter(dbComm, "@Pro_Action", DbType.String, Pro_Action)
            db.AddInParameter(dbComm, "@Pro_Check", DbType.String, Pro_Check)

            Dim FeatureList As New List(Of NmetalSampleProcessInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleProcess(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function NmetalSampleProcessMain_GetList2(ByVal Pro_Type As String, ByVal PM_M_Code As String) As List(Of NmetalSampleProcessInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcessMain_GetList2")

            db.AddInParameter(dbComm, "@Pro_Type", DbType.String, Pro_Type)
            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)

            Dim FeatureList As New List(Of NmetalSampleProcessInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleProcess(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function NmetalSampleProcessMain_GetList3(ByVal PM_M_Code As String, ByVal Pro_Check As String) As List(Of NmetalSampleProcessInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcessMain_GetList3")

            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
            db.AddInParameter(dbComm, "@Pro_Check", DbType.String, Pro_Check)

            Dim FeatureList As New List(Of NmetalSampleProcessInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleProcess(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function NmetalSampleProcessSub_GetNum(ByVal PM_M_Code As String) As NmetalSampleProcessInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcessSub_GetNum")
            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    Return FillNmetalSampleProcess(reader)
                End While
                Return Nothing
            End Using
        End Function
        ''' <summary>
        ''' 产品工艺子表添加
        ''' </summary>
        ''' <param name="objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleProcessSub_Add(ByVal objinfo As NmetalSampleProcessInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcessSub_Add")

                db.AddInParameter(dbComm, "@Pro_NO", DbType.String, objinfo.Pro_NO)
                db.AddInParameter(dbComm, "@PS_NO", DbType.String, objinfo.PS_NO)
                db.AddInParameter(dbComm, "@PS_Num", DbType.String, objinfo.PS_Num)
                db.AddInParameter(dbComm, "@D_Name", DbType.String, objinfo.D_Name)
                db.AddInParameter(dbComm, "@PS_Name", DbType.String, objinfo.PS_Name)
                db.AddInParameter(dbComm, "@PS_Type", DbType.String, objinfo.PS_Type)
                db.AddInParameter(dbComm, "@PS_Remark", DbType.String, objinfo.PS_Remark)
                db.AddInParameter(dbComm, "@PS_Check", DbType.Boolean, objinfo.PS_Check)
                'db.AddInParameter(dbComm, "@PS_PaiXu", DbType.String, objinfo.PS_PaiXu)
                db.AddInParameter(dbComm, "@PS_Enable", DbType.Boolean, objinfo.PS_Enable)
                db.AddInParameter(dbComm, "@PS_OutPut", DbType.String, objinfo.PS_OutPut)
                db.AddInParameter(dbComm, "@PS_Weight", DbType.Double, objinfo.PS_Weight)
                db.AddInParameter(dbComm, "@PS_BarCodeBit", DbType.Boolean, objinfo.PS_BarCodeBit)
                db.AddInParameter(dbComm, "@PS_OtherWeight", DbType.Decimal, objinfo.PS_OtherWeight)        '張偉    2014-07-07
                db.ExecuteNonQuery(dbComm)
                NmetalSampleProcessSub_Add = True

            Catch ex As Exception
                NmetalSampleProcessSub_Add = False
            End Try
        End Function
        ''' <summary>
        ''' 产品工艺子表修改
        ''' </summary>
        ''' <param name="objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleProcessSub_Update(ByVal objinfo As NmetalSampleProcessInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcessSub_Update")

                db.AddInParameter(dbComm, "@Pro_NO", DbType.String, objinfo.Pro_NO)
                db.AddInParameter(dbComm, "@PS_NO", DbType.String, objinfo.PS_NO)
                db.AddInParameter(dbComm, "@PS_Num", DbType.String, objinfo.PS_Num)
                db.AddInParameter(dbComm, "@D_Name", DbType.String, objinfo.D_Name)
                db.AddInParameter(dbComm, "@PS_Name", DbType.String, objinfo.PS_Name)
                db.AddInParameter(dbComm, "@PS_Type", DbType.String, objinfo.PS_Type)
                db.AddInParameter(dbComm, "@PS_Remark", DbType.String, objinfo.PS_Remark)
                db.AddInParameter(dbComm, "@PS_Check", DbType.Boolean, objinfo.PS_Check)
                'db.AddInParameter(dbComm, "@PS_PaiXu", DbType.String, objinfo.PS_PaiXu)
                db.AddInParameter(dbComm, "@PS_Enable", DbType.Boolean, objinfo.PS_Enable)
                db.AddInParameter(dbComm, "@PS_OutPut", DbType.String, objinfo.PS_OutPut)
                db.AddInParameter(dbComm, "@PS_Weight", DbType.Double, objinfo.PS_Weight)
                db.AddInParameter(dbComm, "@PS_BarCodeBit", DbType.Boolean, objinfo.PS_BarCodeBit)
                db.AddInParameter(dbComm, "@PS_OtherWeight", DbType.Decimal, objinfo.PS_OtherWeight)        '張偉   2014-07-07

                db.ExecuteNonQuery(dbComm)
                NmetalSampleProcessSub_Update = True

            Catch ex As Exception
                NmetalSampleProcessSub_Update = False
            End Try
        End Function
        ''' <summary>
        ''' 新增重量修改
        ''' </summary>
        ''' <param name="objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleProcessSub_UpdateOtherWeight(ByVal objinfo As NmetalSampleProcessInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcessSub_UpdateOtherWeight")

                db.AddInParameter(dbComm, "@Pro_NO", DbType.String, objinfo.Pro_NO)
                db.AddInParameter(dbComm, "@PS_NO", DbType.String, objinfo.PS_NO)
                db.AddInParameter(dbComm, "@PS_OtherWeight", DbType.Decimal, objinfo.PS_OtherWeight)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleProcessSub_UpdateOtherWeight = True

            Catch ex As Exception
                NmetalSampleProcessSub_UpdateOtherWeight = False
            End Try
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleProcessSub_Update2(ByVal objinfo As NmetalSampleProcessInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcessSub_Update2")

                db.AddInParameter(dbComm, "@Pro_NO", DbType.String, objinfo.Pro_NO)
                db.AddInParameter(dbComm, "@PS_NO", DbType.String, objinfo.PS_NO)
                db.AddInParameter(dbComm, "@PS_Enable", DbType.Boolean, objinfo.PS_Enable)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleProcessSub_Update2 = True

            Catch ex As Exception
                NmetalSampleProcessSub_Update2 = False
            End Try
        End Function

        ''' <summary>
        ''' 产品工艺子表刪除
        ''' </summary>
        ''' <param name="Pro_NO"></param>
        ''' <param name="PS_NO"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleProcessSub_Delete(ByVal Pro_NO As String, ByVal PS_NO As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcessSub_Delete")

                db.AddInParameter(dbComm, "@Pro_NO", DbType.String, Pro_NO)
                db.AddInParameter(dbComm, "@PS_NO", DbType.String, PS_NO)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleProcessSub_Delete = True
            Catch ex As Exception
                NmetalSampleProcessSub_Delete = False
            End Try
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Pro_NO"></param>
        ''' <param name="PS_NO"></param>
        ''' <param name="D_Name"></param>
        ''' <param name="PS_Name"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleProcessSub_GetList(ByVal Pro_NO As String, ByVal PS_NO As String, ByVal D_Name As String, ByVal PS_Name As String, ByVal PS_Type As String, ByVal PS_Enable As String) As List(Of NmetalSampleProcessInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcessSub_GetList")

            db.AddInParameter(dbComm, "@Pro_NO", DbType.String, Pro_NO)
            db.AddInParameter(dbComm, "@PS_NO", DbType.String, PS_NO)
            db.AddInParameter(dbComm, "@D_Name", DbType.String, D_Name)
            db.AddInParameter(dbComm, "@PS_Name", DbType.String, PS_Name)
            db.AddInParameter(dbComm, "@PS_Type", DbType.String, PS_Type)
            db.AddInParameter(dbComm, "@PS_Enable", DbType.Boolean, PS_Enable)

            Dim FeatureList As New List(Of NmetalSampleProcessInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleProcess(reader))
                End While
                Return FeatureList
            End Using
        End Function

        'Public Function Process_Add(ByVal objinfo As NmetalSampleProcessInfo) As Boolean
        '    Try
        '        Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
        '        Dim dbComm As DbCommand = db.GetStoredProcCommand("Process_Add")

        '        db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, objinfo.PM_M_Code)
        '        db.AddInParameter(dbComm, "@Pro_M_NO", DbType.String, objinfo.Pro_M_NO)
        '        db.AddInParameter(dbComm, "@M_Code", DbType.String, objinfo.M_Code)

        '        db.ExecuteNonQuery(dbComm)
        '        Process_Add = True

        '    Catch ex As Exception
        '        Process_Add = False
        '    End Try
        'End Function
        'Public Function Process_Update(ByVal objinfo As NmetalSampleProcessInfo) As Boolean
        '    Try
        '        Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
        '        Dim dbComm As DbCommand = db.GetStoredProcCommand("Process_Update")

        '        db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, objinfo.PM_M_Code)
        '        db.AddInParameter(dbComm, "@Pro_M_NO", DbType.String, objinfo.Pro_M_NO)
        '        db.AddInParameter(dbComm, "@M_Code", DbType.String, objinfo.M_Code)
        '        db.ExecuteNonQuery(dbComm)

        '        Process_Update = True

        '    Catch ex As Exception
        '        Process_Update = False
        '    End Try
        'End Function
        '''' <summary>
        '''' 
        '''' </summary>
        '''' <param name="Pro_M_NO"></param>
        '''' <param name="PM_M_Code"></param>
        '''' <param name="M_Code"></param>
        '''' <returns></returns>
        '''' <remarks></remarks>
        'Public Function Process_GetList(ByVal Pro_M_NO As String, ByVal PM_M_Code As String, ByVal M_Code As String) As List(Of NmetalSampleProcessInfo)

        '    Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
        '    Dim dbComm As DbCommand = db.GetStoredProcCommand("Process_GetList")

        '    db.AddInParameter(dbComm, "@Pro_M_NO", DbType.String, Pro_M_NO)
        '    db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
        '    db.AddInParameter(dbComm, "@M_Code", DbType.String, M_Code)

        '    Dim FeatureList As New List(Of NmetalSampleProcessInfo)
        '    Using reader As IDataReader = db.ExecuteReader(dbComm)
        '        While reader.Read
        '            FeatureList.Add(FillNmetalSampleProcess(reader))
        '        End While
        '        Return FeatureList
        '    End Using
        'End Function

        Public Function FillNmetalSampleProcess(ByVal reader As IDataReader) As NmetalSampleProcessInfo

            On Error Resume Next
            Dim pmi As New NmetalSampleProcessInfo

            pmi.PM_M_Code = reader("PM_M_Code").ToString
            pmi.Pro_M_NO = reader("Pro_M_NO").ToString
            pmi.Pro_NO = reader("Pro_NO").ToString
            pmi.M_Code = reader("M_Code").ToString
            pmi.Pro_Type = reader("Pro_Type").ToString
            pmi.Type3ID = reader("Type3ID").ToString
            pmi.PM_Type = reader("PM_Type").ToString
            pmi.Pro_Weight = reader("Pro_Weight").ToString
            pmi.Pro_Remark = reader("Pro_Remark").ToString
            pmi.Pro_Describe = reader("Pro_Describe").ToString
            pmi.Pro_Rate = reader("Pro_Rate").ToString

            pmi.PS_File = reader("PS_File").ToString


            If reader("Pro_Photo") Is DBNull.Value Then
                pmi.Pro_Photo = Nothing
            Else
                pmi.Pro_Photo = reader("Pro_Photo")
            End If

            pmi.Pro_Action = reader("Pro_Action").ToString
            If reader("Pro_AddDate") Is DBNull.Value Then
                pmi.Pro_AddDate = Nothing
            Else
                pmi.Pro_AddDate = CStr(reader("Pro_AddDate"))
            End If
            If reader("Pro_EditDate") Is DBNull.Value Then
                pmi.Pro_EditDate = Nothing
            Else
                pmi.Pro_EditDate = CStr(reader("Pro_EditDate"))
            End If
            If reader("Pro_Check") Is DBNull.Value Then
                pmi.Pro_Check = Nothing
            Else
                pmi.Pro_Check = reader("Pro_Check")
            End If

            If reader("Pro_CheckDate") Is DBNull.Value Then
                pmi.Pro_CheckDate = Nothing
            Else
                pmi.Pro_CheckDate = CStr(reader("Pro_CheckDate"))
            End If

            pmi.Pro_CheckRemark = reader("Pro_CheckRemark").ToString
            pmi.Pro_CheckAction = reader("Pro_CheckAction").ToString

            pmi.PS_NO = reader("PS_NO").ToString
            pmi.PS_Num = reader("PS_Num").ToString


            pmi.PS_Name = reader("PS_Name").ToString


            pmi.PS_Type = reader("PS_Type").ToString
            pmi.PS_Remark = reader("PS_Remark").ToString

            If reader("PS_Check") Is DBNull.Value Then
                pmi.PS_Check = False
            Else
                pmi.PS_Check = reader("PS_Check")
            End If


            If reader("PS_Enable") Is DBNull.Value Then
                pmi.PS_Enable = False
            Else
                pmi.PS_Enable = reader("PS_Enable")
            End If

            pmi.PS_OutPut = reader("PS_OutPut")
            'pmi.PS_PaiXu = reader("PS_PaiXu").ToString
            pmi.D_Name = reader("D_Name").ToString
            If reader("PS_Weight") Is DBNull.Value Then
                pmi.PS_Weight = 0
            Else
                pmi.PS_Weight = reader("PS_Weight")
            End If

            pmi.IndexNO = reader("IndexNO")
            pmi.M_Name = reader("M_Name").ToString
            pmi.M_Gauge = reader("M_Gauge").ToString
            pmi.M_Unit = reader("M_Unit").ToString
            pmi.CustomerNO = reader("CustomerNO").ToString
            pmi.PM_JiYu = reader("PM_JiYu").ToString
            pmi.ActionName = reader("ActionName").ToString
            pmi.CheckActionName = reader("CheckActionName").ToString

            pmi.SO_SampleID = reader("SO_SampleID").ToString
            pmi.PS_BarCodeBit = reader("PS_BarCodeBit")

            pmi.D_Dep = reader("D_Dep").ToString  '部門表名称
            pmi.D_ID = reader("D_ID").ToString    '部門表ID 
            pmi.D_Type = reader("D_Type").ToString '部門表类型
            pmi.UseCount = reader("UseCount") + reader("UseCountW")
            pmi.SWI_Qty = reader("SWI_Qty")
            pmi.PS_NoName = "<" + reader("PS_Num").ToString.Trim + ">" + reader("PS_Name").ToString.Trim
            pmi.PS_ShowReportBit = reader("PS_ShowReportBit")
            pmi.PS_OtherWeight = reader("PS_OtherWeight")               '張偉    2014-07-07
            Return pmi

        End Function

        ''' <summary>
        ''' 已进入物料收發的工序只修改PS_NUM
        ''' </summary>
        ''' <param name="objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>

        Public Function NmetalSampleProcessSub_Update3(ByVal objinfo As NmetalSampleProcessInfo) As Boolean
            '2012-4-16對已进入物料收發的工序只修改PS_NUM
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcessSub_Update3")

                db.AddInParameter(dbComm, "@Pro_NO", DbType.String, objinfo.Pro_NO)
                db.AddInParameter(dbComm, "@PS_NO", DbType.String, objinfo.PS_NO)
                db.AddInParameter(dbComm, "@PS_Num", DbType.String, objinfo.PS_Num)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleProcessSub_Update3 = True

            Catch ex As Exception
                NmetalSampleProcessSub_Update3 = False
            End Try
        End Function

        ''' <summary>
        ''' 是否自動描條碼
        ''' </summary>
        ''' <param name="objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleProcessSub_UpdatePS_BarCodeBit(ByVal objinfo As NmetalSampleProcessInfo) As Boolean

            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcessSub_UpdatePS_BarCodeBit")

                db.AddInParameter(dbComm, "@Pro_NO", DbType.String, objinfo.Pro_NO)
                db.AddInParameter(dbComm, "@PS_NO", DbType.String, objinfo.PS_NO)
                db.AddInParameter(dbComm, "@PS_BarCodeBit", DbType.Boolean, objinfo.PS_BarCodeBit)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleProcessSub_UpdatePS_BarCodeBit = True

            Catch ex As Exception
                NmetalSampleProcessSub_UpdatePS_BarCodeBit = False
            End Try
        End Function

    End Class


End Namespace

