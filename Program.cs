using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroTable
{
    class Program
    {
        static void Main(string[] args)
        {
            //читаем файл
            string path = Directory.GetCurrentDirectory();
            FileTableRepository fileTableRepository = new FileTableRepository(path, ',');

            //производим подсчеты
            Cell[][] cells = fileTableRepository.GetAll();
            Table table = new Table(cells);
            table.CalculateTable();

            //выводим для просмотра
            table.Display();

            //сохраняем в файл
            fileTableRepository.SaveAll(cells);
            Console.WriteLine("Нажмите любую клавишу для закрытия окна");
            Console.ReadLine();
        }
    }
}
