-- Tabla de Recetas
CREATE TABLE Recipe (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Title NVARCHAR(MAX),
                        Link NVARCHAR(MAX),
                        Source NVARCHAR(255),
                        Site NVARCHAR(255)
);

-- Tabla de Ingredientes
CREATE TABLE Ingredient (
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            RecipeId INT,
                            Ingredient NVARCHAR(MAX),
                            FOREIGN KEY (RecipeId) REFERENCES Recipe(Id)
);

-- Tabla de Direcciones
CREATE TABLE Direction (
                           Id INT IDENTITY(1,1) PRIMARY KEY,
                           RecipeId INT,
                           Step NVARCHAR(MAX),
                           FOREIGN KEY (RecipeId) REFERENCES Recipe(Id)
);

CREATE TABLE NER (
                     Id INT IDENTITY(1,1) PRIMARY KEY,
                     RecipeId INT,
                     Ingredient NVARCHAR(MAX),
                     FOREIGN KEY (RecipeId) REFERENCES Recipe(Id)
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
