﻿namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qtdMovimentos { get; protected set; }
        public Tabuleiro tabuleiro { get; protected set; }

        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            this.tabuleiro = tabuleiro;
            this.cor = cor;
            qtdMovimentos = 0;
            posicao = null;
        }

        public abstract bool[,] movimentosPossiveis();

        public void incrementarQtdMovimentos()
        {
            qtdMovimentos++;
        }
    }
}
