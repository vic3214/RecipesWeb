-- Crear una tabla temporal para almacenar los pasos individuales
CREATE TABLE #Steps (RecipeId INT, Step NVARCHAR(MAX));

-- Usar una CTE para asignar RecipeID y extraer el contenido entre corchetes
;WITH CTE_TempRecipes AS (
    SELECT
        ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RecipeId,
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
INSERT INTO Direction (RecipeId, Step)
SELECT RecipeId, Step
FROM #Steps
WHERE Step IS NOT NULL AND Step <> '';

-- Limpiar la tabla temporal
DROP TABLE #Steps;
