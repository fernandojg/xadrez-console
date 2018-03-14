using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Partida partida = new Partida();
            while (!partida.terminada)
            {
                try
                {
                    Tela.executarTurno(partida);
                }
                catch (TabuleiroException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro inesperado: " + e.Message);
                    Console.ReadLine();
                }
            }
            Tela.showWinner(partida);
            Console.ReadLine();
        }
    }
}
