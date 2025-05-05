Para configurar o banco é só rodar criar o sql no Mysql e criar um arquivo appsettings.json com a connection string, com conteudo: 

{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;user=root;password=root;database=db_poca"
  }
}