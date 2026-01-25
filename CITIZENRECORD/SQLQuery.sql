CREATE DATABASE CITIZENDB
GO

USE CITIZENDB
GO

CREATE TABLE CITIZEN_TBL (
    Citizen_ID VARCHAR(12) PRIMARY KEY,
    Citizen_Name VARCHAR(30),
    Age INT,
    City VARCHAR(30),
    Status VARCHAR(10)
);
GO

INSERT INTO CITIZEN_TBL (Citizen_ID, Citizen_Name, Age, City, Status)
VALUES 
('C10001', 'Ramesh', 35, 'Chennai', 'Active'),
('C10002', 'Sita', 28, 'Coimbatore', 'Active');
GO

INSERT INTO CITIZEN_TBL (Citizen_ID,Citizen_Name,Age,City,Status) VALUES ('C10003','sanjay',22,'Dindigul','Active');
GO
INSERT INTO CITIZEN_TBL (Citizen_ID,Citizen_Name,Age,City,Status) VALUES ('C10004','prakash',22,'Dindigul','Active');
GO

SELECT * FROM CITIZEN_TBL;
select count(*) from citizen_tbl where citizen_ID = 'C10002';
