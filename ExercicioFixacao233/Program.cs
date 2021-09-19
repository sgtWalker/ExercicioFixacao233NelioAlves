using ExercicioFixacao233.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

#region Enunciado

/*
    Fazer um programa para ler os dados (nome, email e salário)
    de funcionários a partir de um arquivo em formato .csv.
    Em seguida mostrar, em ordem alfabética, o email dos
    funcionários cujo salário seja superior a um dado valor
    fornecido pelo usuário.
    Mostrar também a soma dos salários dos funcionários cujo
    nome começa com a letra 'M'.
    Input file:
    Maria,maria@gmail.com,3200.00
    Alex,alex@gmail.com,1900.00
    Marco,marco@gmail.com,1700.00
    Bob,bob@gmail.com,3500.00
    Anna,anna@gmail.com,2800.00

    Execution:
    Enter full file path: c:\temp\in.txt
    Enter salary: 2000.00
    Email of people whose salary is more than 2000.00:
    anna@gmail.com
    bob@gmail.com
    maria@gmail.com
    Sum of salary of people whose name starts with 'M': 4900.00
 */

#endregion

namespace ExercicioFixacao233
{
    class Program
    {
        private static void Main(string[] args)
        {
            string arquivo = SolicitarCaminhoArquivo();
            ValidarExtensao(arquivo);
            var funcionarios = ProcessarArquivo(arquivo);
            var salario = SolicitarValorParaPesquisa();
            ImprimirFuncionariosSalarioInformado(funcionarios, salario);
            ImprimirSomaSalariosLetraM(funcionarios);

        }

        private static string SolicitarCaminhoArquivo()
        {
            try
            {
                Console.Write("Entre com o caminho completo do arquivo: ");
                string arquivo = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(arquivo))
                    throw new Exception("Informe o caminho completo do arquivo!");

                return arquivo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Método: SolicitarCaminhoArquivo, erro: {ex.Message} - {(ex.InnerException == null ? "" : ex.InnerException.Message)}");
            }
        }

        private static void ValidarExtensao(string arquivo)
        {
            DirectoryInfo dInfo = new DirectoryInfo(arquivo);

            if (dInfo.Extension != ".csv")
                throw new Exception("Informe apenas arquivos com a extensão .csv");
        }

        private static List<Employee> ProcessarArquivo(string arquivo)
        {
            try
            {
                List<Employee> funcionarios = new List<Employee>();

                using(var sr = File.OpenText(arquivo))
                {
                    while (!sr.EndOfStream)
                    {
                        string linha = sr.ReadLine();
                        if (!string.IsNullOrWhiteSpace(linha))
                        {
                            var quebraLinha = linha.Split(',');
                            funcionarios.Add(new Employee
                            {
                                Name = quebraLinha[0],
                                Email = quebraLinha[1],
                                Sallary = double.Parse(quebraLinha[2], CultureInfo.InvariantCulture)
                            });
                        }
                    }
                }

                return funcionarios;
            }
            catch (Exception ex)
            {
                throw new Exception($"Método: ProcessarArquivo, erro: {ex.Message} - {(ex.InnerException == null ? "" : ex.InnerException.Message)}");
            }
        }

        private static double SolicitarValorParaPesquisa()
        {
            try
            {
                Console.Write("Informe o salário: ");
                return double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new Exception($"Método: SolicitarValorParaPesquisa, erro: {ex.Message} - {(ex.InnerException == null ? "" : ex.InnerException.Message)}");
            }

        }

        private static void ImprimirFuncionariosSalarioInformado(List<Employee> funcionarios, double salario)
        {
            try
            {
                var funcionariosFiltrados = funcionarios
                    .Where(x => x.Sallary > salario)
                    .OrderBy(x => x.Name)
                    .ToList();

                funcionariosFiltrados.ForEach(x => Console.WriteLine(x.Email));

            }
            catch (Exception ex)
            {
                throw new Exception($"Método: ImprimirFuncionariosSalarioInformado, erro: {ex.Message} - {(ex.InnerException == null ? "" : ex.InnerException.Message)}");
            }
        }
        
        private static void ImprimirSomaSalariosLetraM(List<Employee> funcionarios)
        {
            try
            {
                var soma = funcionarios
                    .Where(x => x.Name.ToLower().StartsWith("m"))
                    .Sum(x => x.Sallary);

                Console.WriteLine($"Soma dos salários das pessoas que o nome começam com a letra 'M': {soma}");
                    
            }
            catch (Exception ex)
            {
                throw new Exception($"Método: ImprimirSomaSalariosLetraM, erro: {ex.Message} - {(ex.InnerException == null ? "" : ex.InnerException.Message)}");
            }
        }
    }
}



