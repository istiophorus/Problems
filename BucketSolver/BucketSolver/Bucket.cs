using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketSolver
{
	public sealed class Bucket: ICloneable
	{
		public Bucket(Int32 bucketMax)
		{
			if (bucketMax <= 0)
			{
				throw new ArgumentOutOfRangeException("bucketMax");
			}

			_bucketMax = bucketMax;
		}

		private readonly Int32 _bucketMax;

		private Int32 _bucket;

		public Int32 Space
		{
			get
			{
				return _bucketMax - _bucket;
			}
		}

		public Int32 State
		{
			get
			{
				return _bucket;
			}
		}

		public Int32 Max
		{
			get
			{
				return _bucketMax;
			}
		}

		public Boolean IsEmpty
		{
			get
			{
				return _bucket == 0;
			}
		}

		public Boolean IsFull
		{
			get
			{
				return _bucket == _bucketMax;
			}
		}

		public void Empty()
		{
			_bucket = 0;
		}

		public void Fill()
		{
			_bucket = _bucketMax;
		}

		public void TryRemove(Int32 amountToRemove)
		{
			if (amountToRemove < 0)
			{
				throw new ArgumentOutOfRangeException("amountToRemove");
			}

			if (amountToRemove > _bucket)
			{
				throw new ArgumentOutOfRangeException("amountToRemove");
			}

			_bucket -= amountToRemove;
		}

		public Int32 TryFill(Int32 maxAvailable)
		{
			if (maxAvailable < 0)
			{
				throw new ArgumentNullException("maxAvailable");
			}

			if (maxAvailable == 0)
			{
				return 0;
			}

			if (_bucket >= _bucketMax)
			{
				return 0;
			}

			Int32 maxAllowed = _bucketMax - _bucket;

			Int32 amountToPour = Math.Min(maxAllowed, maxAvailable);

			_bucket += amountToPour;

			return amountToPour;
		}

		public Object Clone()
		{
			Bucket result = new Bucket(_bucketMax);

			result._bucket = _bucket;

			return result;
		}
	}
}
