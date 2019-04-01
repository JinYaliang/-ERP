Imports System.Data.Common
Namespace LFERP.Library.KnifeWare

    Public Class KnifeChangeControl
        Public Function KnifeChange_GetNUM(ByVal NDate As String) As KnifeChangeInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("KnifeChange_GetNUM")

            db.AddInParameter(dbComm, "@NDate", DbType.String, NDate)

            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    Return FillKnifeChange_GetNUM(reader)
                End While
                Return Nothing
            End Using
        End Function
        Public Function FillKnifeChange_GetNUM(ByVal reader As IDataReader) As KnifeChangeInfo
            Dim ci As New KnifeChangeInfo
            '�y����
            ci.CH_Num = reader("CH_Num").ToString
            Return ci

        End Function
        Public Function KnifeChange_Add(ByVal obj As KnifeChangeInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbcomm As DbCommand = db.GetStoredProcCommand("KnifeChange_Add")

                db.AddInParameter(dbcomm, "@CH_Num", DbType.String, obj.CH_Num) '����y����
                db.AddInParameter(dbcomm, "@BR_NO", DbType.String, obj.BR_NO) '�w�����(�٤M�O����)
                db.AddInParameter(dbcomm, "@M_Code", DbType.String, obj.M_Code) '�M��s�X
                db.AddInParameter(dbcomm, "@WH_ID", DbType.String, obj.WH_ID) '�ܮw�N��
                db.AddInParameter(dbcomm, "@CBegin_Qty", DbType.Int32, obj.CBegin_Qty) '�ק�e�ƶq

                db.AddInParameter(dbcomm, "@CEnd_Qty", DbType.Int32, obj.CEnd_Qty) '�ק�Z�ƶq
                db.AddInParameter(dbcomm, "@CKType", DbType.String, obj.CKType) '��������()
                db.AddInParameter(dbcomm, "@CReMark", DbType.String, obj.CReMark) '�ƪ`
                db.AddInParameter(dbcomm, "@C_Action", DbType.String, obj.C_Action) '�ާ@��
                db.AddInParameter(dbcomm, "@C_Date", DbType.String, obj.C_Date) '�ާ@��

                db.ExecuteNonQuery(dbcomm)
                KnifeChange_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                KnifeChange_Add = False
            End Try
        End Function

        Public Function KnifeChange_Update(ByVal obj As KnifeChangeInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbcomm As DbCommand = db.GetStoredProcCommand("KnifeChange_Update")

                db.AddInParameter(dbcomm, "@CH_Num", DbType.String, obj.CH_Num) '����y����
                db.AddInParameter(dbcomm, "@BR_NO", DbType.String, obj.BR_NO) '�w�����(�٤M�O����)
                db.AddInParameter(dbcomm, "@M_Code", DbType.String, obj.M_Code) '�M��s�X
                db.AddInParameter(dbcomm, "@WH_ID", DbType.String, obj.WH_ID) '�ܮw�N��
                db.AddInParameter(dbcomm, "@CBegin_Qty", DbType.Int32, obj.CBegin_Qty) '�ק�e�ƶq

                db.AddInParameter(dbcomm, "@CEnd_Qty", DbType.Int32, obj.CEnd_Qty) '�ק�Z�ƶq
                db.AddInParameter(dbcomm, "@CKType", DbType.String, obj.CKType) '��������()
                db.AddInParameter(dbcomm, "@CReMark", DbType.String, obj.CReMark) '�ƪ`
                db.AddInParameter(dbcomm, "@C_Action", DbType.String, obj.C_Action) '�ާ@��
                db.AddInParameter(dbcomm, "@C_Date", DbType.String, obj.C_Date) '�ާ@��

                db.ExecuteNonQuery(dbcomm)
                KnifeChange_Update = True
            Catch ex As Exception
                MsgBox(ex.Message)
                KnifeChange_Update = False
            End Try
        End Function

        Public Function KnifeChange_GetList(ByVal CH_Num As String, ByVal BR_NO As String, ByVal M_Code As String, ByVal WH_ID As String, ByVal CKType As String, ByVal C_DateStart As String, ByVal C_DateEnd As String) As List(Of KnifeChangeInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbcomm As DbCommand = db.GetStoredProcCommand("KnifeChange_GetList")

            db.AddInParameter(dbcomm, "@CH_Num", DbType.String, CH_Num)
            db.AddInParameter(dbcomm, "@BR_NO", DbType.String, BR_NO)
            db.AddInParameter(dbcomm, "@M_Code", DbType.String, M_Code)
            db.AddInParameter(dbcomm, "@WH_ID", DbType.String, WH_ID)
            db.AddInParameter(dbcomm, "@CKType", DbType.String, CKType)

            db.AddInParameter(dbcomm, "@C_DateStart", DbType.String, C_DateStart)
            db.AddInParameter(dbcomm, "@C_DateEnd", DbType.String, C_DateEnd)

            Dim FeatureList As New List(Of KnifeChangeInfo)
            Using reader As IDataReader = db.ExecuteReader(dbcomm)
                While reader.Read
                    FeatureList.Add(FillKnifeChange(reader))
                End While
                Return FeatureList
            End Using
        End Function
#Region "Ū����������"
        Public Function KnifeChangeType_GetList(ByVal ChangeID As String, ByVal ChangeName As String) As List(Of KnifeChangeInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbcomm As DbCommand = db.GetStoredProcCommand("KnifeChangeType_GetList")

            db.AddInParameter(dbcomm, "@ChangeID", DbType.String, ChangeID)
            db.AddInParameter(dbcomm, "@ChangeName", DbType.String, ChangeName)

            Dim FeatureList As New List(Of KnifeChangeInfo)
            Using reader As IDataReader = db.ExecuteReader(dbcomm)
                While reader.Read
                    FeatureList.Add(FillKnifeChangeType(reader))
                End While
                Return FeatureList
            End Using
        End Function


        Public Function FillKnifeChangeType(ByVal reader As IDataReader) As KnifeChangeInfo
            On Error Resume Next
            Dim ci As New KnifeChangeInfo
            '�y����
            ci.ChangeID = reader("ChangeID")     '�۰ʽs��ID
            ci.ChangeName = reader("ChangeName").ToString     '����y����

            Return ci
        End Function
#End Region

        Public Function FillKnifeChange(ByVal reader As IDataReader) As KnifeChangeInfo
            On Error Resume Next
            Dim ci As New KnifeChangeInfo
            '�y����
            ci.AutoID = reader("AutoID")     '�۰ʽs��ID
            ci.CH_Num = reader("CH_Num").ToString     '����y����
            ci.BR_NO = reader("BR_NO").ToString     '�w�����(�٤M�O����)
            ci.M_Code = reader("M_Code").ToString     '�M��s�X
            ci.WH_ID = reader("WH_ID").ToString     '�ܮw�N��

            ci.CBegin_Qty = reader("CBegin_Qty")     '�ק�e�ƶq
            ci.CEnd_Qty = reader("CEnd_Qty")     '�ק�Z�ƶq
            ci.CKType = reader("CKType").ToString     '��������()
            ci.CReMark = reader("CReMark").ToString     '�ƪ`
            ci.C_Action = reader("C_Action").ToString     '�ާ@��

            ci.C_Date = reader("C_Date").ToString     '�ާ@��
            ci.WH_Name = reader("WH_Name").ToString
            ci.WH_PName = reader("WH_PName").ToString
            ci.C_ActionName = reader("C_ActionName").ToString

            ci.M_Name = reader("M_Name").ToString
            ci.M_Gauge = reader("M_Gauge").ToString

            ci.CKTypeName = reader("CKTypeName").ToString

            ci.BRPer_ID = reader("BRPer_ID").ToString
            ci.BRPer_Name = reader("BRPer_Name").ToString

            Return ci

        End Function

        Public Function KnifeChange_Delete(ByVal CH_Num As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbcomm As DbCommand = db.GetStoredProcCommand("KnifeChange_Delete")
                db.AddInParameter(dbcomm, "@CH_Num", DbType.String, CH_Num)
                db.ExecuteNonQuery(dbcomm)
                KnifeChange_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                KnifeChange_Delete = False
            End Try
        End Function



        '2014-02-08
        Public Function KnifeChange_GetList1(ByVal BRPer_ID As String, ByVal CH_Num As String, ByVal BR_NO As String, ByVal M_Code As String, ByVal WH_ID As String, ByVal CKType As String, ByVal C_DateStart As String, ByVal C_DateEnd As String) As List(Of KnifeChangeInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbcomm As DbCommand = db.GetStoredProcCommand("KnifeChange_GetList1")


            db.AddInParameter(dbcomm, "@BRPer_ID", DbType.String, BRPer_ID)

            db.AddInParameter(dbcomm, "@CH_Num", DbType.String, CH_Num)
            db.AddInParameter(dbcomm, "@BR_NO", DbType.String, BR_NO)
            db.AddInParameter(dbcomm, "@M_Code", DbType.String, M_Code)
            db.AddInParameter(dbcomm, "@WH_ID", DbType.String, WH_ID)
            db.AddInParameter(dbcomm, "@CKType", DbType.String, CKType)

            db.AddInParameter(dbcomm, "@C_DateStart", DbType.String, C_DateStart)
            db.AddInParameter(dbcomm, "@C_DateEnd", DbType.String, C_DateEnd)

            Dim FeatureList As New List(Of KnifeChangeInfo)
            Using reader As IDataReader = db.ExecuteReader(dbcomm)
                While reader.Read
                    FeatureList.Add(FillKnifeChange(reader))
                End While
                Return FeatureList
            End Using
        End Function

    End Class
End Namespace
