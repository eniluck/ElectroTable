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
        private readonly string fileInName;
        private readonly char separator;
        //private readonly string fileOutName;

        public FileTableRepository(string fileName, char separator)
        {
            this.fileInName = fileName;
            this.separator = separator;
        }

        /// <summary>
        /// Получить все данные из файла
        /// </summary>
        /// <returns>Массив ячеек</returns>
        public Cell[][] GetAll()
        {
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
            throw new NotImplementedException();
        }
    }
}
