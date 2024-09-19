using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineQuizApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using OnlineQuizApp.Model;

namespace OnlineQuizApp.Pages.Questions
{
	public class EditModel : PageModel
	{
		private readonly ApplicationDbContext _context;

		[BindProperty]
		public Question Question { get; set; }

		[BindProperty]
		public List<Answer> Answers { get; set; }

		public EditModel(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> OnGetAsync(int id)
		{
			// Fetch the question and associated answers from the database
			Question = await _context.Questions.Include(q => q.Answers).FirstOrDefaultAsync(q => q.Id == id);

			if (Question == null)
			{
				return NotFound();
			}

			Answers = Question.Answers;

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			// Update the question and its answers
			_context.Attach(Question).State = EntityState.Modified;
			foreach (var answer in Answers)
			{
				_context.Attach(answer).State = EntityState.Modified;
			}

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_context.Questions.Any(q => q.Id == Question.Id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return RedirectToPage("./Index");
		}
	}
}