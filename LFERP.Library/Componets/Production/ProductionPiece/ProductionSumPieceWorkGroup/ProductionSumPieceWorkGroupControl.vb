Imports System.Data.Common

Namespace LFERP.Library.ProductionSumPieceWorkGroup


    Public Class ProductionSumPieceWorkGroupControl


        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="PP_AutoID"></param>
        ''' <param name="GP_NO"></param>
        ''' <param name="Per_NO"></param>
        ''' <param name="G_NO"></param>
        ''' <param name="DepID"></param>
        ''' <param name="FacID"></param>
        ''' <param name="Pro_Type"></param>
        ''' <param name="PM_M_Code"></param>
        ''' <param name="PM_Type"></param>
        ''' <param name="PS_NO"></param>
        ''' <param name="PS_NameS"></param>
        ''' <param name="GP_DateStart"></param>
        ''' <param name="GP_Action"></param>
        ''' <param name="GP_DateEnd"></param>
        ''' <param name="Print_Action">���L�M��</param>
        ''' <returns></returns>
        ''' <remarks></remarks>




        Public Function ProductionSumPieceWorkGroup_GetList(ByVal PP_AutoID As String, ByVal GP_NO As String, ByVal Per_NO As String, ByVal G_NO As String, ByVal DepID As String, ByVal FacID As String, ByVal Pro_Type As String, _
                                                            ByVal PM_M_Code As String, ByVal PM_Type As String, ByVal PS_NO As String, ByVal PS_NameS As String, ByVal GP_DateStart As String, _
                                                            ByVal GP_Action As String, ByVal GP_DateEnd As String, ByVal Print_Action As String) As List(Of ProductionSumPieceWorkGroupInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("ProductionSumPieceWorkGroup_GetList")

            db.AddInParameter(dbComm, "@PP_AutoID", DbType.String, PP_AutoID) ' --/�u���s��

            db.AddInParameter(dbComm, "@GP_NO", DbType.String, GP_NO) ' --/�p��渹
            db.AddInParameter(dbComm, "@Per_NO", DbType.String, Per_NO) '--/���u�s��
            db.AddInParameter(dbComm, "@G_NO", DbType.String, G_NO) ' --/�էO�s��
            db.AddInParameter(dbComm, "@DepID", DbType.String, DepID) '-- /�����s��
            db.AddInParameter(dbComm, "@FacID", DbType.String, FacID) '-- /�t�O

            db.AddInParameter(dbComm, "@Pro_Type", DbType.String, Pro_Type) '--/�u������
            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code) ' -���~�s��
            db.AddInParameter(dbComm, "@PM_Type", DbType.String, PM_Type) ' /�t��W��
            db.AddInParameter(dbComm, "@PS_NO", DbType.String, PS_NO) '�j�u�ǦW��
            db.AddInParameter(dbComm, "@PS_NameS", DbType.String, PS_NameS) '�p�u�ǦW��

            db.AddInParameter(dbComm, "@GP_DateStart", DbType.String, GP_DateStart) '/�p����
            db.AddInParameter(dbComm, "@GP_Action", DbType.String, GP_Action) '-/�ާ@�H
            db.AddInParameter(dbComm, "@GP_DateEnd", DbType.String, GP_DateEnd) ' --> < =


            Dim FeatureList As New List(Of ProductionSumPieceWorkGroupInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillProductionSumPieceWorkGroup(reader, GP_DateStart, GP_DateEnd, Print_Action))
                End While
                Return FeatureList
            End Using

        End Function


        ''' <summary>
        ''' ����էO�p�ɲέp�ɫ��w�O��
        ''' </summary>
        ''' <param name="reader"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FillProductionSumPieceWorkGroup(ByVal reader As IDataReader, ByVal FGP_DateStart As String, ByVal FGP_DateEnd As String, ByVal FPrint_Action As String) As ProductionSumPieceWorkGroupInfo
            Dim ai As New ProductionSumPieceWorkGroupInfo
            ''���L�M��
            ai.Print_Action = FPrint_Action
            ai.GP_DateStart = FGP_DateStart
            ai.GP_DateEnd = FGP_DateEnd


            If reader("GP_NO") Is DBNull.Value Then ' /�p��渹
                ai.GP_NO = Nothing
            Else
                ai.GP_NO = reader("GP_NO").ToString
            End If

            If reader("Per_NO") Is DBNull.Value Then ' /���u�s��
                ai.Per_NO = Nothing
            Else
                ai.Per_NO = reader("Per_NO").ToString
            End If

            If reader("G_NO") Is DBNull.Value Then ' /�էO�s��
                ai.G_NO = Nothing
            Else
                ai.G_NO = reader("G_NO").ToString
            End If

            If reader("DepID") Is DBNull.Value Then ' /�����s��
                ai.DepID = Nothing
            Else
                ai.DepID = reader("DepID").ToString
            End If

            If reader("FacID") Is DBNull.Value Then ' /�t�O
                ai.FacID = Nothing
            Else
                ai.FacID = reader("FacID").ToString
            End If

            ''-------------------------
            If reader("Pro_Type") Is DBNull.Value Then ' /�u������   ''�������O��
                ai.Pro_Type = Nothing
            Else
                ai.Pro_Type = reader("Pro_Type").ToString
            End If

            If reader("PM_M_Code") Is DBNull.Value Then ' /���~�s��''�������O��
                ai.PM_M_Code = Nothing
            Else
                ai.PM_M_Code = reader("PM_M_Code").ToString
            End If

            If reader("PM_Type") Is DBNull.Value Then ' /�t��W��''�������O��
                ai.PM_Type = Nothing
            Else
                ai.PM_Type = reader("PM_Type").ToString
            End If

            If reader("PS_NO") Is DBNull.Value Then ' /�j�u�ǦW�� ''�������O��
                ai.PS_NO = Nothing
            Else
                ai.PS_NO = reader("PS_NO").ToString
            End If



            If reader("PS_NameS") Is DBNull.Value Then ' /�p�u�ǦW��''�������O��
                ai.PS_NameS = Nothing
            Else
                ai.PS_NameS = reader("PS_NameS").ToString
            End If
            ''-------------------------------------------------------

            If reader("GP_factor") Is DBNull.Value Then ' /�ӥ]�t��
                ai.GP_factor = Nothing
            Else
                ai.GP_factor = reader("GP_factor").ToString
            End If

            If reader("GP_factor") Is DBNull.Value Then ' /�ӥ]�t��
                ai.GP_factor = Nothing
            Else
                ai.GP_factor = reader("GP_factor")
            End If

            If reader("GP_Qty") Is DBNull.Value Then ' /�ƶq
                ai.GP_Qty = Nothing
            Else
                ai.GP_Qty = reader("GP_Qty")
            End If

            If reader("GP_Date") Is DBNull.Value Then ' /�p����
                ai.GP_Date = Nothing
            Else
                ai.GP_Date = reader("GP_Date")
            End If

            If reader("GP_AddDate") Is DBNull.Value Then ' /�O�����
                ai.GP_AddDate = Nothing
            Else
                ai.GP_AddDate = reader("GP_AddDate")
            End If

            If reader("GP_Action") Is DBNull.Value Then ' /�ާ@�H
                ai.GP_Action = Nothing
            Else
                ai.GP_Action = reader("GP_Action")
            End If

            If reader("GP_Remark") Is DBNull.Value Then ' /�ƪ`
                ai.GP_Remark = Nothing
            Else
                ai.GP_Remark = reader("GP_Remark").ToString
            End If
            '1''--------------------------�~��r�q
            If reader("GP_ActionName") Is DBNull.Value Then ' /�ާ@���W
                ai.GP_ActionName = Nothing
            Else
                ai.GP_ActionName = reader("GP_ActionName").ToString
            End If

            If reader("GP_Per_Name") Is DBNull.Value Then ' /���u�W�m�W
                ai.GP_Per_Name = Nothing
            Else
                ai.GP_Per_Name = reader("GP_Per_Name").ToString
            End If

            If reader("GP_DepName") Is DBNull.Value Then ' /�����W
                ai.GP_DepName = Nothing
            Else
                ai.GP_DepName = reader("GP_DepName").ToString
            End If

            If reader("GP_FacName") Is DBNull.Value Then ' /�t�O�W
                ai.GP_FacName = Nothing
            Else
                ai.GP_FacName = reader("GP_FacName").ToString
            End If

            If reader("GP_G_Name") Is DBNull.Value Then ' /�էO�W
                ai.GP_G_Name = Nothing
            Else
                ai.GP_G_Name = reader("GP_G_Name").ToString
            End If

            If reader("PS_Name") Is DBNull.Value Then ' /�j�u�ǦW��
                ai.PS_Name = Nothing
            Else
                ai.PS_Name = reader("PS_Name").ToString
            End If


            If reader("PP_AutoID") Is DBNull.Value Then ' �u������AUTOID
                ai.PP_AutoID = Nothing
            Else
                ai.PP_AutoID = reader("PP_AutoID").ToString
            End If

            If reader("GP_Price") Is DBNull.Value Then ' �u��
                ai.GP_Price = Nothing
            Else
                ai.GP_Price = reader("GP_Price")
            End If


            Return ai

        End Function
        ''' <summary>
        ''' �W�[�O��
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionSumPieceWorkGroup_Add(ByVal obj As ProductionSumPieceWorkGroupInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbcomm As DbCommand = db.GetStoredProcCommand("ProductionSumPieceWorkGroup_Add")


                db.AddInParameter(dbcomm, "@PP_AutoID", DbType.String, obj.PP_AutoID) '--/�u���y�{�����s��
                db.AddInParameter(dbcomm, "@GP_Price", DbType.Double, obj.GP_Price) '--/�u��

                db.AddInParameter(dbcomm, "@GP_NO", DbType.String, obj.GP_NO) '--/�p��渹
                db.AddInParameter(dbcomm, "@Per_NO", DbType.String, obj.Per_NO) '-- /���u�s��
                db.AddInParameter(dbcomm, "@G_NO", DbType.String, obj.G_NO) ' --/�էO�s��
                db.AddInParameter(dbcomm, "@DepID", DbType.String, obj.DepID) ' --/�����s��
                db.AddInParameter(dbcomm, "@FacID", DbType.String, obj.FacID) ' --/�t�O

                db.AddInParameter(dbcomm, "@Pro_Type", DbType.String, obj.Pro_Type)  '/�u������
                db.AddInParameter(dbcomm, "@PM_M_Code", DbType.String, obj.PM_M_Code) ' - /���~�s��
                db.AddInParameter(dbcomm, "@PM_Type", DbType.String, obj.PM_Type) ' -- /�t��W��    
                db.AddInParameter(dbcomm, "@PS_NO", DbType.String, obj.PS_NO) '  -- /�j�u�ǦW��
                db.AddInParameter(dbcomm, "@PS_NameS", DbType.String, obj.PS_NameS) '--/�ާ@�H

                db.AddInParameter(dbcomm, "@GP_factor", DbType.Double, obj.GP_factor) '--/�ӥ]�t��
                db.AddInParameter(dbcomm, "@GP_Qty", DbType.Int32, obj.GP_Qty) ' - /�ƶq
                db.AddInParameter(dbcomm, "@GP_Date", DbType.String, obj.GP_Date) ' -- /�p����    
                db.AddInParameter(dbcomm, "@GP_AddDate", DbType.String, obj.GP_AddDate) '  -- /�O�����
                db.AddInParameter(dbcomm, "@GP_Action", DbType.String, obj.GP_Action) '--/�ާ@�H

                db.AddInParameter(dbcomm, "@GP_Remark", DbType.String, obj.GP_Remark) '--/�ƪ`


                db.ExecuteNonQuery(dbcomm)
                ProductionSumPieceWorkGroup_Add = True

            Catch ex As Exception
                MsgBox(ex.Message)
                ProductionSumPieceWorkGroup_Add = False
            End Try
        End Function
        ''' <summary>
        ''' ��s�O��
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionSumPieceWorkGroup_Update(ByVal obj As ProductionSumPieceWorkGroupInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbcomm As DbCommand = db.GetStoredProcCommand("ProductionSumPieceWorkGroup_Update")

                db.AddInParameter(dbcomm, "@PP_AutoID", DbType.String, obj.PP_AutoID) '--/�u���y�{�����s��
                db.AddInParameter(dbcomm, "@GP_Price", DbType.Double, obj.GP_Price) '--/�u��

                db.AddInParameter(dbcomm, "@GP_NO", DbType.String, obj.GP_NO) '--/�p��渹
                db.AddInParameter(dbcomm, "@Per_NO", DbType.String, obj.Per_NO) '-- /���u�s��
                db.AddInParameter(dbcomm, "@G_NO", DbType.String, obj.G_NO) ' --/�էO�s��
                db.AddInParameter(dbcomm, "@DepID", DbType.String, obj.DepID) ' --/�����s��
                db.AddInParameter(dbcomm, "@FacID", DbType.String, obj.FacID) ' --/�t�O

                db.AddInParameter(dbcomm, "@Pro_Type", DbType.String, obj.Pro_Type)  '/�u������
                db.AddInParameter(dbcomm, "@PM_M_Code", DbType.String, obj.PM_M_Code) ' - /���~�s��
                db.AddInParameter(dbcomm, "@PM_Type", DbType.String, obj.PM_Type) ' -- /�t��W��    
                db.AddInParameter(dbcomm, "@PS_NO", DbType.String, obj.PS_NO) '  -- /�j�u�ǦW��
                db.AddInParameter(dbcomm, "@PS_NameS", DbType.String, obj.PS_NameS) '--/�ާ@�H

                db.AddInParameter(dbcomm, "@GP_factor", DbType.Double, obj.GP_factor) '--/�ӥ]�t��
                db.AddInParameter(dbcomm, "@GP_Qty", DbType.Int32, obj.GP_Qty) ' - /�ƶq
                db.AddInParameter(dbcomm, "@GP_Date", DbType.String, obj.GP_Date) ' -- /�p����    
                db.AddInParameter(dbcomm, "@GP_AddDate", DbType.String, obj.GP_AddDate) '  -- /�O�����
                db.AddInParameter(dbcomm, "@GP_Action", DbType.String, obj.GP_Action) '--/�ާ@�H

                db.AddInParameter(dbcomm, "@GP_Remark", DbType.String, obj.GP_Remark) '--/�ƪ`


                db.ExecuteNonQuery(dbcomm)
                ProductionSumPieceWorkGroup_Update = True

            Catch ex As Exception
                MsgBox(ex.Message)
                ProductionSumPieceWorkGroup_Update = False
            End Try
        End Function
        ''' <summary>
        ''' �R���ӤH�p��W��
        ''' </summary>
        ''' <param name="GP_NO"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionSumPieceWorkGroup_Delete(ByVal GP_NO As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbcomm As DbCommand = db.GetStoredProcCommand("ProductionSumPieceWorkGroup_Delete")

                db.AddInParameter(dbcomm, "@GP_NO", DbType.String, GP_NO)

                db.ExecuteNonQuery(dbcomm)
                ProductionSumPieceWorkGroup_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                ProductionSumPieceWorkGroup_Delete = False
            End Try
        End Function


        ''' <summary>
        ''' ����禬�渹 FillProductionSumPieceWorkGroup1 �@�_��W�Ϧ�
        ''' </summary>
        ''' <param name="Ndate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>

        Public Function ProductionSumPieceWorkGroup_GetNO(ByVal Ndate As String) As ProductionSumPieceWorkGroupInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("ProductionSumPieceWorkGroup_GetNO")
            db.AddInParameter(dbComm, "@NDate", DbType.String, Ndate)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    Return FillProductionSumPieceWorkGroup1(reader)
                End While
                Return Nothing
            End Using
        End Function
        ''' <summary>
        ''' ��^TOP1�էO�渹(�]�n���J���ƾڤ��h��W�w�@���)�H�����t��
        ''' </summary>
        ''' <param name="reader"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FillProductionSumPieceWorkGroup1(ByVal reader As IDataReader) As ProductionSumPieceWorkGroupInfo
            Dim ai1 As New ProductionSumPieceWorkGroupInfo
            If reader("GP_NO") Is DBNull.Value Then ai1.GP_NO = Nothing Else ai1.GP_NO = reader("GP_NO").ToString '
            Return ai1

        End Function

        '' <summary>
        ''' ���L�ɵL�ƾڮɡA�եΦ����
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NothingNew() As List(Of ProductionSumPieceWorkGroupInfo)
            Dim pi As New ProductionSumPieceWorkGroupInfo
            Dim FeatureList As New List(Of ProductionSumPieceWorkGroupInfo)
            FeatureList.Add(NothingFillProductionSumPieceWorkGroup())
            Return FeatureList
        End Function

        Public Function NothingFillProductionSumPieceWorkGroup() As ProductionSumPieceWorkGroupInfo
            Dim ai1 As New ProductionSumPieceWorkGroupInfo
            ai1.GP_NO = Nothing '            *  nvarchar(50)                /�p��渹
            ai1.Per_NO = Nothing '           *  nvarchar(50)                /���u�s��
            ai1.G_NO = Nothing '              *  nvarchar(50)                /�էO�s��
            ai1.DepID = Nothing '             *  nvarchar(50)                /�����s��
            ai1.FacID = Nothing '             *  nvarchar(50)                /�t�O

            ai1.Pro_Type = Nothing '          *  nvarchar(50)                /�u������
            ai1.PM_M_Code = Nothing '         *  nvarchar(50)                /���~�s��
            ai1.PM_Type = Nothing '          *  nvarchar(50)                /�t��W��

            ai1.PS_NameS = Nothing '          *  nvarchar(50)                /�p�u�ǦW��

            ai1.GP_factor = 0 '         *  float                       /�ӥ]�t��
            ai1.GP_Qty = 0 '            *  int                         /�ƶq 
            ai1.GP_Date = Nothing '           *  datetime                    /�p����
            ai1.GP_AddDate = Nothing '        *  datetime                 /�O�����
            ai1.GP_Action = Nothing  '        *  nvarchar(50)                /�ާ@�H

            ai1.GP_Remark = Nothing '         *  nvarchar(MAX)               /�ƪ`

            '�~��r�q
            ai1.GP_ActionName = Nothing ' �ާ@���W (SystemUser)
            ai1.GP_Per_Name = Nothing '    ���u�W�m�W(ProductionPiecePersonnel)
            ai1.GP_DepName = Nothing '    �����W
            ai1.GP_FacName = Nothing '    �t�O�W
            ai1.GP_G_Name = Nothing '      �էO�W(ProductionPieceWorkGroup)    ���Ҥ���

            ai1.PS_Name = Nothing                    '    /�j�u�ǦW��  ProcessSub
            ai1.PS_NO = Nothing                      '    �j�u�ǽs��   ProductionPieceProcess

            ai1.PP_AutoID = Nothing ''�u�����y�{
            ai1.GP_Price = 0  ''�u��

            ai1.GP_DateEnd = Nothing
            ai1.GP_DateStart = Nothing

            ai1.Print_Action = Nothing
            Return ai1
        End Function


    End Class

    '

End Namespace













