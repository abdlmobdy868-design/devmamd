

----------------1. CREATE Company 
CREATE TABLE Company (
    CompanyName VARCHAR(100) PRIMARY KEY,
    Address VARCHAR(200),
    Phone VARCHAR(20)
);

------------2. CREATE Doctor 
CREATE TABLE Doctor (
    DoctorID INT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100),
    Phone VARCHAR(20),
    Specialty VARCHAR(50),
    YearsOfExperience INT
);

-------------3. CREATE Drug 
CREATE TABLE Drug (
    DrugID INT PRIMARY KEY,
    TradeName VARCHAR(100) NOT NULL,
    DrugStrength VARCHAR(50),
    CompanyName VARCHAR(100),
    FOREIGN KEY (CompanyName) REFERENCES Company(CompanyName) ON DELETE CASCADE
);

-- ---------4. CREATE Patient 
CREATE TABLE Patient (
    UR_Number VARCHAR(20) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Address VARCHAR(200),
    Age INT,
    Email VARCHAR(100),
    Phone VARCHAR(20),
    MedicareCardNumber VARCHAR(50),
    PrimaryDoctorID INT,
    FOREIGN KEY (PrimaryDoctorID) REFERENCES Doctor(DoctorID) ON DELETE SET NULL
);

------------------- 5. CREATE Prescription 
CREATE TABLE Prescription (
    PrescriptionID INT PRIMARY KEY IDENTITY(1,1),
    Date DATE NOT NULL,
    Quantity INT,
    UR_Number VARCHAR(20),
    DoctorID INT,
    DrugID INT,
    FOREIGN KEY (UR_Number) REFERENCES Patient(UR_Number),
    FOREIGN KEY (DoctorID) REFERENCES Doctor(DoctorID),
    FOREIGN KEY (DrugID) REFERENCES Drug(DrugID)
);
-


----. 1-SELECT 
SELECT * FROM Doctor;

---. 2-ORDER BY 
SELECT * 
FROM Patient ORDER BY Age ASC;

----. 3-OFFSET FETCH 
SELECT * 
FROM Patient ORDER BY UR_Number OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY;

-----. 4-SELECT TOP 
SELECT TOP 5
* FROM Doctor;

----. 5-SELECT DISTINCT 
SELECT DISTINCT
Address FROM Patient;

----. 6-WHERE 
SELECT *
FROM Patient WHERE Age = 25;

----. 7-NULL 
SELECT *
FROM Patient WHERE Email IS NULL;

----. 8-AND 
SELECT *
FROM Doctor WHERE YearsOfExperience > 5 AND Specialty = 'Cardiology';

-- 9-IN 
SELECT *
FROM Doctor WHERE Specialty IN ('Cardiology', 'Dermatology');

---. 10-BETWen
SELECT *
FROM Patient WHERE Age BETWEEN 18 AND 30;

--. 11-LIKE 
SELECT *
FROM Doctor WHERE Name LIKE 'Dr.%';

--. 12-Aliases 
SELECT Name
AS DoctorName, Email AS DoctorEmail FROM Doctor;

-- . 13-JOIN 
SELECT P.Name AS PatientName, 
Pr.* FROM Prescription Pr JOIN Patient P ON Pr.UR_Number = P.UR_Number;

--. 14-GROUP BY 
SELECT Address AS City
, COUNT(*) AS PatientCount FROM Patient GROUP BY address;

--. 15-HAVING 
SELECT Address AS City
, COUNT(*) AS PatientCount FROM Patient GROUP BY address HAVING COUNT(*) > 3;

--. 16-EXISTS 
SELECT * FROM Patient 
P WHERE EXISTS (SELECT 1 FROM Prescription Pr WHERE Pr.UR_Number = P.UR_Number);

-- . 17-UNION 
SELECT Name
, 'Doctor' AS Type FROM Doctor UNION SELECT Name, 'Patient' AS Type FROM Patient;

--. 18-INSERT Doctor 
INSERT INTO Doctor
(DoctorID, Name, Email, Phone, Specialty, YearsOfExperience) VALUES (2, 'Dr. Temp', 'temp@mail.com', '012', 'Neurology', 3);

--. 19-INSERT Patient 
INSERT INTO Patient 
(UR_Number, Name, Age, Address, Email, Phone) VALUES ('UR2', 'Temp Patient', 30, 'Alex', 'temp@mail.com', '012');

--. 20-UPDATE 
UPDATE Doctor
SET Phone = '099';

-- . 21-UPDATE JOIN 
UPDATE P SET P.Address
= 'Giza' FROM Patient P JOIN Prescription Pr ON P.UR_Number = Pr.UR_Number;

-- . 22-DELETE 
DELETE FROM
Patient;

-- 23-TRANSACTION 
BEGIN TRANSACTION;

INSERT INTO Doctor (DoctorID, Name, Email, Specialty) VALUES (3, 'Dr. New', 'new@mail.com', 'Pediatrics');

INSERT INTO Patient (UR_Number, Name, Age, PrimaryDoctorID) VALUES ('UR3', 'New Patient', 15, 3);

COMMIT TRANSACTION;


