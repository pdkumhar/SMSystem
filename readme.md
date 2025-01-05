# SMSystem - Student Management System

SMSystem is a web-based Student Management System developed using ASP.NET Core MVC. It allows users to manage student records, including personal details, class information, and more.

## Table of Contents

- [Installation](#installation)
- [Prerequisites](#prerequisites)
- [Usage](#usage)
- [Features](#features)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

## Installation

Follow these steps to set up the project locally.

### **1. Clone the repository:**

To get the project on your local machine, run the following command in your terminal or command prompt:

git clone https://github.com/pdkumhar/SMSystem.git

This will create a local copy of the project on your computer.

### **2. Navigate to the project folder:**

After cloning, navigate to the project directory by running:


### **3. Restore NuGet packages:**

Run the following command to restore all necessary dependencies for the project:


### **4. Set up the database:**

If this is your first time running the project, you will need to set up the database. Run the migration command to ensure the database schema is up-to-date:


This will apply any pending migrations and set up the tables in your SQL database.

### **5. Build the project:**

Once everything is restored and set up, build the project with:


This ensures that the project is compiled and ready to run.

### **6. Run the project:**

Finally, run the project locally using:


The application will start running on `http://localhost:5000` (or another available port if specified).

## Prerequisites

Before you can run this project, make sure you have the following installed:

- **.NET Core SDK** version 5.0 or later, which you can download from [here](https://dotnet.microsoft.com/download/dotnet).
- **SQL Server** (or SQL Express) to store the database.

## Usage

After setting up and running the project, open your browser and navigate to `http://localhost:5000` (or the specific port indicated by the terminal). You will be able to:

- View a list of students.
- Add a new student.
- Edit existing student information.
- Delete student records.

## Features

- User-friendly interface for managing student records.
- Includes features like adding, editing, and deleting student details.
- Database integration with SQL Server.
- Validation for student data (e.g., Roll Number, Mobile, Email).
- Responsive design for desktop and mobile.

## Contributing

We welcome contributions to the project! If you want to contribute, follow these steps:

### **1. Fork the repository:**

Click the "Fork" button at the top of this repository to create a copy under your own GitHub account.

### **2. Clone your fork:**

Clone your forked repository to your local machine using:


### **3. Create a new branch:**

Create a new branch for your changes:


### **4. Make your changes:**

Make the necessary changes or add new features.

### **5. Commit your changes:**

Once you're happy with your changes, commit them:


### **6. Push your changes:**

Push the changes to your forked repository:


### **7. Create a Pull Request:**

Go to the GitHub page of your forked repository and click on "Compare & pull request". Provide a description of your changes and submit the pull request to the original repository.
