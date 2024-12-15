-- Crear una tabla temporal para dividir los ingredientes
CREATE TABLE TempNER (RecipeId INT, Ingredient NVARCHAR(MAX));

-- Usar una CTE para asignar RecipeID basado en el n�mero de fila y procesar los ingredientes directamente
;WITH CTE_TempRecipes AS (
    SELECT
        ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RecipeID,
        REPLACE(REPLACE(REPLACE(Ner, '[', ''), ']', ''), '"', '') AS CleanedNer
    FROM TempRecipes
)
-- Insertar los ingredientes en la tabla temporal dividiendo la cadena
 INSERT INTO TempNER (RecipeId, Ingredient)
 SELECT
     RecipeId,
     LTRIM(RTRIM(value)) AS Ingredient
 FROM CTE_TempRecipes
          CROSS APPLY STRING_SPLIT(CleanedNer, ',');

-- Insertar los ingredientes en la tabla NER
INSERT INTO NER (RecipeId, Ingredient)
SELECT RecipeId, Ingredient FROM TempNER;

-- Limpiar la tabla temporal
DROP TABLE TempNER;
