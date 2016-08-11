using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketSolver
{
	public sealed class Program
	{
		public static void Main(string[] args)
		{
			Int32 res = new Solver(7, 3, 5, Console.Out).Solve();

			Console.WriteLine("#####################################################################");

			res = new Solver(7, 4, 2, Console.Out).Solve();

			Console.WriteLine("#####################################################################");

			res = new Solver(7, 4, 4, Console.Out).Solve();

			Console.WriteLine("#####################################################################");

			res = new Solver(6, 4, 1, Console.Out).Solve();

			Console.WriteLine("#####################################################################");

			res = new Solver(11, 4, 1, Console.Out).Solve();
		}
	}
}


