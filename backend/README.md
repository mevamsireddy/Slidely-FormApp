# Backend

## Overview

This is a simple Express server made with TypeScript that handles form submissions and retrieves saved submissions from a JSON file.

## Endpoints

- **GET /api/ping** - Returns true.
- **POST /api/submit** - Accepts name, email, phone, github_link, and stopwatch_time in the request body.
- **GET /api/read** - Accepts an index query parameter to retrieve the (index+1)th form submission.

## Running the Server

1. **Install dependencies:**
   ```bash
   npm install
2. **Start the server:**
   ```bash
    npm start or npx ts-node src/server.ts
    
    The server will run on `http://localhost:3000`

## Project structure
```bash
/backend/
├── src/
│   ├── routes/
│   │   ├── submissions.ts    # Route handling submission endpoints
│   ├── app.ts                # Main application logic
│   ├── server.ts             # Express server setup
├── db.json                   # JSON file for storing submissions (mock database)
├── .gitignore                # Git ignore file
├── package.json              # Node.js dependencies
├── tsconfig.json             # TypeScript configuration
└── README.md                 # Backend README file
```
## Running the Server
Navigate to the backend folder. Install the dependencies by running `npm install`. Start the server using `npm start`. The server will run on `http://localhost:3000`. You can test the endpoints using a tool like Postman or curl.

By following these steps, you will have a functional Express backend server in TypeScript with the required endpoints to handle form submissions and retrievals.