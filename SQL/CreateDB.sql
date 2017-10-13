USE master;
IF db_id('FridgeDB') is not null
BEGIN  
	DROP DATABASE FridgeDB;
END  

CREATE DATABASE FridgeDB;

GO

USE FridgeDB;

CREATE TABLE Persons (
    id int IDENTITY(1,1) PRIMARY KEY,
    login varchar(255) NOT NULL,
    password varchar(255) NOT NULL
);

CREATE TABLE Product (
    id int IDENTITY(1,1) PRIMARY KEY,
	persons_id int NOT NULL,
    name varchar(255) NOT NULL,
	price  decimal NOT NULL,
    amount  integer NOT NULL,
	CONSTRAINT fk_persons_id
		FOREIGN KEY (persons_id)
		REFERENCES Persons(Id) ON DELETE CASCADE
);


INSERT INTO Persons
VALUES ( 'admin', 'admin');

INSERT INTO Product
VALUES ( 1, 'test', 2.5, 5);

INSERT INTO Product
VALUES ( 1, 'test2', 1, 1);

SHUTDOWN WITH NOWAIT