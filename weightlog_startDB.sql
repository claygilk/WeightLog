--USE master
--GO

DROP DATABASE IF EXISTS weight_log
GO

CREATE DATABASE weight_log
GO

USE weight_log
GO

CREATE TABLE users (
	id INTEGER IDENTITY PRIMARY KEY,
	name NVARCHAR(120) NOT NULL,
	password_hash NVARCHAR(120) NOT NULL
)

CREATE TABLE lifts (
	id INTEGER IDENTITY PRIMARY KEY,
	name NVARCHAR(50) NOT NULL,
	category NVARCHAR(50),
)

CREATE TABLE set_log (
	id INTEGER IDENTITY PRIMARY KEY,
	date DATE NOT NULL,
	lift_id INTEGER FOREIGN KEY REFERENCES lifts(id),
	user_id INTEGER FOREIGN KEY REFERENCES users(id),
	weight INTEGER,
	reps INTEGER,
	rpe INTEGER CHECK(rpe < 11 AND rpe > 0),
)

CREATE TABLE maxes (
	id INTEGER IDENTITY PRIMARY KEY,
	user_id INTEGER FOREIGN KEY REFERENCES users(id),
	lift_id INTEGER FOREIGN KEY REFERENCES lifts(id),
	max_weight INTEGER NOT NULL
)


INSERT INTO users(name, password_hash) VALUES ('clay', 'clayhash');
INSERT INTO users(name, password_hash) VALUES ('alice', 'alicehash');
INSERT INTO users(name, password_hash) VALUES ('bob', 'bobhash');

INSERT INTO lifts (name) VALUES ('Squat');
INSERT INTO lifts (name) VALUES ('Bench');
INSERT INTO lifts (name) VALUES ('Press');
INSERT INTO lifts (name) VALUES ('Deadlift');

INSERT INTO set_log (date, lift_id, user_id, weight, reps, rpe)  VALUES('07-06-2021', 1, 1, 205, 5, 7); 
INSERT INTO set_log (date, lift_id, user_id, weight, reps, rpe)  VALUES('07-06-2021', 1, 1, 235, 5, 7); 
INSERT INTO set_log (date, lift_id, user_id, weight, reps, rpe)  VALUES('07-06-2021', 1, 1, 260, 5, 7); 
INSERT INTO set_log (date, lift_id, user_id, weight, reps, rpe)  VALUES('07-06-2021', 1, 1, 235, 5, 7); 
INSERT INTO set_log (date, lift_id, user_id, weight, reps, rpe)  VALUES('07-06-2021', 1, 1, 205, 5, 7); 

INSERT INTO set_log (date, lift_id, user_id, weight, reps)  VALUES('07-06-2021', 4, 1, 385, 5); 
INSERT INTO set_log (date, lift_id, user_id, weight, reps)  VALUES('07-06-2021', 4, 1, 385, 5); 
INSERT INTO set_log (date, lift_id, user_id, weight, reps)  VALUES('07-06-2021', 4, 1, 385, 5); 
INSERT INTO set_log (date, lift_id, user_id, weight, reps)  VALUES('07-06-2021', 4, 1, 385, 5); 
INSERT INTO set_log (date, lift_id, user_id, weight, reps)  VALUES('07-06-2021', 4, 1, 385, 5); 

INSERT INTO set_log (date, lift_id, user_id, weight, reps)  VALUES('07-06-2021', 2, 2, 205, 5); 
INSERT INTO set_log (date, lift_id, user_id, weight, reps)  VALUES('07-06-2021', 2, 2, 235, 5); 
INSERT INTO set_log (date, lift_id, user_id, weight, reps)  VALUES('07-06-2021', 2, 2, 260, 5); 
INSERT INTO set_log (date, lift_id, user_id, weight, reps)  VALUES('07-06-2021', 2, 2, 235, 5); 
INSERT INTO set_log (date, lift_id, user_id, weight, reps)  VALUES('07-06-2021', 2, 2, 205, 5); 

INSERT INTO set_log (date, lift_id, user_id, weight, reps)  VALUES('07-06-2021', 2, 3, 385, 5); 
INSERT INTO set_log (date, lift_id, user_id, weight, reps)  VALUES('07-06-2021', 2, 3, 385, 5); 
INSERT INTO set_log (date, lift_id, user_id, weight, reps)  VALUES('07-06-2021', 2, 3, 385, 5); 
INSERT INTO set_log (date, lift_id, user_id, weight, reps)  VALUES('07-06-2021', 2, 3, 385, 5); 
INSERT INTO set_log (date, lift_id, user_id, weight, reps)  VALUES('07-06-2021', 2, 3, 385, 5); 

INSERT INTO maxes (user_id, lift_id, max_weight) VALUES (1, 1, 275);
INSERT INTO maxes (user_id, lift_id, max_weight) VALUES (1, 2, 225);
INSERT INTO maxes (user_id, lift_id, max_weight) VALUES (1, 3, 385);
INSERT INTO maxes (user_id, lift_id, max_weight) VALUES (1, 4, 136);

INSERT INTO maxes (user_id, lift_id, max_weight) VALUES (2, 1, 200);
INSERT INTO maxes (user_id, lift_id, max_weight) VALUES (2, 2, 150);
INSERT INTO maxes (user_id, lift_id, max_weight) VALUES (3, 3, 300);
INSERT INTO maxes (user_id, lift_id, max_weight) VALUES (3, 4, 350);

select * from users;
select * from set_log;
select * from lifts;
select * from maxes;

SELECT m.max_weight AS max FROM maxes m
JOIN lifts l ON l.id = m.lift_id
WHERE l.name = 'Squat'
AND m.user_id = 1;