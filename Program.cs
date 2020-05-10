using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroTable
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"D:\Sources\ElectroTable\ElectroTable\bin\Debug\in.txt";
            FileTableRepository fileTableRepository = new FileTableRepository(fileName,',');
            Cell[][] cells = fileTableRepository.GetAll();

            Table table = new Table(cells);
            table.CalculateTable();

            table.Display();
            Console.ReadLine();
        }
    }
}
