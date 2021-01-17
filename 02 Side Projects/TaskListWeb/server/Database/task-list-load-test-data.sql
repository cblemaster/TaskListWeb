USE task_list
GO

BEGIN TRANSACTION;

-- ***** LOAD SAMPLE DATA INTO TABLES ***** -- 
INSERT INTO folder (folder_name, created_date) VALUES ('Bills and finance', GETDATE());
INSERT INTO folder (folder_name, created_date) VALUES ('Home', GETDATE());
INSERT INTO folder (folder_name, created_date) VALUES ('Personal', GETDATE());
INSERT INTO folder (folder_name, created_date) VALUES ('Pets', GETDATE());
INSERT INTO folder (folder_name, created_date) VALUES ('Self-care', GETDATE());
INSERT INTO folder (folder_name, created_date) VALUES ('Tech Elevator - Pathway', GETDATE());
INSERT INTO folder (folder_name, created_date) VALUES ('Tech Elevator - Technical', GETDATE());

INSERT INTO task (task_name, created_date, due_date, reminder, recurrence_id, is_complete, is_important, folder_id) VALUES ('Commit yesterday''s homework to Bitbucket', GETDATE(), GETDATE() + 7, GETDATE() + 7, 1, 1, 1, (SELECT folder_id FROM folder WHERE folder_name='Tech Elevator - Technical'));
INSERT INTO task (task_name, created_date, due_date, reminder, recurrence_id, is_complete, is_important, folder_id) VALUES ('Today''s reading', GETDATE(), GETDATE() + 7, GETDATE() + 7, 1, 0, 0, (SELECT folder_id FROM folder WHERE folder_name='Tech Elevator - Technical'));
INSERT INTO task (task_name, created_date, due_date, reminder, recurrence_id, is_complete, is_important, folder_id) VALUES ('Write technical resume', GETDATE(), GETDATE() + 7, GETDATE() + 7, 2, 1, 0, (SELECT folder_id FROM folder WHERE folder_name='Tech Elevator - Pathway'));
INSERT INTO task (task_name, created_date, due_date, reminder, recurrence_id, is_complete, is_important, folder_id) VALUES ('Post resume doc and pdf to GDrive Pathway folder', GETDATE(), GETDATE() + 7, GETDATE() + 7, 3, 0, 0, (SELECT folder_id FROM folder WHERE folder_name='Tech Elevator - Pathway'));
INSERT INTO task (task_name, created_date, due_date, reminder, recurrence_id, is_complete, is_important, folder_id) VALUES ('Trim George''s nails', GETDATE(), GETDATE() + 7, GETDATE() + 7, 4, 1, 0, (SELECT folder_id FROM folder WHERE folder_name='Pets'));
INSERT INTO task (task_name, created_date, due_date, reminder, recurrence_id, is_complete, is_important, folder_id) VALUES ('Pick up George meds from vet', GETDATE(), GETDATE() + 7, GETDATE() + 7, 5, 0, 1, (SELECT folder_id FROM folder WHERE folder_name='Pets'));
INSERT INTO task (task_name, created_date, due_date, reminder, recurrence_id, is_complete, is_important, folder_id) VALUES ('Call Mom and Dad', GETDATE(), GETDATE() + 7, GETDATE() + 7, 6, 1, 0, (SELECT folder_id FROM folder WHERE folder_name='Personal'));
INSERT INTO task (task_name, created_date, due_date, reminder, recurrence_id, is_complete, is_important, folder_id) VALUES ('Pick up tux from cleaners', GETDATE(), GETDATE() + 7, GETDATE() + 7, 1, 0, 0, (SELECT folder_id FROM folder WHERE folder_name='Personal'));
INSERT INTO task (task_name, created_date, due_date, reminder, recurrence_id, is_complete, is_important, folder_id) VALUES ('Clean gutters', GETDATE(), GETDATE() + 7, GETDATE() + 7, 2, 1, 0, (SELECT folder_id FROM folder WHERE folder_name='Home'));
INSERT INTO task (task_name, created_date, due_date, reminder, recurrence_id, is_complete, is_important, folder_id) VALUES ('Weatherproof windows', GETDATE(), GETDATE() + 7, GETDATE() + 7, 3, 0, 1, (SELECT folder_id FROM folder WHERE folder_name='Home'));
INSERT INTO task (task_name, created_date, due_date, reminder, recurrence_id, is_complete, is_important, folder_id) VALUES ('Pay car insurance', GETDATE(), GETDATE() + 7, GETDATE() + 7, 4, 1, 0, (SELECT folder_id FROM folder WHERE folder_name='Bills and finance'));
INSERT INTO task (task_name, created_date, due_date, reminder, recurrence_id, is_complete, is_important, folder_id) VALUES ('Call investors back', GETDATE(), GETDATE() + 7, GETDATE() + 7, 5, 0, 0, (SELECT folder_id FROM folder WHERE folder_name='Bills and finance'));
INSERT INTO task (task_name, created_date, due_date, reminder, recurrence_id, is_complete, is_important, folder_id) VALUES ('Go for a hike', GETDATE(), GETDATE() + 7, GETDATE() + 7, 6, 1, 1, (SELECT folder_id FROM folder WHERE folder_name='Self-care'));
INSERT INTO task (task_name, created_date, due_date, reminder, recurrence_id, is_complete, is_important, folder_id) VALUES ('Meditate', GETDATE(), GETDATE() + 7, GETDATE() + 7, 1, 0, 0, (SELECT folder_id FROM folder WHERE folder_name='Self-care'));

COMMIT TRANSACTION;