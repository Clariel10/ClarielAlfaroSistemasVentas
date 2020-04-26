--OJO DEBERAS CREAR LAS RELACIONES POR TI MISMO, AQUI TE DEJO SOLO LAS TABLAS.
drop database sistema_ventas
create database sistema_ventas;

Create table tb_venta(
idVenta int primary Key identity(1,1),
idDocumento int,
iDCliente int,
iDUsuario int,
totalVenta decimal,
fecha Date

);

insert into tb_venta (idDocumento,iDCliente,iDUsuario,totalVenta,fecha) values (1,1,1,5.5,GETDATE());

create table tb_documento(
iDDocumento int primary key identity (1,1),
nombreDocumento varchar (50)
);
insert into tb_documento (nombreDocumento) values ('TICKET');
insert into tb_documento (nombreDocumento) values ('FACTURA');

create table tb_cliente (
iDCliente int primary key identity(1,1),
nombreCliente varchar (50),
direccionCliente varchar(50),
duiCliente varchar(50)
);

insert into tb_cliente(nombreCliente,direccionCliente,duiCliente) values ('Clariel','Chalatenango','000000017');
insert into tb_cliente(nombreCliente,direccionCliente,duiCliente) values ('Jenny','Chalatenango','15654545132');

create table detalleVenta(
iDDetalleVenta int primary key identity(1,1),
idVenta int,
idProducto int,
cantidad int,
precio Decimal(12,5),
total Decimal
);

create table producto(
idProducto int primary key identity (1,1),
nombreProducto varchar(50),
precioProducto varchar (50),
estadoProducto varchar (50)
);

--Llaves Foraneas

alter table roles_usuarios add foreign key (Id_Usuario) references tb_usuarios(Id);
Alter table tb_venta add foreign key (iDDocumento) references tb_documento(idDocumento);
Alter table tb_venta add foreign key (iDCliente) references tb_cliente(iDCliente);
Alter table tb_venta add foreign key (iDUsuario) references tb_usuarios(Id);
Alter table detalleVenta add foreign key (idVenta) references tb_venta(idVenta);
Alter table detalleVenta add foreign key (idProducto) references producto(idProducto);

insert into producto (nombreProducto,precioProducto,estadoProducto) values ('cama','50','nuevo');
insert into producto (nombreProducto,precioProducto,estadoProducto) values ('computadora','150','nuevo');
insert into producto (nombreProducto,precioProducto,estadoProducto) values ('impresora','50','usado');
insert into producto (nombreProducto,precioProducto,estadoProducto) values ('mouse','350','nuevo');
insert into producto (nombreProducto,precioProducto,estadoProducto) values ('sofa','50','usado');
insert into producto (nombreProducto,precioProducto,estadoProducto) values ('telefono','450','nuevo');


select * from tb_usuarios