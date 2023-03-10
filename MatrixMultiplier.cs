public class MatrixMultiplier
{
  public void Assigment01()
  {
    //size of matrixes
    int Arows = (int)Math.Pow(10, 6);
    int Acols = (int)Math.Pow(10, 3);

    int Brows = Acols;
    int Bcols = (int)Math.Pow(10, 6);

    int Crows = Bcols;
    int Ccols = 1;

    //(A*B)*C == A*(B*C)
    /*
    Wikipedia https://en.wikipedia.org/wiki/Matrix_multiplication
    For example, if A, B and C are matrices of respective sizes 10×30, 30×5, 5×60, 
    computing (AB)C needs 10×30×5 + 10×5×60 = 4,500 multiplications, 
    while computing A(BC) needs 30×5×60 + 10×30×60 = 27,000 multiplications.
    */


    //B matrix is loaded to memory one row at the time
    var Brow = new double[Bcols];
    //C matrix is small so it can be loaded as once
    var C = MatrixMultiplier.CreateMatrixWithRandowNumbers(Crows, Ccols);
    var BC = new double[Brows, Ccols];

    //First multiply B and C because then result is smaller matrix and there is need to do less operations
    //B*C
    for (int rowI = 0; rowI < Brows; rowI++)
    {
      Brow = MatrixMultiplier.CreateRowWithRandowNumbers(Bcols);
      for (int colI = 0; colI < Bcols; colI++)
      {
        BC[rowI, 0] += Brow[colI] * C[colI, 0];
      }
    }

    //Then simply multiply A with result of BC to get D
    //A*BC
    var Arow = new double[Bcols];
    var D = new double[Arows, Ccols];

    var hist = new int[101];
    var pdf = new double[101];
    var cdf = new double[101];

    for (int rowI = 0; rowI < Arows; rowI++)
    {
      //same as with B. A matrix is loaded to memoery one row at the time
      Arow = MatrixMultiplier.CreateRowWithRandowNumbers(Acols);
      for (int colI = 0; colI < Acols; colI++)
      {
        var a = Arow[colI];
        //using value to identify bin in histogram
        var aHistIndex = (int)(Math.Round(a, 2, MidpointRounding.AwayFromZero) * 100);
        //create histogram to later to create PDF and from PDF CDF
        hist[aHistIndex]++;
        D[rowI, 0] += a * BC[colI, 0];
      }
    }

    //for the normalization
    double aSum = (double)(Arows * Acols);

    for (int i = 0; i < hist.Length; i++)
    {
      //create normalized PDF
      pdf[i] = hist[i] / aSum;
      double prevCdf = i > 0 ? cdf[i - 1] : 0;
      // 
      cdf[i] += prevCdf + pdf[i];
      //print CDF value to console to copy paste value to Excel
      //to have plot
      Console.WriteLine(cdf[i]);
    }
  }

  private static double[,] CreateMatrixWithRandowNumbers(int m, int n)
  {
    //create new matrix with random values
    var mat = new double[m, n];
    for (int i = 0; i < m; i++)
    {
      for (int j = 0; j < n; j++)
      {
        mat[i, j] = Random.Shared.NextDouble();
      }
    }

    return mat;
  }

  private static double[] CreateRowWithRandowNumbers(int n)
  {
    //create single row vector of matrix
    var mat = new double[n];
    for (int j = 0; j < n; j++)
    {
      mat[j] = Random.Shared.NextDouble();
    }

    return mat;
  }
}