using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineQuizApp.Data;
using OnlineQuizApp.Model;

namespace OnlineQuizApp.Pages.Quiz
{
	public class IndexModel : PageModel
	{
		private readonly ApplicationDbContext _context;
		public List<Question> Questions { get; set; }

		public IndexModel(ApplicationDbContext context)
		{
			_context = context;
		}

		public void OnGet()
		{
			Questions = _context.Questions.Include(q => q.Answers).OrderBy(q => Guid.NewGuid()).ToList(); // Randomize question order
		}

		public IActionResult OnPostSubmitQuiz(Dictionary<string, int> userAnswers)
		{
			int correctAnswers = 0;
			foreach (var question in _context.Questions.Include(q => q.Answers))
			{
				if (question.Answers.Any(a => a.IsCorrect && userAnswers.ContainsValue(a.Id)))
				{
					correctAnswers++;
				}
			}

			int totalQuestions = _context.Questions.Count();
			return RedirectToPage("/Quiz/Score", new { score = correctAnswers, total = totalQuestions });
		}
	}
}
