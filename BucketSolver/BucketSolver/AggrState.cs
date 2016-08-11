using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketSolver
{
	public sealed class AggrState : ICloneable
	{
		public AggrState(Bucket bucketA, Bucket bucketB)
		{
			if (null == bucketA)
			{
				throw new ArgumentNullException("bucketA");
			}

			if (null == bucketB)
			{
				throw new ArgumentNullException("bucketB");
			}

			_bucketA = bucketA;

			_bucketB = bucketB;
		}

		private readonly Bucket _bucketA;

		private readonly Bucket _bucketB;

		public Int32 StateA
		{
			get
			{
				return _bucketA.State;
			}
		}

		public Int32 StateB
		{
			get
			{
				return _bucketB.State;
			}
		}

		public void FillA()
		{
			_bucketA.Fill();
		}

		public void FillB()
		{
			_bucketB.Fill();
		}

		public void EmptyA()
		{
			_bucketA.Empty();
		}

		public void EmptyB()
		{
			_bucketB.Empty();
		}

		public void MoveFromBToA()
		{
			Int32 amount = _bucketA.TryFill(_bucketB.State);

			_bucketB.TryRemove(amount);
		}

		public void MoveFromAToB()
		{
			Int32 amount = _bucketB.TryFill(_bucketA.State);

			_bucketA.TryRemove(amount);
		}

		public String Key
		{
			get
			{
				return String.Format("[{0}:{1};{2}:{3}]", _bucketA.State, _bucketA.Space, _bucketB.State, _bucketB.Space);
			}
		}

		public Object Clone()
		{
			return new AggrState((Bucket)_bucketA.Clone(), (Bucket)_bucketB.Clone());
		}
	}
}

