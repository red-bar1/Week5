--create database Esami

create table Studente(
	ID int identity(1,1),
	Nome varchar(50) not null,
	Cognome varchar (50) not null,
	AnnoNascita int not null,
	primary key (ID),
	check (AnnoNascita < 2021)
)

create table Esame(
	ID int identity(1,1),
	Nome varchar(50) not null,
	CFU int not null,
	DataEsame date not null,
	Votazione varchar(20),
	Esito varchar(20) not null,
	StudenteID int not null,
	foreign key (StudenteID) references Studente(ID),
	primary key (ID),
)

insert into Studente values ('Giulia', 'Tuttobene', 1992)
insert into Esame values ('Reti Neurali', 6, '2020-01-31', 'Trenta', 'Passato', 2)
insert into Esame values ('Biofisica Computazionale', 6, '2019-06-27', 'Trenta', 'Passato', 2)

select * from Studente
