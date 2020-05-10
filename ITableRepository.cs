using System.Collections.Generic;

namespace ElectroTable
{
    internal interface ITableRepository
    {
        /// <summary>
        /// Получить все данные из источника
        /// </summary>
        /// <returns>Зубчатый (jugged) массив ячеек</returns>
        Cell[][] GetAll();

        /// <summary>
        /// Сохраняет массив ячеек
        /// </summary>
        /// <param name="table">Массив ячеек</param>
        /// <returns>Результат сохранения</returns>
        bool SaveAll(Cell[][] table);
    }
}