/*
================================================================================
 INSTRUCCIONES PARA EJECUTAR ESTE SCRIPT (schema.sql)
================================================================================

Este script crea la base de datos "readhub", sus tablas y carga los datos
desde archivos CSV usando LOAD DATA LOCAL INFILE.

IMPORTANTE:
- NO usar MySQL Shell (mysqlsh) para ejecutar este script.
- NO usar X Protocol (puerto 33060).
- NO usar MySQL Workbench para importar CSV.
- SOLO usar el cliente clásico de MySQL (mysql.exe).

--------------------------------------------------------------------------------
 PASO 1 — Abrir PowerShell o CMD y ubicarse en el directorio del script
--------------------------------------------------------------------------------

cd C:\Users\javie\Repositories\ReadHub\src\backend\data

Asegúrate de que en este directorio existan:
- schema.sql
- users.csv
- books.csv
- transactions.csv

--------------------------------------------------------------------------------
 PASO 2 — Ejecutar el cliente clásico de MySQL con LOCAL INFILE habilitado
--------------------------------------------------------------------------------

mysql --local-infile=1 -u root -p

(ingresar la contraseña cuando la pida)

--------------------------------------------------------------------------------
 PASO 3 — Seleccionar la base de datos (si ya existe)
--------------------------------------------------------------------------------

USE readhub;

--------------------------------------------------------------------------------
 PASO 4 — Ejecutar el script
--------------------------------------------------------------------------------

SOURCE schema.sql;

--------------------------------------------------------------------------------
 NOTAS IMPORTANTES
--------------------------------------------------------------------------------

1. Este script usa rutas relativas:
   LOAD DATA LOCAL INFILE 'users.csv'
   Por eso DEBE ejecutarse desde el mismo directorio donde están los CSV.

2. Si aparece el error:
   ERROR 2068 (HY000): LOAD DATA LOCAL INFILE file request rejected
   significa que NO usaste:
   mysql --local-infile=1 -u root -p

3. Si aparece el error:
   ERROR 3948: Loading local data is disabled
   significa que NO estás usando el cliente clásico (mysql.exe).

4. Para verificar los datos importados:
   SELECT COUNT(*) FROM users;
   SELECT COUNT(*) FROM books;
   SELECT COUNT(*) FROM transactions;

5. Ver los datos:
   SELECT * FROM users;
   SELECT * FROM books;
   SELECT * FROM transactions;

================================================================================
 FIN DE INSTRUCCIONES
================================================================================
*/