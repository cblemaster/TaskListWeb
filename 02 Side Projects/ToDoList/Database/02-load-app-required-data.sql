USE ToDoList;
GO

BEGIN TRANSACTION

-- ***** LOAD APPLICATION DATA INTO TABLES ***** -- 
INSERT INTO folder_sort_field (name) VALUES ('due_date');
INSERT INTO folder_sort_field (name) VALUES ('name');
INSERT INTO folder_sort_field (name) VALUES ('none');

INSERT INTO folder_sort_order (name) VALUES ('asc');
INSERT INTO folder_sort_order (name) VALUES ('desc');
INSERT INTO folder_sort_order (name) VALUES ('none');

INSERT INTO item_recurrence (name) VALUES ('daily');
INSERT INTO item_recurrence (name) VALUES ('weekdays');
INSERT INTO item_recurrence (name) VALUES ('weekly');
INSERT INTO item_recurrence (name) VALUES ('monthly');
INSERT INTO item_recurrence (name) VALUES ('annually');
INSERT INTO item_recurrence (name) VALUES ('none');

INSERT INTO todo_folder (name, created_date, folder_sort_field_id, folder_sort_order_id) VALUES ('Tasks', GETDATE(), (SELECT id FROM folder_sort_field WHERE name = 'none'), (SELECT id FROM folder_sort_order WHERE name = 'none'));

COMMIT TRANSACTION