Namespace LFERP.DataSetting
    Public Class DepartmentControler

        Public Function Department_Add(ByVal objinfo As DepartmentInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("Department_Add")

                db.AddInParameter(dbComm, "DPT_ID", DbType.String, objinfo.DPT_ID)
                db.AddInParameter(dbComm, "DPT_Name", DbType.String, objinfo.DPT_Name)
                db.AddInParameter(dbComm, "DPT_PID", DbType.String, objinfo.DPT_PID)
                db.ExecuteNonQuery(dbComm)
                Department_Add = True

            Catch ex As Exception
                MsgBox(ex.Message)
                Department_Add = False
            End Try

        End Function
        Public Function Department_Update(ByVal objinfo As DepartmentInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("Department_Update")

                db.AddInParameter(dbComm, "DPT_ID", DbType.String, objinfo.DPT_ID)
                db.AddInParameter(dbComm, "DPT_Name", DbType.String, objinfo.DPT_Name)
                db.AddInParameter(dbComm, "DPT_PID", DbType.String, objinfo.DPT_PID)
                db.ExecuteNonQuery(dbComm)
                Department_Update = True
            Catch ex As Exception
                MsgBox(ex.Message)
                Department_Update = False
            End Try

        End Function

#Region "李超新增20140415"
        Public Function BriName_Update(ByVal objinfo As DepartmentInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleBriName_Update")
                db.AddInParameter(dbComm, "@DepID", DbType.String, objinfo.DepID)
                db.AddInParameter(dbComm, "@D_Enabled", DbType.Boolean, objinfo.D_Enabled)
                db.AddInParameter(dbComm, "@D_Alarm", DbType.Boolean, objinfo.D_Alarm)
                db.AddInParameter(dbComm, "@IsWeight", DbType.Boolean, objinfo.IsWeight)
                db.AddInParameter(dbComm, "@ErrorRate", DbType.Decimal, objinfo.ErrorRate)
                db.ExecuteNonQuery(dbComm)
                BriName_Update = True
            Catch ex As Exception
                MsgBox(ex.Message)
                BriName_Update = False
            End Try
        End Function
        ''' <summary>
        ''' 2014-04-22
        ''' 姚     駿
        ''' 更新所有部門的稱重誤差率
        ''' </summary>
        ''' <param name="objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SampleBriName_UpdateAll(ByVal objinfo As DepartmentInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleBriName_UpdateAll")
                db.AddInParameter(dbComm, "@IsWeight", DbType.Boolean, objinfo.IsWeight)
                db.AddInParameter(dbComm, "@ErrorRate", DbType.Decimal, objinfo.ErrorRate)
                db.ExecuteNonQuery(dbComm)
                SampleBriName_UpdateAll = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleBriName_UpdateAll = False
            End Try
        End Function

        Public Function BriName_GetList(ByVal DepID As String) As List(Of DepartmentInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleBriName_GetList")
            db.AddInParameter(dbComm, "@DepID", DbType.String, DepID)
            Dim FeatureList As New List(Of DepartmentInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillDepartment(reader))
                End While
                Return FeatureList
            End Using
        End Function
#End Region

        Public Function Department_Delete(ByVal DPT_ID As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("Department_Delete")

                db.AddInParameter(dbComm, "DPT_ID", DbType.String, DPT_ID)
                db.ExecuteNonQuery(dbComm)
                Department_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                Department_Delete = False
            End Try

        End Function
        Public Function Department_GetID(ByVal DPT_PID As String) As DepartmentInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbcommand As DbCommand = db.GetStoredProcCommand("Department_GetID")
            db.AddInParameter(dbcommand, "@DPT_PID", DbType.String, DPT_PID)
            Using reader As IDataReader = db.ExecuteReader(dbcommand)
                While reader.Read
                    Return FillDepartment(reader)
                End While
                Return Nothing
            End Using
        End Function
        Public Function Department_Get(ByVal DPT_ID As String, ByVal DPT_Name As String, ByVal DPT_PID As String) As DepartmentInfo

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbcomm As DbCommand = db.GetStoredProcCommand("Department_Get")

            db.AddInParameter(dbcomm, "DPT_ID", DbType.String, DPT_ID)
            db.AddInParameter(dbcomm, "DPT_Name", DbType.String, DPT_Name)
            db.AddInParameter(dbcomm, "DPT_PID", DbType.String, DPT_PID)
            Using reader As IDataReader = db.ExecuteReader(dbcomm)
                While reader.Read
                    Return FillDepartment(reader)
                End While
                Return Nothing
            End Using
        End Function
        Public Function Department_GetList(ByVal DPT_ID As String, ByVal DPT_Name As String, ByVal DPT_PID As String) As List(Of DepartmentInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("Department_GetList")

            db.AddInParameter(dbComm, "DPT_ID", DbType.String, DPT_ID)
            db.AddInParameter(dbComm, "DPT_Name", DbType.String, DPT_Name)
            db.AddInParameter(dbComm, "DPT_PID", DbType.String, DPT_PID)
            Dim FeatureList As New List(Of DepartmentInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillDepartment(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function Department_GetList1(ByVal DPT_ID As String, ByVal DPT_Name As String, ByVal DPT_PID As String) As List(Of DepartmentInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("Department_GetList1")

            db.AddInParameter(dbComm, "DPT_ID", DbType.String, DPT_ID)
            db.AddInParameter(dbComm, "DPT_Name", DbType.String, DPT_Name)
            db.AddInParameter(dbComm, "DPT_PID", DbType.String, DPT_PID)
            Dim FeatureList As New List(Of DepartmentInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillDepartment(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function BriName_Add(ByVal objinfo As DepartmentInfo) As Boolean

            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("BriName_Add")

                db.AddInParameter(dbComm, "DepID", DbType.String, objinfo.DepID)
                db.AddInParameter(dbComm, "DepName", DbType.String, objinfo.DepName)
                db.AddInParameter(dbComm, "FacID", DbType.String, objinfo.FacID)

                db.ExecuteNonQuery(dbComm)
                BriName_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                BriName_Add = False
            End Try
        End Function

        Public Function BriName_UpdateName(ByVal objinfo As DepartmentInfo) As Boolean

            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("BriName_UpdateName")

                db.AddInParameter(dbComm, "DepID", DbType.String, objinfo.DepID)
                db.AddInParameter(dbComm, "DepName", DbType.String, objinfo.DepName)
                db.ExecuteNonQuery(dbComm)
                BriName_UpdateName = True
            Catch ex As Exception
                MsgBox(ex.Message)
                BriName_UpdateName = False
            End Try
        End Function

        Public Function BriName_GetList(ByVal DepID As String, ByVal DepName As String, ByVal FacID As String) As List(Of DepartmentInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("BriName_GetList")

            db.AddInParameter(dbComm, "DepID", DbType.String, DepID)
            db.AddInParameter(dbComm, "DepName", DbType.String, DepName)
            db.AddInParameter(dbComm, "FacID", DbType.String, FacID)

            Dim FeatureList As New List(Of DepartmentInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillDepartment(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Friend Function FillDepartment(ByVal reader As IDataReader) As DepartmentInfo
            '對應取得的數據
            On Error Resume Next

            Dim objInfo As New DepartmentInfo
            objInfo.DPT_ID = reader("DPT_ID").ToString
            objInfo.DPT_Name = reader("DPT_Name").ToString
            objInfo.DPT_PID = reader("DPT_PID").ToString

            objInfo.DepID = reader("DepID").ToString
            objInfo.DepName = reader("DepName").ToString
            objInfo.FacID = reader("FacID").ToString
            objInfo.ArtType = reader("ArtType").ToString

            objInfo.FacName = reader("FacName").ToString
            objInfo.DPT_PName = reader("DPT_PName").ToString
            objInfo.D_Alarm = reader("D_Alarm")
            objInfo.D_Enabled = reader("D_Enabled")
            objInfo.IsWeight = reader("IsWeight")
            objInfo.ErrorRate = reader("ErrorRate")
            objInfo.D_Cycle = reader("D_Cycle")

            '--------------------------------------------------------------------

            objInfo.CheckLock = reader("CheckLock")

            objInfo.CheckDwonRate = reader("CheckDwonRate")
            objInfo.CheckUpRate = reader("CheckUpRate")
            objInfo.OutDwonRate = reader("OutDwonRate")
            objInfo.OutUpRate = reader("OutUpRate")

            objInfo.InDwonRate = reader("InDwonRate")
            objInfo.InUpRate = reader("InUpRate")

            objInfo.WasteDwonRate = reader("WasteDwonRate")
            objInfo.WasteUpRate = reader("WasteUpRate")

            Return objInfo
        End Function

#Region "獲得部門模塊樹狀的詳細信息"
        Private Sub GetDPTModule1(ByVal PID As String, ByVal tNodes As Windows.Forms.TreeView)
            '導入第一層類型   
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("Department_GetList")
            db.AddInParameter(dbComm, "@DPT_PID", DbType.String, PID)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    Dim tree_leaf As New Windows.Forms.TreeNode()
                    tree_leaf.Tag = reader("DPT_ID")
                    tree_leaf.Text = reader("DPT_Name")
                    tNodes.Nodes.Add(tree_leaf)
                End While
            End Using
        End Sub
        Private Sub GetDPTModule2(ByVal ID1 As Integer, ByVal PID As String, ByVal tNodes As Windows.Forms.TreeView)
            '導入第一層類型   
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("Department_GetList")
            db.AddInParameter(dbComm, "@DPT_PID", DbType.String, PID)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    Dim tree_leaf As New Windows.Forms.TreeNode()
                    tree_leaf.Tag = reader("DPT_ID")
                    tree_leaf.Text = reader("DPT_Name")
                    tNodes.Nodes(ID1).Nodes.Add(tree_leaf)
                End While
            End Using
        End Sub
        Private Sub GetDPTModule3(ByVal ID1 As Integer, ByVal ID2 As Integer, ByVal PID As String, ByVal tNodes As Windows.Forms.TreeView)
            '導入第2層類型   
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("Department_GetList")
            db.AddInParameter(dbComm, "@DPT_PID", DbType.String, PID)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    Dim tree_leaf As New Windows.Forms.TreeNode()
                    tree_leaf.Tag = reader("DPT_ID")
                    tree_leaf.Text = reader("DPT_Name")
                    tNodes.Nodes(ID1).Nodes(ID2).Nodes.Add(tree_leaf)
                End While
            End Using
        End Sub
        Private Sub GetDPTModule4(ByVal ID1 As Integer, ByVal ID2 As Integer, ByVal ID3 As Integer, ByVal PID As String, ByVal tNodes As Windows.Forms.TreeView)
            '導入第3層類型   
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("Department_GetList")
            db.AddInParameter(dbComm, "@DPT_PID", DbType.String, PID)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    Dim tree_leaf As New Windows.Forms.TreeNode()
                    tree_leaf.Tag = reader("DPT_ID")
                    tree_leaf.Text = reader("DPT_Name")
                    tNodes.Nodes(ID1).Nodes(ID2).Nodes(ID3).Nodes.Add(tree_leaf)
                End While
            End Using
        End Sub
        Private Sub GetDPTModule5(ByVal ID1 As Integer, ByVal ID2 As Integer, ByVal ID3 As Integer, ByVal ID4 As Integer, ByVal PID As String, ByVal tNodes As Windows.Forms.TreeView)
            '導入第4層類型   
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("Department_GetList")
            db.AddInParameter(dbComm, "@DPT_PID", DbType.String, PID)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    Dim tree_leaf As New Windows.Forms.TreeNode()
                    tree_leaf.Tag = reader("DPT_ID")
                    tree_leaf.Text = reader("DPT_Name")
                    tNodes.Nodes(ID1).Nodes(ID2).Nodes(ID3).Nodes(ID4).Nodes.Add(tree_leaf)
                End While
            End Using
        End Sub
        Public Sub GetDepartmentModule(ByVal TreeView As Windows.Forms.TreeView)
            TreeView.Nodes.Clear()
            GetDPTModule1("00", TreeView)
            Dim iNodeNum As Integer = TreeView.GetNodeCount(False)
            Dim A, B, C, D As Integer
            For A = 0 To iNodeNum - 1
                '導入第二層類型 
                GetDPTModule2(A, TreeView.Nodes(A).Tag, TreeView)
                Dim Nodecount As Integer = TreeView.Nodes.Item(A).GetNodeCount(False)
                For B = 0 To Nodecount - 1
                    ''導入第三層類型
                    GetDPTModule3(A, B, TreeView.Nodes(A).Nodes(B).Tag, TreeView)
                    Dim NodecountC As Integer = TreeView.Nodes.Item(A).Nodes.Item(B).GetNodeCount(False)
                    For C = 0 To NodecountC - 1
                        GetDPTModule4(A, B, C, TreeView.Nodes(A).Nodes(B).Nodes(C).Tag, TreeView)
                        Dim NodecountD As Integer = TreeView.Nodes.Item(A).Nodes.Item(B).Nodes.Item(C).GetNodeCount(False)
                        For D = 0 To NodecountD - 1
                            GetDPTModule5(A, B, C, D, TreeView.Nodes(A).Nodes(B).Nodes(C).Nodes(D).Tag, TreeView)
                        Next
                    Next
                Next
            Next
        End Sub

#End Region


#Region "针对贵金属加"
        Public Function BriNameRateLock_Update(ByVal objinfo As DepartmentInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("BriNameRateLock_Update")
                db.AddInParameter(dbComm, "@DepID", DbType.String, objinfo.DepID)
                db.AddInParameter(dbComm, "@CheckLock", DbType.Boolean, objinfo.CheckLock)

                db.AddInParameter(dbComm, "@CheckDwonRate", DbType.Decimal, objinfo.CheckDwonRate)
                db.AddInParameter(dbComm, "@CheckUpRate", DbType.Decimal, objinfo.CheckUpRate)

                db.AddInParameter(dbComm, "@OutDwonRate", DbType.Decimal, objinfo.OutDwonRate)
                db.AddInParameter(dbComm, "@OutUpRate", DbType.Decimal, objinfo.OutUpRate)

                db.AddInParameter(dbComm, "@InDwonRate", DbType.Decimal, objinfo.InDwonRate)
                db.AddInParameter(dbComm, "@InUpRate", DbType.Decimal, objinfo.InUpRate)

                db.AddInParameter(dbComm, "@WasteDwonRate", DbType.Decimal, objinfo.WasteDwonRate)
                db.AddInParameter(dbComm, "@WasteUpRate", DbType.Decimal, objinfo.WasteUpRate)

                db.ExecuteNonQuery(dbComm)
                BriNameRateLock_Update = True
            Catch ex As Exception
                MsgBox(ex.Message)
                BriNameRateLock_Update = False
            End Try
        End Function
#End Region


    End Class

End Namespace