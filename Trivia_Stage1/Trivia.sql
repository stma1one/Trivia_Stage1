CREATE DATABASE Trivia;
USE Trivia;
CREATE TABLE Ranks(
	rankid int IDENTITY (1, 1) PRIMARY KEY,
	rankName nvarchar(255)
	)
CREATE TABLE Users(
	id int IDENTITY (1, 1) PRIMARY KEY,
	email nvarchar(255) NOT NULL,
	pswrd nvarchar(50) NOT NULL,
	username nvarchar(50) NOT NULL,
	points int,
	questionsadded int,
	rankid int,
	CONSTRAINT FK_Rank FOREIGN KEY (rankid)
	REFERENCES Ranks(rankid)
	)
CREATE TABLE Statuses(
	id int IDENTITY (1, 1) PRIMARY KEY,
	currentStatus nvarchar(255)
	)
CREATE TABLE Subjects(
	id int IDENTITY (1, 1) PRIMARY KEY,
	subjectName nvarchar(255)
	)
CREATE TABLE Questions(
	id int IDENTITY (1, 1) PRIMARY KEY,
	text nvarchar(255),
	rightAnswer nvarchar(255),
	wrongAnswer1 nvarchar(255),
	wrongAnswer2 nvarchar(255),
	wrongAnswer3 nvarchar(255),
	userId int,
	statusId int,
	subjectId int,
	CONSTRAINT FK_UserToQuestion FOREIGN KEY (userId)
	REFERENCES Users(id),
	CONSTRAINT FK_StatusToQuestion FOREIGN KEY (statusId)
	REFERENCES Statuses(id),
	CONSTRAINT FK_SubjectToQuestion FOREIGN KEY (subjectId)
	REFERENCES Subjects(id)
	)
INSERT INTO Users (email, pswrd, username, points, questionsadded)
VALUES ('talkazyo@gmail.com', '12345678', 'ro', 0, 5)
INSERT INTO Ranks(rankName)
VALUES ('Admin')
INSERT INTO Ranks(rankName)
VALUES ('Master')
INSERT INTO Ranks(rankName)
VALUES ('Rookie')
UPDATE Users SET [rankid] = 1
WHERE id=1
INSERT INTO Subjects (subjectName)
VALUES ('Sports')
INSERT INTO Subjects (subjectName)
VALUES ('Politics')
INSERT INTO Subjects (subjectName)
VALUES ('History')
INSERT INTO Subjects (subjectName)
VALUES ('Science')
INSERT INTO Subjects (subjectName)
VALUES ('Ramon School')
INSERT INTO Questions (text, rightAnswer, wrongAnswer1, wrongAnswer2, wrongAnswer3, userId, subjectId)
VALUES ('How many classes are there in 11th grade in Ramon High School ?', '6', '5', '7', '8', 1, 5)
INSERT INTO Questions (text, rightAnswer, wrongAnswer1, wrongAnswer2, wrongAnswer3, userId, subjectId)
VALUES ('How many jews did Hitler kill during the holocaust ?', '6 million', '5 million', '7 million', '8 million', 1, 3)
INSERT INTO Questions (text, rightAnswer, wrongAnswer1, wrongAnswer2, wrongAnswer3, userId, subjectId)
VALUES ('Who is the current president of the USA ?', 'Joe Biden', 'Donald J Trump', 'Barak Obama', 'George Washington', 1, 2)
INSERT INTO Questions (text, rightAnswer, wrongAnswer1, wrongAnswer2, wrongAnswer3, userId, subjectId)
VALUES ('Who holds the current world record for the fastest 100 meters run ?', 'Usain Bolt', 'Tyson Gay', 'Yohan Blake', 'Asafa Powell', 1, 1)
INSERT INTO Questions (text, rightAnswer, wrongAnswer1, wrongAnswer2, wrongAnswer3, userId, subjectId)
VALUES ('How many elements are in the periodic table ?', '118', '120', '125', '112', 1, 4)
INSERT INTO Statuses (currentStatus)
VALUES ('Approved')
INSERT INTO Statuses (currentStatus)
VALUES ('Pending')
INSERT INTO Statuses (currentStatus)
VALUES ('Denied')
UPDATE Questions SET [statusId] = 1
WHERE userId = 1

