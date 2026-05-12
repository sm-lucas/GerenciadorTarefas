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

        }
    }
}