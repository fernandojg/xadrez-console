using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        private Partida partida;
        public bool testingXeque;

        public Rei(Tabuleiro tabuleiro, Cor cor, Partida partida) : base(tabuleiro, cor)
        {
            this.partida = partida;
            testingXeque = false;
        }

        public override string ToString()
        {
            return "R";
        }

        private bool testeTorreRoque(Posicao posicao)
        {
            Peca peca = null;
            if (tabuleiro.posicaoValida(posicao))
            {
                peca = tabuleiro.peca(posicao);
            }    
            return (peca != null && peca is Torre && peca.qtdMovimentos == 0 && peca.cor == cor);
        }

        private bool testeRoquePequeno()
        {
            testingXeque = true;
            Posicao towerPos = new Posicao(posicao.linha, posicao.coluna + 3);
            if (!partida.xeque && qtdMovimentos == 0 && testeTorreRoque(towerPos))
            {
                Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);
                Posicao origem = new Posicao(posicao.linha, posicao.coluna);

                if (tabuleiro.peca(p1) == null && tabuleiro.peca(p2) == null)
                {
                    if (!partida.testeXeque(origem, p1) && !partida.testeXeque(origem, p2))
                    {
                        testingXeque = false;
                        return true;
                    }
                }
            }
            testingXeque = false;
            return false;
        }

        private bool testeRoqueGrande()
        {
            testingXeque = true;
            Posicao towerPos = new Posicao(posicao.linha, posicao.coluna - 4);
            if (!partida.xeque && qtdMovimentos == 0 && testeTorreRoque(towerPos))
            {
                Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);
                Posicao origem = new Posicao(posicao.linha, posicao.coluna);

                if (tabuleiro.peca(p1) == null && tabuleiro.peca(p2) == null && tabuleiro.peca(p3) == null)
                {
                    if (!partida.testeXeque(origem, p1) && !partida.testeXeque(origem, p2) && !partida.testeXeque(origem, p3))
                    {
                        testingXeque = false;
                        return true;
                    }
                }
            }
            testingXeque = false;
            return false;
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

            if (partida.jogadorAtual == cor && testeRoquePequeno())
            {
                mat[posicao.linha, posicao.coluna + 2] = true;
            }

            if (partida.jogadorAtual == cor && testeRoqueGrande())
            {
                mat[posicao.linha, posicao.coluna - 3] = true;
            }

            return mat;
        }
    }
}
