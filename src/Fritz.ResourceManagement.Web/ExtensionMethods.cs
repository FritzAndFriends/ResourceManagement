using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Fritz.ResourceManagement.Web
{

	public static class ExtensionMethods
	{

		public static DbContextOptionsBuilder UseMyDatabase(this DbContextOptionsBuilder options, IConfiguration config = null)
		{
			options.UseSqlite(config.GetConnectionString("sqlitedb"));
			return options;
		}

		public static DbContextOptionsBuilder UseMyDatabase(this DbContextOptionsBuilder options, string connectionString)
		{
			options.UseSqlite(connectionString);
			return options;
		}


	}

}
