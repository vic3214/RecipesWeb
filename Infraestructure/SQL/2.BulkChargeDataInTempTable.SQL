BULK INSERT TempRecipes
FROM 'C:\Users\victo\OneDrive\Escritorio\recipes_data.csv'
WITH (
    FIELDTERMINATOR = ',',  -- Delimitador de campos
    ROWTERMINATOR = '\n',   -- Delimitador de filas
    FIRSTROW = 2,           -- Omitir la fila de encabezado
    CODEPAGE = 'UTF-8',     -- Usar UTF-8
	FORMAT = 'CSV',         -- Cargar datos soportando csv
	FIELDQUOTE = '"'        -- Separacion de los campos del csv
);
