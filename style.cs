
-- ========== 1. إنشاء الجداول ==========
CREATE TABLE Company (
    CompanyName VARCHAR(100) PRIMARY KEY,
    Address VARCHAR(200),
    Phone VARCHAR(20)
);

CREATE TABLE Doctor (
    DoctorID INT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100),
    Phone VARCHAR(20),
    Specialty VARCHAR(50),
    YearsOfExperience INT
);

CREATE TABLE Drug (
    DrugID INT PRIMARY KEY,
    TradeName VARCHAR(100) NOT NULL,
    DrugStrength VARCHAR(50),
    CompanyName VARCHAR(100),
    FOREIGN KEY (CompanyName) REFERENCES Company(CompanyName) ON DELETE CASCADE
);

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

-- ========== 2. DML Exercises ==========

-- 1. SELECT
SELECT * FROM Doctor;

-- 2. ORDER BY
SELECT * FROM Patient ORDER BY Age ASC;

-- 3. OFFSET FETCH
SELECT * FROM Patient 
ORDER BY UR_Number 
OFFSET 4 ROWS FETCH NEXT 10 ROWS ONLY;

-- 4. SELECT TOP
SELECT TOP 5 * FROM Doctor;

-- 5. SELECT DISTINCT
SELECT DISTINCT Address FROM Patient;

-- 6. WHERE
SELECT * FROM Patient WHERE Age = 25;

-- 7. NULL
SELECT * FROM Patient WHERE Email IS NULL;

-- 8. AND
SELECT * FROM Doctor 
WHERE YearsOfExperience > 5 AND Specialty = 'Cardiology';

-- 9. IN
SELECT * FROM Doctor 
WHERE Specialty IN ('Dermatology', 'Oncology');

-- 10. BETWEEN
SELECT * FROM Patient 
WHERE Age BETWEEN 18 AND 30;

-- 11. LIKE
SELECT * FROM Doctor 
WHERE Name LIKE 'Dr.%';

-- 12. Column & Table Aliases
SELECT Name AS DoctorName, Email AS DoctorEmail FROM Doctor;

-- 13. Joins
SELECT P.Name AS PatientName, Pr.* 
FROM Prescription Pr
JOIN Patient P ON Pr.UR_Number = P.UR_Number;

-- 14. GROUP BY
SELECT Address AS City, COUNT(*) AS PatientCount 
FROM Patient 
GROUP BY Address;

-- 15. HAVING
SELECT Address AS City, COUNT(*) AS PatientCount 
FROM Patient 
GROUP BY Address 
HAVING COUNT(*) > 3;

-- 16. EXISTS
SELECT * FROM Patient P
WHERE EXISTS (
    SELECT 1 FROM Prescription Pr WHERE Pr.UR_Number = P.UR_Number
);

-- 17. UNION
SELECT Name, 'Doctor' AS Type FROM Doctor
UNION
SELECT Name, 'Patient' AS Type FROM Patient;

-- 18. INSERT
INSERT INTO Doctor (DoctorID, Name, Email, Phone, Specialty, YearsOfExperience)
VALUES (101, 'Dr. Ahmed Ali', 'ahmed@mail.com', '01012345678', 'Cardiology', 8);

-- 19. INSERT Multiple Rows
INSERT INTO Patient (UR_Number, Name, Age, Address, Email, Phone)
VALUES 
('UR001', 'Ali', 20, 'Cairo', 'ali@mail.com', '0101'),
('UR002', 'Mona', 25, 'Alex', 'mona@mail.com', '0102'),
('UR003', 'Sara', 30, 'Suez', 'sara@mail.com', '0103');

-- 20. UPDATE
UPDATE Doctor 
SET Phone = '01199999' 
WHERE DoctorID = 101;

-- 21. UPDATE JOIN
UPDATE P
SET P.Address = 'New City'
FROM Patient P
JOIN Prescription Pr ON P.UR_Number = Pr.UR_Number
WHERE Pr.DoctorID = 101;

-- 22. DELETE
DELETE FROM Patient 
WHERE UR_Number = 'UR001';
