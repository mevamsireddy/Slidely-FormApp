import express, { Request, Response } from 'express';
import cors from 'cors';
import fs from 'fs';
import path from 'path';

const app = express();
app.use(cors());
app.use(express.json());

const dbFilePath = path.join(__dirname, '../db.json');

// Ensure db.json exists with initial empty submissions array
if (!fs.existsSync(dbFilePath)) {
  const initialData = { submissions: [] };
  fs.writeFileSync(dbFilePath, JSON.stringify(initialData), 'utf-8');
}

// Root route
app.get('/', (req: Request, res: Response) => {
  res.send('Server is up and running');
});

// Ping endpoint to check if server is alive
app.get('/api/ping', (req: Request, res: Response) => {
  res.send(true);
});

// POST /api/submit - Handle new submissions
app.post('/api/submit', (req: Request, res: Response) => {
  const { name, email, phone, github_link, stopwatch_time } = req.body;

  console.log('Received submission:', req.body); // Debugging: log received body

  // Validate required fields
  if (!name || !email || !phone || !github_link || !stopwatch_time) {
    console.error('Missing required fields:', req.body);
    return res.status(400).json({ message: 'Missing required fields' });
  }

  const newSubmission = { name, email, phone, github_link, stopwatch_time };

  fs.readFile(dbFilePath, 'utf-8', (err, data) => {
    if (err) {
      console.error('Error reading database:', err);
      return res.status(500).json({ message: 'Error reading database', error: err });
    }

    let db;
    try {
      db = JSON.parse(data);
    } catch (parseError) {
      console.error('Error parsing database JSON:', parseError);
      return res.status(500).json({ message: 'Error parsing database JSON', error: parseError });
    }

    db.submissions.push(newSubmission);

    fs.writeFile(dbFilePath, JSON.stringify(db, null, 2), (err) => {
      if (err) {
        console.error('Error writing to database:', err);
        return res.status(500).json({ message: 'Error writing to database', error: err });
      }

      res.status(201).json(newSubmission); // Send back the new submission object
    });
  });
});

// GET /api/read - Retrieve all submissions
app.get('/api/read', (req: Request, res: Response) => {
  fs.readFile(dbFilePath, 'utf-8', (err, data) => {
    if (err) {
      console.error('Error reading database:', err);
      return res.status(500).send('Error reading database');
    }

    try {
      const db = JSON.parse(data);
      res.status(200).json(db.submissions);
    } catch (parseError) {
      console.error('Error parsing database JSON:', parseError);
      return res.status(500).send('Error parsing database JSON');
    }
  });
});

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
  console.log(`Server is running on http://localhost:${PORT}`);
});