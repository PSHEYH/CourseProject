using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseProject
{
    public static class Algorithms
    {
        public static double[,] Normalize((int, int)[] resolution, double[] prices, int[,] other, DataGridView dataGrid)
        {

            int rows = other.GetUpperBound(0) + 1;
            int columns = other.Length / rows;

            double[,] result = new double[rows, columns + 2];

            int[] weight = new int[rows];
            int[][] others = new int[columns-1][];

            for (int i = 0; i < columns-1; i++)
            {
                others[i] = new int[rows];
            }

            for (int i = 0; i < columns - 1; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (i == 0)
                    {
                        weight[j] = other[j, i];
                    }
                    else
                    {
                        others[i-1][j] = other[j,i];
                    }
                }
            }

            int minWeight = Min(weight);
            (int, int) maxResolution = Max(resolution);
            double minPrice = Min(prices);

            int[] otherMax = new int[columns - 1];
            for (int i = 0; i < columns - 1; i++)
            {
                otherMax[i] = Max(others[i]);
            }

            double[] normalizeResolution = new double[rows];
            double[] normalizeWeights = new double[rows];
            double[] normalizePrices = new double[rows];
            double[,] otherNormalize = new double[rows, columns - 1];
            
            for (int i = 0; i < rows; i++)
            {
                normalizeResolution[i] = TupleDivide(resolution[i],maxResolution);
                normalizeWeights[i] = (double)minWeight / (double)weight[i];
                normalizePrices[i] = (double)minPrice / (double)prices[i];
            }

            for (int i = 0; i < columns - 1; i++)
            {
                if (i == columns - 2)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        otherNormalize[j, i] = other[j, columns - 1];
                    }
                }
                else
                {
                    for (int j = 0; j < rows; j++)
                    {
                        otherNormalize[j, i] = (double)others[i][j] / (double)otherMax[i];

                    }
                }
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns + 2; j++)
                {
                    if (j == 0)
                    {
                        result[i, j] = normalizeResolution[i];
                        dataGrid[j + 1, i].Value = result[i, j].ToString();
                    }
                    else if (j == 1)
                    {
                        result[i, j] = normalizeWeights[i];
                    }
                    else if (j != columns + 1)
                    {
                        result[i, j] = otherNormalize[i, j - 2];
                    }
                    else
                    {
                        result[i, j] = normalizePrices[i];
                    }
                }
            }

            double[] lenght = new double[columns+2];


            for (int i = 0; i < columns + 2; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    lenght[i] += Math.Pow(result[j, i],2);
                }

                lenght[i] = Math.Sqrt(lenght[i]);
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns + 2; j++)
                {
                    result[i, j] = result[i, j] / lenght[j];
                    dataGrid[j + 1, i].Value = result[i, j];
                }
            }

            return result;
        }

        private static double TupleDivide((int,int) first,(int,int) second)
        {

            if(second.Item1>first.Item1 && second.Item2>first.Item2)
            {
                if (second.Item1 - first.Item1 > second.Item2 - first.Item2)
                    return (double)first.Item1 / (double)second.Item1;
                else
                    return (double)first.Item2 / (double)second.Item2;
            }
            else if(first.Item1 > second.Item1 && first.Item2 < second.Item2)
            {
                return (double)first.Item1 / (double)second.Item2;
            }
            else if(first.Item1 < second.Item1 && first.Item2 > second.Item2)
            {
                return (double)first.Item2 / (double)second.Item1;
            }
            else
            {
                return (double)first.Item1/(double)second.Item1;
            }
        }
        public static double[] GetColumnByIndex(double[,] matrix,int index)
        {
            
            int rows = matrix.GetUpperBound(0) + 1;
            int columns = matrix.Length / rows;
            double[] results = new double[rows];

            for (int i = 0; i < rows; i++)
            {
                results[i] = matrix[i, index - 1];
            }
            return results;
        }

        public static int Min(int[] column)
        {
            int min = 0;
            for (int i = 1; i < column.Length; i++)
            {
                if (column[min] > column[i])
                    min = i;
            }
            return column[min];
        }
        public static double Min(double[] column)
        {
            int min = 0;
            for (int i = 1; i < column.Length; i++)
            {
                if (column[min] > column[i])
                    min = i;
            }
            return column[min];
        }

        public static int Max(int[] column)
        {
            int max = 0;
            for (int i = 1; i < column.Length; i++)
            {
                if (column[max] < column[i])
                    max = i;
            }
            return column[max];
        }

        public static (int,int) Max((int,int)[] resolution)
        {
            int max = 0;

            for (int i = 1; i < resolution.Length; i++)
            {
                if (resolution[max].Item1 < resolution[i].Item1 && resolution[max].Item2 < resolution[i].Item2)
                    max = i;
                else if(resolution[max].Item1 < resolution[i].Item1 && resolution[max].Item2 > resolution[i].Item2)
                {
                    if (resolution[i].Item1 - resolution[max].Item1 > resolution[max].Item2 - resolution[i].Item2)
                        max = i;
                }
            }
            return resolution[max];
        }

        public static double[] Average(int[,] matrix)
        {
            int rows = matrix.GetUpperBound(0) + 1;
            int columns = matrix.Length / rows;
            double[] average = new double[rows];

            double avg;
            for (int i = 0; i < rows; i++)
            {
                avg = 0;
                for (int j = 0; j < columns; j++)
                {
                    avg += matrix[i, j];
                }
                avg = avg / (double)columns;
                average[i] = avg;
            }
            return average;
        }

        public static double[] MedianRang(int[,] matrix)
        {
            int rows = matrix.GetUpperBound(0) + 1;
            int columns = matrix.Length / rows;
            double[] medians = new double[rows];
            int[] currentRow = new int[columns];


            for (int i = 0; i < rows; i++)
            {

                for (int j = 0; j < columns; j++)
                {
                    currentRow[j] = matrix[i, j];
                }
                OrderByAscending(currentRow);
                medians[i] = Median(currentRow);
            }
            return medians;
        }
        public static void OrderByAscending(int[] row)
        {
            for (int i = 0; i < row.Length - 1; i++)
            {
                for (int j = i + 1; j < row.Length; j++)
                {
                    if (row[i] > row[j])
                    {
                        Swap(ref row[i], ref row[j]);
                    }
                }
            }
        }

        public static void OrderByAscending(double[] row, int[] addArray)
        {
            
            for (int i = 0; i < row.Length; i++)
            {
                addArray[i] = i + 1;
            }

            for (int i = 0; i < row.Length - 1; i++)
            {
                for (int j = i + 1; j < row.Length; j++)
                {
                    if (row[i] > row[j])
                    {
                        Swap(ref row[i], ref row[j]);
                        Swap(ref addArray[i], ref addArray[j]);
                    }
                }

            }
        }

        public static void OrderByDescending(double[] row, int[] addArray)
        {

            for (int i = 0; i < row.Length; i++)
            {
                addArray[i] = i + 1;
            }
            for (int i = 1; i < row.Length; i++)
            {
                for (int j = 0; j < row.Length - i; j++)
                {
                    if (row[j] < row[j + 1])
                    {
                        Swap(ref row[j], ref row[j + 1]);
                        Swap(ref addArray[j], ref addArray[j + 1]);
                    }
                }

            }
        }

        public static void SetRanking(int[] rankingOfIndex, double[] array, ref string resultRow)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (i == 0)
                {
                    if (array[i] == array[i + 1])
                    {
                        resultRow += $"[(A{rankingOfIndex[i]},";
                    }
                    else
                        resultRow += $"[A{rankingOfIndex[i]},";
                }
                else if (i > 0 && i < array.Length - 1)
                {
                    if (array[i] == array[i - 1] && array[i] != array[i + 1])
                    {
                        resultRow += $"A{rankingOfIndex[i]}),";
                    }
                    else if (array[i] != array[i - 1] && array[i] == array[i + 1])
                    {
                        resultRow += $"(A{rankingOfIndex[i]},";
                    }
                    else
                        resultRow += $"A{rankingOfIndex[i]},";
                }
                else if (i == array.Length - 1)
                {
                    if (array[i] == array[i - 1])
                    {
                        resultRow += $"A{rankingOfIndex[i]})]";
                    }
                    else
                    {
                        resultRow += $"A{rankingOfIndex[i]}]";
                    }
                }
            }
        }

        public static double[] GetWeights(double[,] matrix)
        {
            int rows = matrix.GetUpperBound(0) + 1;
            int columns = matrix.Length / rows;

            double[] weights = new double[rows];
            double[] teta = new double[rows];
            double sumTet = 0;
            double Mult = 1;

            for (int i = 0; i < rows; i++)
            {
                Mult = 1;
                for (int j = 0; j <columns; j++)
                {
                    Mult *= matrix[i, j];
                }
                teta[i] = Math.Pow(Mult,1.0/rows);
                sumTet += teta[i];
            }

            for (int i = 0; i < rows; i++)
            {
                weights[i] = teta[i] / sumTet;
            }

            return weights;


        }

        public static void Swap(ref int first, ref int second)
        {
            int tmp;
            tmp = first;
            first = second;
            second = tmp;
        }

        public static void Swap(ref double first, ref double second)
        {
            double tmp;
            tmp = first;
            first = second;
            second = tmp;
        }

        public static double Median(int[] row)
        {
            if (row.Length % 2 != 0)
                return row[row.Length / 2];
            else
            {
                return (row[row.Length / 2] + row[row.Length / 2 - 1]) / 2.0;
            }
        }
    }
}
