// app.ts
import express, { Request, Response } from 'express';
import cors from 'cors';
import path from 'path';
import submissionsRouter from './routes/submissions';

const app = express();
app.use(cors());
app.use(express.json());

// Root route
app.get('/', (req: Request, res: Response) => {
  res.send('Server is up and running');
});

// Mount submissionsRouter for handling submissions under /submit
app.use('/submit', submissionsRouter);

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
  console.log(`Server is running on http://localhost:${PORT}`);
});