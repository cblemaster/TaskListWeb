USE [master]
GO
/****** Object:  Database [ToDoList]    Script Date: 11/1/2020 12:43:13 PM ******/
CREATE DATABASE [ToDoList]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ToDoList', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ToDoList.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ToDoList_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ToDoList_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ToDoList] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ToDoList].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ToDoList] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ToDoList] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ToDoList] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ToDoList] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ToDoList] SET ARITHABORT OFF 
GO
ALTER DATABASE [ToDoList] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ToDoList] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ToDoList] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ToDoList] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ToDoList] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ToDoList] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ToDoList] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ToDoList] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ToDoList] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ToDoList] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ToDoList] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ToDoList] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ToDoList] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ToDoList] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ToDoList] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ToDoList] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ToDoList] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ToDoList] SET RECOVERY FULL 
GO
ALTER DATABASE [ToDoList] SET  MULTI_USER 
GO
ALTER DATABASE [ToDoList] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ToDoList] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ToDoList] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ToDoList] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ToDoList] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ToDoList', N'ON'
GO
ALTER DATABASE [ToDoList] SET QUERY_STORE = OFF
GO
USE [ToDoList]
GO
/****** Object:  Table [dbo].[folder_sort_field]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[folder_sort_field](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](10) NOT NULL,
 CONSTRAINT [PK_folder_sort_field] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_folder_sort_field] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[folder_sort_order]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[folder_sort_order](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](4) NOT NULL,
 CONSTRAINT [PK_folder_sort_order] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_folder_sort_order] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[item_recurrence]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[item_recurrence](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](10) NOT NULL,
 CONSTRAINT [PK_item_recurrence] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_item_recurrence] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[todo_folder]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[todo_folder](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](30) NOT NULL,
	[folder_sort_order_id] [int] NULL,
	[folder_sort_field_id] [int] NULL,
	[created_date] [datetime] NOT NULL,
 CONSTRAINT [PK_todo_folder] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_todo_folder] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[todo_item]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[todo_item](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[due_date] [datetime] NULL,
	[reminder] [datetime] NULL,
	[created_date] [datetime] NOT NULL,
	[item_recurrence_id] [int] NULL,
	[is_complete] [bit] NULL,
	[is_important] [bit] NULL,
	[todo_folder_id] [int] NULL,
 CONSTRAINT [PK_todo_item] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[todo_folder]  WITH CHECK ADD  CONSTRAINT [FK_todo_folder_folder_sort_field] FOREIGN KEY([folder_sort_field_id])
REFERENCES [dbo].[folder_sort_field] ([id])
GO
ALTER TABLE [dbo].[todo_folder] CHECK CONSTRAINT [FK_todo_folder_folder_sort_field]
GO
ALTER TABLE [dbo].[todo_folder]  WITH CHECK ADD  CONSTRAINT [FK_todo_folder_folder_sort_order] FOREIGN KEY([folder_sort_order_id])
REFERENCES [dbo].[folder_sort_order] ([id])
GO
ALTER TABLE [dbo].[todo_folder] CHECK CONSTRAINT [FK_todo_folder_folder_sort_order]
GO
ALTER TABLE [dbo].[todo_item]  WITH CHECK ADD  CONSTRAINT [FK_todo_item_item_recurrence] FOREIGN KEY([item_recurrence_id])
REFERENCES [dbo].[item_recurrence] ([id])
GO
ALTER TABLE [dbo].[todo_item] CHECK CONSTRAINT [FK_todo_item_item_recurrence]
GO
ALTER TABLE [dbo].[todo_item]  WITH CHECK ADD  CONSTRAINT [FK_todo_item_todo_folder] FOREIGN KEY([todo_folder_id])
REFERENCES [dbo].[todo_folder] ([id])
GO
ALTER TABLE [dbo].[todo_item] CHECK CONSTRAINT [FK_todo_item_todo_folder]
GO
/****** Object:  StoredProcedure [dbo].[folder_create]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[folder_create] @name varchar(30), @sortfield varchar(10), @sortorder varchar(4)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO todo_folder (name, folder_sort_field_id, folder_sort_order_id, created_date)
	VALUES (@name, (SELECT id FROM folder_sort_field WHERE name = @sortfield), (SELECT id FROM folder_sort_order WHERE name = @sortorder), GETDATE());
	DECLARE @newfolderid int = (SELECT SCOPE_IDENTITY())
	SELECT @newfolderid;
END
GO
/****** Object:  StoredProcedure [dbo].[folder_delete]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[folder_delete] @id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	-- SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM todo_folder
	WHERE todo_folder.id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[folder_get_all]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[folder_get_all]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT tdf.id, tdf.name folder_name, fsf.name sort_field, fso.name sort_order
	FROM todo_folder tdf
	INNER JOIN folder_sort_field fsf ON fsf.id = tdf.folder_sort_field_id
	INNER JOIN folder_sort_order fso ON fso.id = tdf.folder_sort_order_id
	ORDER BY tdf.id;
END
GO
/****** Object:  StoredProcedure [dbo].[folder_get_by_id]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[folder_get_by_id] @id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT tdf.id, tdf.name folder_name, fsf.name sort_field, fso.name sort_order
	FROM todo_folder tdf
	INNER JOIN folder_sort_field fsf ON fsf.id = tdf.folder_sort_field_id
	INNER JOIN folder_sort_order fso ON fso.id = tdf.folder_sort_order_id
	WHERE tdf.id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[folder_get_by_name]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[folder_get_by_name] @name varchar(30)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT tdf.id, tdf.name, fsf.name sort_field, fso.name sort_order
	FROM todo_folder tdf
	INNER JOIN folder_sort_field fsf ON fsf.id = tdf.folder_sort_field_id
	INNER JOIN folder_sort_order fso ON fso.id = tdf.folder_sort_order_id
	WHERE tdf.name = @name;
END
GO
/****** Object:  StoredProcedure [dbo].[folder_update]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[folder_update] @id int, @name varchar(30), @sortfield varchar(10), @sortorder varchar(4)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	-- SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE todo_folder
	SET name = @name,
	folder_sort_field_id = (SELECT id FROM folder_sort_field WHERE name = @sortfield),
	folder_sort_order_id = (SELECT id FROM folder_sort_order WHERE name = @sortorder)
	WHERE todo_folder.id = @id

END
GO
/****** Object:  StoredProcedure [dbo].[item_create]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[item_create] @name varchar(50), @itemrecurrence varchar(10), @foldername varchar(30), @duedate datetime, @reminder datetime, @iscomplete bit, @isimportant bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO todo_item (name, item_recurrence_id, todo_folder_id, created_date, due_date, reminder, is_complete, is_important)
	VALUES (@name, (SELECT id FROM item_recurrence WHERE name = @itemrecurrence), (SELECT id FROM todo_folder WHERE name = @foldername), GETDATE(), @duedate, @reminder, @iscomplete, @isimportant)
	DECLARE @newitemid int = (SELECT SCOPE_IDENTITY())
	SELECT @newitemid;
END
GO
/****** Object:  StoredProcedure [dbo].[item_delete]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[item_delete] @id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	-- SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM todo_item
	WHERE todo_item.id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[item_get_all]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[item_get_all]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT tdi.id, tdi.name item_name, tdi.created_date, tdi.due_date, tdi.is_complete, tdi.is_important, tdi.reminder, ir.name item_recurrence, tdf.name folder_name
	FROM todo_item tdi
	INNER JOIN item_recurrence ir ON ir.id = tdi.item_recurrence_id
	INNER JOIN todo_folder tdf ON tdf.id = tdi.todo_folder_id	
	ORDER BY tdi.id;
END
GO
/****** Object:  StoredProcedure [dbo].[item_get_by_folder_name]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[item_get_by_folder_name] @name varchar(30)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT tdi.id, tdi.name item_name, tdi.created_date, tdi.due_date, tdi.is_complete, tdi.is_important, tdi.reminder, ir.name item_recurrence, tdf.name folder_name
	FROM todo_item tdi
	INNER JOIN item_recurrence ir ON ir.id = tdi.item_recurrence_id
	INNER JOIN todo_folder tdf ON tdf.id = tdi.todo_folder_id	
	WHERE tdf.name = @name;
END
GO
/****** Object:  StoredProcedure [dbo].[item_get_by_id]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[item_get_by_id] @id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT tdi.id, tdi.name item_name, tdi.created_date, tdi.due_date, tdi.is_complete, tdi.is_important, tdi.reminder, ir.name item_recurrence, tdf.name folder_name
	FROM todo_item tdi
	INNER JOIN item_recurrence ir ON ir.id = tdi.item_recurrence_id
	INNER JOIN todo_folder tdf ON tdf.id = tdi.todo_folder_id	
	WHERE tdi.id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[item_get_by_item_name]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[item_get_by_item_name] @name varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT tdi.id, tdi.name item_name, tdi.created_date, tdi.due_date, tdi.is_complete, tdi.is_important, tdi.reminder, ir.name item_recurrence, tdf.name folder_name
	FROM todo_item tdi
	INNER JOIN item_recurrence ir ON ir.id = tdi.item_recurrence_id
	INNER JOIN todo_folder tdf ON tdf.id = tdi.todo_folder_id	
	WHERE tdi.name = @name;
END
GO
/****** Object:  StoredProcedure [dbo].[item_recurrence_get_all]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[item_recurrence_get_all]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT ir.id, ir.name item_recurrence
FROM item_recurrence ir
ORDER BY ir.id;
END
GO
/****** Object:  StoredProcedure [dbo].[item_recurrence_get_by_id]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[item_recurrence_get_by_id] @id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT ir.id, ir.name item_recurrence
FROM item_recurrence ir
WHERE ir.id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[item_recurrence_get_by_name]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[item_recurrence_get_by_name] @name varchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT ir.id, ir.name item_recurrence
FROM item_recurrence ir
WHERE ir.name = @name;
END
GO
/****** Object:  StoredProcedure [dbo].[item_update]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[item_update] @id int, @name varchar(50), @itemrecurrence varchar(10), @foldername varchar(30), @duedate datetime, @reminder datetime, @iscomplete bit, @isimportant bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	-- SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE todo_item
	SET name = @name,
	item_recurrence_id = (SELECT id FROM item_recurrence WHERE name = @itemrecurrence),
	todo_folder_id = (SELECT id FROM todo_folder WHERE name = @foldername),
	due_date = @duedate,
	reminder = @reminder,
	is_complete = @iscomplete,
	is_important = @isimportant
	WHERE todo_item.id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[sort_field_get_all]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sort_field_get_all] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT fsf.id, fsf.name sort_field
FROM folder_sort_field fsf
ORDER BY fsf.id;
END
GO
/****** Object:  StoredProcedure [dbo].[sort_field_get_by_id]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sort_field_get_by_id] @id int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT fsf.id, fsf.name sort_field
FROM folder_sort_field fsf
WHERE fsf.id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[sort_field_get_by_name]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sort_field_get_by_name] @name varchar(10) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT fsf.id, fsf.name sort_field
FROM folder_sort_field fsf
WHERE fsf.name = @name;
END
GO
/****** Object:  StoredProcedure [dbo].[sort_order_get_all]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sort_order_get_all] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT fso.id, fso.name sort_order
FROM folder_sort_order fso
ORDER BY fso.id;
END
GO
/****** Object:  StoredProcedure [dbo].[sort_order_get_by_id]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sort_order_get_by_id] @id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT fso.id, fso.name sort_order
FROM folder_sort_order fso
WHERE fso.id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[sort_order_get_by_name]    Script Date: 11/1/2020 12:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sort_order_get_by_name] @name varchar(4)
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT fso.id, fso.name sort_order
FROM folder_sort_order fso
WHERE fso.name = @name;
END
GO
USE [master]
GO
ALTER DATABASE [ToDoList] SET  READ_WRITE 
GO
