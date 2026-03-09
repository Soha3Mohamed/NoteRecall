- In User for example when i write CreatedAt property i choose to give it a default value of DateTime.UtcNow, but when i write UpdatedAt property i choose to give it a default value of DateTime.Now. 
  I do this because i don't want to manually set the CreatedAt property when i create a new User
  The same is for UpdatedAt property, i want it to be automatically set ath the creation of the object and when i update a User after that, I will manually update it to that time.

- User
 ↓
Write Note
 ↓
AI generates Questions
 ↓
Questions scheduled
 ↓
Daily ReviewSession
 ↓
User answers (Active Recall)
 ↓
Score recorded
 ↓
Spaced Repetition updates schedule
 ↓
Questions appear again later

- Rule when it comes to deciding whether to create a repository for an entity or not, is to see if you will need to call it from database directly or not. if yes then make as repo if not don;t make a repo
  Example of that is order. order itseld contains a list of order items, and order item contains a product, so when i want to get an order i will get the order with its items and products,
     so i will not need to call the database to get the order items or the products,
	 so i will not make a repository for them, but i will make a repository for the order because i will need to call it from database directly.
- 