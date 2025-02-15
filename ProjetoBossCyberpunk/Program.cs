﻿using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualBasic.FileIO;

namespace MyApp
{
    internal class Program
    {
        // Lista para armazenar os inimigos
        static List<Inimigo> inimigos = new List<Inimigo>();

        static void Main(string[] args)
        {
            Console.WriteLine(@"
░█████╗░██╗░░░██╗██████╗░███████╗██████╗░██████╗░██╗░░░██╗███╗░░██╗██╗░░██╗  ██████╗░███████╗██████╗░
██╔══██╗╚██╗░██╔╝██╔══██╗██╔════╝██╔══██╗██╔══██╗██║░░░██║████╗░██║██║░██╔╝  ██╔══██╗██╔════╝██╔══██╗
██║░░╚═╝░╚████╔╝░██████╦╝█████╗░░██████╔╝██████╔╝██║░░░██║██╔██╗██║█████═╝░  ██████╔╝█████╗░░██║░░██║
██║░░██╗░░╚██╔╝░░██╔══██╗██╔══╝░░██╔══██╗██╔═══╝░██║░░░██║██║╚████║██╔═██╗░  ██╔══██╗██╔══╝░░██║░░██║
╚█████╔╝░░░██║░░░██████╦╝███████╗██║░░██║██║░░░░░╚██████╔╝██║░╚███║██║░╚██╗  ██║░░██║███████╗██████╔╝
░╚════╝░░░░╚═╝░░░╚═════╝░╚══════╝╚═╝░░╚═╝╚═╝░░░░░░╚═════╝░╚═╝░░╚══╝╚═╝░░╚═╝  ╚═╝░░╚═╝╚══════╝╚═════╝░
");
            while (true)
            {                
                ExibirMenu();
                int opcaoMenu = int.Parse(Console.ReadLine());
                switch (opcaoMenu)
                {
                    case 0:
                        Console.WriteLine("Volte sempre");
                        return;
                    case 1:
                        CriarInimigo();
                        Console.Clear();
                        break;
                    case 2:
                        ListarInimigos();                      
                        break;
                    case 3:
                        AtirarInimigos();                      
                        break;
                    case 4:
                        CortarInimigos();
                        break;
                    case 5:
                        DeletarInimigos();
                        
                        break;
                    case 6:
                        AlterarInimigos();
                        
                        break;
                    default:
                        Console.WriteLine("Opção inválida, favor tentar novamente.");
                        
                        break;
                }
            }
        }
        static void DeletarInimigos() {
            if (inimigos.Count == 0)
            {
                Console.WriteLine("Nenhum inimigo foi registrado.");
                return ;
            }

            ListarInimigos();
            Console.WriteLine("\nSelecione o inimigo para deletar.");
            int inimigoIndex = int.Parse(Console.ReadLine()) - 1;

            if (inimigoIndex < 0 || inimigoIndex >= inimigos.Count)
            {
                Console.WriteLine("Inimigo inválido.");
                return;
            }
            Inimigo inimigo = inimigos[inimigoIndex];
            Console.WriteLine($"\nVocê selecionou {inimigo.Nome}.");
            inimigos.Remove(inimigo);
            Console.WriteLine("Inimigo removido com sucesso!");

        }
        static void AlterarInimigos() { }
        static void ExibirMenu()
        {
            Console.WriteLine("\n-----------MENU--------------------");
            Console.WriteLine("Digite 0 para sair do programa");
            Console.WriteLine("Digite 1 para registrar um novo Inimigo");
            Console.WriteLine("Digite 2 para ver todos os inimigos");
            Console.WriteLine("Digite 3 para dar dano por disparo");
            Console.WriteLine("Digite 4 para dar dano corpo a corpo");
            Console.WriteLine("Digite 5 para Deletar um inimigo existente");
            Console.WriteLine("Digite 6 para Alterar as propriedades de um inimigo");
        }

        static void CriarInimigo()
        {
            Console.WriteLine("\nDigite o nome do Inimigo:");
            string nome = Console.ReadLine();
            Console.WriteLine($"\nDigite a vida de {nome}:");
            int vida = int.Parse(Console.ReadLine());
            Console.WriteLine($"\nDigite a armadura de {nome}:");
            int armadura = int.Parse(Console.ReadLine());
            Console.WriteLine($"\nDigite a iniciativa de {nome}");
            int iniciativa = int.Parse(Console.ReadLine());

            // Adicionar o inimigo à lista
            inimigos.Add(new Inimigo(nome, vida, armadura, iniciativa));
            Console.WriteLine("\nInimigo registrado com sucesso!");
        }

        static void ListarInimigos()
        {
            if (inimigos.Count == 0)
            {
                Console.WriteLine("Nenhum inimigo foi registrado ainda.");
                return;
            }

            Console.WriteLine("\n----- Lista de Inimigos ----- (POR INICIATIVA)");
            var inimigosOrdenados = inimigos.OrderByDescending (i => i.Iniciativa);
            for (int i = 0; i < inimigos.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {inimigos[i].Nome} - Vida: {inimigos[i].Vida}, Armadura: {inimigos[i].Armadura}, Iniciativa: {inimigos[i].Iniciativa}");
            }
        }

        static void CortarInimigos()
        {
            if (inimigos.Count == 0)
            {
                Console.WriteLine("Nenhum inimigo foi registrado.");
                return;
            }
            ListarInimigos();
            Console.WriteLine("\nSelecione o inimigo que deseja danificar.");
            int inimigoIndex = int.Parse(Console.ReadLine()) - 1;

            if (inimigoIndex < 0 || inimigoIndex >= inimigos.Count)
            {
                Console.WriteLine("Inimigo inválido.");
                return;
            }

            Inimigo inimigo = inimigos[inimigoIndex];
            Console.WriteLine($"\nVocê selecionou {inimigo.Nome}.");
            Console.WriteLine($"Quando de dano corpo a corpo {inimigo.Nome} levou?");
            string danoInput = Console.ReadLine();

            if (!string.IsNullOrEmpty(danoInput))
            {
                int dano = int.Parse(danoInput);
                if (dano <= (inimigo.Armadura / 2))
                {
                    Console.WriteLine($"Como o dano foi menor ou igual a armadura de {inimigo.Nome}, nenhum dano foi aplicado");
                }
                else
                {
                    // Se o dano for maior que a armadura, subtrai a armadura e aplica o restante na vida
                    int danoEfetivo = dano - inimigo.Armadura;
                    inimigo.Vida -= danoEfetivo; // Subtrai o dano efetivo da vida do inimigo

                    // Diminui 1 da armadura do inimigo
                    inimigo.Armadura -= 1;
                    if (inimigo.Armadura <= 0)
                    {
                        inimigo.Armadura = 0;
                    }

                    if (inimigo.Vida <= 0)
                    {
                        Console.WriteLine($"você acabou com {inimigo.Nome}! ({inimigo.Nome} foi removido)");
                        inimigos.Remove(inimigo); //mata o inimigo e o remove da lista
                    }
                    else
                    {
                        // Exibe o resultado
                        Console.WriteLine($"{inimigo.Nome} agora tem {inimigo.Vida} de vida e {inimigo.Armadura} de armadura.");
                    }
                }

            }
            else
            {
                Console.WriteLine("Nenhum dano foi aplicado.");
            }
        }

        static void AtirarInimigos()
        {
            if (inimigos.Count == 0)
            {
                Console.WriteLine("Nenhum inimigo detectado.");
                return;
            }

            // Exibe a lista de inimigos criados para o usuario selecionar
            ListarInimigos();

            Console.WriteLine("\nSelecione o inimigo que você deseja danificar:");
            int inimigoIndex = int.Parse(Console.ReadLine()) - 1; // Subtrai 1 para acessar a lista corretamente

            if (inimigoIndex < 0 || inimigoIndex >= inimigos.Count)
            {
                Console.WriteLine("Inimigo inválido.");
                return;
            }

            Inimigo inimigo = inimigos[inimigoIndex];
            Console.WriteLine($"\nVocê selecionou {inimigo.Nome}.");
            Console.WriteLine($"Quanto de dano {inimigo.Nome} levou? (pressione Enter para não dar dano):");
            string danoInput = Console.ReadLine();

            // Verifica se o usuário inseriu algum valor para dano
            if (!string.IsNullOrEmpty(danoInput))
            {
                int dano = int.Parse(danoInput); // Converte a entrada para inteiro

                // Verifica se o dano é menor ou igual à armadura
                if (dano <= inimigo.Armadura)
                {
                    Console.WriteLine($"Como o dano foi menor ou igual à armadura de {inimigo.Nome}, nenhum dano foi aplicado.");
                }
                else
                {
                    // Se o dano for maior que a armadura, subtrai a armadura e aplica o restante na vida
                    int danoEfetivo = dano - inimigo.Armadura;
                    inimigo.Vida -= danoEfetivo; // Subtrai o dano efetivo da vida do inimigo

                    // Diminui 1 da armadura do inimigo
                    inimigo.Armadura -= 1;
                    if (inimigo.Armadura <= 0)
                    {
                        inimigo.Armadura = 0;
                    }

                    if (inimigo.Vida <= 0)
                    {
                        Console.WriteLine($"você acabou com {inimigo.Nome}! Acaba com eles Choom");
                        inimigos.Remove(inimigo); //mata o inimigo e o remove da lista
                    }
                    else
                    {
                        // Exibe o resultado
                        Console.WriteLine($"{inimigo.Nome} agora tem {inimigo.Vida} de vida e {inimigo.Armadura} de armadura.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Nenhum dano foi aplicado.");
            }
        }
    }

    // Classe para representar um Inimigo
    class Inimigo
    {
        public string Nome { get; set; }
        public int Vida { get; set; }
        public int Armadura { get; set; }
       
        public int Iniciativa { get; set; }

        public Inimigo(string nome, int vida, int armadura, int iniciativa)
        {
            Nome = nome;
            Vida = vida;
            Armadura = armadura;
            Iniciativa = iniciativa;
        }
    }
}