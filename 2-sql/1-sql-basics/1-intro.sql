-- this is sql
-- two dashes for a sql comment

-- if nothing's highlighted, "execute" will run the whole file
-- if something's highlighted, only that will run.
-- use this to fill a file with many commands that you never run all at once,
--   just one at a time by highlighting them before pressing execute/run.

-- t-sql reference:
-- https://docs.microsoft.com/en-us/sql/t-sql/language-reference


-- string (varchar) literal in sql, single quotes
-- semicolons are not required
-- usually (depending on collation) nothing in sql is case sensitive
SELECT 'Hello world';

SELECT 2 + 2;

SELECT SYSUTCDATETIME();

-- SELECT (columns) FROM (table)
--            ^ aka "select-list"
SELECT FirstName, LastName FROM Customer;

-- all columns
SELECT * FROM Customer;
SELECT * FROM Employee;

-- columns in the result set don't need to directly match columns in a table
SELECT FirstName + ' ' + LastName FROM Customer;

-- column alias (see the difference in output)
SELECT FirstName + ' ' + LastName AS FullName FROM Customer;
-- two syntaxes for including spaces:
SELECT FirstName + ' ' + LastName AS "Full Name" FROM Customer;
SELECT FirstName + ' ' + LastName AS [Full Name] FROM Customer;

-- filtering with WHERE

-- in SQL, line breaks and spacing doesn't matter.
-- semicolon doesn't matter either.
SELECT *
FROM Customer
WHERE LEN(FirstName) > 5;

SELECT *
FROM Customer
WHERE Country = 'Brazil';

-- operators... < <= = >= >
-- not-equals is either != or <>
-- boolean operators... AND OR NOT

-- every customer with first name starting with B
SELECT *
FROM Customer
WHERE FirstName >= 'B' AND FirstName < 'C';

-- case insensitivity is pretty broad
sElEcT *
FROM customER
where firstname >= 'b' and FIRSTNAME < 'C';

-- aggregate functions
SELECT COUNT(*)
FROM Customer;

-- other built-in aggregate functions... SUM, MIN, MAX, AVG
SELECT SUM(Total)
FROM Invoice;

SELECT AVG(Total)
FROM Invoice;

-- aggregate functions by default aggregate all rows into 1.
-- if we want to group into more than just 1 result row, we can use GROUP BY
SELECT *
FROM Invoice
GROUP BY CustomerId; -- doesnt work
--  error: Column 'Invoice.InvoiceId' is invalid in the select list because it
-- is not contained in either an aggregate function or the GROUP BY clause.

-- how much has each customer bought in total
SELECT CustomerId, SUM(Total)
FROM Invoice
GROUP BY CustomerId;

-- how can i do filtering based on after the rows have been grouped by group by

-- all customer IDs that have bought more than $40 worth of product
SELECT CustomerId, SUM(Total)
FROM Invoice
WHERE SUM(Total) > 40
GROUP BY CustomerId; -- doesn't work, use HAVING

-- all customer IDs that have bought more than $40 worth of product
SELECT CustomerId, SUM(Total)
FROM Invoice
GROUP BY CustomerId
HAVING SUM(Total) > 40;

-- sorting with ORDER BY

-- all customer IDs that have bought more than $40 worth of product outside the US
SELECT CustomerId, SUM(Total)
FROM Invoice
WHERE BillingCountry != 'USA'
GROUP BY CustomerId
HAVING SUM(Total) > 40
ORDER BY SUM(Total) DESC, CustomerId;

-- other options with select statement...
-- SELECT DISTINCT (remove duplicate rows as the last step)
-- SELECT TOP(N) (discard after the first N rows)

--logical order of execution of the SELECT statement

-- FROM
-- WHERE
-- GROUP BY
-- HAVING
-- SELECT
-- ORDER BY

-- exercises
-- 1. list all customers (full names, customer ID, and country) who are not in the US

-- 2. list all customers from brazil

-- 3. list all sales agents
SELECT *
FROM Employee
WHERE Title = 'Sales Support Agent';

-- 4. show a list of all countries in billing addresses on invoices.

-- 5. how many invoices were there in 2009, and what was the sales total for that year?
--    (extra challenge: find the invoice count sales total for every year, using one query)

-- 6. how many line items were there for invoice #37?

-- 7. how many invoices per country?

-- 8. show total sales per country, ordered by highest sales first.
