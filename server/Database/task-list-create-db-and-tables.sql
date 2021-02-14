USE master
GO

-- drop database if it exists
IF DB_ID('task_list') IS NOT NULL
BEGIN
	ALTER DATABASE task_list SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE task_list;
END

-- create database
CREATE DATABASE task_list
GO

USE task_list
GO

BEGIN TRANSACTION;

-- create tables
CREATE TABLE folder (
	folder_id		int IDENTITY(1,1)	NOT NULL,
	folder_name		varchar(50)			NOT NULL		UNIQUE,
	created_date	datetime			NOT NULL,
	CONSTRAINT PK_folder PRIMARY KEY (folder_id)
)

CREATE TABLE recurrence (
	recurrence_id	int IDENTITY(1,1)	NOT NULL,
	recurrence_name varchar(10)			NOT NULL		UNIQUE,
	CONSTRAINT PK_recurrence PRIMARY KEY (recurrence_id)
)

CREATE TABLE task (
	task_id			int IDENTITY(1,1)	NOT NULL,
	task_name		varchar(255)		NOT NULL,
	due_date		datetime			NULL,
	reminder		datetime			NULL,
	created_date	datetime			NOT NULL,
	recurrence_id	int					NOT NULL,
	is_complete		bit					NOT NULL,
	is_important	bit					NOT NULL,
	folder_id		int					NOT NULL,
	CONSTRAINT PK_task PRIMARY KEY (task_id) 
)

ALTER TABLE task WITH CHECK ADD CONSTRAINT FK_task_folder FOREIGN KEY(folder_id)
REFERENCES folder (folder_id)

ALTER TABLE task CHECK CONSTRAINT FK_task_folder

ALTER TABLE task WITH CHECK ADD CONSTRAINT FK_task_recurrence FOREIGN KEY(recurrence_id)
REFERENCES recurrence (recurrence_id)

ALTER TABLE task CHECK CONSTRAINT FK_task_recurrence

-- Load app required folders
INSERT INTO folder (folder_name, created_date) VALUES ('Tasks', GETDATE());
INSERT INTO folder (folder_name, created_date) VALUES ('Recurring', GETDATE());
INSERT INTO folder (folder_name, created_date) VALUES ('Important', GETDATE());

-- Load app required recurrences
INSERT INTO recurrence (recurrence_name) VALUES ('none');
INSERT INTO recurrence (recurrence_name) VALUES ('daily');
INSERT INTO recurrence (recurrence_name) VALUES ('weekdays');
INSERT INTO recurrence (recurrence_name) VALUES ('weekly');
INSERT INTO recurrence (recurrence_name) VALUES ('monthly');
INSERT INTO recurrence (recurrence_name) VALUES ('annually');

COMMIT TRANSACTION;