use master 
--drop database TriviaDB
--Go

create database TriviaDB
Go

use [TriviaDB]
Go

create table Ranking
(
[RankName] nvarchar (10) not null,
[RankId] int  not null primary key
);


create table Player
(
[Email] nvarchar(50) not null primary key,
[Name] nvarchar(10) not null,
[Rank] int not null foreign key references Ranking(RankId),
[Points] int not null,
[Password] nvarchar(8) not null
);

create table StatusQuestions
(
[StatusId] int not null primary key,
[StatusDescription] nvarchar (10) not null
);

create table SubjectQuestions
(
[SubjectId] int identity(1,1) not null primary key,
[SubjectName] nvarchar (10) not null
);


create table Questions
(
[Subject] nvarchar (30) not null, 
[Text] nvarchar (100) not null,
[QuestionId] int identity(1,1) not null primary key,
[Ranswer] nvarchar (50) not null,
[Wanswer1] nvarchar (50) not null,
[Wanswer2] nvarchar (50) not null,
[Wanswer3] nvarchar (50) not null,
[CreatedBy] nvarchar (50) not null foreign key references Player(Email),
[StatusId] int not null foreign key references StatusQuestions(StatusId),
[SubjectId] int not null foreign key references SubjectQuestions(SubjectId)
);
go

insert into Ranking (RankId, [RankName]) values(1, 'Manager');
insert into Ranking (RankId, [RankName]) values(2, 'Master');
insert into Ranking (RankId, [RankName]) values(3, 'Tiron');
select * FROM Ranking;
go

insert into StatusQuestions (StatusId, [StatusDescription]) values(1,'Approve' );
insert into StatusQuestions (StatusId, [StatusDescription]) values(2,'Waiting' );
insert into StatusQuestions (StatusId, [StatusDescription]) values(3,'Fail' );
select * FROM StatusQuestions;
go 

insert into SubjectQuestions ([SubjectName]) values('History' );
insert into SubjectQuestions ([SubjectName]) values('Sport' );
insert into SubjectQuestions ([SubjectName]) values('TV' );
insert into SubjectQuestions ([SubjectName]) values('Movies' );
insert into SubjectQuestions ([SubjectName]) values('Fashion' );
select * FROM SubjectQuestions;
go

insert into Player ([Email], [Name], [Rank],[Password], [Points]) values('noa.fisher.20072gmail.com', 'Noa', 3,'1234', 0);
select * FROM Player;
go










