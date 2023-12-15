CREATE DATABASE LibraryDB;
USE LibraryDB;

CREATE TABLE Books (
    BookId INT PRIMARY KEY,
    Title NVARCHAR(MAX),
    Author NVARCHAR(MAX),
    Genre NVARCHAR(MAX),
    Quantity INT
);



INSERT INTO Books (BookId, Title, Author, Genre, Quantity) VALUES
(1, 'The Great Gatsby', 'F. Scott Fitzgerald', 'Classic', 20),
(2, 'To Kill a Mockingbird', 'Harper Lee', 'Fiction', 15),
(3, '1984', 'George Orwell', 'Dystopian', 25),
(4, 'Pride and Prejudice', 'Jane Austen', 'Romance', 18),
(5, 'The Catcher in the Rye', 'J.D. Salinger', 'Coming-of-age', 12),
(6, 'The Hobbit', 'J.R.R. Tolkien', 'Fantasy', 30),
(7, 'The Da Vinci Code', 'Dan Brown', 'Mystery', 22);

select * from Books
