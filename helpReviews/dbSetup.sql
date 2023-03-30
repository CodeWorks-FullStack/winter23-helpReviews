CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

-- Sammys Sammies
-- Andrew's Awesome lemonde
-- Scottys Tots
-- Ry's Wraps
-- Cup of Joe
-- Dusty's Dinner
-- Parker's Pit Pigs

CREATE TABLE restaurants(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  ownerId VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(1000) NOT NULL DEFAULT 'No Description',
  imgUrl VARCHAR(255) NOT NULL DEFAULT 'https://images.unsplash.com/photo-1511914678378-2906b1f69dcf?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&h=400&w=687&q=80',
  category VARCHAR(100) NOT NULL DEFAULT 'un-categorized',
  exposure INT NOT NULL DEFAULT 0,
  shutdown BOOLEAN NOT NULL DEFAULT false,
  
  FOREIGN KEY (ownerId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

INSERT INTO restaurants
(name, description, imgUrl, category, ownerId)
VALUES
("Parker's Pit Pigs", "When pigs fly", 'https://images.unsplash.com/photo-1470858831619-ca02d796b2a5?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1469&q=80', 'Pig', '631b5b5fa7f0b66bb817725a');

CREATE TABLE reviews(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  title VARCHAR(255) NOT NULL,
  body VARCHAR(1000) NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  restaurantId int NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',

  Foreign Key (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  Foreign Key (restaurantId) REFERENCES restaurants(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';


INSERT INTO reviews
(title, body, `restaurantId`, `creatorId`)
VALUES
("It's literally fries and a guy rapping", "I don't know what i was expecting, i guess some sort of play on words? nope. just fries and dude rapping. Fries were good.", 4, '634844a08c9d1ba02348913d');


SELECT
rest.*,
COUNT(rv.id) AS reviewCount,
owner.*
FROM restaurants rest
left JOIN reviews rv ON rest.id = rv.restaurantId
JOIN accounts owner ON owner.id = rest.`ownerId`
GROUP BY rest.id
;

 SELECT
    rest.*,
    COUNT(rv.id) AS reviewCount,
    owner.*
    FROM restaurants rest
    LEFT JOIN reviews rv ON rv.restaurantId = rest.id
    JOIN accounts owner ON rest.ownerId = owner.id
    WHERE rest.id = 4
    GROUP BY rest.id;