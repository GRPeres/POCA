namespace POCA.API.Response
{
    public class MateriaProgressoResponse
    {
        public int IdMateria { get; set; }
        public string NomeMateria { get; set; } = string.Empty;
        public int TotalAtividades { get; set; }
        public int AtividadesRespondidas { get; set; }
        public double Progresso { get; set; }
    }

}
