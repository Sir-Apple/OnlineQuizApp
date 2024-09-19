using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineQuizApp.Model
{
	public class Question
	{
		public int Id { get; set; }

		[Required]
		public string Text { get; set; }

		public List<Answer> Answers { get; set; }
	}

	public class Answer
	{
		public int Id { get; set; }

		[Required]
		public string Text { get; set; }

		public bool IsCorrect { get; set; }

		public int QuestionId { get; set; }
		public Question Question { get; set; }
	}
}

