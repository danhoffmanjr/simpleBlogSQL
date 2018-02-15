INSERT INTO Posts
	(Title, Author, Permalink, PostContent)
	VALUES ('5', 'Dan Hoffman', '5', 'stuff here...');

SELECT * FROM Posts

DELETE FROM Posts

TRUNCATE TABLE Ratings

SELECT post.*, rating.* 
FROM Posts post INNER JOIN Ratings rating 
ON post.Id = rating.PostId

SELECT DISTINCT p.*, AVG(r.Score) average_rating
FROM Posts p INNER JOIN Ratings r 
ON p.Id = r.PostId GROUP BY p.Id, p.Author, p.Title, p.Permalink, p.PostContent, p.CreateDate, p.UpdateDate;

SELECT DISTINCT p.*, AVG(r.Score) average_rating, COUNT(r.Id) reviews
FROM Posts p LEFT OUTER JOIN Ratings r 
ON p.Id = r.PostId WHERE p.Id = 4 GROUP BY p.Id, p.Author, p.Title, p.Permalink, p.PostContent, p.CreateDate, p.UpdateDate;

SELECT p.*, r.*
FROM Posts p LEFT OUTER JOIN Ratings r 
ON p.Id = r.PostId

UPDATE Ratings
SET Score = 0 WHERE Score IS NULL

SELECT post.Id, rating.PostId, AVG(rating.Score) average, COUNT(rating.Id) reviews
FROM Posts post INNER JOIN Ratings rating
ON post.Id = rating.PostId
GROUP BY post.Id, rating.PostId;

INSERT INTO Ratings
	(PostId, Score)
	VALUES (4, 5);

/* Get by Id statement */
SELECT DISTINCT p.*, AVG(r.Score) average_rating, COUNT(r.Id) reviews 
FROM Posts p LEFT OUTER JOIN Ratings r 
ON p.Id = r.PostId 
WHERE p.Id = 4 
GROUP BY p.Id, p.Author, p.Title, p.Permalink, p.PostContent, p.CreateDate, p.UpdateDate;

UPDATE Posts SET Title = 'editd', Author = 'edited', Permalink = 'edited', PostContent = 'edited', UpdateDate = getdate() WHERE Id = 2

SELECT DISTINCT p.*, AVG(r.Score) average_rating, COUNT(r.Id) reviews FROM Posts p LEFT OUTER JOIN Ratings r ON p.Id = r.PostId WHERE p.Id = 4 GROUP BY p.Id, p.Author, p.Title, p.Permalink, p.PostContent, p.CreateDate, p.UpdateDate