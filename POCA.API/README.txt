Colocar um appsettings assim:

{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },

    "ConnectionStrings": {
        "POCADB": "server=localhost;port=3306;user=seu_usuario;password=sua_senha;database=db_poca"
    },

    "OpenAI": {
        "ApiKey": "A_senha_da_api_do_chat_gpt"
    }
}
