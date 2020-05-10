using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElectroTable
{
    public class Table
    {
        //Зубчатый массив
        private readonly Cell[][] cells;

        /// <summary>
        /// Количество строк массива
        /// </summary>
        public int Rows {
            get
            {
                return cells.Length;
            }
        }

        public Table(Cell[][] cells)
        {
            this.cells = cells;
            initCells();
        }

        /// <summary>
        /// Метод для инициализации ячеек
        /// </summary>
        public void initCells()
        {
            //зададим имена для каждой ячейки R<строка>С<столбец>
            for (int i = 0; i < cells.Length; i++)
            {
                for (int j = 0; j < cells[i].Length; j++)
                {
                    cells[i][j].Name = $"R{i+1}C{j+1}";
                }
            }
        }

        /// <summary>
        /// Получить количество столбцов у строки
        /// </summary>
        /// <returns>количество столбцов</returns>
        public int getRowColls(int row)
        {
            return cells[row].Length;
        }

        /// <summary>
        /// Получить ячейку по имени 
        /// </summary>
        /// <param name="cellName">Имя ячейки. Например: R3C4 </param>
        /// <returns>Ячейка таблицы</returns>
        public Cell GetCellByName(string cellName)
        {
            int rPosition = cellName.IndexOf('R');
            int cPostion = cellName.IndexOf('C');

            //TODO: Проверка на вид R<число>C<число>
            string sCellRow = cellName.Substring(rPosition+1, cPostion - rPosition - 1);
            string sCellColl = cellName.Substring(cPostion+1, cellName.Length - cPostion - 1);

            int iCellRow = Int32.Parse(sCellRow);
            int iCellCol = Int32.Parse(sCellColl);

            //TODO: Сравнить количество строк и столбцов у массива и iCellRow, iCellColl. Если выходит за размер - то вывести ошибку

            return cells[iCellRow-1][iCellCol-1];
        }
            
        /// <summary>
        /// Основной метод для запуска расчета значений ячеек
        /// </summary>
        public void CalculateTable()
        {
            for (int i = 0; i < cells.Length; i++)
            {
                for (int j = 0; j < cells[i].Length; j++)
                {
                    ReplaceCellReferences(cells[i][j]);
                    CalculateCell(cells[i][j]);
                }
            }
        }

        /// <summary>
        /// Метод для очистки ячейки от ссылок
        /// </summary>
        /// <param name="cell">ячейка</param>
        public void ReplaceCellReferences(Cell cell)
        {
            if (cell.IsCleared)
                return;

            //Если ячейка содержит ссылку, то 
            IEnumerable<string> references = getReferencesFromString(cell.Data);
            if (references != null)
            {
                string replacedString = cell.Data;

                foreach (var cellName in references)
                {
                    //Возьмем ячейку по имени
                    Cell currentCell = GetCellByName(cellName);
 
                    //Если ячейка не очищена от ссылок, то сделать очистку
                    if (!currentCell.IsCleared) {
                        ReplaceCellReferences(currentCell );
                    }

                    //вычеслить значение ячейки
                    if (!currentCell.IsCalulated)
                        CalculateCell(currentCell );

                    //заменить в строке ссылку на вычесленное значение
                    replacedString = replacedString.Replace(cellName, currentCell.Value.ToString());
                    
                    //TODO: Подумать как не зациклиться
                }
                cell.ClearData = replacedString;

            } else
            {
                cell.ClearData = cell.Data;
                cell.IsCleared = true;
            }
        }

        /// <summary>
        /// Вычисляет значение
        /// </summary>
        /// <param name="cell">Ячейка</param>
        public void CalculateCell(Cell cell)
        {
            //если уже вычислено, то возвращаемся
            if (cell.IsCalulated)
                return;

            //вычисляем значение
            cell.Value = RPN.Calculate(cell.ClearData);
            cell.IsCalulated = true;
        }

        /// <summary>
        /// Находит в строке ссылки на другие ячейки
        /// </summary>
        /// <param name="data">строка для поиска</param>
        /// <returns>список найденных ссылок</returns>
        public IEnumerable<string> getReferencesFromString(string data)
        {
            //Регулярное выражение для поиска ссылки вида R<число>C<число>
            Regex regex = new Regex(@"R(\d*)C(\d*)");
            MatchCollection matchCollection = regex.Matches(data);
            if (matchCollection.Count>0)
            {
                List<string> result = new List<string>();
                foreach (Match match in matchCollection)
                {
                    result.Add(match.Value);
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// Вывод на консоль
        /// </summary>
        public void Display()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < getRowColls(i); j++)
                {
                    Console.Write(cells[i][j]);
                    if (j != getRowColls(i)-1) 
                        Console.Write(" ,");
                }
                Console.WriteLine();
            }
        }
    }
}
