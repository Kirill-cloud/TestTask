﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DBRepository
{
    class RepositoryContextFactory : IRepositoryContextFactory
	{
		public RepositoryContext CreateDbContext(string connectionString)
		{
			var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
			optionsBuilder.UseSqlServer(connectionString);

			return new RepositoryContext(optionsBuilder.Options);
		}
	}
}
