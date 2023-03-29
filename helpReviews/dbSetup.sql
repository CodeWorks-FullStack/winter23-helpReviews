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



