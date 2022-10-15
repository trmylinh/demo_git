create database BanHang
use BanHang

create table tblMatHang(
	MaSP nvarchar(5) primary key not null,
	TenSP nvarchar(30),
	NgaySX date,
	NgayHH date,
	DonVi nvarchar(10),
	DonGia float,
	GhiChu nvarchar(200)

);
select * from tblMatHang

insert tblMatHang(MaSP, TenSP, NgaySX, NgayHH, DonVi, DonGia) 
values ('SP001','Ban phim Dell','2015/01/03','2018/08/15','Chiec','500000'),('SP002','Chuot khong day','2015/01/03','2018/08/15','Cai','150000');
