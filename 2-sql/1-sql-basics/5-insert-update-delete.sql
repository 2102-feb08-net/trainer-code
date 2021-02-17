-- SQL commands are divided in a few categories/sublanguages.
-- Data Manipulation Language (DML)
--   SELECT, INSERT, UPDATE, DELETE, TRUNCATE
--  these commands all operate on rows of tables.

-- Data Definition Language
--   CREATE, ALTER, DROP
--  these commands operate on other objects, like entire tables, or functions, views, etc.

-- Data Control Language (DCL) manages permissions/users/auth
--     GRANT, REVOKE

-- rest of DML besides SELECT is for adding/changing/removing rows

-- INSERT

SELECT * FROM Genre;

INSERT INTO Genre VALUES (100, 'Miscellaneous');

INSERT INTO Genre (GenreId) VALUES (101);

INSERT INTO Genre (GenreId, Name) VALUES (102, 'Misc 2');

SELECT * FROM Genre WHERE GenreId >= 100;

INSERT INTO Genre (GenreId, Name) VALUES
	(103, 'Misc 3'),
	(104, 'Misc 4');

INSERT INTO Genre (GenreId, Name)
	SELECT GenreId + 10, Name + '!'
	FROM Genre
	WHERE GenreId = 104;

-- INSERT can also do things like read CSV files etc

-- UPDATE

-- without a WHERE, would modify every row
UPDATE Genre
SET Name = 'Misc 1'  -- , otherthing = value
WHERE GenreId = 101;

-- without a WHERE, would delete every row (one by one)
DELETE FROM Genre
WHERE GenreId >= 100;

-- this command deletes all rows all at once
--TRUNCATE TABLE Genre;

-- exercises

-- 1. insert two new records into the employee table.

-- 2. insert two new records into the tracks table.

-- 3. update customer Aaron Mitchell's name to Robert Walter

-- 4. delete one of the employees you inserted.

-- 5. delete customer Robert Walter.
