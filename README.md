# FormApp (Windows Desktop App)

Welcome to the FormApp repository! This project is a Windows desktop application developed using Visual Studio and Visual Basic. It provides a simple yet powerful interface for managing submissions and interacting with a backend server.

## Features

- **Create New Submissions**: Easily create new form submissions with fields for name, email, phone, GitHub link, and stopwatch time.
- **View Submissions**: Navigate through existing submissions.
- **Error Handling**: Robust client-side validation and error messages ensure data integrity and user feedback.
- **Keyboard Shortcuts**: Efficiently navigate and interact with the application using keyboard shortcuts for common actions. Press `Ctrl + H` to show the list of keyboard shortcuts.

## Screenshots

![Screenshot 1](screenshots/FormApp.png)
![Screenshot 2](screenshots/ViewSubmissions.png)
![Screenshot 3](screenshots/CreateSubmissions.png)

## Project Stucture
```bash
/FormApp/
├── FormApp/
│   ├── Form1.vb                    # Main form for application
│   ├── FormViewSubmissions.vb      # Form for viewing submissions
│   ├── FormCreateSubmission.vb     # Form for creating new submissions
│   ├── ...                         # Other form files and related code
│   ├── FormApp.sln                 # Visual Studio solution file
│   └── ...                         # Other solution files

/backend/
├── src/
│   ├── routes/
│   │   ├── submissions.ts          # Route handling submission endpoints
│   ├── app.ts                      # Main application logic
│   ├── server.ts                   # Express server setup
├── db.json                         # JSON file for storing submissions (mock database)
├── .gitignore                      # Git ignore file
├── package.json                    # Node.js dependencies
├── tsconfig.json                   # TypeScript configuration
└── README.md                       # Backend README file
```

## Installation Instructions

1. **Clone the Repository**:
- GitHub `https://github.com/mevamsireddy/Slidely-FormApp.git`


2. **Open in Visual Studio**:
- Open Visual Studio.
- Navigate to `File` -> `Open` -> `Project/Solution`.
- Select the `FormApp.sln` file in the root directory of the repository.


3. **Restore NuGet Packages**:
- Once the project is loaded in Visual Studio, right-click on the solution in the Solution Explorer.
- Select `Restore NuGet Packages` to ensure all dependencies are installed.


4. **Build and Run**:
- Build the solution (`Ctrl+Shift+B` or `Build` -> `Build Solution`).
- Run the application (`F5` or `Debug` -> `Start Debugging`).


## Usage

1. **Creating New Submissions**:
- Click on the "Create New Submission" button or press `Ctrl + N` to open the form.
- Fill in the required fields (name, email, phone, GitHub link, stopwatch time).
- Click "Submit" or press `Ctrl + S` to send the data to the backend server.

2. **Viewing Submissions**:
- Click on the "View Submissions" button or press `Ctrl + V` to navigate through existing submissions.
- Use `Ctrl + P` for previous and `Ctrl + N` for next to cycle through submissions.

3. **Toggle Stopwatch**:
- In the submission form (`FormCreateSubmission`), click "Toggle Stopwatch" or press `Ctrl + T` to start or pause the stopwatch.