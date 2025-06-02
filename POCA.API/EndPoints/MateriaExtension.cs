using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCA.API.Requests.Materia;
using POCA.Banco.Model;
namespace POCA.API.EndPoints
{
    public static class MateriaExtension
    {
        public static void AddEndpointsMaterias(this WebApplication app)
        {
            var group = app.MapGroup("/materias")
                           .WithTags("Materias");

            // GET materia
            group.MapGet("/", async ([FromServices] DbPocaContext context) =>
            {
                return Results.Ok(await context.TbMaterias.ToListAsync());
            });

            // GET materia by ID
            group.MapGet("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var materia = await context.TbMaterias.FindAsync(id);
                return materia is null ? Results.NotFound() : Results.Ok(materia);
            });

            // POST new materia
            group.MapPost("/", async ([FromServices] DbPocaContext context,
                                    [FromBody] MateriaRequest request) =>
            {
                var materia = new TbMateria
                {
                };

                context.TbMaterias.Add(materia);
                await context.SaveChangesAsync();
                return Results.Created($"/Materias/{materia.IdMateria}", materia);
            });

            // PUT update materia
            group.MapPut("/{id}", async ([FromServices] DbPocaContext context,
                           int id,
                           [FromBody] MateriaRequest request) =>
            {
                var materia = await context.TbMaterias.FindAsync(id);
                if (materia is null) return Results.NotFound();

                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            // DELETE materia
            group.MapDelete("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var materia = await context.TbMaterias.FindAsync(id);
                if (materia is null) return Results.NotFound();

                context.TbMaterias.Remove(materia);
                await context.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
