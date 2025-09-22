﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCA.API.Requests.Aluno;
using POCA.API.Response;
using POCA.API.Responses;
using POCA.Banco.Model;

namespace POCA.API.EndPoints
{
    public static class AlunoExtension
    {
        public static void AddEndpointsAlunos(this WebApplication app)
        {
            var group = app.MapGroup("/alunos")
                           .WithTags("Alunos")
                           .WithOpenApi();

            // GET all alunos with basic info
            group.MapGet("/", async ([FromServices] DbPocaContext context) =>
            {
                var alunos = await context.TbAlunos
                    .Select(a => new AlunoResponse(
                        a.IdAluno,
                        a.NomeAluno,
                        a.NascimentoAluno,
                        a.ProgressoAluno,
                        a.ContatoAluno,
                        a.EmailAluno,
                        a.TbMateriasIdMateria.Select(m => m.IdMateria),
                        a.TbPessoasIdPessoas.Select(p => p.IdPessoa)
                    ))
                    .ToListAsync();

                return Results.Ok(alunos);
            });

            // GET aluno by ID with details
            group.MapGet("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var aluno = await context.TbAlunos
                    .Include(a => a.TbMateriasIdMateria)
                    .Include(a => a.TbPessoasIdPessoas)
                    .FirstOrDefaultAsync(a => a.IdAluno == id);

                if (aluno is null)
                    return Results.NotFound();

                var response = new AlunoResponse(
                    aluno.IdAluno,
                    aluno.NomeAluno,
                    aluno.NascimentoAluno,
                    aluno.ProgressoAluno,
                    aluno.ContatoAluno,
                    aluno.EmailAluno,
                    aluno.TbMateriasIdMateria.Select(m => m.IdMateria),
                    aluno.TbPessoasIdPessoas.Select(p => p.IdPessoa)
                );

                return Results.Ok(response);
            });

            // POST new aluno
            group.MapPost("/", async ([FromServices] DbPocaContext context,
                                    [FromBody] AlunoCreateRequest request) =>
            {
                var aluno = new TbAluno
                {
                    NomeAluno = request.NomeAluno,
                    NascimentoAluno = request.NascimentoAluno,
                    ProgressoAluno = request.ProgressoAluno,
                    ContatoAluno = request.ContatoAluno,
                    EmailAluno = request.EmailAluno
                };

                context.TbAlunos.Add(aluno);
                await context.SaveChangesAsync();

                var response = new AlunoResponse(
                    aluno.IdAluno,
                    aluno.NomeAluno,
                    aluno.NascimentoAluno,
                    aluno.ProgressoAluno,
                    aluno.ContatoAluno,
                    aluno.EmailAluno,
                    null,
                    null
                );

                return Results.Created($"/alunos/{aluno.IdAluno}", response);
            });

            // PUT update aluno
            group.MapPut("/{id}", async ([FromServices] DbPocaContext context,
                           int id,
                           [FromBody] AlunoEditRequest request) =>
            {
                if (id != request.IdAluno)
                    return Results.BadRequest("ID mismatch");

                var aluno = await context.TbAlunos.FindAsync(id);
                if (aluno is null)
                    return Results.NotFound();

                aluno.NomeAluno = request.NomeAluno;
                aluno.NascimentoAluno = request.NascimentoAluno;
                aluno.ProgressoAluno = request.ProgressoAluno;
                aluno.ContatoAluno = request.ContatoAluno;
                aluno.EmailAluno = request.EmailAluno;

                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            // DELETE aluno
            group.MapDelete("/{id}", async ([FromServices] DbPocaContext context, int id) =>
            {
                var aluno = await context.TbAlunos.FindAsync(id);
                if (aluno is null)
                    return Results.NotFound();

                context.TbAlunos.Remove(aluno);
                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            // GET all matérias for an aluno
            group.MapGet("/{idAluno}/materias",
                async ([FromServices] DbPocaContext context, int idAluno) =>
                {
                    var aluno = await context.TbAlunos
                        .Include(a => a.TbMateriasIdMateria)
                            .ThenInclude(m => m.TbProfessoresIdProfessors)
                        .Include(a => a.TbMateriasIdMateria)
                            .ThenInclude(m => m.TbAlunosIdAlunos)
                        .Include(a => a.TbMateriasIdMateria)
                            .ThenInclude(m => m.TbAtividadesIdAtividades)
                        .FirstOrDefaultAsync(a => a.IdAluno == idAluno);

                    if (aluno is null)
                        return Results.NotFound("Aluno not found");

                    var response = aluno.TbMateriasIdMateria.Select(m => new MateriaResponse(
                        m.IdMateria,
                        m.NomeMateria,
                        m.TbProfessoresIdProfessors?.Select(p => p.IdProfessor),
                        m.TbAlunosIdAlunos?.Select(a => a.IdAluno),
                        m.TbAtividadesIdAtividades?.Select(at => at.IdAtividade)
                    ));

                    return Results.Ok(response);
                });

            // Additional endpoints for relationships
            group.MapPost("/{idAluno}/materias/{idMateria}",
                async ([FromServices] DbPocaContext context, int idAluno, int idMateria) =>
                {
                    var aluno = await context.TbAlunos
                        .Include(a => a.TbMateriasIdMateria)
                        .FirstOrDefaultAsync(a => a.IdAluno == idAluno);

                    if (aluno is null)
                        return Results.NotFound("Aluno not found");

                    var materia = await context.TbMaterias.FindAsync(idMateria);
                    if (materia is null)
                        return Results.NotFound("Materia not found");

                    if (!aluno.TbMateriasIdMateria.Any(m => m.IdMateria == idMateria))
                    {
                        aluno.TbMateriasIdMateria.Add(materia);
                        await context.SaveChangesAsync();
                    }

                    return Results.NoContent();
                });

            group.MapDelete("/{idAluno}/materias/{idMateria}",
                async ([FromServices] DbPocaContext context, int idAluno, int idMateria) =>
                {
                    var aluno = await context.TbAlunos
                        .Include(a => a.TbMateriasIdMateria)
                        .FirstOrDefaultAsync(a => a.IdAluno == idAluno);

                    if (aluno is null)
                        return Results.NotFound("Aluno not found");

                    var materia = aluno.TbMateriasIdMateria
                        .FirstOrDefault(m => m.IdMateria == idMateria);

                    if (materia is null)
                        return Results.NotFound("Materia not associated with aluno");

                    aluno.TbMateriasIdMateria.Remove(materia);
                    await context.SaveChangesAsync();
                    return Results.NoContent();
                });
            // GET progresso do aluno em cada matéria
            group.MapGet("/{idAluno}/materias/progresso",
                async ([FromServices] DbPocaContext context, int idAluno) =>
                {
                    var result = await context.TbMaterias
                        .Where(m => m.TbAlunosIdAlunos.Any(a => a.IdAluno == idAluno))
                        .Select(m => new MateriaProgressoResponse
                        {
                            IdMateria = m.IdMateria,
                            NomeMateria = m.NomeMateria,
                            TotalAtividades = m.TbAtividadesIdAtividades.Count(),
                            AtividadesRespondidas = m.TbAtividadesIdAtividades
                                .Count(a => a.TbRespostasIdRespostas.Any(r => r.IdAluno == idAluno)),
                            Progresso = (double)m.TbAtividadesIdAtividades
                                .Count(a => a.TbRespostasIdRespostas.Any(r => r.IdAluno == idAluno))
                                / (m.TbAtividadesIdAtividades.Count() == 0 ? 1 : m.TbAtividadesIdAtividades.Count()) * 100
                        })
                        .ToListAsync();

                    return Results.Ok(result);
                });

        }
    }
}