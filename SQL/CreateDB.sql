USE master;
IF db_id('FridgeDB') is not null
BEGIN  
	DROP DATABASE FridgeDB;
END  

CREATE DATABASE FridgeDB;

GO

USE FridgeDB;

CREATE TABLE Account (
    login varchar(255) PRIMARY KEY,
    password varchar(255) NOT NULL
);

CREATE TABLE Product (
    id int IDENTITY(1,1) PRIMARY KEY,
	userLogin varchar(255) NOT NULL,
    name varchar(255) NOT NULL,
	price decimal(16,2) NOT NULL,
    amount integer NOT NULL,
	CONSTRAINT fk_persons_id
		FOREIGN KEY (userLogin)
		REFERENCES Account(login) ON DELETE CASCADE
);


INSERT INTO Account
VALUES ( 'admin', 'admin');

INSERT INTO Account
VALUES ( 'a', 'a');

USE FridgeDB;

INSERT INTO Product
VALUES ( 'admin', 'test', 2.5, 5);
INSERT INTO Product
VALUES ( 'admin', 'test2', 1, 1);
INSERT INTO Product
VALUES ( 'admin', 'test2', 1, 1);
INSERT INTO Product
VALUES ( 'admin', 'test2', 1, 1);
INSERT INTO Product
VALUES ( 'admin', 'test2', 1, 1);
INSERT INTO Product
VALUES ( 'admin', 'test2', 1, 1);