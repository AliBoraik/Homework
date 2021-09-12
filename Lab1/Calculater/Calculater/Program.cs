﻿using System;

namespace Calculater
{
    class Program
    {
        static int Main(string[] args)
        {         
            if (args.Length == 3)
            {
                string numbver_1 ;
                string numbver_2 ;
                string operation ;

                MyCalculater myCalcuater = new MyCalculater();

                numbver_1 = args[0];
                operation = args[1];
                numbver_2 = args[2];

                if (myCalcuater.IsNumbers(numbver_1, numbver_2))
                {
                    if (MyCalculater.IsOperation(operation))
                    {
                        Console.WriteLine(myCalcuater.Proces(numbver_1, operation, numbver_2));
                        return 0;
                    }
                }
            }
            else
            {
                Console.WriteLine("неверное количество аргументов");
            }
            return 1;
        }
    }
}
