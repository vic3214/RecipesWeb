-- DROP TABLES

DROP TABLE IF EXISTS Ingredients;
DROP TABLE IF EXISTS Directions;
DROP TABLE IF EXISTS NER;
DROP TABLE IF EXISTS Recipes;
DROP TABLE IF EXISTS TempRecipes;

-- CREATE TABLES
-- Tabla de Recetas
CREATE TABLE Recipes (
                         RecipeID INT IDENTITY(1,1) PRIMARY KEY,
                         Title NVARCHAR(MAX),
                         Link NVARCHAR(255),
                         Source NVARCHAR(50),
                         Site NVARCHAR(100)
);

-- Tabla de Ingredientes
CREATE TABLE Ingredients (
                             IngredientID INT IDENTITY(1,1) PRIMARY KEY,
                             RecipeID INT,
                             Ingredient NVARCHAR(MAX),
                             FOREIGN KEY (RecipeID) REFERENCES Recipes(RecipeID)
);

-- Tabla de Direcciones
CREATE TABLE Directions (
                            DirectionID INT IDENTITY(1,1) PRIMARY KEY,
                            RecipeID INT,
                            Step NVARCHAR(100),
                            FOREIGN KEY (RecipeID) REFERENCES Recipes(RecipeID)
);

-- Tabla de NER
CREATE TABLE NER (
                     NerID INT IDENTITY(1,1) PRIMARY KEY,
                     RecipeID INT,
                     Ingredient NVARCHAR(100),
                     FOREIGN KEY (RecipeID) REFERENCES Recipes(RecipeID)
);

-- Tabla temporal para cargar los datos inicialmente y traspasarlos a las demas tablas
CREATE TABLE TempRecipes (
                             Title NVARCHAR(MAX),
                             Ingredients NVARCHAR(MAX),
                             Directions NVARCHAR(MAX),
                             Link NVARCHAR(MAX),
                             Source NVARCHAR(MAX),
                             NER NVARCHAR(MAX),
                             Site NVARCHAR(MAX)
);

-- END TABLE CREATIONS

-- Carga de datos en la tabla temporal

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

-- END CARGA DE DATOS
     
-- Cargar Title, Link, Source y Site en tabla Recipes

INSERT INTO recipes (title, link, source, site)
SELECT title, link, source, site
FROM temprecipes;

-- END Carga Title, Link, Source y Site en tabla Recipes

-- Cargar NER

-- Crear una tabla temporal para dividir los ingredientes
CREATE TABLE #Ingredients (RecipeID INT, Ingredient NVARCHAR(MAX));

-- Usar una CTE para asignar RecipeID basado en el n�mero de fila y procesar los ingredientes directamente
;WITH CTE_TempRecipes AS (
    SELECT
        ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RecipeID,
            REPLACE(REPLACE(REPLACE(Ner, '[', ''), ']', ''), '"', '') AS CleanedNer
    FROM TempRecipes
)
-- Insertar los ingredientes en la tabla temporal dividiendo la cadena
 INSERT INTO #Ingredients (RecipeID, Ingredient)
SELECT
    RecipeID,
    LTRIM(RTRIM(value)) AS Ingredient
FROM CTE_TempRecipes
    CROSS APPLY STRING_SPLIT(CleanedNer, ',');

-- Insertar los ingredientes en la tabla NER
INSERT INTO NER (RecipeID, Ingredient)
SELECT RecipeID, Ingredient FROM #Ingredients;

-- Limpiar la tabla temporal
DROP TABLE #Ingredients;

-- END Cargar NER

-- Carga de Ingredientes

-- Usar una CTE para asignar RecipeID y extraer el contenido entre corchetes
;WITH CTE_TempRecipes AS (
    SELECT
        ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RecipeID, 
        -- Extraer contenido entre corchetes
            SUBSTRING(
                    Ingredients,
                    CHARINDEX('[', Ingredients) + 1,
                    CHARINDEX(']', Ingredients) - CHARINDEX('[', Ingredients) - 1
            ) AS IngredientsList
    FROM TempRecipes
    WHERE CHARINDEX('[', Ingredients) > 0 AND CHARINDEX(']', Ingredients) > CHARINDEX('[', Ingredients)
)
-- Dividir cada ingredient en frases separadas por comillas dobles
 INSERT INTO #Ingredients (RecipeID, Ingredient)
SELECT
    RecipeID,
    LTRIM(RTRIM(value)) AS Ingredient
FROM CTE_TempRecipes
    CROSS APPLY STRING_SPLIT(IngredientsList, '"')
WHERE LTRIM(RTRIM(value)) <> '' -- Evitar valores vac�os
  AND value <> ',';               -- Evitar comas solas

-- Insertar los pasos procesados en la tabla Ingredients
INSERT INTO Ingredients (RecipeID, Ingredient)
SELECT RecipeID, Ingredient
FROM #Ingredients
WHERE Ingredient IS NOT NULL AND Ingredient <> '';

-- Limpiar la tabla temporal
DROP TABLE #Ingredients;

-- END Carga de Ingredientes

-- Carga de Directions
-- Crear una tabla temporal para almacenar los pasos individuales
CREATE TABLE #Steps (RecipeID INT, Step NVARCHAR(MAX));

-- Usar una CTE para asignar RecipeID y extraer el contenido entre corchetes
;WITH CTE_TempRecipes AS (
    SELECT
        ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RecipeID, 
        -- Extraer contenido entre corchetes
            SUBSTRING(
                    Directions,
                    CHARINDEX('[', Directions) + 1,
                    CHARINDEX(']', Directions) - CHARINDEX('[', Directions) - 1
            ) AS DirectionsList
    FROM TempRecipes
    WHERE CHARINDEX('[', Directions) > 0 AND CHARINDEX(']', Directions) > CHARINDEX('[', Directions)
)
-- Dividir cada direcci�n en frases separadas por comillas dobles
 INSERT INTO #Steps (RecipeID, Step)
SELECT
    RecipeID,
    LTRIM(RTRIM(value)) AS Step
FROM CTE_TempRecipes
    CROSS APPLY STRING_SPLIT(DirectionsList, '"')
WHERE LTRIM(RTRIM(value)) <> '' -- Evitar valores vac�os
  AND value <> ',';               -- Evitar comas solas

-- Insertar los pasos procesados en la tabla Directions
INSERT INTO Directions (RecipeID, Step)
SELECT RecipeID, Step
FROM #Steps
WHERE Step IS NOT NULL AND Step <> '';

-- Limpiar la tabla temporal
DROP TABLE #Steps;

-- END Carga de Directions

-- Eliminar tabla temporal

DROP TABLE TempRecipes

-- END Eliminar tabla temporal
