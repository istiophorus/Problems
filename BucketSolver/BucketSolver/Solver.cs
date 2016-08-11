using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BucketSolver
{
	public sealed class Solver
	{
		private readonly Bucket _bucketA;

		private readonly Bucket _bucketB;

		private readonly Int32 _target;

		private readonly TextWriter _writer;

		public Solver(Int32 volumeA, Int32 volumeB, Int32 target, TextWriter writer)
		{
			if (volumeA <= 0)
			{
				throw new ArgumentOutOfRangeException("volumeA");
			}

			if (volumeB <= 0)
			{
				throw new ArgumentOutOfRangeException("volumeB");
			}

			if (target < 0)
			{
				throw new ArgumentOutOfRangeException("target");
			}

			if (target > Math.Max(volumeA, volumeB))
			{
				throw new ArgumentOutOfRangeException("target");
			}

			if (null == writer)
			{
				throw new ArgumentNullException("writer");
			}

			_writer = writer;

			_bucketA = new Bucket(volumeA);

			_bucketB = new Bucket(volumeB);

			_target = target;
		}

		public Int32 Solve()
		{
			HashSet<String> states = new HashSet<String>();

			AggrState state = new AggrState(_bucketA, _bucketB);

			states.Add(state.Key);

			Stack<String> operations = new Stack<String>();

			List<String[]> solutions = new List<String[]>();

			Int32 minLength = Int32.MaxValue;

			SolveImpl(state, states, operations, solutions, ref minLength);

			if (solutions.Count <= 0)
			{
				return -1;
			}
			
			solutions.Sort((x, y) => x.Length.CompareTo(y.Length));

			PrintResult(solutions[0]);

			return solutions[0].Length;			
		}

		private static readonly Expression<Action<AggrState>>[] Actions = new Expression<Action<AggrState>>[]
			{
				s => s.EmptyA(),
				s => s.EmptyB(),
				s => s.FillA(),
				s => s.FillB(),
				s => s.MoveFromAToB(),
				s => s.MoveFromBToA()
			};

		private void SolveImpl(AggrState state, HashSet<String> states, Stack<String> operations, List<String[]> solutions, ref Int32 minLength)
		{
			if (state.StateA == _target || state.StateB == _target)
			{
				//// remember solution if this is shorter or equal to the shortest found

				if (operations.Count < minLength)
				{
					solutions.Add(operations.ToArray());

					minLength = operations.Count;
				}

				return;
			}

			if (operations.Count > minLength)
			{
				//// do not look for soution longer than the shortes one

				return;
			}

			foreach (Expression<Action<AggrState>> expression in Actions)
			{
				AggrState newState = (AggrState)state.Clone();

				Action<AggrState> action = expression.Compile();

				action(newState);

				if (states.Contains(newState.Key))
				{
					//// this state has been already visited

					continue;
				}

				states.Add(newState.Key);

				String operation = String.Format("{0} {1} {2}", state.Key, ((MethodCallExpression)expression.Body).Method.Name, newState.Key);

				operations.Push(operation);

				SolveImpl(newState, states, operations, solutions, ref minLength);

				states.Remove(newState.Key);

				operations.Pop();

			}
		}

		private void PrintResult(IEnumerable<String> operations)
		{
			foreach (String item in operations)
			{
				_writer.WriteLine(item);
			}
		}
	}
}

