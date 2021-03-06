Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Data.Sql
Namespace LFERP.Library.NmetalSampleManager.NmetalSampleWareInventory
    Public Class NmetalSampleWareInventoryControler
        Public Function NmetalSampleWareInventory_Update(ByVal objinfo As NmetalSampleWareInventoryInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleWareInventory_Update")

                db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "@M_Code", DbType.String, objinfo.M_Code)
                db.AddInParameter(dbComm, "@PS_NO", DbType.String, objinfo.PS_NO)
                db.AddInParameter(dbComm, "@SWI_Qty", DbType.Int32, objinfo.SWI_Qty)
                db.AddInParameter(dbComm, "@SWI_Weight", DbType.Decimal, objinfo.SWI_Weight)

                db.AddInParameter(dbComm, "@ModifyUserID", DbType.String, objinfo.ModifyUserID)
                db.AddInParameter(dbComm, "@ModifyDate", DbType.DateTime, objinfo.ModifyDate)
                db.AddInParameter(dbComm, "@D_ID", DbType.String, objinfo.D_ID)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleWareInventory_Update = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleWareInventory_Update = False
            End Try
        End Function

        Public Function NmetalSampleWareInventory_Getlist(ByVal PM_M_Code As String, ByVal M_Code As String, ByVal PS_NO As String, ByVal AutoID As String, ByVal ReportEmpty As Boolean, ByVal D_ID As String) As List(Of NmetalSampleWareInventoryInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleWareInventory_Getlist")

            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
            db.AddInParameter(dbComm, "@M_Code", DbType.String, M_Code)
            db.AddInParameter(dbComm, "@PS_NO", DbType.String, PS_NO)
            db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)
            db.AddInParameter(dbComm, "@D_ID", DbType.String, D_ID)

            Dim FeatureList As New List(Of NmetalSampleWareInventoryInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleWareInventoryType(reader))
                End While

                If FeatureList.Count <= 0 And ReportEmpty Then
                    FeatureList.Add(New NmetalSampleWareInventoryInfo())
                End If

                Return FeatureList
            End Using
        End Function
        Public Function NmetalSampleWareInventoryPS_NO_GetList(ByVal PS_NO As String) As List(Of NmetalSampleWareInventoryInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleWareInventoryPS_NO_GetList")

            db.AddInParameter(dbComm, "@PS_NO", DbType.String, PS_NO)

            Dim FeatureList As New List(Of NmetalSampleWareInventoryInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleWareInventoryType(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function NmetalSampleWareInventoryA_Getlist(ByVal D_ID As String, ByVal PS_NO As String, ByVal PM_M_Code As String, ByVal ReportEmpty As Boolean) As List(Of NmetalSampleWareInventoryInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleWareInventoryA_Getlist")

            db.AddInParameter(dbComm, "@D_ID", DbType.String, D_ID)
            db.AddInParameter(dbComm, "@PS_NO", DbType.String, PS_NO)
            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)

            Dim FeatureList As New List(Of NmetalSampleWareInventoryInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleWareInventoryType(reader))
                End While

                If FeatureList.Count <= 0 And ReportEmpty Then
                    FeatureList.Add(New NmetalSampleWareInventoryInfo())
                End If

                Return FeatureList
            End Using
        End Function



        Friend Function FillNmetalSampleWareInventoryType(ByVal reader As IDataReader) As NmetalSampleWareInventoryInfo
            '对应取得的数据
            On Error Resume Next
            Dim objInfo As New NmetalSampleWareInventoryInfo
            objInfo.AutoID = reader("AutoID").ToString

            objInfo.PM_M_Code = reader("PM_M_Code").ToString
            objInfo.PS_NO = reader("PS_NO").ToString
            objInfo.SWI_Qty = reader("SWI_Qty")
            objInfo.OutQty = reader("OutQty")
            objInfo.InQty = reader("InQty")
            objInfo.BarCodeCount = reader("BarCodeCount")

            objInfo.LoseCount = reader("LoseCount")
            objInfo.DamageCount = reader("DamageCount")
            objInfo.FinishedCount = reader("FinishedCount")
            objInfo.ReturnCount = reader("ReturnCount")
            objInfo.SendCount = reader("SendCount")

            objInfo.M_Code = reader("M_Code").ToString

            objInfo.PS_Name = reader("PS_Name").ToString
            objInfo.D_Dep = reader("D_Dep").ToString
            objInfo.D_ID = reader("D_ID").ToString

            objInfo.AddUserID = reader("AddUserID").ToString
            objInfo.AddUserName = reader("AddUserName").ToString
            objInfo.AddDate = CDate(reader("AddDate").ToString)
            objInfo.ModifyDate = CDate(reader("ModifyDate").ToString)
            objInfo.ModifyUserID = reader("ModifyUserID").ToString
            objInfo.SO_SampleID = reader("SO_SampleID").ToString
            objInfo.MaterialTypeName = reader("MaterialTypeName").ToString
            objInfo.Borrow_Qty = reader("Borrow_Qty")
            objInfo.SO_ID = reader("SO_ID").ToString
            objInfo.RepayQty = reader("RepayQty")
            objInfo.AvailableQty = reader("AvailableQty")
            objInfo.NoBorrow_Qty = reader("NoBorrow_Qty")
            objInfo.SWI_Weight = reader("SWI_Weight")

            objInfo.TWeight = reader("TWeight")
            objInfo.CountBarcode = reader("CountBarcode")
            objInfo.TheoryWasteWeight = objInfo.SWI_Weight - objInfo.TWeight

            objInfo.LoseWeight = reader("LoseWeight") '丢失重量
            objInfo.DamageWeight = reader("DamageWeight") '损坏重量
            objInfo.FinishedWeight = reader("FinishedWeight") '完工重量
            objInfo.SendWeight = reader("SendWeight") '寄送重量

            Return objInfo
        End Function


        '
        Public Function NmetalSampleProcessInventory_GetList(ByVal Pro_Type As String, ByVal PM_M_Code As String, ByVal PM_Type As String) As List(Of NmetalSampleWareInventoryInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcessInventory_GetList")

            db.AddInParameter(dbComm, "@Pro_Type", DbType.String, Pro_Type)
            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
            db.AddInParameter(dbComm, "@PM_Type", DbType.String, PM_Type)

            Dim FeatureList As New List(Of NmetalSampleWareInventoryInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleWareInventoryType(reader))
                End While

                Return FeatureList
            End Using
        End Function

        Public Function NmetalSampleOrdersMainInvent_GetList(ByVal PM_M_Code As String, ByVal MaterialTypeID As String, ByVal SO_SampleID As String, ByVal D_ID As String) As List(Of NmetalSampleWareInventoryInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersMainInvent_GetList")

            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
            db.AddInParameter(dbComm, "@MaterialTypeID", DbType.String, MaterialTypeID)
            db.AddInParameter(dbComm, "@SO_SampleID", DbType.String, SO_SampleID)
            db.AddInParameter(dbComm, "@D_ID", DbType.String, D_ID)


            Dim FeatureList As New List(Of NmetalSampleWareInventoryInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleWareInventoryType(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function NmetalSampleOrdersMainInventA_GetList(ByVal PM_M_Code As String, ByVal MaterialTypeID As String, ByVal SO_SampleID As String, ByVal D_ID As String) As List(Of NmetalSampleWareInventoryInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersMainInventA_GetList")

            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
            db.AddInParameter(dbComm, "@MaterialTypeID", DbType.String, MaterialTypeID)
            db.AddInParameter(dbComm, "@SO_SampleID", DbType.String, SO_SampleID)
            db.AddInParameter(dbComm, "@D_ID", DbType.String, D_ID)

            Dim FeatureList As New List(Of NmetalSampleWareInventoryInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleWareInventoryType(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function NmetalSampleWareInventoryChange_Add(ByVal objinfo As NmetalSampleWareInventoryInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleWareInventoryChange_Add")

                db.AddInParameter(dbComm, "@D_ID", DbType.String, objinfo.D_ID)
                db.AddInParameter(dbComm, "@PS_NO", DbType.String, objinfo.PS_NO)
                db.AddInParameter(dbComm, "@SWI_QtyQ", DbType.Int32, objinfo.SWI_QtyQ)
                db.AddInParameter(dbComm, "@SWI_QtyH", DbType.Int32, objinfo.SWI_QtyH)

                db.AddInParameter(dbComm, "@AddUserID", DbType.String, objinfo.AddUserID)
                db.AddInParameter(dbComm, "@AddDate", DbType.DateTime, objinfo.AddDate)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleWareInventoryChange_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleWareInventoryChange_Add = False
            End Try
        End Function

    End Class
End Namespace

