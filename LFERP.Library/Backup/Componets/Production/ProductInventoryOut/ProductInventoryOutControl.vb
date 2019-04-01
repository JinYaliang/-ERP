Namespace LFERP.Library.Production.ProductInventoryOut
    Public Class ProductInventoryOutControl
#Region "�w�s�ܶq�Ѽ�"
        Private m_DbExec As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
        Private m_DbComm As DbCommand
#End Region

#Region "�K�[���~�w�s"
        ''' <param name="objinfo">�w�s�H��</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductInventoryOut_Add(ByVal objinfo As ProductInventoryOutInfo) As Boolean
            Try
                m_DbComm = m_DbExec.GetStoredProcCommand("ProductInventoryOut_Add")
                m_DbExec.AddInParameter(m_DbComm, "@PO_NO", DbType.String, objinfo.PO_NO)
                m_DbExec.AddInParameter(m_DbComm, "@Pro_Type", DbType.String, objinfo.Pro_Type)
                m_DbExec.AddInParameter(m_DbComm, "@PM_M_Code", DbType.String, objinfo.PM_M_Code)
                m_DbExec.AddInParameter(m_DbComm, "@PM_Type", DbType.String, objinfo.PM_Type)
                m_DbExec.AddInParameter(m_DbComm, "@M_Code", DbType.String, objinfo.M_Code)
                m_DbExec.AddInParameter(m_DbComm, "@WH_ID", DbType.String, objinfo.WH_ID)
                m_DbExec.AddInParameter(m_DbComm, "@PO_Qty", DbType.Int32, objinfo.PO_Qty)
                m_DbExec.AddInParameter(m_DbComm, "@PO_Action", DbType.String, objinfo.PO_Action)
                m_DbExec.AddInParameter(m_DbComm, "@PO_Remark", DbType.String, objinfo.PO_Remark)
                m_DbExec.AddInParameter(m_DbComm, "@PO_Date", DbType.DateTime, objinfo.PO_Date)



                If (m_DbExec.ExecuteNonQuery(m_DbComm)) > 0 Then
                    ProductInventoryOut_Add = True
                Else
                    ProductInventoryOut_Add = False
                End If

            Catch ex As Exception
                ProductInventoryOut_Add = False
            End Try
        End Function
#End Region

#Region "��s���~�w�s"
        ''' <param name="objinfo">���~�H��</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductInventoryOut_Update(ByVal objinfo As ProductInventoryOutInfo) As Boolean
            Try
                m_DbComm = m_DbExec.GetStoredProcCommand("ProductInventoryOut_Update")
                m_DbExec.AddInParameter(m_DbComm, "@AutoID", DbType.String, objinfo.AutoID)
                m_DbExec.AddInParameter(m_DbComm, "@PO_NO", DbType.String, objinfo.PO_NO)
                m_DbExec.AddInParameter(m_DbComm, "@Pro_Type", DbType.String, objinfo.Pro_Type)
                m_DbExec.AddInParameter(m_DbComm, "@PM_M_Code", DbType.String, objinfo.PM_M_Code)
                m_DbExec.AddInParameter(m_DbComm, "@PM_Type", DbType.String, objinfo.PM_Type)
                m_DbExec.AddInParameter(m_DbComm, "@M_Code", DbType.String, objinfo.M_Code)
                m_DbExec.AddInParameter(m_DbComm, "@WH_ID", DbType.String, objinfo.WH_ID)
                m_DbExec.AddInParameter(m_DbComm, "@PO_Qty", DbType.Int32, objinfo.PO_Qty)
                m_DbExec.AddInParameter(m_DbComm, "@PO_Action", DbType.String, objinfo.PO_Action)
                m_DbExec.AddInParameter(m_DbComm, "@PO_Remark", DbType.String, objinfo.PO_Remark)
                m_DbExec.AddInParameter(m_DbComm, "@PO_Date", DbType.Date, objinfo.PO_Date)

                If (m_DbExec.ExecuteNonQuery(m_DbComm)) > 0 Then
                    ProductInventoryOut_Update = True
                Else
                    ProductInventoryOut_Update = False
                End If

            Catch ex As Exception
                ProductInventoryOut_Update = False
            End Try
        End Function
#End Region

#Region "�ھڲ��~�s���R�����~�w�s"
        ''' <summary>
        ''' �۰ʽs��
        ''' </summary>
        ''' <param name="autoID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductInventoryOut_Delete(ByVal autoID As String) As Boolean
            Try
                m_DbComm = m_DbExec.GetStoredProcCommand("ProductInventoryOut_Delete")
                m_DbExec.AddInParameter(m_DbComm, "@AutoID", DbType.String, autoID)
                If (m_DbExec.ExecuteNonQuery(m_DbComm)) > 0 Then
                    ProductInventoryOut_Delete = True
                Else
                    ProductInventoryOut_Delete = False
                End If
            Catch ex As Exception
                ProductInventoryOut_Delete = False
            End Try
        End Function
#End Region

#Region "�f�ֲ��~�w�s"
        ''' <param name="objinfo">���~�H��</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductInventoryOut_Check(ByVal objinfo As ProductInventoryOutInfo) As Boolean
            Try
                m_DbComm = m_DbExec.GetStoredProcCommand("ProductInventoryOut_Check")
                m_DbExec.AddInParameter(m_DbComm, "@AutoID", DbType.String, objinfo.AutoID)
                m_DbExec.AddInParameter(m_DbComm, "@PO_Check", DbType.Boolean, objinfo.PO_Check)
                m_DbExec.AddInParameter(m_DbComm, "@PO_CheckAction", DbType.String, objinfo.PO_CheckAction)
                m_DbExec.AddInParameter(m_DbComm, "@PO_CheckDate", DbType.DateTime, objinfo.PO_CheckDate)
                m_DbExec.AddInParameter(m_DbComm, "@PO_CheckRemark", DbType.String, objinfo.PO_CheckRemark)
                m_DbExec.AddInParameter(m_DbComm, "@PO_EndQty", DbType.String, objinfo.PO_EndQty)
                m_DbExec.AddInParameter(m_DbComm, "@PO_Remark", DbType.String, objinfo.PO_Remark)

                If (m_DbExec.ExecuteNonQuery(m_DbComm)) > 0 Then
                    ProductInventoryOut_Check = True
                Else
                    ProductInventoryOut_Check = False
                End If

            Catch ex As Exception
                ProductInventoryOut_Check = False
            End Try
        End Function
#End Region

#Region "��R���~�w�s�ƾ�"

        ''' <param name="reader">Ū����R</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FillProductInventoryOutInfo(ByVal reader As IDataReader) As ProductInventoryOutInfo
            On Error Resume Next
            Dim pwInfo As New ProductInventoryOutInfo
            pwInfo.AutoID = reader("AutoID").ToString
            pwInfo.PO_NO = reader("PO_NO").ToString
            pwInfo.Pro_Type = reader("Pro_Type").ToString
            pwInfo.PM_Type = reader("PM_Type").ToString
            pwInfo.M_Code = reader("M_Code").ToString
            pwInfo.WH_ID = reader("WH_ID").ToString
            pwInfo.PM_M_Code = reader("PM_M_Code").ToString

            pwInfo.PO_CheckAction = reader("PO_CheckAction").ToString
            pwInfo.PO_CheckRemark = reader("PO_CheckRemark").ToString
            pwInfo.PWS_OutName = reader("PWS_OutName").ToString
            pwInfo.M_Gauge = reader("M_Gauge").ToString
            pwInfo.M_Name = reader("M_Name").ToString
            pwInfo.M_Unit = reader("M_Unit").ToString
            pwInfo.PO_Remark = reader("PO_Remark").ToString



            pwInfo.PWS_ActionName = reader("PWS_ActionName").ToString
            pwInfo.PWS_CheckActionName = reader("PWS_CheckActionName").ToString

            If reader("PO_Qty") Is DBNull.Value Then
                pwInfo.PO_Qty = 0
            Else
                pwInfo.PO_Qty = CInt(reader("PO_Qty"))
            End If

            If reader("PO_EndQty") Is DBNull.Value Then
                pwInfo.PO_EndQty = 0
            Else
                pwInfo.PO_EndQty = CInt(reader("PO_EndQty"))
            End If

            If reader("PO_Date") Is DBNull.Value Then
                pwInfo.PO_Date = Nothing
            Else
                pwInfo.PO_Date = CStr(reader("PO_Date"))
            End If

            If reader("PO_CheckDate") Is DBNull.Value Then
                pwInfo.PO_CheckDate = Nothing
            Else
                pwInfo.PO_CheckDate = CStr(reader("PO_CheckDate"))
            End If

            If reader("PO_Check") Is DBNull.Value Then
                pwInfo.PO_Check = Nothing
            Else
                pwInfo.PO_Check = CStr(reader("PO_Check"))
            End If


            If reader("PM_JiYu") Is DBNull.Value Then
                pwInfo.PM_JiYu = Nothing
            Else
                pwInfo.PM_JiYu = reader("PM_JiYu")
            End If

            Return pwInfo
        End Function
#End Region

#Region "�d�߲��~�w�s"

        ''' <param name="po_NO">�X�w�渹</param>
        ''' <param name="pro_Type">�u������</param>
        ''' <param name="pm_M_Code">���~�s��</param>
        ''' <param name="pm_Type">���O</param>
        ''' <param name="m_Code">���ƽs�X</param>
        ''' <param name="wh_ID">�ܮw�s��</param>
        ''' <param name="po_Date1">�X�w�ɶ��q</param>
        ''' <param name="po_Date2">�X�w�ɶ��q</param>
        ''' <param name="po_Check">�f��</param>
        ''' <returns> List(Of ProductInventoryOutInfo)</returns>
        ''' <remarks></remarks>
        Public Function ProductInventoryOut_GetList(ByVal au_ID As String, ByVal po_NO As String, ByVal pro_Type As String, ByVal pm_M_Code As String, ByVal pm_Type As String, ByVal m_Code As String, ByVal wh_ID As String, ByVal po_Date1 As String, ByVal po_Date2 As String, ByVal po_Check As String) As List(Of ProductInventoryOutInfo)

            m_DbComm = m_DbExec.GetStoredProcCommand("ProductInventoryOut_GetList")
            m_DbExec.AddInParameter(m_DbComm, "@AutoID", DbType.String, au_ID)
            m_DbExec.AddInParameter(m_DbComm, "@PO_NO", DbType.String, po_NO)
            m_DbExec.AddInParameter(m_DbComm, "@Pro_Type", DbType.String, pro_Type)
            m_DbExec.AddInParameter(m_DbComm, "@PM_M_Code", DbType.String, pm_M_Code)
            m_DbExec.AddInParameter(m_DbComm, "@PM_Type", DbType.String, pm_Type)
            m_DbExec.AddInParameter(m_DbComm, "@M_Code", DbType.String, m_Code)
            m_DbExec.AddInParameter(m_DbComm, "@WH_ID", DbType.String, wh_ID)
            m_DbExec.AddInParameter(m_DbComm, "@PO_Date1", DbType.String, po_Date1)
            m_DbExec.AddInParameter(m_DbComm, "@PO_Date2", DbType.String, po_Date2)
            m_DbExec.AddInParameter(m_DbComm, "@PO_Check", DbType.Boolean, po_Check)


            Dim FeatureList As New List(Of ProductInventoryOutInfo)
            Using reader As IDataReader = m_DbExec.ExecuteReader(m_DbComm)
                While reader.Read
                    FeatureList.Add(FillProductInventoryOutInfo(reader))
                End While
                Return FeatureList
            End Using


        End Function
#End Region

#Region "�ھڲ��~�s���d�߲��~�w�s"

        ''' <param name="NDate">���~���</param>
        ''' <returns>ProductInventoryOutInfo</returns>
        ''' <remarks></remarks>
        Public Function ProductInventoryOut_GetNo(ByVal NDate As String) As ProductInventoryOutInfo
            m_DbComm = m_DbExec.GetStoredProcCommand("ProductInventoryOut_GetNO")
            m_DbExec.AddInParameter(m_DbComm, "@NDate", DbType.String, NDate)
            Using reader As IDataReader = m_DbExec.ExecuteReader(m_DbComm)
                While reader.Read
                    Return FillProductInventoryOutInfo(reader)
                End While
                Return Nothing
            End Using
        End Function

#End Region

    End Class
End Namespace
