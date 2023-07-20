USE MarketplaceAppDB
GO

CREATE TABLE Quest(
	questId INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,
	userId INT NOT NULL,
	publishingDate DATE DEFAULT (getdate()),
	endDate DATE DEFAULT (DATEADD (wk, 1, getdate())),
	isAvailable BIT NOT NULL DEFAULT 1,
	title NVARCHAR(MAX) NOT NULL,
	[description] NVARCHAR(400) NULL,
	isSolved BIT NOT NULL DEFAULT 0
)
GO

CREATE TABLE [User](
	userId INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,
	userName NVARCHAR (20) NOT NULL UNIQUE,
	[name] NVARCHAR (100) NOT NULL,
	surname NVARCHAR (100) NOT NULL,
	email NVARCHAR (100) NOT NULL UNIQUE,
	registrationDate DATE DEFAULT (getdate()),
	category NVARCHAR(20) NOT NULL,
	[login] NVARCHAR(30) NOT NULL DEFAULT ' ' UNIQUE,
	[password] NVARCHAR (30) NOT NULL DEFAULT ' '
)
GO

CREATE TABLE Solution(
	solutionId INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,
	userId INT NOT NULL,
	questId INT NOT NULL,
	[description] NVARCHAR (400),
	sourceLink NVARCHAR (MAX) NOT NULL,
	rating INT,
	isSelected BIT NOT NULL DEFAULT 0,
)
GO

CREATE TABLE Comment(
	commentId INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,
	solutionId INT NOT NULL,
	userId INT NOT NULL,
	[text] NVARCHAR (400)
)
GO

ALTER TABLE Comment
WITH CHECK ADD CONSTRAINT FK_Comment_Solution FOREIGN KEY (solutionId)
REFERENCES Solution (solutionId)
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE Comment
WITH CHECK ADD CONSTRAINT FK_Comment_User FOREIGN KEY (userId)
REFERENCES [User] (userId)
--ON UPDATE CASCADE
--ON DELETE CASCADE
GO

ALTER TABLE Quest
WITH CHECK ADD CONSTRAINT FK_Quest_User FOREIGN KEY (userId)
REFERENCES [User] (userId)
--ON UPDATE CASCADE
--ON DELETE CASCADE 
GO

ALTER TABLE Solution
WITH CHECK ADD CONSTRAINT FK_Solution_Quest FOREIGN KEY (questId)
REFERENCES Quest (questId)
ON UPDATE CASCADE
ON DELETE CASCADE 
GO

ALTER TABLE Solution
WITH CHECK ADD CONSTRAINT FK_Solution_User FOREIGN KEY (userId)
REFERENCES [User] (userId)
ON UPDATE CASCADE
ON DELETE CASCADE 
GO

INSERT INTO [User] (userName, name, surname, email, category, [login], [password])
VALUES
('User1', 'Name1', 'Surname1', 'Email1@gmail.com', 'Admin', 'Login1', 'Password1'),
('User2', 'Name2', 'Surname2', 'Email2@mail.ru', 'Admin', 'Login2', 'Password2'),
('User3', 'Name3', 'Surname3', 'Email3@yandex.ru', 'User', 'Login3', 'Password3'),
('User4', 'Name4', 'Surname4', 'Email4@inbox.ru', 'User', 'Login4', 'Password4')
GO

--SELECT * FROM [User]

INSERT INTO Quest (userId, title, [description]) VALUES
('1', 'Task1', 'The first task.'),
('2', 'Task2', 'The second task')
GO

INSERT INTO Quest (userId, title, [description]) VALUES
('1', 'Task3', 'The third task')
GO

--SELECT * FROM Quest

INSERT INTO Solution (userId, questId, description, sourceLink)
VALUES
('3', '1', 'The third users solution of the first task', 'https://www.youtube.com/watch?v=SryQxUeChMc&list=PLdo4fOcmZ0oX7uTkjYwvCJDG2qhcSzwZ6'),
('4', '1', 'The fourth users solution of the first task', 'https://www.youtube.com/watch?v=DCYVfLT5_QI&list=PLdo4fOcmZ0oX7uTkjYwvCJDG2qhcSzwZ6&index=2&pp=iAQB'),
('3', '2', 'The thirds users solution of the second task', 'https://www.youtube.com/watch?v=c-wN-fc594c&list=PLdo4fOcmZ0oX7uTkjYwvCJDG2qhcSzwZ6&index=3&pp=iAQB')
GO

--SELECT * FROM Solution


