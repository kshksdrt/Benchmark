using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace MyBenchmarks
{
  public class LoopALot
  {
    private const int N = 100000;

    private readonly MD5 md5 = MD5.Create();
    private readonly SHA256 sha256 = SHA256.Create();

    public LoopALot()
    {
    }

    [Benchmark]
    public void Combined()
    {
      for (int i = 0; i < N; i++)
      {
        md5.ComputeHash(new byte[] { });
        if (i == 0)
        {
          md5.ComputeHash(sha256.ComputeHash(new byte[] { }).ToArray());
          md5.ComputeHash(sha256.ComputeHash(new byte[] { }).ToArray());
        }
        sha256.ComputeHash(new byte[] { });
      }
    }

    [Benchmark]
    public void Separate()
    {
      for (int i = 0; i < N; i++)
      {
        md5.ComputeHash(new byte[] { });
      }

      md5.ComputeHash(sha256.ComputeHash(new byte[] { }).ToArray());
      md5.ComputeHash(sha256.ComputeHash(new byte[] { }).ToArray());

      for (int i = 0; i < N; i++)
      {
        sha256.ComputeHash(new byte[] { });
      }
    }

    private void AnotherMethod()
    {
      for (int i = 0; i < N; i++)
      {
        sha256.ComputeHash(new byte[] { });
      }
    }
  }

  public class Program
  {
    public static void Main(string[] args)
    {
      var summary = BenchmarkRunner.Run<LoopALot>();
    }
  }
}
