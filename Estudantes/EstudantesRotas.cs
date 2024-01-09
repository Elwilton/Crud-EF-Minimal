using System;
namespace ApiCrud.Estudantes
{
	public static class EstudantesRotas
	{
		public static void AddRotasEstudantes(this WebApplication app)
		{
			//Criando estudante
		
			app.MapGet(pattern: "estudantes",
				handler: () => new Estudante(nome: "Cristian"));
		}
	}
}

