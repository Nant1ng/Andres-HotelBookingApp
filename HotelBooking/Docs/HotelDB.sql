USE HotelDB;

--G
SELECT 
  * 
FROM 
  Guest;

SELECT 
  * 
FROM 
  Booking;

SELECT 
  * 
FROM 
  Invoice;

SELECT 
  * 
FROM 
  Room;

SELECT 
  * 
FROM 
  Guest 
WHERE 
  RIGHT(FirstName, 3) = 'ard';

SELECT 
  * 
FROM 
  Guest 
WHERE 
  Age IS NOT NULL;

SELECT 
  * 
FROM 
  Invoice 
WHERE 
  IsPayed = 0;

SELECT 
  * 
FROM 
  Room 
WHERE 
  RoomType = 1;

SELECT 
  * 
FROM 
  Room 
ORDER BY 
  Price;

SELECT 
  * 
FROM 
  Booking 
ORDER BY 
  StartDate;
--

-- VG
SELECT 
  * 
FROM 
  Room 
WHERE 
  Price =(
    SELECT 
      MIN(Price) 
    FROM 
      Room
  );

SELECT 
  COUNT(*) AS AmountOfBookings, 
  Guest.FirstName 
FROM 
  Booking 
  JOIN Guest ON Booking.GuestId = Guest.Id 
GROUP BY 
  GuestId, 
  Guest.FirstName 
ORDER BY 
  AmountOfBookings DESC;

SELECT 
  CONCAT(startDate, ' - ', endDate) AS 'CheckIn - CheckOut', 
  AmountOfGuest, 
  ExtraBed, 
  Room.RoomName, 
  CONCAT(
    Guest.FirstName, ' ', Guest.LastName
  ) AS Fullname, 
  Invoice.Total 
FROM 
  Booking 
  JOIN Room ON Booking.RoomId = Room.Id 
  JOIN Guest ON Booking.GuestId = Guest.Id 
  JOIN Invoice ON Booking.InvoiceId = Invoice.Id;

SELECT 
    RoomId,
	Room.RoomName,
    (SELECT AVG(AmountOfGuest) 
     FROM Booking 
     WHERE IsActive = 1 AND RoomId = RoomId) AS TotalGuestsPerRoom
FROM 
    Booking
JOIN 
    Room ON RoomId = Room.Id 
GROUP BY 
    RoomId, Room.RoomName;

SELECT 
  CONCAT(
    FirstName, ' ', LastName
  ) AS fullname, 
  RIGHT(
    REPLACE(PhoneNumber, ' ', ''), 
    4
  ) AS code 
FROM 
  Guest;