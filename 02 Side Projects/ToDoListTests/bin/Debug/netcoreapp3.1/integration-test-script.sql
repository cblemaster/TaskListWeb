-- delete data from all tables
DELETE FROM todo_item;
DELETE FROM todo_folder;
DELETE FROM folder_sort_order;
DELETE FROM folder_sort_field;
DELETE FROM item_recurrence;

-- Insert application-required data
INSERT INTO folder_sort_field (name) VALUES ('due_date');
DECLARE @newSortFieldId int = (SELECT SCOPE_IDENTITY());
DECLARE @newSortFieldName varchar(10) = (SELECT name FROM folder_sort_field WHERE id = @newSortFieldId);
--INSERT INTO folder_sort_field (name) VALUES ('name');
--INSERT INTO folder_sort_field (name) VALUES ('none');

INSERT INTO folder_sort_order (name) VALUES ('asc');
DECLARE @newSortOrderId int = (SELECT SCOPE_IDENTITY());
DECLARE @newSortOrderName varchar(4) = (SELECT name FROM folder_sort_order WHERE id = @newSortOrderId);
--INSERT INTO folder_sort_order (name) VALUES ('desc');
--INSERT INTO folder_sort_order (name) VALUES ('none');

INSERT INTO item_recurrence (name) VALUES ('daily');
DECLARE @newItemRecurrenceId int = (SELECT SCOPE_IDENTITY());
DECLARE @newItemRecurrenceName varchar(10) = (SELECT name FROM item_recurrence WHERE id = @newItemRecurrenceId);
--INSERT INTO item_recurrence (name) VALUES ('weekdays');
--INSERT INTO item_recurrence (name) VALUES ('weekly');
--INSERT INTO item_recurrence (name) VALUES ('monthly');
--INSERT INTO item_recurrence (name) VALUES ('annually');
--INSERT INTO item_recurrence (name) VALUES ('none');

-- Insert some fake test data for todo folders and todo items
INSERT INTO todo_folder (name, created_date, folder_sort_field_id, folder_sort_order_id) VALUES ('TEST', GETDATE(), @newSortFieldId, @newSortOrderId);
DECLARE @newToDoFolderId int = (SELECT SCOPE_IDENTITY());
DECLARE @newToDoFolderName varchar(30) = (SELECT name FROM todo_folder WHERE id = @newToDoFolderId);

--INSERT INTO todo_folder (name, created_date, folder_sort_field_id, folder_sort_order_id) VALUES ('Bills and finance', GETDATE(), @newSortFieldId, @newSortOrderId);
--INSERT INTO todo_folder (name, created_date, folder_sort_field_id, folder_sort_order_id) VALUES ('Home', GETDATE(), @newSortFieldId, @newSortOrderId);
--INSERT INTO todo_folder (name, created_date, folder_sort_field_id, folder_sort_order_id) VALUES ('Personal', GETDATE(), @newSortFieldId, @newSortOrderId);
--INSERT INTO todo_folder (name, created_date, folder_sort_field_id, folder_sort_order_id) VALUES ('Pets', GETDATE(), @newSortFieldId, @newSortOrderId);
--INSERT INTO todo_folder (name, created_date, folder_sort_field_id, folder_sort_order_id) VALUES ('Self-care', GETDATE(), @newSortFieldId, @newSortOrderId);
--INSERT INTO todo_folder (name, created_date, folder_sort_field_id, folder_sort_order_id) VALUES ('Tech Elevator - Pathway', GETDATE(), @newSortFieldId, @newSortOrderId);
--INSERT INTO todo_folder (name, created_date, folder_sort_field_id, folder_sort_order_id) VALUES ('Tech Elevator - Technical', GETDATE(), @newSortFieldId, @newSortOrderId);
--INSERT INTO todo_folder (name, created_date, folder_sort_field_id, folder_sort_order_id) VALUES ('Tasks', GETDATE(), @newSortFieldId, @newSortOrderId);

INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Commit yesterday''s homework to Bitbucket', GETDATE(), GETDATE() + 7, GETDATE() + 7, @newItemRecurrenceId, 0, 0, @newToDoFolderId);
DECLARE @newToDoItemId int = (SELECT SCOPE_IDENTITY());
DECLARE @newToDoItemName varchar(50) = (SELECT name FROM todo_item WHERE id = @newToDoItemId);
--INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Today''s reading', GETDATE(), GETDATE() + 7, GETDATE() + 7, @newItemRecurrenceId, 0, 0, @newToDoFolderId);
--INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Write technical resume', GETDATE(), GETDATE() + 7, GETDATE() + 7, @newItemRecurrenceId, 0, 0, @newToDoFolderId);
--INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Post resume doc and pdf to GDrive Pathway folder', GETDATE(), GETDATE() + 7, GETDATE() + 7, @newItemRecurrenceId, 0, 0, (SELECT id FROM todo_folder WHERE name='Tech Elevator - Pathway'));
--INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Trim George''s nails', GETDATE(), GETDATE() + 7, GETDATE() + 7, @newItemRecurrenceId, 0, 0, @newToDoFolderId);
--INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Pick up George meds from vet', GETDATE(), GETDATE() + 7, GETDATE() + 7, @newItemRecurrenceId, 0, 0, @newToDoFolderId);
--INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Call Mom and Dad', GETDATE(), GETDATE() + 7, GETDATE() + 7, @newItemRecurrenceId, 0, 0, @newToDoFolderId);
--INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Pick up tux from cleaners', GETDATE(), GETDATE() + 7, GETDATE() + 7, @newItemRecurrenceId, 0, 0, @newToDoFolderId);
--INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Clean gutters', GETDATE(), GETDATE() + 7, GETDATE() + 7, @newItemRecurrenceId, 0, 0, @newToDoFolderId);
--INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Weatherproof windows', GETDATE(), GETDATE() + 7, GETDATE() + 7, @newItemRecurrenceId, 0, 0, @newToDoFolderId);
--INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Pay car insurance', GETDATE(), GETDATE() + 7, GETDATE() + 7, @newItemRecurrenceId, 0, 0, @newToDoFolderId);
--INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Call investors back', GETDATE(), GETDATE() + 7, GETDATE() + 7, @newItemRecurrenceId, 0, 0, @newToDoFolderId);
--INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Go for a hike', GETDATE(), GETDATE() + 7, GETDATE() + 7, @newItemRecurrenceId, 0, 0, @newToDoFolderId);
--INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Meditate', GETDATE(), GETDATE() + 7, GETDATE() + 7, @newItemRecurrenceId, 0, 0, @newToDoFolderId);

-- return the ids for the inserted data
SELECT @newSortFieldId AS newSortFieldId, @newSortOrderId AS newSortOrderId, @newItemRecurrenceId AS newItemRecurrenceId, @newToDoFolderId AS newToDoFolderId, @newToDoItemId AS newToDoItemId, @newToDoItemName AS newToDoItemName, @newSortFieldName AS newSortFieldName, @newSortOrderName AS newSortOrderName, @newItemRecurrenceName AS newItemRecurrence, @newToDoFolderName AS newFolderName;