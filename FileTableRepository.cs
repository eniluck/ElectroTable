using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
        /// Метод создает зубчатый массив ячеек из массива строк
        /// </summary>
        /// <param name="lines">Массив строк</param>
        /// <returns>Массив ячеек</returns>
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

        /// <summary>
        /// Метод создает массив ячеек из строки
        /// </summary>
        /// <param name="line">строка</param>
        /// <returns>Массив ячеек</returns>
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

        /// <summary>
        /// Метод сохраняет в файл массив ячеек
        /// </summary>
        /// <param name="table">Массив ячеек</param>
        /// <returns>Удачно ли сохранение</returns>
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

        /// <summary>
        /// Сохраняет массив ячеек в массив строк с разделителем
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
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
                        sbLine.Append($" {separator}");
                }
                allLines.Add(sbLine.ToString());
            }
            return allLines;
        }
    }
}
