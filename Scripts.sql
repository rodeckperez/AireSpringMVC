create database AireSpring
go 

use AireSpring

go


create table employees(
EmployeeId int identity(1,1) primary key,
Name varchar(50) not null,
LastName varchar(50) not null,
Phone varchar(30) not null,
ZipCode int not null,
HireDate date not null
)

GO

create procedure sp_getEmployees
AS
BEGIN
SELECT EmployeeId, Name, LastName, Phone, ZipCode,  HireDate FROM employees
order by HireDate desc
END

GO

create procedure sp_createEmployee
@name  varchar(50),
@lastName varchar(50),
@phone varchar(30),
@zipCode int,
@hiringDate date
AS
BEGIN
BEGIN TRAN ADDNEWEMPLOYEE
BEGIN TRY 
INSERT INTO employees(Name, LastName, Phone, ZipCode, HireDate) 
VALUES
(@name, @lastName, @phone, @zipCode, @hiringDate)
COMMIT TRAN ADDNEWEMPLOYEE
END TRY
BEGIN CATCH 
ROLLBACK TRAN ADDNEWEMPLOYEE
END CATCH 

END
