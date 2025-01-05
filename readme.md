# SMSystem - Student Management System

SMSystem is a web-based Student Management System developed using ASP.NET Core MVC. It allows users to manage student records, including personal details, class information, and more.

## Table of Contents

- [Installation](#installation)
- [Prerequisites](#prerequisites)
- [Usage](#usage)
- [Features](#features)
- [Database Operations](#database-operations)
- [Contributing](#contributing)
- [Using SQL Server Management Studio (SSMS)](#using-sql-server-management-studio-ssms)

---

## Installation

Follow these steps to set up the project locally.

### 1. Clone the repository:
To get the project on your local machine, run the following command in your terminal or command prompt:

```
git clone https://github.com/pdkumhar/SMSystem.git
```

This will create a local copy of the project on your computer.

### 2. Navigate to the project folder:
After cloning, navigate to the project directory by running:

```
cd SMSystem
```

### 3. Restore NuGet packages:
Run the following command to restore all necessary dependencies for the project:

```
dotnet restore
```

### 4. Set up the database:
If this is your first time running the project, you will need to set up the database. Run the migration command to ensure the database schema is up-to-date:

```
dotnet ef database update
```

This will apply any pending migrations and set up the tables in your SQL database.

### 5. Build the project:
Once everything is restored and set up, build the project with:

```
dotnet build
```

This ensures that the project is compiled and ready to run.

### 6. Run the project:
Finally, run the project locally using:

```
dotnet run
```

The application will start running on `http://localhost:5000` (or another available port if specified).

---

## Prerequisites

Before you can run this project, make sure you have the following installed:

- **.NET Core SDK** version 5.0 or later, which you can download from [here](https://dotnet.microsoft.com/download/dotnet).
- **SQL Server** (or SQL Express) to store the database.

---

## Usage

After setting up and running the project, open your browser and navigate to `http://localhost:5000` (or the specific port indicated by the terminal). You will be able to:

- View a list of students.
- Add a new student.
- Edit existing student information.
- Delete student records.

---

## Features

- User-friendly interface for managing student records.
- Includes features like adding, editing, and deleting student details.
- Database integration with SQL Server.
- Validation for student data (e.g., Roll Number, Mobile, Email).
- Responsive design for desktop and mobile.

---

## Database Operations

Below are some example SQL commands that demonstrate how to interact with the `Students` table in your database.

### 1. **Insert a New Student Record**

This query inserts a new student record into the `Students` table.

```sql
INSERT INTO dbo.Students (FirstName, MiddleName, LastName, Class, Mobile, Email, Address, Photograph)
VALUES ('John', 'Doe', 'Smith', 10, '1234567890', 'john.doe@example.com', '123 Main St', '/uploads/john.jpg');
```

---

### 2. **Select All Students**

This query retrieves all student records from the `Students` table.

```sql
SELECT * FROM dbo.Students;
```

---

### 3. **Update a Student's Mobile Number**

This query updates the mobile number of a student with a specific `RollNumber`.

```sql
UPDATE dbo.Students
SET Mobile = '9876543210'
WHERE RollNumber = 'RR00001';
```

---

### 4. **Delete a Student Record**

This query deletes a student record based on a specific `RollNumber`.

```sql
DELETE FROM dbo.Students
WHERE RollNumber = 'RR00004';
```

---

### 5. **Count Total Students**

This query counts the total number of students in the `Students` table.

```sql
SELECT COUNT(*) FROM dbo.Students;
```

---

### 6. **Select Students by Class**

This query retrieves all students who are in a specific class (e.g., Class 10).

```sql
SELECT * FROM dbo.Students
WHERE Class = 10;
```

---

### 7. **Order Students by Last Name**

This query retrieves all students ordered by their last name in ascending order.

```sql
SELECT * FROM dbo.Students
ORDER BY LastName;
```

---

### 8. **Delete a Student by Serial Number**

This query deletes a student record by the `SrNo` (Serial Number).

```sql
DELETE FROM dbo.Students
WHERE SrNo = 30;
```

---

### 9. **Generate Next Roll Number and Insert New Student Record**

This query demonstrates how to automatically generate the next `RollNumber` by incrementing the last Roll Number, and then inserting a new student record.

```sql
-- Get the last RollNumber
DECLARE @lastRollNumber NVARCHAR(10);

SELECT TOP 1 @lastRollNumber = RollNumber
FROM dbo.Students
ORDER BY SrNo DESC;

-- Extract numeric part, increment it by 1, and generate the new RollNumber
DECLARE @nextRollNumber NVARCHAR(10);
IF @lastRollNumber IS NOT NULL
BEGIN
    SET @nextRollNumber = 'RR' + RIGHT('00000' + CAST(CAST(SUBSTRING(@lastRollNumber, 3, LEN(@lastRollNumber)) AS INT) + 1 AS VARCHAR(5)), 5);
END
ELSE
BEGIN
    SET @nextRollNumber = 'RR00001'; -- If no records exist, start from RR00001
END

-- Now, insert the student record with the generated RollNumber
INSERT INTO dbo.Students (RollNumber, FirstName, MiddleName, LastName, Class, Mobile, Email, Address, Photograph)
VALUES (@nextRollNumber, 'John', 'Doe', 'Smith', 10, '1234567890', 'john.doe@example.com', '123 Main St', '/uploads/john.jpg');
```

---

## Contributing

We welcome contributions to the project! If you want to contribute, follow these steps:

### 1. **Fork the repository:**

Click the "Fork" button at the top of this repository to create a copy under your own GitHub account.

### 2. **Clone your fork:**

Clone your forked repository to your local machine using:

```
git clone https://github.com/pdkumhar/SMSystem.git
```

### 3. **Create a new branch:**

Create a new branch for your changes:

```
git checkout -b feature-name
```

### 4. **Make your changes:**

Make the necessary changes or add new features.

### 5. **Commit your changes:**

Once you're happy with your changes, commit them:

```
git commit -m "Add feature or fix issue"
```

### 6. **Push your changes:**

Push the changes to your forked repository:

```
git push origin feature-name
```

### 7. **Create a Pull Request:**

Go to the GitHub page of your forked repository and click on "Compare & pull request". Provide a description of your changes and submit the pull request to the original repository.

---

# Using SQL Server Management Studio (SSMS)

SQL Server Management Studio (SSMS) is a powerful tool for managing SQL Server databases. In this guide, we'll explain how to open SSMS, connect to a server, and run different queries in a new query window.

## Steps to Open and Run Queries in SSMS

### 1. **Open SQL Server Management Studio (SSMS)**

- First, **open SSMS** from your Start menu or by searching for "SQL Server Management Studio" in the search bar.
- The SSMS **login window** will appear where you'll need to connect to your SQL Server instance.

### 2. **Connect to SQL Server**

- In the **Connect to Server** window, enter the following details:
    - **Server Name**: Enter the name or IP address of the SQL Server instance you want to connect to. For local servers, you can use `localhost` or `.` (dot).
    - **Authentication**: Choose the authentication method:
        - **Windows Authentication**: Use your Windows credentials.
        - **SQL Server Authentication**: Enter your SQL Server login and password.
- After entering the credentials, click **Connect**.

### 3. **Open a New Query Window**

- Once connected to the server, in the **Object Explorer** window (on the left side), you will see your databases listed.
- To open a new query window, click on **New Query** at the top of the toolbar or use the shortcut `Ctrl + N`.
- A new tab will open in the query window, and you can start typing your SQL queries.

### 4. **Choose the Database**

- In the query window, at the top left, there is a **dropdown menu** next to the "Database" label. Select the database you want to run your queries against.
- If you don't select a database, queries will be executed in the context of the default `master` database.

### 5. **Start Writing Your SQL Queries**

You can now start writing different SQL queries. Here’s how you can run various types of queries in SSMS.

#### Example 1: **Select All Students**

```sql
SELECT * FROM dbo.Students;
```

- After writing the query, click on **Execute** or press `F5` to run the query.

#### Example 2: **Insert a New Student Record**

```sql
INSERT INTO dbo.Students (FirstName, MiddleName, LastName, Class, Mobile, Email, Address, Photograph)
VALUES ('Jane', 'Marie', 'Doe', 12, '9876543210', 'jane.doe@example.com', '456 Oak St', '/uploads/jane.jpg');
```

- Click **Execute** to insert the record into the `Students` table.

#### Example 3: **Update a Student's Mobile Number**

```sql
UPDATE dbo.Students
SET Mobile = '5551234567'
WHERE RollNumber = 'RR00002';
```

- Click **Execute** to update the student's information.

#### Example 4: **Delete a Student Record**

```sql
DELETE FROM dbo.Students
WHERE RollNumber = 'RR00003';
```

- Click **Execute** to delete the record.

---

### 6. **Running Multiple Queries**

If you have multiple queries that you want to run in a single batch, you can simply separate them with a semicolon (`;`), like this:

```sql
-- Select All Students
SELECT * FROM dbo.Students;
GO
-- Insert a New Student Record
INSERT INTO dbo.Students (FirstName, LastName, Class) VALUES ('Jake', 'Taylor', 11);
GO
```

Click **Execute** to run all the queries in sequence.

---

### 7. **Viewing Query Results**

- Once you execute a query, the results will appear in the **Results** pane at the bottom of the SSMS window.
- If your query affects data (e.g., an `INSERT`, `UPDATE`, or `DELETE`), SSMS will show the number of rows affected.
- For `SELECT` queries, you will see the result set displayed in a tabular format.

---

### 8. **Saving Queries**

You can save your queries for future use:

- To save a query, go to the **File** menu and select **Save As** or use the shortcut `Ctrl + S`.
- Choose a location on your computer and save the file with a `.sql` extension.

---

### 9. **Error Handling**

If there’s an error in your SQL query (e.g., a syntax error, wrong table name), SSMS will display an error message in the **Messages** pane. Review the error message, correct the issue, and run the query again.

---

## Additional Tips

- **Use `GO` to separate batches**: Sometimes you need to execute multiple batches in one query window. Use `GO` to separate batches of SQL commands.
  
  ```sql
  SELECT * FROM dbo.Students;
  GO
  INSERT INTO dbo.Students (FirstName, LastName, Class) VALUES ('Jake', 'Taylor', 11);
  GO
  ```

- **Use `Ctrl + R` to toggle the Results pane** if it’s hidden.

- **Use the Object Explorer** to navigate through your databases, tables, views, and stored procedures easily.

---

## Conclusion

SQL Server Management Studio (SSMS) is a great tool to run and manage your SQL queries. With SSMS, you can execute basic queries, update records, insert new data, and perform a variety of database operations all in one place. Following these steps, you'll be able to run SQL queries smoothly and efficiently.

