using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineQuizApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using OnlineQuizApp.Model;

namespace OnlineQuizApp.Pages.Quiz
{
	public class TakeQuizModel : PageModel
	{
		private readonly ApplicationDbContext _context;

		public List<Question> Questions { get; set; }

		[BindProperty]
		public Dictionary<int, int> UserAnswers { get; set; } = new Dictionary<int, int>();

		public TakeQuizModel(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> OnGetAsync()
		{
			Questions = await _context.Questions.Include(q => q.Answers).ToListAsync();

			Console.WriteLine($"Total Questions Fetched: {Questions.Count}");
			foreach (var question in Questions)
			{
				Console.WriteLine($"Question: {question.Text}, Answers: {string.Join(", ", question.Answers.Select(a => a.Text))}");
			}

			Questions = Questions.OrderBy(q => Guid.NewGuid()).ToList();

			return Page();
		}

		public IActionResult OnPostSubmitQuiz()
		{
			int correctAnswers = 0;
			foreach (var question in _context.Questions.Include(q => q.Answers))
			{
				var selectedAnswerId = UserAnswers.ContainsKey(question.Id) ? UserAnswers[question.Id] : -1;
				if (question.Answers.Any(a => a.Id == selectedAnswerId && a.IsCorrect))
				{
					correctAnswers++;
				}
			}

			return RedirectToPage("/Quiz/Score", new { score = correctAnswers, total = Questions.Count });
		}
	}
}