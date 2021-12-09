using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public static class Mapper
    {
        public static string ConvertToString((int, int) resolution)
        {
            return $"{resolution.Item1}x{resolution.Item2}";
        }


        public static void StartWithDigit(string cell)
        {

            if (!Char.IsDigit(cell[0]))
            {
                throw new Exception(" Value doesn't start with a digit");
            }
        }

        public static int MapFromSpeakerToInt(string cell)
        {
            if (cell == "Mono" || cell == "mono" || cell == "MONO")
            {
                return 0;
            }
            else if (cell == "Stereo" || cell == "stereo" || cell == "STEREO")
            {
                return 1;
            }
            throw new Exception("  Not correct value in cell for speaker");
        }

        public static (int, int) MapFromResolutionToInt(string cell)
        {
            int first = 0;
            int second = 0;

            StartWithDigit(cell);

            string result = "";
            for (int i = 0; i < cell.Length; i++)
            {
                if (i != 0 && cell[i] == 'x')
                {
                    first = Convert.ToInt32(result);
                    result = "";
                }
                else if (!Char.IsDigit(cell[i]))
                    break;
                else
                {
                    result += cell[i];
                }
            }

            second = Convert.ToInt32(result);
            return (first, second);
        }

        public static int MapFromWeightToInt(string cell)
        {
            int value;
            StartWithDigit(cell);
            string result = "";
            for (int i = 0; i < cell.Length; i++)
            {
                if (cell[i] == ' ' || !Char.IsDigit(cell[i]))
                {
                    break;
                }
                else
                {
                    result += cell[i];
                }
            }
            value = Convert.ToInt32(result);
            return value;
        }

        public static double MapFromPriceToDouble(string cell)
        {
            double value;
            StartWithDigit(cell);
            string result = "";

            for (int i = 0; i < cell.Length; i++)
            {
                if (cell[i] == ' ' || !Char.IsDigit(cell[i]) && cell[i] != ',' && cell[i] != '.')
                {
                    break;
                }
                else
                    result += cell[i];
            }

            if (result.Contains("."))
            {
                result.Replace('.', ',');
            }

            value = Convert.ToDouble(result);
            return value;

        }
    }
}
