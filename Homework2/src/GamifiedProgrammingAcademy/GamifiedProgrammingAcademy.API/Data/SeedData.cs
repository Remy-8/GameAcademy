#nullable disable
using GamifiedProgrammingAcademy.Domain.Entities;
using GamifiedProgrammingAcademy.Infrastructure.Context;

namespace GamifiedProgrammingAcademy.API.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(GamifiedAcademyContext context)
        {
            // Verificar si ya hay datos
            if (context.Challenges.Any()) return;

            // Crear algunos challenges básicos
            var challenges = new[]
            {
                new Challenge
                {
                    Title = "Hola Mundo",
                    Description = "Escribe un programa que muestre 'Hola Mundo' en la consola",
                    Points = 10,
                    Difficulty = "Beginner",
                    Category = "Basics",
                    ProblemStatement = "Crea un programa que imprima 'Hola Mundo' en la consola.",
                    ExpectedOutput = "Hola Mundo",
                    TestCases = "Input: Ninguno, Output: Hola Mundo",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Challenge
                {
                    Title = "Suma de dos números",
                    Description = "Escribe una función que sume dos números",
                    Points = 15,
                    Difficulty = "Beginner",
                    Category = "Math",
                    ProblemStatement = "Crea una función que reciba dos números y retorne su suma.",
                    ExpectedOutput = "La suma de los dos números",
                    TestCases = "Input: 5, 3 | Output: 8",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Challenge
                {
                    Title = "Número par o impar",
                    Description = "Determina si un número es par o impar",
                    Points = 20,
                    Difficulty = "Beginner",
                    Category = "Logic",
                    ProblemStatement = "Escribe un programa que determine si un número es par o impar.",
                    ExpectedOutput = "Par o Impar",
                    TestCases = "Input: 4 | Output: Par, Input: 7 | Output: Impar",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Challenge
                {
                    Title = "Tabla de multiplicar",
                    Description = "Genera la tabla de multiplicar del 1 al 10 de un número",
                    Points = 25,
                    Difficulty = "Intermediate",
                    Category = "Loops",
                    ProblemStatement = "Crea un programa que muestre la tabla de multiplicar de un número del 1 al 10.",
                    ExpectedOutput = "Tabla completa de multiplicar",
                    TestCases = "Input: 5 | Output: 5x1=5, 5x2=10, ..., 5x10=50",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Challenge
                {
                    Title = "Invertir una cadena",
                    Description = "Invierte el orden de los caracteres en una cadena",
                    Points = 30,
                    Difficulty = "Intermediate",
                    Category = "Strings",
                    ProblemStatement = "Escribe una función que invierta una cadena de texto.",
                    ExpectedOutput = "Cadena invertida",
                    TestCases = "Input: 'hola' | Output: 'aloh'",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            };

            // Crear algunos badges básicos
            var badges = new[]
            {
                new Badge
                {
                    Name = "Primer Paso",
                    Description = "Completa tu primer desafío",
                    IconUrl = "🎯",
                    BadgeType = "Achievement",
                    UnlockCondition = "Completar 1 desafío",
                    RequiredPoints = 10,
                    Rarity = "Common",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Badge
                {
                    Name = "Principiante",
                    Description = "Completa 3 desafíos",
                    IconUrl = "🌟",
                    BadgeType = "Progress",
                    UnlockCondition = "Completar 3 desafíos",
                    RequiredPoints = 50,
                    Rarity = "Common",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Badge
                {
                    Name = "Matemático",
                    Description = "Completa 3 desafíos de matemáticas",
                    IconUrl = "🧮",
                    BadgeType = "Category",
                    UnlockCondition = "Completar 3 desafíos de Math",
                    RequiredPoints = 75,
                    Rarity = "Rare",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Badge
                {
                    Name = "Explorador",
                    Description = "Completa desafíos de 3 categorías diferentes",
                    IconUrl = "🗺️",
                    BadgeType = "Achievement",
                    UnlockCondition = "Completar desafíos de 3 categorías",
                    RequiredPoints = 100,
                    Rarity = "Epic",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Badge
                {
                    Name = "Maestro Programador",
                    Description = "Alcanza 500 puntos",
                    IconUrl = "👑",
                    BadgeType = "Special",
                    UnlockCondition = "Alcanzar 500 puntos",
                    RequiredPoints = 500,
                    Rarity = "Legendary",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            };

            // Crear algunos usuarios de prueba
            var users = new[]
            {
                new User
                {
                    Username = "admin",
                    Email = "admin@gameacademy.com",
                    FirstName = "Admin",
                    LastName = "User",
                    TotalPoints = 0,
                    Level = 1,
                    ExperiencePoints = 0,
                    JoinDate = DateTime.UtcNow,
                    Avatar = "👨‍💻",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new User
                {
                    Username = "estudiante1",
                    Email = "estudiante1@gameacademy.com",
                    FirstName = "Juan",
                    LastName = "Pérez",
                    TotalPoints = 25,
                    Level = 1,
                    ExperiencePoints = 25,
                    JoinDate = DateTime.UtcNow.AddDays(-7),
                    Avatar = "🧑‍🎓",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new User
                {
                    Username = "estudiante2",
                    Email = "estudiante2@gameacademy.com",
                    FirstName = "María",
                    LastName = "García",
                    TotalPoints = 45,
                    Level = 1,
                    ExperiencePoints = 45,
                    JoinDate = DateTime.UtcNow.AddDays(-5),
                    Avatar = "👩‍🎓",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            };

            // Agregar datos a la base de datos
            await context.Challenges.AddRangeAsync(challenges);
            await context.Badges.AddRangeAsync(badges);
            await context.Users.AddRangeAsync(users);

            await context.SaveChangesAsync();
        }
    }
}