Com o banco criado localmente rode no terminal do nuGetPackage Manager (Ferramentas/Nuget Package Manager/Terminal do Nuget):

Scaffold-DbContext "server=localhost;port=3306;user=USERNAME;password=SENHA;database=db_poca" MySql.EntityFrameworkCore -o Model 

Caso nao estiver no visual studio o codigo deve ser rodado na pasta do projeto POCA.Banco com o seguinte codigo:

dotnet ef dbcontext scaffold "server=localhost;port=3306;user=USERNAME;password=SENHA;database=NOMEDATABELA" MySql.EntityFrameworkCore -o Model 

para criar o modelo C# e o context automaticamente

