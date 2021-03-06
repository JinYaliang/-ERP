Namespace LFERP.Library.NmetalSampleManager.NmetalSampInventoryWeightCheck

    Public Class NmetalSampInventoryWeightCheckSubControler
        ''' <summary>
        ''' 新增
        ''' </summary>
        ''' <param name="objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampInventoryWeightCheckSub_Add(ByVal objinfo As NmetalSampInventoryWeightCheckSubInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampInventoryWeightCheckSub_Add")

                db.AddInParameter(dbComm, "@CH_NO", DbType.String, objinfo.CH_NO)
                db.AddInParameter(dbComm, "@Code_ID", DbType.String, objinfo.Code_ID)
                db.AddInParameter(dbComm, "@SO_SampleID", DbType.String, objinfo.SO_SampleID)
                db.AddInParameter(dbComm, "@CH_QQty", DbType.Int32, objinfo.CH_QQty)
                db.AddInParameter(dbComm, "@CH_QWeight", DbType.Decimal, objinfo.CH_QWeight)

                db.AddInParameter(dbComm, "@CH_Qty", DbType.Int32, objinfo.CH_Qty)
                db.AddInParameter(dbComm, "@CH_Weight", DbType.Decimal, objinfo.CH_Weight)
                db.AddInParameter(dbComm, "@ErrorRate", DbType.Double, objinfo.ErrorRate)
                db.AddInParameter(dbComm, "@Remark", DbType.String, objinfo.Remark)

                db.AddInParameter(dbComm, "@StatusType", DbType.String, objinfo.StatusType)
                db.AddInParameter(dbComm, "@D_ID", DbType.String, objinfo.D_ID)

                db.ExecuteNonQuery(dbComm)
                NmetalSampInventoryWeightCheckSub_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampInventoryWeightCheckSub_Add = False
            End Try
        End Function
        ''' <summary>
        ''' 修改
        ''' </summary>
        ''' <remarks></remarks>
        Public Function NmetalSampInventoryWeightCheckSub_Update(ByVal objinfo As NmetalSampInventoryWeightCheckSubInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampInventoryWeightCheckSub_Update")

                db.AddInParameter(dbComm, "@AutoID", DbType.String, objinfo.AutoID)
                db.AddInParameter(dbComm, "@CH_NO", DbType.String, objinfo.CH_NO)
                db.AddInParameter(dbComm, "@Code_ID", DbType.String, objinfo.Code_ID)
                db.AddInParameter(dbComm, "@SO_SampleID", DbType.String, objinfo.SO_SampleID)
                db.AddInParameter(dbComm, "@CH_QQty", DbType.Int32, objinfo.CH_QQty)

                db.AddInParameter(dbComm, "@CH_QWeight", DbType.Decimal, objinfo.CH_QWeight)
                db.AddInParameter(dbComm, "@CH_Qty", DbType.Int32, objinfo.CH_Qty)
                db.AddInParameter(dbComm, "@CH_Weight", DbType.Decimal, objinfo.CH_Weight)
                db.AddInParameter(dbComm, "@ErrorRate", DbType.Double, objinfo.ErrorRate)
                db.AddInParameter(dbComm, "@Remark", DbType.String, objinfo.Remark)

                db.AddInParameter(dbComm, "@StatusType", DbType.String, objinfo.StatusType)
                db.AddInParameter(dbComm, "@D_ID", DbType.String, objinfo.D_ID)

                db.ExecuteNonQuery(dbComm)
                NmetalSampInventoryWeightCheckSub_Update = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampInventoryWeightCheckSub_Update = False
            End Try
        End Function
        ''' <summary>
        ''' 刪除
        ''' </summary>
        ''' <param name="CH_NO"></param>
        ''' <param name="AutoID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampInventoryWeightCheckSub_Delete(ByVal AutoID As String, ByVal CH_NO As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampInventoryWeightCheckSub_Delete")

                db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)
                db.AddInParameter(dbComm, "@CH_NO", DbType.String, CH_NO)

                db.ExecuteNonQuery(dbComm)
                NmetalSampInventoryWeightCheckSub_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampInventoryWeightCheckSub_Delete = False
            End Try
        End Function
        ''' <summary>
        ''' 查询
        ''' </summary>
        ''' <param name="AutoID"></param>
        ''' <param name="CH_NO"></param>
        ''' <param name="Code_ID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampInventoryWeightCheckSub_GetList(ByVal AutoID As String, ByVal CH_NO As String, ByVal Code_ID As String) As List(Of NmetalSampInventoryWeightCheckSubInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampInventoryWeightCheckSub_GetList")

            db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)
            db.AddInParameter(dbComm, "@CH_NO", DbType.String, CH_NO)
            db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)

            Dim FeatureList As New List(Of NmetalSampInventoryWeightCheckSubInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillSampInventoryWeightCheckSub(reader))
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
        Friend Function FillSampInventoryWeightCheckSub(ByVal reader As IDataReader) As NmetalSampInventoryWeightCheckSubInfo
            On Error Resume Next

            Dim objInfo As New NmetalSampInventoryWeightCheckSubInfo

            objInfo.AutoID = reader("AutoID")
            objInfo.CH_NO = reader("CH_NO").ToString
            objInfo.Code_ID = reader("Code_ID").ToString
            objInfo.SO_SampleID = reader("SO_SampleID").ToString
            objInfo.CH_QQty = CInt(reader("CH_QQty").ToString)

            objInfo.CH_QWeight = reader("CH_QWeight")
            objInfo.CH_Qty = reader("CH_Qty")
            objInfo.CH_Weight = reader("CH_Weight")
            objInfo.ErrorRate = reader("ErrorRate")
            objInfo.Remark = reader("Remark").ToString

            '-------------------
            objInfo.StatusType = reader("StatusType").ToString
            objInfo.D_ID = reader("D_ID").ToString
            objInfo.StatusTypeName = reader("StatusTypeName").ToString
            objInfo.D_Name = reader("D_Name").ToString

            Return objInfo
        End Function
    End Class
End Namespace