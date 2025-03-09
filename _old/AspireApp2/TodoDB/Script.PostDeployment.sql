-- This file contains SQL statements that will be executed after the build script.

-- Create application user 
if (serverproperty('Edition') = 'SQL Azure') begin

    if not exists (select * from sys.database_principals where [type] in ('E', 'S') and [name] = 'todo_dab_user')
    begin 
        create user [todo_dab_user] with password = 'rANd0m_PAzzw0rd!'
        alter role db_owner add member [todo_dab_user]
    end

end

-- Insert sample data
delete from dbo.todos where id in
(
    '00000000-0000-0000-0000-000000000001',
    '00000000-0000-0000-0000-000000000002',
    '00000000-0000-0000-0000-000000000003',
    '00000000-0000-0000-0000-000000000004',
    '00000000-0000-0000-0000-000000000005'
);
insert into dbo.todos 
(
    [id],
    [title],
	[completed],
	[owner_id],
	[position]
) 
values
    ('00000000-0000-0000-0000-000000000001', N'Hello world', 0, 'public', 1),
    ('00000000-0000-0000-0000-000000000002', N'This is done', 1, 'public', 2),
    ('00000000-0000-0000-0000-000000000003', N'And this is not done (yet!)', 0, 'public', 4),
    ('00000000-0000-0000-0000-000000000004', N'This is a ☆☆☆☆☆ tool!', 0, 'public', 3),
    ('00000000-0000-0000-0000-000000000005', N'Add support for sorting', 1, 'public', 5)
;
