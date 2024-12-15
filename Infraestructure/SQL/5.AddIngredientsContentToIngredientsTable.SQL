-- Crear una tabla temporal para almacenar los pasos individuales
CREATE TABLE TempIngredients (RecipeId INT, Ingredient NVARCHAR(MAX));

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
 INSERT INTO TempIngredients (RecipeID, Ingredient)
 SELECT
     RecipeID,
     LTRIM(RTRIM(value)) AS Ingredient
 FROM CTE_TempRecipes
          CROSS APPLY STRING_SPLIT(IngredientsList, '"')
 WHERE LTRIM(RTRIM(value)) <> '' -- Evitar valores vac�os
   AND value <> ',';               -- Evitar comas solas

-- Insertar los pasos procesados en la tabla Ingredients
INSERT INTO Ingredient (RecipeId, Ingredient)
SELECT RecipeID, Ingredient
FROM TempIngredients
WHERE Ingredient IS NOT NULL AND Ingredient <> '';

-- Limpiar la tabla temporal
DROP TABLE TempIngredients;
