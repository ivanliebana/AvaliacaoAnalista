﻿/*
 Você deve implementar nessa aplicação console uma lógica que satisfaça os seguintes requisitos:
    1 - A aplicação deve solicitar ao usuário que ele digite um número.
    2 - A aplicação deve ler essa entrada e garantir que foi digitado um número inteiro, positivo, maior que 0 e menor que 20.
    3 - A aplicação deve gerar uma sequência de Fibonacci com a quantidade de repetições informada pelo usuário.
    Obs: A seguência de Fibonacci segue o padrão 1,1,2,3,5,8 e assim por diante, sendo o próximo número a soma do último número com o seu anterior.
*/

using Helper;

namespace Teste2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write(CustomMessage.MSG0003);
                var input = Console.ReadLine();

                if (input.IsNumericText())
                {
                    var enteredNumber = input.ToInt32();

                    if (enteredNumber <= 0 || enteredNumber >= 20)
                        Console.WriteLine(CustomMessage.MSG0005);
                    else
                    {
                        var result = Util.GetFibonacciFrom(1, enteredNumber);
                        Console.WriteLine($"Sequência Fibonacci: {string.Join(", ", result)}");
                        break;
                    }
                }
                else
                    Console.WriteLine(CustomMessage.MSG0004);
            }
        }
    }
}