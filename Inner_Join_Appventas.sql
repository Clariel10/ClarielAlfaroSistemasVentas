use sistema_ventas;

select ID, Email, tipo_rol

from tb_usuarios
inner join roles_usuarios

on tb_usuarios.Id = roles_usuarios.Id_Rol_Usuario

