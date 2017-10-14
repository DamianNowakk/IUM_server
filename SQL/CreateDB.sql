USE master;
IF db_id('FridgeDB') is not null
BEGIN  
	DROP DATABASE FridgeDB;
END  

CREATE DATABASE FridgeDB;

GO

USE FridgeDB;

CREATE TABLE Person (
    login varchar(255) PRIMARY KEY,
    password varchar(255) NOT NULL
);

CREATE TABLE Product (
    id int IDENTITY(1,1) PRIMARY KEY,
	person_login varchar(255) NOT NULL,
    name varchar(255) NOT NULL,
	price  decimal NOT NULL,
    amount  integer NOT NULL,
	CONSTRAINT fk_persons_id
		FOREIGN KEY (persons_login)
		REFERENCES Person(login) ON DELETE CASCADE
);


INSERT INTO Person
VALUES ( 'admin', 'admin');

INSERT INTO Product
VALUES ( 'admin', 'test', 2.5, 5);

INSERT INTO Product
VALUES ( 'admin', 'test2', 1, 1);