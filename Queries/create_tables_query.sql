drop table Users;
drop table Courses;
drop table Students;
drop table Workers;
drop table Administrators;
drop table Lecturers;

create table Users (
	UserName varchar(50) primary key not null,
	FirstName varchar(50) not null,
	LastName varchar(50) not null,
	Address varchar(50)
)


create table Courses(
	Name varchar(50) primary key not null,
	LecturerName varchar(50) not null,
	Hour real not null,
	day varchar(10) not null,
	MoedADate Date not null,
	MoedBDate Date not null

)

create table Students (
	UserName varchar(50) references Users(UserName) primary key,
	CourseName varchar(50) references Courses(Name),
	MoedAGrade real,
	MoedBGrage real
)

create table Workers (
	UserName varchar(50) references Users(UserName) primary key,
	Salary real not null
)

create table Administrators (
	UserName varchar(50) references Workers(UserName) primary key
)

create table Lecturers (
	UserName varchar(50) references Workers(UserName) primary key,
	CourseName varchar(50) references Courses(Name)
)