using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroTable
{
    public class FileTableRepository : ITableRepository
    {
        //входная директория
        private readonly string directory;
        private const string inFilename = "in.txt";
        private const string outFilename = "out.txt";
        //Разделитель в файле
        private readonly char separator;
        //private readonly string fileOutName;

        public FileTableRepository(string directory, char separator)
        {
            this.directory = directory;
            this.separator = separator;
        }

        /// <summary>
        /// Получить все данные из файла
        /// </summary>
        /// <returns>Массив ячеек</returns>
        public Cell[][] GetAll()
        {
            string fileInName = Path.Combine(directory, inFilename);
            if (File.Exists(fileInName))
            {
                IEnumerable<string> stringLines = File.ReadLines(fileInName);
                return LinesToArray(stringLines.ToArray());
            }
            throw new FileNotFoundException("Выбранный для чтения файл ненайден");
        }

        /// <summary>
        /// Массив ячеек из строк
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        private Cell[][] LinesToArray(string[] lines)
        {
            int n = lines.Count();
            Cell[][] resultArray = new Cell[n][];

            for (int i = 0; i < n; i++)
            {
                resultArray[i] = LineToCells(lines[i]);
            }

            return resultArray;
        }

        private Cell[] LineToCells(string line)
        {
            string[] stringArray = line.Split(separator);

            Cell[] cells = new Cell[stringArray.Length];
            for (int i = 0; i < stringArray.Length; i++)
            {
                cells[i] = new Cell() { Data = stringArray[i] };
            }

            return cells;
        }

        public bool SaveAll(Cell[][] table)
        {
            List<string> allLines = getLines(table);

            string fileOutName = Path.Combine(directory, outFilename);

            using (TextWriter writer = new StreamWriter(fileOutName))
            {
                foreach (String line in allLines)
                    writer.WriteLine(line);
            }
            return true;
        }

        private List<string> getLines(Cell[][] table)
        {
            List<string> allLines = new List<string>();
            for (int i = 0; i < table.Length; i++)
            {
                StringBuilder sbLine = new StringBuilder();
                for (int j = 0; j < table[i].Length; j++)
                {
                    sbLine.Append(table[i][j].Value);
                    if (j != table[i].Length - 1)
                        sbLine.Append(" ,");
                }
                allLines.Add(sbLine.ToString());
            }
            return allLines;
        }
    }
}
