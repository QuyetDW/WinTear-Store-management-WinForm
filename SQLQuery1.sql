CREATE DATABASE QuanLyWinTear
GO

USE QuanLyWinTear
GO

CREATE TABLE TableFood
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) DEFAULT N'Bạn chưa có tên',
	status NVARCHAR(100) DEFAULT N'Trống'
)
GO

CREATE TABLE Account
(
	UserName NVARCHAR(100) PRIMARY KEY,
	DisplayName NVARCHAR(100)NOT NULL DEFAULT N'WinTear',
	PassWord NVARCHAR(100) NOT NULL DEFAULT 0,
	Type INT NOT NULL DEFAULT 0
)
GO

CREATE TABLE FoodCategory
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên'
)
GO

CREATE TABLE Food
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
	idCategory INT NOT NULL,
	price FLOAT NOT NULL DEFAULT 0

	FOREIGN KEY (idCategory) REFERENCES dbo.FoodCategory(id)
)
GO

CREATE TABLE Bill
(
	id INT IDENTITY PRIMARY KEY,
	DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	DateCheckOut DATE,
	idTable INT NOT NULL,
	status INT NOT NULL DEFAULT 0

	FOREIGN KEY (idTable) REFERENCES dbo.TableFood(id)
)
GO

CREATE TABLE BillInfo
(
	id INT IDENTITY PRIMARY KEY,
	idBill INT NOT NULL,
	idFood INT NOT NULL,
	count INT NOT NULL DEFAULT 0

	FOREIGN KEY (idBill) REFERENCES dbo.Bill(id),
	FOREIGN KEY (idFood) REFERENCES dbo.Food(id)
)
GO



INSERT INTO dbo.Account (UserName, DisplayName, PassWord, Type)
VALUES (N'winner', N'winner', N'123', 0)
INSERT INTO dbo.Account (UserName, DisplayName, PassWord, Type)
VALUES (N'quyet', N'quyetnguyen', N'abc', 1)
INSERT INTO dbo.Account (UserName, DisplayName, PassWord, Type)
VALUES (N'quyetdw', N'quyetdw', N'9203', 0)
INSERT INTO dbo.Account (UserName, DisplayName, PassWord, Type)
VALUES (N'wintear', N'wintear', N'aaa', 1)
GO

INSERT dbo.FoodCategory (name)
VALUES (N'Trà')
INSERT dbo.FoodCategory (name)
VALUES (N'Trà sữa')
INSERT dbo.FoodCategory (name)
VALUES (N'Sinh tố')
INSERT dbo.FoodCategory (name)
VALUES (N'Coffee')
INSERT dbo.FoodCategory (name)
VALUES (N'Ăn vặt')
INSERT dbo.FoodCategory (name)
VALUES (N'Khác')
GO

INSERT dbo.Food (name, idCategory, price)
VALUES (N'Trà chanh nha đam', 1, 15000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Trà đào cam sả', 1, 30000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Trà quất lắc sữa', 1, 20000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Trà dâu hạt chia', 1, 25000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Trà sữa Socola', 2, 25000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Trà sữa Matcha đá xay', 2, 30000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Trà sữa trân châu đường đen', 2, 35000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Trà sữa kem cheese nướng', 2, 25000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Sinh tố mãn cầu', 3, 25000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Sinh tố bơ', 3, 25000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Sinh tố ổi', 3, 25000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Sinh tố lúa mạch', 3, 20000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Nâu đá', 4, 25000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Bạc xỉu', 4, 30000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Cafe muối', 4, 30000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Cafe trứng', 4, 30000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Hướng dương ngũ vị', 5, 15000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Phô mai que', 5, 30000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Khoai lang kén', 5, 45000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Bánh cookie', 5, 35000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Bánh bông lan trứng chảy', 5, 70000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Tokbokki', 6, 65000)
INSERT dbo.Food (name, idCategory, price)
VALUES (N'Mỳ cay 7 cấp độ', 6, 75000)
GO

INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status)
VALUES (GETDATE(), GETDATE(), 1, 0)
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status)
VALUES (GETDATE(), GETDATE(), 2, 0)
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status)
VALUES (GETDATE(), GETDATE(), 3, 1)
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status)
VALUES (GETDATE(), GETDATE(), 2, 1)
GO

INSERT dbo.BillInfo (idBill, idFood, count)
VALUES (1, 1, 2)
INSERT dbo.BillInfo (idBill, idFood, count)
VALUES (1, 2, 3)
INSERT dbo.BillInfo (idBill, idFood, count)
VALUES (1, 4, 1)
INSERT dbo.BillInfo (idBill, idFood, count)
VALUES (2, 1, 3)
INSERT dbo.BillInfo (idBill, idFood, count)
VALUES (2, 5, 2)
GO

DECLARE @i INT = 1
WHILE @i <= 10
BEGIN
	INSERT dbo.TableFood (name) VALUES (N'Bàn '+ CAST(@i AS nvarchar(100)))
	SET @i = @i + 1
END
GO




CREATE PROC USP_GetAccountByUserName
@userName nvarchar(100)
AS
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName
END
GO


CREATE PROC USP_GetTableList
AS SELECT * FROM dbo.TableFood
GO


CREATE PROC USP_InsertBill
@idTable INT
AS
BEGIN
	INSERT dbo.Bill(DateCheckIn, DateCheckOut, idTable, status, discount)
	VALUES (GETDATE(), NULL, @idTable, 0, 0)
END
GO


CREATE PROC USP_InsertBillInfo
@idBill INT, @idFood INT, @count INT
AS
BEGIN
	INSERT dbo.BillInfo(idBill, idFood, count)
	VALUES (@idBill, @idFood, @count)
END
GO


alter PROC USP_InsertBillInfo
@idBill INT, @idFood INT, @count INT
AS
BEGIN
	DECLARE @isExitsBillInfo INT
	DECLARE @foodCount INT = 1

	SELECT @isExitsBillInfo = id, @foodCount = b.count
	FROM dbo.BillInfo AS b
	WHERE idBill = @idBill AND idFood = @idFood

	IF(@isExitsBillInfo > 0)
	BEGIN
		DECLARE @newCount INT = @foodCount + @count
		IF (@newCount > 0)
			UPDATE dbo.BillInfo SET count = @foodCount + @count WHERE idFood = @idFood
		ELSE 
			DELETE dbo.BillInfo WHERE idBill = @idBill AND idFood = @idFood
	END
	ELSE
	BEGIN
		INSERT dbo.BillInfo (idBill, idFood, count)
		VALUES (@idBill, @idFood, @count)
	END
END
GO


CREATE TRIGGER UTG_UpdateBillInfo
ON dbo.BillInfo FOR INSERT, UPDATE
AS
BEGIN 
	DECLARE @idBill INT
	SELECT @idBill = idBill FROM inserted
	DECLARE @idTable INT
	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill AND status = 0
	UPDATE dbo.TableFood SET status = N'Có người' WHERE id = @idTable

END
GO


CREATE TRIGGER UTG_UpdateBill
ON dbo.Bill FOR UPDATE
AS
BEGIN
	DECLARE @idBill INT
	SELECT @idBill = id FROM inserted
	DECLARE @idTable INT
	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill
	DECLARE @count INT = 0
	SELECT @count = COUNT(*) FROM dbo.Bill WHERE idTable = @idTable AND status = 0
	IF(@count = 0)
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable
END
GO


	
CREATE USP_SwitchTable
@idTable1 int, @idTable2 int
AS BEGIN

	DECLARE @idFistBill int
	DECLARE @idSeconrdBill int

	DECLARE @isFistTableEmty int = 1
	DECLARE @isSecondTableEmty int = 1

	SELECT @idSeconrdBill = id FROM dbo.Bill WHERE idTable = @idTable2 AND status = 0
	SELECT @idFistBill = id FROM dbo.Bill WHERE idTable = @idTable1 AND status = 0

	IF (@idFistBill IS NULL)
	BEGIN
		INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status)
		VALUES (GETDATE(), NULL, @idTable1, 0)
		SELECT @idFistBill = MAX (id) FROM dbo.Bill WHERE idTable = @idTable1 AND status = 0
	END

		SELECT @idFistBill = MAX (id) FROM dbo.Bill WHERE idTable = @idFistBill
	IF (@idSeconrdBill IS NULL)
	BEGIN
		INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status)
		VALUES (GETDATE(), NULL, @idTable2, 0)
		SELECT @idSeconrdBill = MAX (id) FROM dbo.Bill WHERE idTable = @idTable2 AND status = 0

	END

	SELECT @isSecondTableEmty = COUNT (*) FROM dbo.BillInfo WHERE idBill = @idSeconrdBill
	SELECT id INTO IDBillInfoTable FROM dbo.BillInfo WHERE idBill = @idSeconrdBill
	UPDATE dbo.BillInfo SET idBill = @idSeconrdBill WHERE idBill = @idFistBill
	UPDATE dbo.BillInfo SET idBill = @idFistBill WHERE id IN (SELECT * FROM IDBillInfoTable)
	DROP TABLE IDBillInfoTable

	IF (@isFistTableEmty = 0)
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable2
	IF (@isSecondTableEmty = 0)
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable1
END
GO

ALTER TABLE dbo.Bill
ADD discount INT

UPDATE dbo.Bill SET discount = 0
GO

CREATE PROC USP_GetListBillByDate
@checkIn date, @checkOut date
AS
BEGIN
	SELECT t.name AS [Tên bàn], b.totalPrice AS [Tổng tiền], DateCheckIn AS [Ngày vào], DateCheckOut AS [Ngày ra], discount AS [Giảm giá (%)]
	FROM dbo.Bill AS b, dbo.TableFood AS t
	WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND b.status = 1
	AND t.id = b.idTable
END
GO

CREATE PROC USP_Login
@userName nvarchar(100), @passWord nvarchar(100)
AS
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName AND PassWord = @passWord
END
GO

CREATE PROC USP_UpdateAccount
@userName NVARCHAR(100), @displayName NVARCHAR(100), @password NVARCHAR(100), @newPassword NVARCHAR(100)
AS
BEGIN
	DECLARE @isRightPass INT = 0
	SELECT @isRightPass = COUNT(*) FROM dbo.Account WHERE UserName = @userName AND PassWord = @password
	IF(@isRightPass = 1)
	BEGIN
		IF (@newPassword = NULL OR @newPassword = '')
		BEGIN
			UPDATE dbo.Account SET DisplayName = @displayName WHERE UserName = @userName
		END
		ELSE
			UPDATE dbo.Account SET DisplayName = @displayName, PassWord = @newPassword WHERE UserName = @userName
	END
END
GO

CREATE TRIGGER UTG_DeleteBillInfo
ON dbo.BillInfo FOR DELETE
AS
BEGIN
	DECLARE @idBillInfo INT
	DECLARE @idBill INT
	SELECT @idBillInfo = id, @idBill = deleted.idBill FROM deleted

	DECLARE @idTable INT
	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill

	DECLARE @count INT = 0

	SELECT @count = COUNT(*) FROM dbo.BillInfo AS bi, dbo.Bill AS b WHERE b.id = bi.idBill AND b.id = @idBill AND b.status = 0

	IF (@count = 0)
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable
END
GO



ALTER TABLE dbo.Bill ADD totalPrice FLOAT

EXEC dbo.USP_InsertBill @idTable = 1
EXEC dbo.USP_GetAccountByUserName @userName = N'quyetdw'


SELECT * FROM dbo.TableFood
SELECT * FROM dbo.Food
SELECT * FROM dbo.Account
SELECT * FROM dbo.FoodCategory
SELECT * FROM dbo.Bill
SELECT * FROM dbo.BillInfo

SELECT f.name, bi.count, f.price, f.price*bi.count AS totalPrice FROM dbo.BillInfo AS bi, dbo.Bill AS b, dbo.Food AS f
WHERE bi.idBill = b.id AND bi.idFood = f.id AND b.idTable = 1


