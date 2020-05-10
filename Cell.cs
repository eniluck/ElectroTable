namespace ElectroTable
{
    public class Cell
    {
        /// <summary>
        /// Имя ячейки
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Выражение
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Флаг является ли выражение очищенным
        /// </summary>
        public bool IsCleared { get; set; }

        /// <summary>
        /// Выражение без ссылок на другие ячейки
        /// </summary>
        public string ClearData { get; set; }

        /// <summary>
        /// Флаг вычислено ли значение
        /// </summary>
        public bool IsCalulated { get; set; }

        /// <summary>
        /// Вычисленное значениe
        /// </summary>
        public int Value { get; set; }

        public override string ToString()
        {
            return $"{Name}({Data}={Value})";
        }
    }
}