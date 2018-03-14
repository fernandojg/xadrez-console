using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

        public override string ToString()
        {
            return "P";
        }

        private bool podeCapturar(Posicao posicao)
        {
            Peca peca = tabuleiro.peca(posicao);
            if (peca != null && peca.cor != cor)
            {
                return true;
            }
            return false;
        }

        protected override bool podeMover(Posicao posicao)
        {
            Peca peca = tabuleiro.peca(posicao);
            return peca == null;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tabuleiro.linhas, tabuleiro.colunas];
            Posicao aux = new Posicao(0, 0);

            if (cor == Cor.Branca)
            {
                aux.definirPosicao(posicao.linha - 1, posicao.coluna);
                if (tabuleiro.posicaoValida(aux) && podeMover(aux))
                {
                    mat[aux.linha, aux.coluna] = true;
                }

                aux.definirPosicao(posicao.linha - 2, posicao.coluna);
                if (qtdMovimentos == 0 && tabuleiro.posicaoValida(aux) && podeMover(aux))
                {
                    mat[aux.linha, aux.coluna] = true;
                }

                aux.definirPosicao(posicao.linha - 1, posicao.coluna + 1);
                if (tabuleiro.posicaoValida(aux) && podeCapturar(aux))
                {
                    mat[aux.linha, aux.coluna] = true;
                }

                aux.definirPosicao(posicao.linha - 1, posicao.coluna - 1);
                if (tabuleiro.posicaoValida(aux) && podeCapturar(aux))
                {
                    mat[aux.linha, aux.coluna] = true;
                }
            }
            else
            {
                aux.definirPosicao(posicao.linha + 1, posicao.coluna);
                if (tabuleiro.posicaoValida(aux) && podeMover(aux))
                {
                    mat[aux.linha, aux.coluna] = true;
                }

                aux.definirPosicao(posicao.linha + 2, posicao.coluna);
                if (qtdMovimentos == 0 && tabuleiro.posicaoValida(aux) && podeMover(aux))
                {
                    mat[aux.linha, aux.coluna] = true;
                }

                aux.definirPosicao(posicao.linha + 1, posicao.coluna + 1);
                if (tabuleiro.posicaoValida(aux) && podeCapturar(aux))
                {
                    mat[aux.linha, aux.coluna] = true;
                }

                aux.definirPosicao(posicao.linha + 1, posicao.coluna - 1);
                if (tabuleiro.posicaoValida(aux) && podeCapturar(aux))
                {
                    mat[aux.linha, aux.coluna] = true;
                }
            }
            return mat;
        }
    }
}
