
CREATE DATABASE TriviaGame;

USE TriviaGame;


CREATE TABLE [Ranks]
(
RankId int Identity(1,1) not null PRIMARY KEY,
RankName nvarchar(150) not null
);

CREATE TABLE [Players]
( 
PlayerId int Identity(1,1) not null PRIMARY KEY,
Mail nvarchar(30) not null,
Name nvarchar(20) not null,
RankId int not null,
CONSTRAINT FK_RankId FOREIGN KEY (RankId) REFERENCES [Ranks](RankId),
Points int not null
);



CREATE TABLE [Subject]
(
SubjectId int Identity(1,1) not null PRIMARY KEY,
SubjectName nvarchar(100) not null
);


CREATE TABLE [Status]
(
StatusId int Identity(1,1) not null PRIMARY KEY,
[Status] nvarchar(200) not null
);



CREATE TABLE [Questions]
(
QuestionId  int Identity(1,1) not null PRIMARY KEY,
StatusId int not null,
CONSTRAINT FK_StatusId FOREIGN KEY (StatusId) REFERENCES [Status](StatusId),
SubjectId int not null,
CONSTRAINT FK_SubjectId FOREIGN KEY (SubjectId) REFERENCES [Subject](SubjectId),
PlayerId int not null,
CONSTRAINT FK_PlayerId FOREIGN KEY (PlayerId) REFERENCES [Players](PlayerId),
Question nvarchar(1000) not null,
RightA nvarchar(300) not null,
WrongA1 nvarchar(300) not null,
WrongA2 nvarchar(300) not null,
WrongA3 nvarchar(300) not null
);

