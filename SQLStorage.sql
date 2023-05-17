create table Products(
id int IDENTITY(1,1) primary key,
productName nvarchar(100),
companyName nvarchar(100))

create table Orders(
id int IDENTITY(1,1) primary key,
productId int ,
productCount int,
orderStatus nvarchar(20) foreign key references [Status](Stat),
orderDataFrom date,
orderDataTo date
foreign key (productId) references Products (id))

create table [Status](
Stat nvarchar(20) primary key
)

insert into [Status] values ('Принят')
insert into [Status] values ('На склад')
insert into [Status] values ('Продан')
insert into [Status] values ('Все')