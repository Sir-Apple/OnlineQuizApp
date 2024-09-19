using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineQuizApp.Pages.Quiz
{
    public class ScoreModel : PageModel
    {
		public int Score { get; set; }
		public int TotalQuestions { get; set; }

		public void OnGet(int score, int total)
		{
			Score = score;
			TotalQuestions = total;
		}
	}
}
