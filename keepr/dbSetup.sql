CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

-- STUB Keep's (Post)

CREATE TABLE IF NOT EXISTS keeps(
id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
creatorId VARCHAR(255) NOT NULL,
name VARCHAR(255) NOT NULL,
description TEXT NOT NULL,
img VARCHAR(255) NOT NULL,
views INT NOT NULL DEFAULT 0,
kept VARCHAR(255) NOT NULL DEFAULT 0,

FOREIGN KEY (creatorId) REFERENCES accounts(id)
) default charset utf8 COMMENT '';


DROP TABLE keeps;


INSERT INTO keeps
(creatorId, name, description, img)
VALUES
('6307add455eeec07370f20c3', 'Lamborghini Countach', 'This lambo is super nice, i love it so much. once i get out of this course im going to buy one. the color is perfect too.', 'https://images.unsplash.com/photo-1663814324463-a41632b2d50f?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=2597&q=80');


INSERT INTO keeps
(creatorId, name, description, img)
VALUES
('6307add455eeec07370f20c3', 'Pink RWB Porche', 'This Porche is very very nice. This picture was actually taken in my personal garage. I just had this porche custom made and sent over to me. Drove it about 11 miles and now it\'ll sit in my garage for eternity!', 'https://images.unsplash.com/photo-1559533675-8471a38d44ef?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80');

INSERT INTO keeps
(creatorId, name, description, img)
VALUES
('6307add455eeec07370f20c3', 'Dumb Electric Car', 'Look at this dumb stupid car! It sucks so much. I hate inovative and no emmission cars. So stupid! I like my cars to make loud boom and pop beacuse loud noise is nice. I want to spend a fortune on burning fossil fuels for the rest of my life!!!', 'https://images.unsplash.com/photo-1647934441921-4ed1e182e4b3?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80');

INSERT INTO keeps
(creatorId, name, description, img)
VALUES
('632d2db050713c8a5beee118', 'Big Tree', 'I love trees!', 'https://images.unsplash.com/photo-1542544703-60b14024561f?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=687&q=80');


SELECT 
 k.*,
 a.*
 FROM keeps k
 JOIN accounts a ON k.creatorId = a.id
 GROUP BY (k.id);
 

-- STUB Vault (collections of keeps)
CREATE TABLE IF NOT EXISTS vaults(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL, 
  description TEXT NOT NULL, 

  FOREIGN KEY (creatorId) REFERENCES accounts(id)
) default charset utf8 COMMENT '';

DROP TABLE vaults;

INSERT INTO vaults
(creatorId, name, description)
VALUES
('6307add455eeec07370f20c3', 'Cars', 'My collection of cars');

INSERT INTO vaults
(creatorId, name, description)
VALUES
('632d2db050713c8a5beee118', 'Favorite Trees', 'My favorite trees');


-- STUB VaultKeep (realtionship betweens vaults and keeps(many to many))
CREATE TABLE IF NOT EXISTS vaultkeeps(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  vaultId INT NOT NULL,
  keepId INT NOT NULL,

  FOREIGN KEY (creatorId) REFERENCES accounts(id),
  FOREIGN KEY (vaultId) REFERENCES vaults (id),
  FOREIGN KEY (keepId) REFERENCES keeps (id)
)default charset utf8 COMMENT '';

DROP TABLE vaultkeeps;

INSERT INTO vaultkeeps
(creatorId, vaultId, keepId)
VALUES
('6307add455eeec07370f20c3', 1, 2);