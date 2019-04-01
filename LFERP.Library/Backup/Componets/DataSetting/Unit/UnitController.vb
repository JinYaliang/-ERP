Namespace LFERP.DataSetting
    Public Class UnitController
        ''' <summary>
        ''' ���o���M��
        ''' </summary>
        ''' <param name="U_IDList">���N���C��,nothing����ܩҦ�</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetUnitList(ByVal U_IDList As String) As List(Of UnitInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("Unit_GetList")
            db.AddInParameter(dbComm, "@U_ID", DbType.String, U_IDList)
            Dim FeatureList As New List(Of UnitInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillUnitType(reader))
                End While
                Return FeatureList
            End Using
        End Function
        Friend Function FillUnitType(ByVal reader As IDataReader) As UnitInfo
            '�������o���ƾ�
            Dim objInfo As New UnitInfo
            objInfo.U_ID = reader("U_ID").ToString
            objInfo.U_Name = reader("U_Name").ToString
            Return objInfo
        End Function

    End Class
End Namespace

