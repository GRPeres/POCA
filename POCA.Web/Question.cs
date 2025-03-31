namespace POCA.Web
{
    public class Question
    {
        public string QuestionText { get; set; } // Propriedade não anulável
        public List<string> Options { get; set; } // Propriedade não anulável
        public int CorrectAnswerIndex { get; set; }

        // Construtor atualizado
        public Question(string questionText, List<string> options, int correctAnswerIndex)
        {
            QuestionText = questionText ?? throw new ArgumentNullException(nameof(questionText));
            Options = options ?? throw new ArgumentNullException(nameof(options));
            CorrectAnswerIndex = correctAnswerIndex;
        }
    }
}
