using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;

namespace CourseProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string fileName = string.Empty;
        private DataTable table;

        List<Smartphone> smartphones = new List<Smartphone>()
        {
            new Smartphone
            {
                Resolution=(1080,2160) ,
                Weight=180,
                BateryVolume=2100,
                MaxFrequency=2400,
                RAM = 4,
                SizeOfStorage=64,
                MatrixOfCamera = 13,
                Speaker=TypeOfSpeaker.Mono,
                Price = 2700.50
            },
            new Smartphone
            {
                Resolution=(828,1792) ,
                Weight=196,
                BateryVolume=2500,
                MaxFrequency=2500,
                RAM = 4,
                SizeOfStorage=64,
                MatrixOfCamera = 12,
                Speaker=TypeOfSpeaker.Stereo,
                Price = 4000.80
            },
            new Smartphone
            {
                Resolution=(1920,1080),
                Weight=186,
                BateryVolume=2000,
                MaxFrequency=1900,
                RAM = 4,
                SizeOfStorage=32,
                MatrixOfCamera = 13,
                Speaker=TypeOfSpeaker.Stereo,
                Price = 5000
            },
            new Smartphone
            {
                Resolution=(1136,640),
                Weight=186,
                BateryVolume=2000,
                MaxFrequency=2100,
                RAM = 4,
                SizeOfStorage=64,
                MatrixOfCamera = 12,
                Speaker = TypeOfSpeaker.Mono,
                Price = 9800
            },
            new Smartphone
            {
                Resolution=(1134,750),
                Weight=200,
                BateryVolume=2400,
                MaxFrequency=2500,
                RAM = 8,
                SizeOfStorage=64,
                MatrixOfCamera = 13,
                Speaker = TypeOfSpeaker.Stereo,
                Price = 10000
            }


        };

        private void Form1_Load(object sender, EventArgs e)
        {
            //dataGridView2.AllowUserToAddRows = false;
            DataTable phoneTable = new DataTable("SMARTPHONES");
            phoneTable.Columns.Add("Ім'я");
            phoneTable.Columns.Add("Розмірність");
            phoneTable.Columns.Add("Вага");
            phoneTable.Columns.Add("Макс. частота процессора");
            phoneTable.Columns.Add("Об'єм накопичувача");
            phoneTable.Columns.Add("Оперативна пам'ять");
            phoneTable.Columns.Add("Об'єм батареї");
            phoneTable.Columns.Add("Камера");
            phoneTable.Columns.Add("Тип динаміка");
            phoneTable.Columns.Add("Ціна");
            int i = 1;
            foreach (Smartphone s in smartphones)
            {
                phoneTable.Rows.Add($"Alt_{i}",
                    ConvertToString(s.Resolution), s.Weight, s.MaxFrequency, s.SizeOfStorage, s.RAM, s.BateryVolume, s.MatrixOfCamera, s.Speaker, s.Price);
                i++;
            }


            dataGridView2.DataSource = phoneTable;

            DataTable coefOfCriterii = new DataTable();
            coefOfCriterii.Columns.Add("Ім'я");
            coefOfCriterii.Columns.Add("Розмірність",typeof(double));
            coefOfCriterii.Columns.Add("Вага", typeof(double));
            coefOfCriterii.Columns.Add("Макс. частота процессора", typeof(double));
            coefOfCriterii.Columns.Add("Об'єм накопичувача", typeof(double));
            coefOfCriterii.Columns.Add("Оперативна пам'ять", typeof(double));
            coefOfCriterii.Columns.Add("Об'єм батареї", typeof(double));
            coefOfCriterii.Columns.Add("Камера", typeof(double));
            coefOfCriterii.Columns.Add("Тип динаміка", typeof(double));
            coefOfCriterii.Columns.Add("Ціна", typeof(double));

            double[] weightCoefs = { 0.12, 0.1, 0.12, 0.1, 0.12, 0.12, 0.14, 0.1, 0.07 };
            for (int j = 0; j < phoneTable.Columns.Count - 1; j++)
            {
                coefOfCriterii.Rows.Add(coefOfCriterii.Columns[j + 1].ColumnName, weightCoefs[j]);
            }

            dataGridView4.DataSource = coefOfCriterii;
            dataGridView4.AutoSizeColumnsMode =
        DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView4.AllowUserToAddRows = false;
        }

        private string ConvertToString((int, int) resolution)
        {
            return $"{resolution.Item1}x{resolution.Item2}";
        }


        private void StartWithDigit(string cell)
        {

            if (!Char.IsDigit(cell[0]))
            {
                throw new Exception(" Value doesn't start with a digit");
            }
        }

        private int MapFromSpeakerToInt(string cell)
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

        private (int, int) MapFromResolutionToInt(string cell)
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

        private int MapFromWeightToInt(string cell)
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
        private double MapFromPriceToDouble(string cell)
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

        private DataTable MakeNamesTable()
        {
            // Create a new DataTable titled 'Names.'
            DataTable namesTable = new DataTable("Names");

            // Add three column objects to the table.
            DataColumn idColumn = new DataColumn();
            idColumn.DataType = System.Type.GetType("System.Int32");
            idColumn.ColumnName = "id";
            idColumn.AutoIncrement = true;
            namesTable.Columns.Add(idColumn);

            DataColumn fNameColumn = new DataColumn();
            fNameColumn.DataType = System.Type.GetType("System.String");
            fNameColumn.ColumnName = "Fname";
            fNameColumn.DefaultValue = "Fname";
            namesTable.Columns.Add(fNameColumn);

            DataColumn lNameColumn = new DataColumn();
            lNameColumn.DataType = System.Type.GetType("System.String");
            lNameColumn.ColumnName = "LName";
            namesTable.Columns.Add(lNameColumn);

            return namesTable;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int rows = dataGridView1.Rows.Count;
            int columns = dataGridView1.Columns.Count - 3;
            int[,] matrix = new int[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = Convert.ToInt32(dataGridView1[j + 1, i].Value);
                }
            }

            double[] average = Algorithms.Average(matrix);
            double[] medians = Algorithms.MedianRang(matrix);

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[dataGridView1.ColumnCount - 2].Value = average[i];
                dataGridView1.Rows[i].Cells[dataGridView1.ColumnCount - 1].Value = medians[i];
            }

            string resultRangs = "";
            int[] addArray = new int[columns];

            Algorithms.OrderByAscending(average, addArray);
            Algorithms.SetRanking(addArray, average, ref resultRangs);

            textBox1.Text = resultRangs;

            resultRangs = "";
            Algorithms.OrderByAscending(medians, addArray);
            Algorithms.SetRanking(addArray, medians, ref resultRangs);

            textBox2.Text = resultRangs;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.Columns.Clear();

            int countOfExperts = (int)numericUpDown1.Value;
            int countOfAlternative = (int)numericUpDown2.Value;


            DataTable table = new DataTable();
            for (int i = 0; i < countOfExperts + 3; i++)
            {
                if (i == 0)
                {
                    table.Columns.Add(" № ", typeof(string));
                }
                else if (i < countOfExperts + 1)
                {
                    table.Columns.Add($" Exp_{i}", typeof(int));
                }
                else if (i == countOfExperts + 1)
                {
                    table.Columns.Add(" Avg ", typeof(double));
                }
                else
                {
                    table.Columns.Add(" MED ", typeof(double));
                }
            }

            for (int i = 0; i < countOfAlternative; i++)
            {
                table.Rows.Add($"Alt {i + 1}");
            }

            dataGridView1.DataSource = table;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView3.Columns.Clear();
            dataGridView3.AllowUserToAddRows = false;
            DataTable phoneTable = new DataTable("SMARTPHONES");
            phoneTable.Columns.Add("Ім'я");
            phoneTable.Columns.Add("Розмірність");
            phoneTable.Columns.Add("Вага");
            phoneTable.Columns.Add("Макс. частота процессора");
            phoneTable.Columns.Add("Об'єм накопичувача");
            phoneTable.Columns.Add("Оперативна пам'ять");
            phoneTable.Columns.Add("Об'єм батареї");
            phoneTable.Columns.Add("Камера");
            phoneTable.Columns.Add("Тип динаміка");
            phoneTable.Columns.Add("Ціна");
            phoneTable.Columns.Add("Коефіцієнти глобальних пріорітетів");


            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                phoneTable.Rows.Add($"Alt {i + 1}");

            }
            dataGridView3.DataSource = phoneTable;
            int rows = dataGridView2.Rows.Count - 1;
            int columns = dataGridView2.Columns.Count - 1;
            string[,] matrixStrings = new string[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrixStrings[i, j] = Convert.ToString(dataGridView2[j + 1, i].Value);

                }
            }

            (int, int)[] resolution = new (int, int)[rows];
            int[,] otherData = new int[rows, columns - 2];
            double[] prices = new double[rows];


            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (j == 0)
                    {
                        resolution[i] = MapFromResolutionToInt(matrixStrings[i, j]);
                    }
                    else if (j > 0 && j < columns - 2)
                    {
                        otherData[i, j - 1] = MapFromWeightToInt(matrixStrings[i, j]);
                    }
                    else if (j == columns - 2)
                    {
                        otherData[i, j - 1] = MapFromSpeakerToInt(matrixStrings[i, j]);
                    }
                    else
                    {
                        prices[i] = MapFromPriceToDouble(matrixStrings[i, j]);
                    }
                }
            }


            double[,] result = Algorithms.Normalize(resolution, prices, otherData, dataGridView3);

            double[] globalCoef = new double[rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    globalCoef[i] += result[i, j] * Convert.ToDouble(dataGridView4[1, j].Value);
                }
            }

            for (int i = 0; i < rows; i++)
            {
                dataGridView3[dataGridView3.Columns.Count - 1, i].Value = globalCoef[i];
            }

        }


        private void OpenExcelFile(string path)
        {
            FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read);

            IExcelDataReader reader = ExcelReaderFactory.CreateReader(fileStream);

            DataSet db = reader.AsDataSet(new ExcelDataSetConfiguration
            {
                ConfigureDataTable = (x) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }

            });

            table = db.Tables[0];
            dataGridView2.DataSource = table;

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = openFileDialog1.ShowDialog();

                if (result == DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;
                    Text = fileName;
                    OpenExcelFile(fileName);
                }
                else
                {
                    throw new Exception(" Файл не був вибраний");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
