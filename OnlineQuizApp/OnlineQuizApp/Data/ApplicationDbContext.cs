﻿using Microsoft.EntityFrameworkCore;
using OnlineQuizApp.Model;

namespace OnlineQuizApp.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Question> Questions { get; set; }
		public DbSet<Answer> Answers { get; set; }
	}
}
