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

				return Results.Ok(novoEstudante);
			});
			app.MapGet(pattern: "estudantes",
				handler: () => new Estudante(nome: "Cristian"));
		}
	}
}

