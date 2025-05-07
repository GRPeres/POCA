namespace POCA.Web.Utils
{
    public class Question
    {
        public string QuestionText { get; set; }
        public List<string> Options { get; set; } 
        public int CorrectAnswerIndex { get; set; }

        public Question(string questionText, List<string> options, int correctAnswerIndex)
        {
            QuestionText = questionText ?? throw new ArgumentNullException(nameof(questionText));
            Options = options ?? throw new ArgumentNullException(nameof(options));
            CorrectAnswerIndex = correctAnswerIndex;
        }
    }
}
