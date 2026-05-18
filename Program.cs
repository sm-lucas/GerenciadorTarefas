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
                // Salva sempre que houver uma alteração
                SalvarDadosNoArquivo();
            }
        }

        static void Adicionar()
        {
            Console.Write("\nO que você precisa fazer? ");
            string desc = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(desc))
            {
                listaTarefas.Add(new Tarefa { Descricao = desc, Concluida = false });
            }
        }

        static void AlternarStatus()
        {
            Console.Write("\nDigite o número da tarefa: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= listaTarefas.Count)
            {
                listaTarefas[index - 1].Concluida = !listaTarefas[index - 1].Concluida;
            }
            else
            {
                Console.WriteLine("Número inválido!");
                Console.ReadKey();
            }
        }

        static void LimparConcluidas()
        {
            int removidas = listaTarefas.RemoveAll(t => t.Concluida);
            Console.WriteLine($"\n{removidas} tarefa(s) removida(s).");
            Console.ReadKey();
        }

        // Lógica para Persistência de Dados (Arquivos)
        static void SalvarDadosNoArquivo()
        {
            try
            {
                List<string> linhas = listaTarefas.Select(t => $"{t.Descricao};{t.Concluida}").ToList();
                File.WriteAllLines(caminhoArquivo, linhas);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao salvar dados: " + ex.Message);
            }
        }

        static void CarregarDadosDoArquivo()
        {
            try
            {
                if (File.Exists(caminhoArquivo))
                {
                    string[] linhas = File.ReadAllLines(caminhoArquivo);
                    foreach (string linha in linhas)
                    {
                        string[] partes = linha.Split(';');
                        if (partes.Length == 2)
                        {
                            listaTarefas.Add(new Tarefa
                            {
                                Descricao = partes[0],
                                Concluida = bool.Parse(partes[1])
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Se o arquivo estiver corrompido, inicia uma lista nova
                listaTarefas = new List<Tarefa>();
            }
        }
    }
}