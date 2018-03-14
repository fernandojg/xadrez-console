using tabuleiro;

namespace xadrez
{
    class Dama : Peca
    {
        public Dama(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

        public override string ToString()
        {
            return "D";
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

            //verifica o movimento para o norte
            aux.definirPosicao(posicao.linha - 1, posicao.coluna);
            while (tabuleiro.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
                if (tabuleiro.peca(aux) != null) break;
                aux.linha--;
            }

            //verifica o movimento para o leste
            aux.definirPosicao(posicao.linha, posicao.coluna + 1);
            while (tabuleiro.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
                if (tabuleiro.peca(aux) != null) break;
                aux.coluna++;
            }

            //verifica o movimento para o sul
            aux.definirPosicao(posicao.linha + 1, posicao.coluna);
            while (tabuleiro.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
                if (tabuleiro.peca(aux) != null) break;
                aux.linha++;
            }

            //verifica o movimento para o oeste
            aux.definirPosicao(posicao.linha, posicao.coluna - 1);
            while (tabuleiro.posicaoValida(aux) && podeMover(aux))
            {
                mat[aux.linha, aux.coluna] = true;
                if (tabuleiro.peca(aux) != null) break;
                aux.coluna--;
            }

            return mat;
        }
    }
}
