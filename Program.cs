using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GerenciadorTarefas
{
    // Modelo da Tarefa
    class Tarefa
    {
        public string Descricao { get; set; }
        public bool Concluida { get; set; }

        public override string ToString()
        {
            return $"[{(Concluida ? "X" : " ")}] {Descricao}";
        }
    }

    class Program
    {
        // Variáveis globais para o banco de dados em memória e o nome do arquivo
        static List<Tarefa> listaTarefas = new List<Tarefa>();
        static string caminhoArquivo = "minhas_tarefas.txt";

        static void Main(string[] args)
        {
            // Tenta carregar dados salvos anteriormente ao iniciar
            CarregarDadosDoArquivo();

            bool executando = true;
            while (executando)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("      GERENCIADOR DE TAREFAS .NET       ");
                Console.WriteLine("========================================");
                // Exibe a lista de tarefas atual
                if (listaTarefas.Count == 0)
                {
                    Console.WriteLine("\nSua lista está vazia.");
                }
                else
                {
                    for (int i = 0; i < listaTarefas.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {listaTarefas[i]}");
                    }
                }

                Console.WriteLine("\n----------------------------------------");
                Console.WriteLine("1. Adicionar Tarefa");
                Console.WriteLine("2. Marcar como Concluída/Pendente");
                Console.WriteLine("3. Limpar Tarefas Concluídas");
                Console.WriteLine("4. Sair");
                Console.Write("\nEscolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Adicionar();
                        break;
                    case "2":
                        AlternarStatus();
                        break;
                    case "3":
                        LimparConcluidas();
                        break;
                    case "4":
                        executando = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Pressione qualquer tecla.");
                        Console.ReadKey();
                        break;
                }

            }
        }
    }
}