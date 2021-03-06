USE [TESTLFERP]
GO
/****** Object:  Table [dbo].[SampleDivert]    Script Date: 01/17/2014 10:15:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SampleDivert](
	[AutoID] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[SD_ID] [nvarchar](50) NULL,
	[Code_ID] [nvarchar](50) NULL,
	[SD_OutSO_ID] [nvarchar](50) NULL,
	[SD_OutD_ID] [nvarchar](50) NULL,
	[SD_OutPS_NO] [nvarchar](50) NULL,
	[SD_InSO_ID] [nvarchar](50) NULL,
	[SD_InD_ID] [nvarchar](50) NULL,
	[SD_InPS_NO] [nvarchar](50) NULL,
	[SD_Qty] [int] NULL,
	[SD_Check] [bit] NULL,
	[SD_CheckDate] [datetime] NULL,
	[SD_CheckAction] [nvarchar](50) NULL,
	[SD_CheckRemark] [nvarchar](200) NULL,
	[SD_CardID] [nvarchar](50) NULL,
	[SD_Remark] [nvarchar](200) NULL,
	[SD_AddUserID] [nvarchar](50) NULL,
	[SD_AddDate] [datetime] NULL,
	[SD_ModifyUserID] [nvarchar](50) NULL,
	[SD_ModifyDate] [datetime] NULL
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'單號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SampleDivert', @level2type=N'COLUMN',@level2name=N'SD_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'條碼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SampleDivert', @level2type=N'COLUMN',@level2name=N'Code_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'發出訂單編號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SampleDivert', @level2type=N'COLUMN',@level2name=N'SD_OutSO_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'發出部門' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SampleDivert', @level2type=N'COLUMN',@level2name=N'SD_OutD_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'發出工序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SampleDivert', @level2type=N'COLUMN',@level2name=N'SD_OutPS_NO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收入訂單編號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SampleDivert', @level2type=N'COLUMN',@level2name=N'SD_InSO_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收入部門' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SampleDivert', @level2type=N'COLUMN',@level2name=N'SD_InD_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收發工序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SampleDivert', @level2type=N'COLUMN',@level2name=N'SD_InPS_NO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'數量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SampleDivert', @level2type=N'COLUMN',@level2name=N'SD_Qty'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'審核' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SampleDivert', @level2type=N'COLUMN',@level2name=N'SD_Check'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'審核時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SampleDivert', @level2type=N'COLUMN',@level2name=N'SD_CheckDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'審核人員' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SampleDivert', @level2type=N'COLUMN',@level2name=N'SD_CheckAction'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'刷卡' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SampleDivert', @level2type=N'COLUMN',@level2name=N'SD_CardID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'備註' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SampleDivert', @level2type=N'COLUMN',@level2name=N'SD_Remark'
GO
/****** Object:  StoredProcedure [dbo].[SampleDivert_Getlist]    Script Date: 01/17/2014 10:15:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author: 
-- Create date: 
-- Description: 
-- =============================================    
CREATE PROCEDURE [dbo].[SampleDivert_Getlist]    
		@AutoID			nvarchar(18)=null,
		@SD_ID			nvarchar(50)=null,
		@Code_ID		nvarchar(50)=null,
		
		@SD_OutSO_ID	nvarchar(50)=null,
		@SD_OutD_ID		nvarchar(50)=null,
		@SD_OutPS_NO	nvarchar(50)=null,
		
		@SD_InSO_ID		nvarchar(50)=null,
		@SD_InD_ID		nvarchar(50)=null,
		@SD_InPS_NO		nvarchar(50)=null,
		
		@SD_Check		bit	=null,
		@SD_AddDate1	nvarchar(50)=null,
		@SD_AddDate2	nvarchar(50)=null
AS    
    
declare @strSQL   varchar(5000)        -- 主语句    
declare @strOrder varchar(1000)        -- 排序类型    
declare @strWhere varchar(1000)        -- 查询条件     
    
set @strOrder=' order by SD_ID desc'    
set @strWhere=''    
    
set @strWhere=@strWhere    
    + isnull(' and a.AutoID ='''+ltrim(@AutoID)+'''','')    
    + isnull(' and a.SD_ID='''+ltrim(@SD_ID)+'''','')    
    + isnull(' and a.Code_ID='''+ltrim(@Code_ID)+'''','') 
       
    + isnull(' and a.SD_OutSO_ID='''+ltrim(@SD_OutSO_ID)+'''','')     
    + isnull(' and a.SD_OutD_ID='''+ltrim(@SD_OutD_ID)+'''','')     
    + isnull(' and a.SD_OutPS_NO ='''+ltrim(@SD_OutPS_NO)+'''','') 
       
    + isnull(' and a.SD_InSO_ID ='''+ltrim(@SD_InSO_ID)+'''','')     
    + isnull(' and a.SD_InD_ID='''+ltrim(@SD_InD_ID)+'''','')    
    + isnull(' and a.SD_InPS_NO='''+ltrim(@SD_InPS_NO)+'''','')   
     
    + isnull(' and a.SD_Check='''+ltrim(@SD_Check)+'''','')    
    + isnull(' and a.SD_AddDate1 >='''+ltrim(@SD_AddDate1)+'''','')    
    + isnull(' and a.SD_AddDate2 <='''+ltrim(@SD_AddDate2)+'''','')    
       
if len(@strWhere)>0 set @strWhere = stuff(@strWhere,1,4,' where ')    
    
 set @strSQL= 'SELECT a.*    
              ,(select U_Name from SystemUser WHERE U_ID=a.SD_CheckAction) as SD_CheckActionName    
              ,(select U_Name from SystemUser WHERE U_ID=a.SD_AddUserID) as SD_AddUserName  
              ,(select U_Name from SystemUser WHERE U_ID=a.SD_ModifyUserID) as SD_ModifyUserName  
                
              ,(select PS_Name from SampleProcessSub where PS_NO=a.SD_OutPS_NO) as SD_OutPS_Name 
              ,(select PS_Name from SampleProcessSub where PS_NO=a.SD_InPS_NO) as SD_InPS_Name
                     
              ,(select DepName from BriName where DepID=a.SD_OutD_ID ) as SD_OutD_Name    
              ,(select DepName from BriName where DepID=a.SD_InD_ID ) as SD_InD_Name    
              
              ,(select top 1 SO_SampleID from dbo.SampleOrdersMain where SO_ID=a.SD_OutSO_ID) as OutSO_SampleID  
              ,(select top 1 SO_SampleID from dbo.SampleOrdersMain where SO_ID=a.SD_InSO_ID) as InSO_SampleID
              ,(select top 1 PM_M_Code from dbo.SampleOrdersMain where SO_ID=a.SD_OutSO_ID) as OutPM_M_Code  
              ,(select top 1 PM_M_Code from dbo.SampleOrdersMain where SO_ID=a.SD_InSO_ID) as InPM_M_Code
              
  FROM SampleDivert As a ' + @strWhere + @strOrder    
    
PRINT @strSQL    
exec(@strSQL)
GO
/****** Object:  StoredProcedure [dbo].[SampleDivert_GetID]    Script Date: 01/17/2014 10:15:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  
-- Create date:
-- Description:
-- =============================================  
CREATE PROCEDURE [dbo].[SampleDivert_GetID]   
  @SD_ID		nvarchar(50)=null  
as  
declare @strSQL   varchar(2000)       -- 主语句  
declare @strOrder varchar(400)        -- 排序类型  
declare @strWhere varchar(1000)  -- 查询条件   
  
set @strOrder = '  order by SD_ID desc' --排序  
  
set @strWhere=''  
  
set @strWhere=@strWhere     
    + isnull(' and SD_ID LIKE '''+ltrim(@SD_ID)+'%''','')  
  
if len(@strWhere)>0 set @strWhere = stuff(@strWhere,1,4,' where')
  
set @strSQL ='SELECT TOP 1  SD_ID FROM SampleDivert' + @strWhere  +@strOrder  
exec (@strSQL)
GO
/****** Object:  StoredProcedure [dbo].[SampleDivert_Delete]    Script Date: 01/17/2014 10:15:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SampleDivert_Delete]
		@AutoID     Numeric(18,0)=null,
		@SD_ID		Nvarchar(50)=null

as
declare @strSQL   varchar(600)       -- 主语句
declare @strOrder varchar(400)        -- 排序类型
declare @strWhere varchar(1000)  -- 查询条件 

set @strWhere=''
set @strWhere=@strWhere
	
       + isnull(' and AutoID='''+ltrim(@AutoID)+'''','')
       + isnull(' and SD_ID='''+ltrim(@SD_ID)+'''','')
       
if len(@strWhere)<>0 
begin  
	if len(@strWhere)>0 set @strWhere = stuff(@strWhere,1,4,' where ')
	set @strSQL= 'delete from  SampleDivert ' + @strWhere
	exec (@strSQL)
end
GO
/****** Object:  StoredProcedure [dbo].[SampleDivert_Update]    Script Date: 01/17/2014 10:15:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  
-- Create date:  
-- Description: 
-- =============================================  
  
CREATE PROCEDURE [dbo].[SampleDivert_Update]
        @AutoID				Numeric(18,0),
           
		@SD_ID				nvarchar(50),
	    @Code_ID			nvarchar(50),
	    @SD_OutSO_ID		nvarchar(50),
	    @SD_OutD_ID			nvarchar(50),
		@SD_OutPS_NO		nvarchar(50),
		
		@SD_InSO_ID			nvarchar(50),
		@SD_InD_ID			nvarchar(50),
		@SD_InPS_NO			nvarchar(50),
		@SD_Qty				int,
		@SD_CardID			nvarchar(50),
		
		@SD_Remark			nvarchar(200)=NULL,
		@SD_ModifyUserID	nvarchar(50),
		@SD_ModifyDate		datetime
as  
UPDATE [SampleDivert]  
   SET [SD_ID] = @SD_ID  
      ,[Code_ID]=@Code_ID  
      ,[SD_OutSO_ID] = @SD_OutSO_ID  
      ,[SD_OutPS_NO] = @SD_OutPS_NO
       
      ,[SD_InSO_ID] = @SD_InSO_ID  
      ,[SD_InD_ID] = @SD_InD_ID
      ,[SD_InPS_NO] = @SD_InPS_NO
      ,[SD_Qty] = @SD_Qty 
      ,[SD_CardID] = @SD_CardID
       
      ,[SD_Remark] = @SD_Remark          
      ,[SD_ModifyUserID] = @SD_ModifyUserID
      ,[SD_ModifyDate] = @SD_ModifyDate
 WHERE AutoID=@AutoID
GO
/****** Object:  StoredProcedure [dbo].[SampleDivert_Check]    Script Date: 01/17/2014 10:15:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  
-- Create date:  
-- Description: 
-- =============================================  
  
CREATE PROCEDURE [dbo].[SampleDivert_Check]
		@SD_ID				nvarchar(50),
		@SD_CheckAction		nvarchar(50),
		@SD_CheckDate		datetime,
		@SD_Check			bit,
		@SD_CheckRemark     nvarchar(200)
as  
UPDATE [SampleDivert]  
   SET [SD_CheckAction] = @SD_CheckAction
      ,[SD_CheckDate]=@SD_CheckDate  
      ,[SD_Check] = @SD_Check
      ,[SD_CheckRemark] = @SD_CheckRemark
 WHERE SD_ID=@SD_ID
GO
/****** Object:  StoredProcedure [dbo].[SampleDivert_Add]    Script Date: 01/17/2014 10:15:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author: 
-- Create date: 
-- Description: 
-- =============================================    
CREATE PROCEDURE [dbo].[SampleDivert_Add]    
	    @SD_ID				nvarchar(50),
	    @Code_ID			nvarchar(50),
	    @SD_OutSO_ID		nvarchar(50),
	    @SD_OutD_ID			nvarchar(50),
		@SD_OutPS_NO		nvarchar(50),
		
		@SD_InSO_ID			nvarchar(50),
		@SD_InD_ID			nvarchar(50),
		@SD_InPS_NO			nvarchar(50),
		@SD_Qty				int,
		@SD_CardID			nvarchar(50),
		
		@SD_Remark			nvarchar(200)=NULL,
		@SD_AddUserID		nvarchar(50),
		@SD_AddDate			datetime  
AS    
BEGIN    
INSERT INTO [SampleDivert]    
           ([SD_ID] 
           ,[Code_ID]    
           ,[SD_OutSO_ID]    
           ,[SD_OutD_ID]    
           ,[SD_OutPS_NO] 
             
           ,[SD_InSO_ID]    
           ,[SD_InD_ID]   
           ,[SD_InPS_NO]  
           ,[SD_Qty]    
           ,[SD_CardID]   
           
           ,[SD_Remark]  
           ,[SD_AddUserID]
           ,[SD_AddDate]  
           )    
     VALUES    
           (@SD_ID
           ,@Code_ID
           ,@SD_OutSO_ID
           ,@SD_OutD_ID    
           ,@SD_OutPS_NO
             
           ,@SD_InSO_ID    
           ,@SD_InD_ID
           ,@SD_InPS_NO
           ,@SD_Qty
           ,@SD_CardID
           
           ,@SD_Remark
           ,@SD_AddUserID
           ,@SD_AddDate 
           )    
END
GO
