using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

        public override string ToString()
        {
            return "R";
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tabuleiro.linhas, tabuleiro.colunas];
            Posicao aux = new Posicao(0, 0);

            for (int i = posicao.linha - 1; i <= posicao.linha + 1; i++)
            {
                for (int j = posicao.coluna - 1; j <= posicao.coluna + 1 ; j++)
                {
                    aux.definirPosicao(i, j);
                    if (tabuleiro.posicaoValida(aux) && podeMover(aux))
                    {
                        mat[i, j] = true;
                    }
                }
            }
            return mat;
        }
    }
}
