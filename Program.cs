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
            string path = Directory.GetCurrentDirectory();
            FileTableRepository fileTableRepository = new FileTableRepository(path, ',');

            Cell[][] cells = fileTableRepository.GetAll();
            Table table = new Table(cells);
            table.CalculateTable();

            table.Display();

            fileTableRepository.SaveAll(cells);
            Console.ReadLine();
        }
    }
}
