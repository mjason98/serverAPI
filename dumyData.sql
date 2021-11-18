-- Create the database
CREATE DATABASE Agenda
GO 

use Agenda
go

--Create the tables and filling it with dummy data
CREATE TABLE dbo.Claustro(
    id int IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(100)
)

INSERT into Claustro VALUES
('Lcs. Mirian'), ('Msc Baez'), ('Phd. Saul')
go

CREATE TABLE dbo.Topics(
    id int IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR (60)
)

INSERT Into Topics VALUES 
('Math'), ('Calculus'), ('Physics'), ('Graphic Programing')
GO 


CREATE TABLE dbo.Lessons(
    id int IDENTITY(1,1) PRIMARY KEY,
    name int,
    prophesor int,
    dateIni datetime,
    dateFin datetime,
    descr VARCHAR(256),

    CONSTRAINT fk_name_id
    FOREIGN KEY (name)
    REFERENCES Topics (id)
    ON DELETE CASCADE ,

    CONSTRAINT fk_prophesor_id
    FOREIGN KEY (prophesor)
    REFERENCES Claustro (id)
    ON DELETE CASCADE
)

INSERT INTO Lessons (name, prophesor, dateIni, dateFin, descr) VALUES
(1, 1, '2021-11-16 14:00:00', '2021-11-16 15:00:00', 'a lesson'), (2, 1, '2021-12-05 10:00:00', '2021-12-05 10:45:00', 'a lesson od things'),
(3, 3, '2021-11-20 08:00:00', '2021-11-20 09:00:00', 'a long odd lesson')  
go

SELECT Lessons.id, Claustro.name, Topics.name from Lessons,Claustro,Topics
WHERE Lessons.name = Topics.id and Lessons.prophesor = Claustro.id
go




