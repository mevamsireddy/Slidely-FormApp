import { Router, Request, Response } from 'express';
import fs from 'fs';
import path from 'path';

const router = Router();
const dbPath = path.resolve(__dirname, '../db.json'); // Corrected path

// Ensure db.json file exists
if (!fs.existsSync(dbPath)) {
  fs.writeFileSync(dbPath, JSON.stringify({ submissions: [] }));
}

// POST /api/submit - Handle new submissions
router.post('/submit', (req: Request, res: Response) => {
  const { name, email, phone, github_link, stopwatch_time } = req.body;

  // Validate required fields
  if (!name || !email || !phone || !github_link || !stopwatch_time) {
    return res.status(400).json({ message: 'Missing required fields' });
  }

  const newSubmission = { name, email, phone, github_link, stopwatch_time };

  try {
    const data = JSON.parse(fs.readFileSync(dbPath, 'utf-8'));
    data.submissions.push(newSubmission);
    fs.writeFileSync(dbPath, JSON.stringify(data, null, 2));
    res.status(201).json(newSubmission); // Send back the new submission object
  } catch (error) {
    console.error('Error saving submission:', error);
    res.status(500).json({ message: 'Error saving submission', error });
  }
});

// GET /api/read - Retrieve submissions
router.get('/read', (req: Request, res: Response) => {
  const index = parseInt(req.query.index as string, 10);

  fs.readFile(dbPath, 'utf-8', (err, data) => {
    if (err) {
      console.error('Error reading database:', err);
      return res.status(500).send('Error reading database');
    }

    const db = JSON.parse(data);

    if (index >= 0 && index < db.submissions.length) {
      res.status(200).json(db.submissions[index]);
    } else {
      res.status(404).json({ message: 'Submission not found' }); // Ensure proper error handling for out-of-range index
    }
  });
});

export default router;