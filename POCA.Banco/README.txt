Para configurar o banco � s� rodar criar o sql no Mysql e criar um arquivo appsettings.json com a connection string, com conteudo: 

{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;user=root;password=root;database=db_poca"
  }
}

Para criar tabela nova a partir do sql: 
Scaffold-DbContext "server=localhost;port=3306;user=root;password=root;database=db_poca" MySql.EntityFrameworkCore -OutputDir Model -f
Add-Migration nomedaatt