create database BookStore;
use BookStore;

CREATE TABLE UserRegister(
	id int primary key identity(1,1),
    name varchar(20),
    phone varchar(20),
	email  varchar(20),
	password varchar(20)
);

Create Procedure AddUser(
@name varchar(20),
@phone varchar(20), 
@email varchar(20),
@password varchar(20) 
)
As
begin insert Into UserRegister values(@name,@phone,@email,@password)
End

Create Procedure EditUser(
@name varchar(20),
@phone varchar(20), 
@email varchar(20),
@password varchar(20)
)
As
begin
update UserRegister set name=@name,phone=@phone,email=@email,password=@password where email=@email
End

Create Procedure GetUser(
@email varchar(20)
)
As
Begin Select * from UserRegister where email=@email
End

CREATE TABLE Book(
	bookid int primary key identity(1,1),
    bookname varchar(20),
    bookdecription varchar(30),
	bookauthor  varchar(20),
	image varchar(100),
	bookcount int,
	bookprice float,
	rating float
);

drop table Book;

Alter Procedure spAddBook(
@bookname varchar(20),
@bookdecription varchar(30), 
@bookauthor varchar(20),
@image varchar(100),
@bookcount int,
@bookprice float,
@rating float 
)
As
begin insert Into Book values(@bookname,@bookdecription,@bookauthor,@image,@bookcount,@bookprice,@rating)
End

Create Procedure spGetAllBooks
As
begin select * from Book
End

Create Procedure spGetBook(
@bookid int
)
As
Begin Select * from Book where bookid=@bookid
End

Alter Procedure EditBook(
@bookid int,
@bookname varchar(20),
@bookdecription varchar(30), 
@bookauthor varchar(20),
@image varchar(100),
@bookcount varchar(20),
@bookprice varchar(20),
@rating varchar(20)
)
As
begin
update Book set bookname=@bookname,bookdecription=@bookdecription,bookauthor=@bookauthor,image=@image,bookcount=@bookcount,bookprice=@bookprice,rating=@rating where bookid=@bookid
End

CREATE TABLE Wishlist(
	wishlistid int primary key identity(1,1),
	bookid int,
    userid int,
);
drop table Wishlist

CREATE PROCEDURE spGetWishList
    @userid INT
	AS
BEGIN
    SELECT *
    FROM Wishlist inner join Book on
    Wishlist.bookid = Book.bookid where userid = @userid
END;

Create Procedure spAddWishlist
 @userid INT,
    @bookid INT
AS
BEGIN
    insert Into Wishlist values(@bookid,@userid)
END;


Create procedure spDeleteBook
(
	@bookid int
)
As
Begin
Delete from Book where bookid=@bookid;
End

Create procedure spDeleteWishList
(
	@wishlistid int
)
As
Begin
Delete from Wishlist where wishlistid=@wishlistid;
End

------cart------
create TABLE Cart
(
    cartid INT PRIMARY KEY IDENTITY(1,1),
    bookid INT,
    userid INT,
    CONSTRAINT bookid FOREIGN KEY (bookid) REFERENCES Book(bookid)
);
drop table Cart;

CREATE PROCEDURE spGetCart
    @userid INT
	AS
BEGIN
    SELECT *
    FROM Cart inner join Book on
    Cart.bookid = Book.bookid where userid = @userid
END;

alter Procedure spAddCart
 @userid INT,
    @bookid INT,
	@bookcount int
AS
BEGIN
begin try
    insert Into Cart values(@bookid,@userid,@bookcount)
	End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
END;

Create procedure spDeleteCart
(
	@cartid int
)
As
Begin
begin try
Delete from Cart where cartid=@cartid;
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
End

------customer details

CREATE TABLE CustomerDetails
(
id int primary key identity(1,1),
    customername varchar(20),
    phone varchar(20),
    address varchar(30),
	city varchar(20),
	state varchar(20),
	typeid int,
	userid int
    CONSTRAINT typeid FOREIGN KEY (typeid) REFERENCES Type(typeid),
	CONSTRAINT userid FOREIGN KEY (userid) REFERENCES UserRegister(id)
);
drop table CustomerDetails

Alter PROCEDURE spGetCustomerDetails
    @userid INT
	AS
BEGIN
begin try
    SELECT *
    FROM CustomerDetails inner join Type on
    CustomerDetails.typeid = Type.typeid where userid = @userid
	End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
END; 

Create Procedure spAddCustomerDetails
 @customername varchar(20),
    @phone varchar(20),
    @address varchar(30),
	@city varchar(20),
	@state varchar(20),
	@typeid int,
	@userid int
AS
BEGIN
begin try
    insert Into CustomerDetails values(@customername,@phone,@address,@city,@state,@typeid,@userid)
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
END;
use BookStore;

Alter Procedure spEditCustomer(
@customername varchar(20),
@customerid int,
    @phone varchar(20),
    @address varchar(30),
	@city varchar(20),
	@state varchar(20),
	@typeid int,
	@userid int
)
As
begin
update CustomerDetails set address=@address,customername=@customername,phone=@phone,city=@city,state=@state,typeid=@typeid where id=@customerid
End

Create procedure spDeleteCustomer
(
	@userid int,
	@customerid int
)
As
Begin
begin try
Delete from CustomerDetails where userid=@userid and id=@customerid;
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
End

----feedback

CREATE TABLE CustomerFeedback
(
    feedbackid INT PRIMARY KEY IDENTITY(1,1),
    bookid INT,
    userid INT,
    description VARCHAR(30),
    rating FLOAT,
    FOREIGN KEY (bookid) REFERENCES Book(bookid),
    FOREIGN KEY (userid) REFERENCES UserRegister(id)
);

Alter Procedure spAddCustomerFeedback
	@bookid int,
	@userid int,
	@description varchar(30),
	@rating float
AS
BEGIN
begin try
    insert Into CustomerFeedback values(@bookid,@userid,@description,@rating)
	End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
END;

alter procedure spGetFeedback
@bookid int
as
begin
select *
FROM
	CustomerFeedback
 inner join 
	Book on CustomerFeedback.bookid=Book.bookid 
	inner join
	UserRegister on CustomerFeedback.userid = UserRegister.id where CustomerFeedback.bookid=@bookid;
	end;

create table OrderPlaced
(
	orderid int primary key Identity(1,1),
	customerid int foreign key references CustomerDetails(id),
	cartid int foreign key references Cart(cartid)
	)

	create Procedure spPlaceOrder
	@customerid int,
	@cartid int
AS
BEGIN
begin try
    insert Into OrderPlaced values(@customerid,@cartid)
	end try 
	begin catch
	Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
END;
---------------------------------------------------------
create table OrderSummary
(
summaryid int primary key identity(1,1),
orderid int foreign key references OrderPlaced(orderid)
);

create procedure spGetOrdersummary 
@orderid int
as
begin
select *
FROM
	OrderSummary
INNER JOIN
	OrderPlaced ON OrderSummary.orderid= OrderPlaced.orderid
Inner JOIN
	Cart ON OrderPlaced.cartid=Cart.cartid
 inner join 
	Book on Cart.bookid=Book.bookid where OrderSummary.orderid=@orderid;
	end;