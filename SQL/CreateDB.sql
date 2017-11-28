USE master;
IF db_id('FridgeDB') is not null
BEGIN  
	DROP DATABASE FridgeDB;
END  

CREATE DATABASE FridgeDB;
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
	CONSTRAINT fk_persons_id
		FOREIGN KEY (userLogin)
		REFERENCES Account(login) ON DELETE CASCADE
);

CREATE TABLE Amount (
    guid varchar(255),
	productId int NOT NULL,
    value int NOT NULL,
	primary key(guid, productId),
	CONSTRAINT fk_product_id
		FOREIGN KEY (productId)
		REFERENCES Product(id) ON DELETE CASCADE
);


USE FridgeDB;

INSERT INTO Account
VALUES ( 'admin', 'admin');

INSERT INTO Account
VALUES ( 'a', 'a');


INSERT INTO Product
VALUES ( 'admin', 'test', 2.5);
INSERT INTO Product
VALUES ( 'admin', 'test2', 1);
INSERT INTO Product
VALUES ( 'admin', 'test2', 1);
INSERT INTO Product
VALUES ( 'admin', 'test2', 1);
INSERT INTO Product
VALUES ( 'admin', 'test2', 1);
INSERT INTO Product
VALUES ( 'admin', 'hahahahha', 1);

INSERT INTO Amount
VALUES ( 'dfghsf', 1, 1);
INSERT INTO Amount
VALUES ( 'qweqwe', 2, 1);
INSERT INTO Amount
VALUES ( 'qweqwe', 3, 1);
INSERT INTO Amount
VALUES ( 'qweqwe', 4, 1);
INSERT INTO Amount
VALUES ( 'qweqwe', 5, 1);
INSERT INTO Amount
VALUES ( 'qweqwe', 6, 1);
