USE ToDoList;
GO

BEGIN TRANSACTION

-- ***** LOAD SAMPLE DATA INTO TABLES ***** -- 
INSERT INTO todo_folder (name, created_date, folder_sort_field_id, folder_sort_order_id) VALUES ('Bills and finance', GETDATE(), (SELECT id FROM folder_sort_field WHERE name = 'none'), (SELECT id FROM folder_sort_order WHERE name = 'none'));
INSERT INTO todo_folder (name, created_date, folder_sort_field_id, folder_sort_order_id) VALUES ('Home', GETDATE(), (SELECT id FROM folder_sort_field WHERE name = 'none'), (SELECT id FROM folder_sort_order WHERE name = 'none'));
INSERT INTO todo_folder (name, created_date, folder_sort_field_id, folder_sort_order_id) VALUES ('Personal', GETDATE(), (SELECT id FROM folder_sort_field WHERE name = 'none'), (SELECT id FROM folder_sort_order WHERE name = 'none'));
INSERT INTO todo_folder (name, created_date, folder_sort_field_id, folder_sort_order_id) VALUES ('Pets', GETDATE(), (SELECT id FROM folder_sort_field WHERE name = 'none'), (SELECT id FROM folder_sort_order WHERE name = 'none'));
INSERT INTO todo_folder (name, created_date, folder_sort_field_id, folder_sort_order_id) VALUES ('Self-care', GETDATE(), (SELECT id FROM folder_sort_field WHERE name = 'none'), (SELECT id FROM folder_sort_order WHERE name = 'none'));
INSERT INTO todo_folder (name, created_date, folder_sort_field_id, folder_sort_order_id) VALUES ('Tech Elevator - Pathway', GETDATE(), (SELECT id FROM folder_sort_field WHERE name = 'none'), (SELECT id FROM folder_sort_order WHERE name = 'none'));
INSERT INTO todo_folder (name, created_date, folder_sort_field_id, folder_sort_order_id) VALUES ('Tech Elevator - Technical', GETDATE(), (SELECT id FROM folder_sort_field WHERE name = 'none'), (SELECT id FROM folder_sort_order WHERE name = 'none'));

INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Commit yesterday''s homework to Bitbucket', GETDATE(), GETDATE() + 7, GETDATE() + 7, (SELECT id FROM item_recurrence WHERE name = 'none'), 0, 0, (SELECT id FROM todo_folder WHERE name='Tech Elevator - Technical'));
INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Today''s reading', GETDATE(), GETDATE() + 7, GETDATE() + 7, (SELECT id FROM item_recurrence WHERE name = 'none'), 0, 0, (SELECT id FROM todo_folder WHERE name='Tech Elevator - Technical'));
INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Write technical resume', GETDATE(), GETDATE() + 7, GETDATE() + 7, (SELECT id FROM item_recurrence WHERE name = 'none'), 0, 0, (SELECT id FROM todo_folder WHERE name='Tech Elevator - Pathway'));
INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Post resume doc and pdf to GDrive Pathway folder', GETDATE(), GETDATE() + 7, GETDATE() + 7, (SELECT id FROM item_recurrence WHERE name = 'none'), 0, 0, (SELECT id FROM todo_folder WHERE name='Tech Elevator - Pathway'));
INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Trim George''s nails', GETDATE(), GETDATE() + 7, GETDATE() + 7, (SELECT id FROM item_recurrence WHERE name = 'none'), 0, 0, (SELECT id FROM todo_folder WHERE name='Pets'));
INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Pick up George meds from vet', GETDATE(), GETDATE() + 7, GETDATE() + 7, (SELECT id FROM item_recurrence WHERE name = 'none'), 0, 0, (SELECT id FROM todo_folder WHERE name='Pets'));
INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Call Mom and Dad', GETDATE(), GETDATE() + 7, GETDATE() + 7, (SELECT id FROM item_recurrence WHERE name = 'none'), 0, 0, (SELECT id FROM todo_folder WHERE name='Personal'));
INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Pick up tux from cleaners', GETDATE(), GETDATE() + 7, GETDATE() + 7, (SELECT id FROM item_recurrence WHERE name = 'none'), 0, 0, (SELECT id FROM todo_folder WHERE name='Personal'));
INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Clean gutters', GETDATE(), GETDATE() + 7, GETDATE() + 7, (SELECT id FROM item_recurrence WHERE name = 'none'), 0, 0, (SELECT id FROM todo_folder WHERE name='Home'));
INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Weatherproof windows', GETDATE(), GETDATE() + 7, GETDATE() + 7, (SELECT id FROM item_recurrence WHERE name = 'none'), 0, 0, (SELECT id FROM todo_folder WHERE name='Home'));
INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Pay car insurance', GETDATE(), GETDATE() + 7, GETDATE() + 7, (SELECT id FROM item_recurrence WHERE name = 'none'), 0, 0, (SELECT id FROM todo_folder WHERE name='Bills and finance'));
INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Call investors back', GETDATE(), GETDATE() + 7, GETDATE() + 7, (SELECT id FROM item_recurrence WHERE name = 'none'), 0, 0, (SELECT id FROM todo_folder WHERE name='Bills and finance'));
INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Go for a hike', GETDATE(), GETDATE() + 7, GETDATE() + 7, (SELECT id FROM item_recurrence WHERE name = 'none'), 0, 0, (SELECT id FROM todo_folder WHERE name='Self-care'));
INSERT INTO todo_item (name, created_date, due_date, reminder, item_recurrence_id, is_complete, is_important, todo_folder_id) VALUES ('Meditate', GETDATE(), GETDATE() + 7, GETDATE() + 7, (SELECT id FROM item_recurrence WHERE name = 'none'), 0, 0, (SELECT id FROM todo_folder WHERE name='Self-care'));

COMMIT TRANSACTION