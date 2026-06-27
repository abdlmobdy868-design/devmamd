1-pharma-company
//parent
CREATE TABLE PHARMA_COMPANY (
    company_name VARCHAR(100) PRIMARY KEY,
    phone VARCHAR(20),
    address VARCHAR(200)
);
---------------------------------------------------

2-doctor
//parent
CREATE TABLE DOCTOR (
    doctor_id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    specialty VARCHAR(100),
    years_experience INT,
    phone VARCHAR(20),
    email VARCHAR(100)
);
-------------------------------------------------------

3- drug
--<pharma_company
CREATE TABLE DRUG (
    trade_name VARCHAR(100),
    strength VARCHAR(50),
    company_name VARCHAR(100) NOT NULL,  -- 
    PRIMARY KEY (trade_name, strength),
    FOREIGN KEY (company_name) REFERENCES PHARMA_COMPANY(company_name) ON DELETE CASCADE -- 
);
------------------------------------------------------------
4patient
--<doctor
CREATE TABLE PATIENT (
    ur_num INT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    age INT,
    email VARCHAR(100),
    phone VARCHAR(20),
    med_card_num VARCHAR(50) UNIQUE,
    primary_doctor_id INT, --  
    FOREIGN KEY (primary_doctor_id) REFERENCES DOCTOR(doctor_id) -- 
);
-----------------------------------------------------------------
5perescription
<--doctor--<patient-<drug
CREATE TABLE PRESCRIPTION (
    ur_num INT, --  PATIENT
    doctor_id INT, --  DOCTOR
    trade_name VARCHAR(100), --  DRUG
    strength VARCHAR(50), --  DRUG
    prescription_date DATE,
    quantity INT NOT NULL,
    PRIMARY KEY (ur_num, doctor_id, trade_name, strength, prescription_date),
    FOREIGN KEY (ur_num) REFERENCES PATIENT(ur_num), --  1
    FOREIGN KEY (doctor_id) REFERENCES DOCTOR(doctor_id), --  2
    FOREIGN KEY (trade_name, strength) REFERENCES DRUG(trade_name, strength) --  3
);
-----------------------------------------------------------------------
