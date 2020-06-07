-- ThinkerThingsCustomers.Customers definition

CREATE TABLE `Customers` (
  `CustomerId`			varchar(32) NOT NULL,
  `Name`				varchar(60) NOT NULL,
  `Address`				varchar(150) NOT NULL,
  `Email`				varchar(60) NOT NULL,
  `DateOfBirth`			datetime NOT NULL,
  `CreatedAt`			timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `UpdatedAt`			timestamp NULL DEFAULT NULL,
  `IsEnable`			bit(1) NOT NULL,
  PRIMARY KEY (`CustomerId`) USING BTREE,
  UNIQUE KEY `Email` (`Email`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;