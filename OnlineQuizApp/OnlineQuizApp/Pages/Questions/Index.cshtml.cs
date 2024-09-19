using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineQuizApp;
using OnlineQuizApp.Data;
using OnlineQuizApp.Model;

namespace OnlineQuizApp.Pages.Questions
{
    public class IndexModel : PageModel
    {
		private readonly ApplicationDbContext _context;

		public IList<Question> Questions { get; set; }

		public IndexModel(ApplicationDbContext context)
		{
			_context = context;
		}

		public void OnGet()
		{
			Questions = _context.Questions.Include(q => q.Answers).ToList();
		}
	}
}
