Namespace LFERP.DataSetting
    ''' <summary>
    ''' �����Ӹ�ƺ޲z
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SupplierControler
        ''' <summary>
        ''' ���o�����Ӹ��
        ''' </summary>
        ''' <param name="Supplier"></param>
        ''' <param name="SupplierName"></param>
        ''' <param name="S_Type"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetSupplierList(ByVal Supplier As String, ByVal SupplierName As String, ByVal S_Type As String) As List(Of SupplierInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("Suppliers_GetList")
            db.AddInParameter(dbComm, "@S_Supplier", DbType.String, Supplier)
            db.AddInParameter(dbComm, "@S_SupplierName", DbType.String, SupplierName)
            db.AddInParameter(dbComm, "@S_Type", DbType.String, S_Type)
            Dim FeatureList As New List(Of SupplierInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillSupplierType(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function GetSuppliersList(ByVal Supplier As String, ByVal SupplierName As String, ByVal S_Type As String) As List(Of SupplierInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("Suppliers_GetList")
            db.AddInParameter(dbComm, "@S_Supplier", DbType.String, Supplier)
            db.AddInParameter(dbComm, "@S_SupplierName", DbType.String, SupplierName)
            db.AddInParameter(dbComm, "@S_TypeID", DbType.String, S_Type)


            Dim FeatureList As New List(Of SupplierInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillSupplierType(reader))
                End While
                Return FeatureList
            End Using
        End Function
        Friend Function FillSupplierType(ByVal reader As IDataReader) As SupplierInfo
            '�������o���ƾ�
            Dim objInfo As New SupplierInfo
            objInfo.S_Supplier = reader("�����ӥN��").ToString
            objInfo.S_SupplierName = reader("�����ӦW��").ToString
            objInfo.S_Email = reader("�l��a�}").ToString
            objInfo.S_Associate = reader("�p�t�H").ToString
            objInfo.S_Tel = reader("�p�t�q��").ToString
            objInfo.S_Fax = reader("Fax_no").ToString
            objInfo.S_Type = reader("����������").ToString
            objInfo.S_Address = reader("�����Ӧa�}").ToString
            objInfo.S_Currency = reader("���O").ToString
            objInfo.S_Contace = reader("�p���覡").ToString
            objInfo.S_Tel1 = reader("�p�t�q��1").ToString
            objInfo.S_Remark = reader("�ƪ`").ToString
            Return objInfo
        End Function

    End Class
End Namespace

