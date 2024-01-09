using System;
using ApiCrud.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Estudantes
{
	public static class EstudantesRotas
	{
		public static void AddRotasEstudantes(this WebApplication app)
		{
			//Criando estudante
			app.MapPost(pattern: "estudantes", handler: async (AddEstudanteRequest request, AppDbContext context) =>
			{
				var jaExiste = await context.Estudantes
                .AnyAsync(estudante => estudante.Nome == request.Nome);

				if (jaExiste)
					return Results.Conflict(error: "Ja existe!");
				
				var novoEstudante = new Estudante(request.Nome);
				await context.Estudantes.AddAsync(novoEstudante);
				await context.SaveChangesAsync();

				var estudanteRetorno = new EstudanteDto(novoEstudante.Id, novoEstudante.Nome);

				return Results.Ok(estudanteRetorno);
			});

			//Retornar all estudantes cadastrados
			app.MapGet(pattern: "estudantes",
				handler: async (AppDbContext context) =>
				{
					var estudantes = await context
					.Estudantes
					.Where(estudante => estudante.Ativo)
					.Select(estudante=> new EstudanteDto(estudante.Id,estudante.Nome))
					.ToListAsync();

					return estudantes;
				});

			//Atualizar nome
			app.MapPut(pattern:"{id:guid}", handler: async (Guid id,UpdateEstudanteRequest request, AppDbContext context) =>
			{
				var estudante = await context.Estudantes
				.SingleOrDefaultAsync(estudante => estudante.Id == id);

				if (estudante == null)
					return Results.NotFound();

				estudante.AtualizarNome(request.Nome);

				await context.SaveChangesAsync();
				return Results.Ok(new EstudanteDto(estudante.Id, estudante.Nome));
			});
		}
	}
}

