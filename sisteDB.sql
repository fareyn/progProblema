Create Database Siste;

Use Siste;

Create Table genero(
id varChar(10) NOT NULL PRIMARY KEY,
precio decimal(5,2) NOT NULL Default 0
);

Create Table pelicula(
id int identity(1,1) PRIMARY KEY,
nombre varChar(50) NOT NULL,
genero varChar(10) NOT NULL Foreign Key references genero(id) on delete cascade,
sinopsis varChar(255) NULL,
falta date default getdate(),
disponible bit default '1',
fretorno date default getdate(),
);
Create Procedure Rentar @mid int
AS
Begin
Update pelicula set disponible='0',fretorno= DateADD(day,2,getDate()) where id=@mid
End;

Create procedure finRenta(@mid int)
AS
begin
Update pelicula set disponible='1', fretorno= GETDATE() where id=@mid;
end;

insert into genero(id, precio) values('terror','50');
insert into genero(id, precio) values('comedia','20');
insert into genero(id, precio) values('drama','100');
insert into genero(id, precio) values('adulto','500');


--executed
--show if rentada
Create function pRen(@mid int)
returns table
as
return
Select id, nombre, disponible, fretorno
From pelicula
where id= @mid and disponible='0';

Create function pDisp(@mid int)
returns table
as
return
Select p.id,p.nombre,p.genero,p.sinopsis,p.falta,p.disponible,g.precio
From pelicula as p, genero as g
where p.genero=g.id and p.disponible='1' and p.id=@mid; 

