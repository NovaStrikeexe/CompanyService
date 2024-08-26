CREATE TABLE Companies (
                           Id SERIAL PRIMARY KEY,
                           Name VARCHAR(100) NOT NULL
);

CREATE TABLE Departments (
                             Id SERIAL PRIMARY KEY,
                             Name VARCHAR(100) NOT NULL,
                             CompanyId INT NOT NULL,
                             FOREIGN KEY (CompanyId) REFERENCES Companies(Id)
);

CREATE TABLE Employees (
                           Id SERIAL PRIMARY KEY,
                           FirstName VARCHAR(50) NOT NULL,
                           LastName VARCHAR(50) NOT NULL,
                           MiddleName VARCHAR(50),
                           DateOfBirth DATE NOT NULL,
                           DepartmentId INT NOT NULL,
                           CompanyId INT NOT NULL,
                           FOREIGN KEY (DepartmentId) REFERENCES Departments(Id),
                           FOREIGN KEY (CompanyId) REFERENCES Companies(Id)
);
