Namespace LFERP.FileManager

    Public Class FileController
        ''' <summary>
        ''' 檢查目錄是否存在,不存在帽建立
        ''' </summary>
        ''' <param name="FolderName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CreateFolder(ByVal FolderName As String) As Boolean
            Try
                Dim Str_Folder As String = FolderName
                Dim ftpReq As Net.FtpWebRequest = Net.WebRequest.Create(Str_Folder)
                ftpReq.Method = Net.WebRequestMethods.Ftp.MakeDirectory
                ftpReq.Credentials = New Net.NetworkCredential(FtpUser, FtpPassWord)
                Dim ftpResp As Net.FtpWebResponse = ftpReq.GetResponse
                ftpResp.Close()
                CreateFolder = True
            Catch ex As Exception
                ' MsgBox(ex.Message & FolderName)
                CreateFolder = False
            End Try
        End Function
        ''' <summary>
        ''' 檢查目錄并建立好當天的目錄格式如200901 / 20090101
        ''' </summary>
        ''' <remarks></remarks>
        Private Function CheckFolder() As String
            Dim temp As String
            Dim strYearMonth, strDay As String
            temp = Format(Now, "yyyyMMdd")
            strYearMonth = FtpServer & "/" & Mid(temp, 1, 6) & "/"
            strDay = strYearMonth & temp & "/"
            Try

                '年月份目錄建立
                If System.IO.Directory.Exists(strYearMonth) = False Then
                    CreateFolder(strYearMonth)
                End If
                '當天目錄建立
                If System.IO.Directory.Exists(strDay) = False Then
                    CreateFolder(strDay)
                End If
                CheckFolder = strDay
            Catch ex As Exception
                CheckFolder = ""
                MsgBox(ex.Message)
            End Try


        End Function
        ''' <summary>
        ''' 上傳文件
        ''' </summary>
        ''' <param name="sourcePathAndFileName">來源文件地址及文件名</param>
        ''' <param name="NewName">存放於服務器上的新文件名</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function UploadFile(ByVal sourcePathAndFileName As String, ByVal NewName As String) As String
            Dim temp As String
            temp = CheckFolder()
            If temp = "" Then
                UploadFile = ""
                Exit Function
            End If
            Try
                My.Computer.Network.UploadFile(sourcePathAndFileName, temp & "/" & NewName, FtpUser, FtpPassWord, True, 100, FileIO.UICancelOption.ThrowException)
                UploadFile = temp

            Catch ex As Exception
                UploadFile = ""
                MsgBox(ex.Message)
                MsgBox(sourcePathAndFileName)
                MsgBox(temp)
            End Try
        End Function
        ''' <summary>
        ''' 下載文件
        ''' </summary>
        ''' <param name="FileAddress"></param>
        ''' <param name="NewPathName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function DownloadFile(ByVal FileAddress As String, ByVal NewPathName As String) As String
            My.Computer.Network.DownloadFile(FtpServer & "/" & FileAddress, NewPathName, FtpUser, FtpPassWord, True, 100, True)
            DownloadFile = NewPathName
        End Function

        ''' <summary>
        ''' 上傳文件
        ''' </summary>
        ''' <param name="strFilePath"></param>
        ''' <param name="objFile"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function File_Update(ByVal strFilePath As String, ByVal objFile As FilesInfo) As Boolean
            ' Dim strM_Code As String = GetM_Code(objMaterial.M_Code)
            '檢查物料編碼編號是否已存在
            Dim tempPath As String
            '生成路徑
            tempPath = Format(Now, "yyyyMM") & "/" & Format(Now, "yyyyMMdd")

            If objFile Is Nothing Then
                File_Update = False
            Else
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("FileDetail_Add")
                db.AddInParameter(dbComm, "@F_No", DbType.String, objFile.F_No)
                db.AddInParameter(dbComm, "@F_Name", DbType.String, objFile.F_Name)
                db.AddInParameter(dbComm, "@F_Detail", DbType.String, objFile.F_Detail)
                db.AddInParameter(dbComm, "@F_Adddate", DbType.String, objFile.F_AddDate)
                db.AddInParameter(dbComm, "@F_Action", DbType.String, objFile.F_Action)
                db.AddInParameter(dbComm, "@F_OldName", DbType.String, objFile.F_OldName)

                Dim returnValue As New SqlParameter("@F_NewName", SqlDbType.VarChar, 88)
                returnValue.Direction = ParameterDirection.Output
                ' db.AddInParameter(dbComm, "@F_NewName", DbType.String, objFile.F_NewName)
                'db.AddInParameter(returnValue)

                dbComm.Parameters.Add(returnValue)

                db.AddInParameter(dbComm, "@F_FileType", DbType.String, objFile.F_FileType)
                db.AddInParameter(dbComm, "@F_FilePath", DbType.String, tempPath)
                db.AddInParameter(dbComm, "@F_Remark", DbType.String, objFile.F_Remark)
                db.AddInParameter(dbComm, "@F_TypeID", DbType.String, objFile.F_TypeID)
                db.AddInParameter(dbComm, "@FB_Type", DbType.String, objFile.FB_Type)
                db.AddInParameter(dbComm, "@FB_TypeNo", DbType.String, objFile.FB_TypeNo)

                Dim tTemp As String = ""
                'Using reader As IDataReader = db.ExecuteReader(dbComm)
                '    While reader.Read
                '        tTemp = reader("Return Value").ToString
                '    End While

                'End Using
                '         string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ToString();
                'SqlConnection conn = new SqlConnection(conStr);
                'SqlCommand cmd = new SqlCommand();
                'cmd.CommandText = "NorthwindSearch";
                'cmd.CommandType = CommandType.StoredProcedure;
                'cmd.Connection=conn;
                'conn.Open();
                'SqlParameter sp = new SqlParameter("@productID", SqlDbType.Int);
                'sp.Value = int.Parse(txtProduct.Text.Trim());
                'cmd.Parameters.Add(sp);

                '//定义输出参数
                'sp = new SqlParameter("@outputValue", SqlDbType.NVarChar,50);
                'sp.Direction = ParameterDirection.Output;
                'cmd.Parameters.Add(sp);
                'cmd.ExecuteNonQuery();
                'txtSupplierID.Text = cmd.Parameters[1].Value.ToString();
                'conn.Close();



                'Dim returnValue As New SqlParameter("@Description", SqlDbType.VarChar, 88)

                'returnValue.Direction = ParameterDirection.Output

                db.ExecuteNonQuery(dbComm)

                tTemp = dbComm.Parameters("@F_NewName").Value.ToString()

                'MsgBox(tTemp)

                File_Update = True
                UploadFile(strFilePath, tTemp)


            End If
        End Function
        ''' <summary>
        ''' 取得選定類型模塊內的單號所附加的檔案清單
        ''' </summary>
        ''' <param name="FB_Type">模塊編號</param>
        ''' <param name="FB_TypeNo">所屬模塊內單號</param>
        ''' <param name="F_No">文件編號" 格式 編號1,編號2,編號3,...</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' 
        Public Function FileBond_GetList(ByVal FB_Type As String, ByVal FB_TypeNo As String, ByVal F_No As String) As List(Of FilesInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("FileBond_GetList")
            If FB_Type Is Nothing Then
                db.AddInParameter(dbComm, "@FB_Type", DbType.String, DBNull.Value)
            Else
                db.AddInParameter(dbComm, "@FB_Type", DbType.String, FB_Type)
            End If
            If FB_TypeNo Is Nothing Then
                db.AddInParameter(dbComm, "@FB_TypeNo", DbType.String, DBNull.Value)
            Else
                db.AddInParameter(dbComm, "@FB_TypeNo", DbType.String, FB_TypeNo)
            End If
            If F_No Is Nothing Then
                db.AddInParameter(dbComm, "@F_No", DbType.String, DBNull.Value)
            Else
                db.AddInParameter(dbComm, "@F_No", DbType.String, F_No)
            End If
            Dim FeatureList As New List(Of FilesInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillFileBondList(reader))
                End While
                Return FeatureList
            End Using

        End Function
      
        Friend Function FillFileBondList(ByVal reader As IDataReader) As FilesInfo
            On Error Resume Next
            '對應取得的數據,多條記錄時使用，
            Dim objInfo As New FilesInfo
            objInfo.F_No = reader("F_No").ToString
            objInfo.F_Name = reader("F_Name").ToString
            objInfo.F_Detail = reader("F_Detail").ToString
            objInfo.F_Action = reader("F_Action").ToString
            If reader("F_AddDate") Is DBNull.Value Then
                objInfo.F_AddDate = Nothing
            Else
                objInfo.F_AddDate = CStr(reader("F_AddDate"))
            End If
            objInfo.F_OldName = reader("F_OldName").ToString
            objInfo.F_NewName = reader("F_NewName").ToString
            objInfo.F_FileType = reader("F_FileType").ToString
            objInfo.F_FilePath = reader("F_FilePath").ToString
            objInfo.F_Remark = reader("F_Remark").ToString
            objInfo.F_TypeID = reader("F_TypeID").ToString
            objInfo.FB_Type = reader("FB_Type").ToString
            objInfo.FB_TypeNo = reader("FB_TypeNo").ToString

            Return objInfo
        End Function
        ''' <summary>
        ''' 打開文件
        ''' </summary>
        ''' <param name="FB_Type"></param>
        ''' <param name="FB_TypeNo"></param>
        ''' <param name="F_No"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function File_Open(ByVal FB_Type As String, ByVal FB_TypeNo As String, ByVal F_No As String) As Boolean
            Dim objInfo As New List(Of FilesInfo)
            Dim strPath As String

            objInfo = FileBond_GetList(FB_Type, FB_TypeNo, F_No)
            If objInfo Is Nothing Then
                File_Open = False
            Else
                strPath = DownloadFile(objInfo(0).F_FilePath & "/" & objInfo(0).F_NewName, System.Windows.Forms.Application.StartupPath & _
                "\TempFile\" & objInfo(0).F_OldName)

                System.Diagnostics.Process.Start(strPath)
                File_Open = True
            End If
            'Application.StartupPath & "\TempFile"

        End Function


        Public Function File_DownLoad(ByVal FB_Type As String, ByVal FB_TypeNo As String, ByVal F_No As String, ByVal SavePath As String) As Boolean
            Dim objInfo As New List(Of FilesInfo)
            Dim strPath As String

            objInfo = FileBond_GetList(FB_Type, FB_TypeNo, F_No)
            If objInfo Is Nothing Then
                File_DownLoad = False
            Else
                strPath = DownloadFile(objInfo(0).F_FilePath & "/" & objInfo(0).F_NewName, SavePath)
                '& "\" & objInfo(0).F_OldName
                'System.Diagnostics.Process.Start(strPath)
                File_DownLoad = True
            End If
        End Function
        ''' <summary>
        ''' 刪除文件
        ''' </summary>
        ''' <param name="FB_Type"></param>
        ''' <param name="FB_TypeNo"></param>
        ''' <param name="F_No"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function File_Delete(ByVal FB_Type As String, ByVal FB_TypeNo As String, ByVal F_No As String) As Boolean

            Dim objInfo As New List(Of FilesInfo)
            Dim objFTP As New FTPAccess


            objInfo = FileBond_GetList(FB_Type, FB_TypeNo, F_No)
            If objInfo Is Nothing Then
                File_Delete = False
            Else
              
                If objFTP.DeleteFile("/LFERP/" & objInfo(0).F_FilePath & "/" & objInfo(0).F_NewName) = True Then

                    Try
                        Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                        Dim dbComm As DbCommand = db.GetStoredProcCommand("FileBond_Delete")

                        db.AddInParameter(dbComm, "@FB_Type", DbType.String, FB_Type)
                        db.AddInParameter(dbComm, "@FB_TypeNo", DbType.String, FB_TypeNo)
                        db.AddInParameter(dbComm, "@F_No", DbType.String, F_No)
                        db.ExecuteNonQuery(dbComm)
                        File_Delete = True
                    Catch ex As Exception
                        File_Delete = False
                    End Try
                    File_Delete = True
                End If
                

            End If

        End Function


    End Class

End Namespace

