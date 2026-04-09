#  NoteRecall API

A backend system that transforms user notes into active recall questions and schedules reviews using spaced repetition.

This project focuses on understanding how learning systems behave internally rather than relying on external AI services.

---

##  Idea

While reading about **active recall** and **spaced repetition**, I wanted to build something that goes beyond basic CRUD.

So instead of just storing notes, NoteRecall:

- Extracts key ideas from notes
- Generates questions (rule-based, AI-inspired)
- Tracks learning progress over time
- Schedules future reviews dynamically

---

##  How It Works

### Flow

User writes a note
↓
System splits content into sentences
↓
Important sentences are selected
↓
Questions are generated
↓
Each question gets a learning state (progress)
↓
User answers questions
↓
System updates difficulty & schedules next review


---

##  Core Concepts

### 1. Question Generation (AI-inspired)

Instead of using an external LLM, this project uses a **rule-based generator** to simulate intelligent behavior:

- Detects patterns like:
  - "X is Y" → Definition questions
  - "X causes Y" → Why questions
- Generates multiple question types
- Keeps answers grounded in the original note

> This can later be replaced with real LLM integration.

---

### 2. Spaced Repetition

Each question has a **learning state**:

- `EaseFactor`
- `Interval`
- `NextReviewDate`

After each answer:
- Difficulty is updated
- Interval changes
- Next review is scheduled

This is a simplified implementation inspired by SM-2 (used in Anki).

---

### 3. Data Modeling (Key Learning Area)

A major focus in this project was designing relationships correctly:

- `Note` → contains content
- `Question` → derived from note
- `QuestionProgress` → learning state (core of the system)
- `ReviewSession` → user interaction session
- `ReviewResult` → answer history

Understanding:
- Foreign keys
- Navigation properties
- Referential integrity
- EF Core relationships

was a key part of building this.

---

##  Architecture

Built using **Clean Architecture**:

Core
Application
Infrastructure
API

- **Core** → Entities & Interfaces
- **Application** → Business logic
- **Infrastructure** → EF Core, repositories
- **API** → Controllers

---

##  Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Mapster
- Serilog (optional if used)

---

##  Key Features

- Create notes
- Automatic question generation
- Rule-based NLP simulation
- Spaced repetition scheduling
- Track review results
- Retrieve due questions

---

##  Example

### Input Note

Photosynthesis is the process by which plants make food.
It produces oxygen.

### Generated Questions

- What is photosynthesis?
- Why is photosynthesis important?

---

##  API Highlights

- `POST /api/notes` → Create note + generate questions  
- `GET /api/reviews/due` → Get questions ready for review  
- `POST /api/reviews/answer` → Submit answer & update progress  

---

##  What I Focused On

This project was mainly about:

- Designing a system, not just endpoints  
- Modeling evolving data over time  
- Understanding EF Core relationships deeply  
- Simulating intelligent behavior without AI  

---

##  Future Improvements

- Integrate real LLMs for smarter question generation  
- Add multiple question types (MCQ, fill-in-the-blank)  
- Notification system for due reviews  
- Background jobs for scheduling  
- Better NLP heuristics  

---

##  Final Thought

This project helped me move from:

> “building APIs”

to

> “designing systems with behavior over time”

---

##  Contact

Feel free to connect or give feedback.
