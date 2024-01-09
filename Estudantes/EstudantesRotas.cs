using System;
namespace ApiCrud.Estudantes
{
	public static class EstudantesRotas
	{
		public static void AddRotasEstudantes(this WebApplication app)
		{
			app.MapGet(pattern: "estudantes",
				handler: () => new Estudante(nome: "Cristian"));
		}
	}
}

