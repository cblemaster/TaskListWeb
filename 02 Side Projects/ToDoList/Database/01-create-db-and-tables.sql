-- Switch to  the system (aka master) database
USE master;
GO

-- Delete the ToDoList database, if it exists
IF EXISTS(SELECT * FROM sys.databases WHERE name = 'ToDoList')
DROP DATABASE ToDoList;
GO

-- Create a new ToDoList database
CREATE DATABASE ToDoList;
GO

-- Switch to ToDoList database
USE ToDoList;
GO

-- Begin a transaction that must complete with no errors
BEGIN TRANSACTION

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

ALTER TABLE [dbo].[todo_folder]  WITH CHECK ADD  CONSTRAINT [FK_todo_folder_folder_sort_field] FOREIGN KEY([folder_sort_field_id])
REFERENCES [dbo].[folder_sort_field] ([id])

ALTER TABLE [dbo].[todo_folder] CHECK CONSTRAINT [FK_todo_folder_folder_sort_field]

ALTER TABLE [dbo].[todo_folder]  WITH CHECK ADD  CONSTRAINT [FK_todo_folder_folder_sort_order] FOREIGN KEY([folder_sort_order_id])
REFERENCES [dbo].[folder_sort_order] ([id])

ALTER TABLE [dbo].[todo_folder] CHECK CONSTRAINT [FK_todo_folder_folder_sort_order]

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

ALTER TABLE [dbo].[todo_item]  WITH CHECK ADD  CONSTRAINT [FK_todo_item_item_recurrence] FOREIGN KEY([item_recurrence_id])
REFERENCES [dbo].[item_recurrence] ([id])

ALTER TABLE [dbo].[todo_item] CHECK CONSTRAINT [FK_todo_item_item_recurrence]

ALTER TABLE [dbo].[todo_item]  WITH CHECK ADD  CONSTRAINT [FK_todo_item_todo_folder] FOREIGN KEY([todo_folder_id])
REFERENCES [dbo].[todo_folder] ([id])

ALTER TABLE [dbo].[todo_item] CHECK CONSTRAINT [FK_todo_item_todo_folder]

COMMIT TRANSACTION;