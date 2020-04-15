use sistema_ventas;

create table roles_usuarios(
Id_Rol_Usuario int primary key identity,
Id_Usuario int,
tipo_rol varchar (50)
);
Go

select * from tb_usuarios