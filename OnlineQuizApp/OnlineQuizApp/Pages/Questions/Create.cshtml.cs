using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineQuizApp.Data;
using OnlineQuizApp.Model;
using System.Threading.Tasks;

namespace OnlineQuizApp.Pages.Questions
{
	public class CreateModel : PageModel
	{
		private readonly ApplicationDbContext _context;

		[BindProperty]
		public Question Question { get; set; }

		[BindProperty]
		public List<Answer> Answers { get; set; }

		public CreateModel(ApplicationDbContext context)
		{
			_context = context;
		}

		public void OnGet()
		{
			Answers = new List<Answer>
			{
				new Answer(),
				new Answer(),
				new Answer(),
				new Answer()
			};
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Questions.Add(Question);
			await _context.SaveChangesAsync();

			foreach (var answer in Answers)
			{
				answer.QuestionId = Question.Id;
				_context.Answers.Add(answer);
			}

			await _context.SaveChangesAsync();

			return RedirectToPage("/Quiz/TakeQuiz");
		}
	}
}