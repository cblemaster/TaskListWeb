USE [ToDoList]
GO
/****** Object:  StoredProcedure [dbo].[folder_create]    Script Date: 11/3/2020 12:20:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[folder_create] @name varchar(30), @sortfield varchar(10), @sortorder varchar(4), @createddate datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO todo_folder (name, folder_sort_field_id, folder_sort_order_id, created_date)
	VALUES (@name, (SELECT id FROM folder_sort_field WHERE name = @sortfield), (SELECT id FROM folder_sort_order WHERE name = @sortorder), @createddate);
	
	SELECT SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[folder_delete]    Script Date: 11/3/2020 12:20:17 PM ******/
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
/****** Object:  StoredProcedure [dbo].[folder_get_all]    Script Date: 11/3/2020 12:20:17 PM ******/
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
	SELECT tdf.id, tdf.name folder_name, fsf.name sort_field, fso.name sort_order, tdf.created_date
	FROM todo_folder tdf
	INNER JOIN folder_sort_field fsf ON fsf.id = tdf.folder_sort_field_id
	INNER JOIN folder_sort_order fso ON fso.id = tdf.folder_sort_order_id
	ORDER BY tdf.id;
END
GO
/****** Object:  StoredProcedure [dbo].[folder_get_by_id]    Script Date: 11/3/2020 12:20:17 PM ******/
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
	SELECT tdf.id, tdf.name folder_name, fsf.name sort_field, fso.name sort_order, tdf.created_date
	FROM todo_folder tdf
	INNER JOIN folder_sort_field fsf ON fsf.id = tdf.folder_sort_field_id
	INNER JOIN folder_sort_order fso ON fso.id = tdf.folder_sort_order_id
	WHERE tdf.id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[folder_get_by_name]    Script Date: 11/3/2020 12:20:17 PM ******/
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
	SELECT tdf.id, tdf.name folder_name, fsf.name sort_field, fso.name sort_order, tdf.created_date
	FROM todo_folder tdf
	INNER JOIN folder_sort_field fsf ON fsf.id = tdf.folder_sort_field_id
	INNER JOIN folder_sort_order fso ON fso.id = tdf.folder_sort_order_id
	WHERE tdf.name = @name;
END
GO
/****** Object:  StoredProcedure [dbo].[folder_update]    Script Date: 11/3/2020 12:20:17 PM ******/
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
/****** Object:  StoredProcedure [dbo].[item_create]    Script Date: 11/3/2020 12:20:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[item_create] @name varchar(50), @itemrecurrence varchar(10), @foldername varchar(30), @duedate datetime, @reminder datetime, @iscomplete bit, @isimportant bit, @createddate datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO todo_item (name, item_recurrence_id, todo_folder_id, created_date, due_date, reminder, is_complete, is_important)
	VALUES (@name, (SELECT id FROM item_recurrence WHERE name = @itemrecurrence), (SELECT id FROM todo_folder WHERE name = @foldername), @createddate, @duedate, @reminder, @iscomplete, @isimportant)
	
	SELECT SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[item_delete]    Script Date: 11/3/2020 12:20:17 PM ******/
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
/****** Object:  StoredProcedure [dbo].[item_get_all]    Script Date: 11/3/2020 12:20:17 PM ******/
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
/****** Object:  StoredProcedure [dbo].[item_get_by_folder_name]    Script Date: 11/3/2020 12:20:17 PM ******/
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
/****** Object:  StoredProcedure [dbo].[item_get_by_id]    Script Date: 11/3/2020 12:20:17 PM ******/
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
/****** Object:  StoredProcedure [dbo].[item_get_by_item_name]    Script Date: 11/3/2020 12:20:17 PM ******/
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
/****** Object:  StoredProcedure [dbo].[item_recurrence_get_all]    Script Date: 11/3/2020 12:20:17 PM ******/
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
/****** Object:  StoredProcedure [dbo].[item_recurrence_get_by_id]    Script Date: 11/3/2020 12:20:17 PM ******/
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
/****** Object:  StoredProcedure [dbo].[item_recurrence_get_by_name]    Script Date: 11/3/2020 12:20:17 PM ******/
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
/****** Object:  StoredProcedure [dbo].[item_update]    Script Date: 11/3/2020 12:20:17 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sort_field_get_all]    Script Date: 11/3/2020 12:20:17 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sort_field_get_by_id]    Script Date: 11/3/2020 12:20:17 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sort_field_get_by_name]    Script Date: 11/3/2020 12:20:17 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sort_order_get_all]    Script Date: 11/3/2020 12:20:17 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sort_order_get_by_id]    Script Date: 11/3/2020 12:20:17 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sort_order_get_by_name]    Script Date: 11/3/2020 12:20:17 PM ******/
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