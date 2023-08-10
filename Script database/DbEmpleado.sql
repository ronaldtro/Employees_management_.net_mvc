create database DbEmpleado

use DbEmpleado

create table departamento (
	idDepartamento int primary key identity(1,1), 
	nombre varchar(50) 
)

create table empleado (
	idEmpleado int primary key identity(1,1),
	nombreCompleto varchar(50),
	idDepartamento int references departamento(idDepartamento),
	sueldo int,
	fechaContrato date
)

insert into departamento(nombre) values ('Administracion'), ('Marketing'), ('Ventas'), ('Comercio')

insert into empleado(nombreCompleto, idDepartamento, sueldo, fechaContrato) values ('Ronald Castro', 2, 500, getdate())


-- Procedimientos almacenados 

create procedure sp_listaDepartamentos
as 
begin
	select idDepartamento, nombre from departamento
end


create procedure sp_listaEmpleados
as
begin
	set dateformat dmy
	select e.idEmpleado, e.nombreCompleto, d.idDepartamento, d.nombre, e.sueldo, convert(char(10), e.fechaContrato, 103) as 'fechaContrato'  from empleado as e
	inner join departamento as d on e.idDepartamento = d.idDepartamento 
end

create procedure sp_guardarEmpleado(
	@nombreCompleto varchar(50),
	@idDepartamento int,
	@sueldo int,
	@fechaContrato varchar(10)
)
as
begin
	set dateformat dmy
	insert into empleado(nombreCompleto, idDepartamento, fechaContrato, sueldo) values (@nombreCompleto, @idDepartamento, convert(date, @fechaContrato) , @sueldo)
end


create procedure sp_editarEmpleado(
	@idEmpleado int,
	@nombreCompleto varchar(50),
	@idDepartamento int,
	@sueldo int,
	@fechaContrato varchar(10)
)
as
begin
	set dateformat dmy
	update empleado set 
	nombreCompleto = @nombreCompleto,
	idDepartamento = @idDepartamento,
	sueldo = @sueldo,
	fechaContrato = convert(date, @fechaContrato)
	where idEmpleado = @idEmpleado
end


create procedure sp_eliminarEmpleado(@idEmpleado int)
as 
begin
	delete from empleado where idEmpleado = @idEmpleado
end