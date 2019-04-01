Namespace LFERP.Library.ProductionPayPersonnel

    Public Class ProductionPayPersonnelContol

        ''' <summary>
        ''' ��^�ӤH�p��A�էO�p�󤤡A�Y�ӤH�����u���`�B
        ''' </summary>
        ''' <param name="PaySum_YYMM"></param>
        ''' <param name="DepID"></param>
        ''' <param name="FacID"></param>
        ''' <param name="Per_NO"></param>
        ''' <param name="Pay_Check"></param>
        ''' <returns></returns>
        ''' <remarks>��^�ӤH�p��A�էO�p�󤤡A�Y�ӤH�����Ҧ��u���`�B</remarks>
        Public Function ProductionPieceMeritedPaySum_View(ByVal PaySum_YYMM As String, ByVal DepID As String, ByVal FacID As String, ByVal Per_NO As String, ByVal Pay_Check As String) As List(Of ProductionPayPersonnelInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbcomm As DbCommand = db.GetStoredProcCommand("ProductionPieceMeritedPaySum_View")

            db.AddInParameter(dbcomm, "@PaySum_YYMM", DbType.String, PaySum_YYMM)
            db.AddInParameter(dbcomm, "@DepID", DbType.String, DepID) '�渹
            db.AddInParameter(dbcomm, "@FacID", DbType.String, FacID) '�էO�s��
            db.AddInParameter(dbcomm, "@Per_NO", DbType.String, Per_NO) '�~��
            db.AddInParameter(dbcomm, "@Pay_Check", DbType.String, Pay_Check) '�f�ֽs��

            Dim FeatureList As New List(Of ProductionPayPersonnelInfo)
            Using reader As IDataReader = db.ExecuteReader(dbcomm)
                While reader.Read
                    FeatureList.Add(FillProductionPayPersonnelSum(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function FillProductionPayPersonnelSum(ByVal reader As IDataReader) As ProductionPayPersonnelInfo
            Dim ai As New ProductionPayPersonnelInfo

            If reader("Per_NO") Is DBNull.Value Then
                ai.Per_NO = Nothing
            Else
                ai.Per_NO = reader("Per_NO").ToString     '�u��
            End If

            If reader("Per_Name") Is DBNull.Value Then
                ai.Per_Name = Nothing
            Else
                ai.Per_Name = reader("Per_Name").ToString     '�m�W
            End If

            If reader("G_NO") Is DBNull.Value Then
                ai.G_NO = Nothing
            Else
                ai.G_NO = reader("G_NO").ToString     '�էO
            End If

            If reader("G_Name") Is DBNull.Value Then
                ai.G_Name = Nothing
            Else
                ai.G_Name = reader("G_Name").ToString     '�էO�W��
            End If

            If reader("FacID") Is DBNull.Value Then
                ai.FacID = Nothing
            Else
                ai.FacID = reader("FacID").ToString     '�t�O
            End If

            If reader("DepID") Is DBNull.Value Then
                ai.DepID = Nothing
            Else
                ai.DepID = reader("DepID").ToString     '����
            End If

            If reader("Per_DepName") Is DBNull.Value Then
                ai.Per_DepName = Nothing
            Else
                ai.Per_DepName = reader("Per_DepName").ToString     '����
            End If

            If reader("Per_FacName") Is DBNull.Value Then
                ai.Per_FacName = Nothing
            Else
                ai.Per_FacName = reader("Per_FacName").ToString     '�t�O
            End If
            ''---------------------------------------------------------------------------------------------------------------------------------

            If reader("PaySum_YYMM") Is DBNull.Value Then
                ai.PaySum_YYMM = Nothing
            Else
                ai.PaySum_YYMM = reader("PaySum_YYMM").ToString     '���
            End If

            If reader("PL_MeritedPaySum") Is DBNull.Value Then
                ai.PL_MeritedPaySum = 0
            Else
                ai.PL_MeritedPaySum = reader("PL_MeritedPaySum")     '���`
            End If

            If reader("PYS_MeritedPaySum") Is DBNull.Value Then
                ai.PYS_MeritedPaySum = 0
            Else
                ai.PYS_MeritedPaySum = reader("PYS_MeritedPaySum")     '���`
            End If

            ai.TotalSum = ai.PL_MeritedPaySum + ai.PYS_MeritedPaySum

            Return ai
        End Function
    End Class
End Namespace