using tabuleiro;

namespace xadrez
{
    class Bispo : Peca
    {
        public Bispo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

        public override string ToString()
        {
            return "B";
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tabuleiro.linhas, tabuleiro.colunas];
            Posicao aux = new Posicao(0, 0);

            for (int i = 1; i < tabuleiro.linhas; i++)
            {
                aux.linha = posicao.linha + i;
                aux.coluna = posicao.coluna + i;
                if (tabuleiro.posicaoValida(aux))
                {
                    if (podeMover(aux))
                    {
                        mat[aux.linha, aux.coluna] = true;
                    }
                    if (tabuleiro.peca(aux) != null) break;
                }
            }

            for (int i = 1; i < tabuleiro.linhas; i++)
            {
                aux.linha = posicao.linha + i;
                aux.coluna = posicao.coluna - i;
                if (tabuleiro.posicaoValida(aux))
                {
                    if (podeMover(aux))
                    {
                        mat[aux.linha, aux.coluna] = true;
                    }
                    if (tabuleiro.peca(aux) != null) break;
                }
            }

            for (int i = 1; i < tabuleiro.linhas; i++)
            {
                aux.linha = posicao.linha - i;
                aux.coluna = posicao.coluna + i;
                if (tabuleiro.posicaoValida(aux))
                {
                    if (podeMover(aux))
                    {
                        mat[aux.linha, aux.coluna] = true;
                    }
                    if (tabuleiro.peca(aux) != null) break;
                }
            }

            for (int i = 1; i < tabuleiro.linhas; i++)
            {
                aux.linha = posicao.linha - i;
                aux.coluna = posicao.coluna - i;
                if (tabuleiro.posicaoValida(aux))
                {
                    if (podeMover(aux))
                    {
                        mat[aux.linha, aux.coluna] = true;
                    }
                    if (tabuleiro.peca(aux) != null) break;
                }
            }
            return mat;
        }
    }
}
