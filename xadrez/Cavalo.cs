using tabuleiro;

namespace xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

        public override string ToString()
        {
            return "C";
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tabuleiro.linhas, tabuleiro.colunas];
            Posicao aux = new Posicao(0, 0);
            int[] advance = new int[] { 2, -2 };
            int[] displace = new int[] { 1, -1 };

            foreach (int linha in advance)
            {
                foreach (int coluna in displace)
                {
                    aux.linha = posicao.linha + linha;
                    aux.coluna = posicao.coluna + coluna;
                    if (tabuleiro.posicaoValida(aux) && podeMover(aux))
                    {
                        mat[aux.linha, aux.coluna] = true;
                    }
                }
            }

            foreach (int coluna in advance)
            {
                foreach (int linha in displace)
                {
                    aux.linha = posicao.linha + linha;
                    aux.coluna = posicao.coluna + coluna;
                    if (tabuleiro.posicaoValida(aux) && podeMover(aux))
                    {
                        mat[aux.linha, aux.coluna] = true;
                    }
                }
            }

            return mat;
        }
    }
}
