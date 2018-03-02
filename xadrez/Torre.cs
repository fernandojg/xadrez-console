using tabuleiro;

namespace xadrez
{
    class Torre : Peca
    {
        public Torre(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

        public override string ToString()
        {
            return "T";
        }

        private bool podeMover(Posicao posicao)
        {
            Peca peca = tabuleiro.peca(posicao);
            return peca == null || peca.cor != cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tabuleiro.linhas, tabuleiro.colunas];
            Posicao aux = new Posicao(0, 0);

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
