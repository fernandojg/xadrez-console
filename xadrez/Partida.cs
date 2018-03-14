using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class Partida
    {
        public Tabuleiro tabuleiro { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }

        public Partida()
        {
            tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
            xeque = false;
        }

        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = tabuleiro.retirarPeca(origem);
            peca.incrementarQtdMovimentos();
            Peca pecaCapturada = tabuleiro.retirarPeca(destino);
            tabuleiro.colocarPeca(peca, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca peca = tabuleiro.retirarPeca(destino);
            tabuleiro.colocarPeca(peca, origem);
            peca.decrementarQtdMovimentos();
            if (pecaCapturada != null)
            {
                tabuleiro.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
        }

        public void realizarJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Não pode colocar seu rei em xeque!");
            }

            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

            if (testeXequeMate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }
        }

        public void validarPosicaoDeOrigem(Posicao posicao)
        {
            if (tabuleiro.peca(posicao) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }

            if (tabuleiro.peca(posicao).cor != jogadorAtual)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }

            if (!tabuleiro.peca(posicao).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possiveis para a peça escolhida!");
            }
        }

        private void mudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        public void validarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!tabuleiro.peca(origem).movimentosPossiveis(destino))
                throw new TabuleiroException("Posição de destino inválida");
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca peca in capturadas)
            {
                if (peca.cor == cor)
                {
                    aux.Add(peca);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca peca in pecas)
            {
                if (peca.cor == cor)
                {
                    aux.Add(peca);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }
        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca) return Cor.Preta;
            else return Cor.Branca;
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não tem Rei da cor " + cor + " no tabuleiro!");
            }

            foreach (Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] aux = x.movimentosPossiveis();
                if (aux[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public bool testeXequeMate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }

            bool[,] aux = new bool[tabuleiro.linhas,tabuleiro.colunas];
            foreach (Peca peca in pecasEmJogo(cor))
            {
                aux = peca.movimentosPossiveis();
                for (int i = 0; i < tabuleiro.linhas; i++)
                {
                    for (int j = 0; j < tabuleiro.colunas; j++)
                    {
                        if (aux[i,j])
                        {
                            Posicao origem = peca.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca capturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, capturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tabuleiro.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
            colocarNovaPeca('a', 8, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tabuleiro, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tabuleiro, Cor.Preta));
            colocarNovaPeca('d', 8, new Rei(tabuleiro, Cor.Preta));
            colocarNovaPeca('e', 8, new Dama(tabuleiro, Cor.Preta));
            colocarNovaPeca('f', 8, new Bispo(tabuleiro, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tabuleiro, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tabuleiro, Cor.Preta));
            colocarNovaPeca('b', 7, new Peao(tabuleiro, Cor.Preta));
            colocarNovaPeca('c', 7, new Peao(tabuleiro, Cor.Preta));
            colocarNovaPeca('d', 7, new Peao(tabuleiro, Cor.Preta));
            colocarNovaPeca('e', 7, new Peao(tabuleiro, Cor.Preta));
            colocarNovaPeca('f', 7, new Peao(tabuleiro, Cor.Preta));
            colocarNovaPeca('g', 7, new Peao(tabuleiro, Cor.Preta));
            colocarNovaPeca('h', 7, new Peao(tabuleiro, Cor.Preta));

            colocarNovaPeca('a', 1, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tabuleiro, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tabuleiro, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tabuleiro, Cor.Branca));
            colocarNovaPeca('e', 1, new Dama(tabuleiro, Cor.Branca));
            colocarNovaPeca('f', 1, new Bispo(tabuleiro, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tabuleiro, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tabuleiro, Cor.Branca));
            colocarNovaPeca('b', 2, new Peao(tabuleiro, Cor.Branca));
            colocarNovaPeca('c', 2, new Peao(tabuleiro, Cor.Branca));
            colocarNovaPeca('d', 2, new Peao(tabuleiro, Cor.Branca));
            colocarNovaPeca('e', 2, new Peao(tabuleiro, Cor.Branca));
            colocarNovaPeca('f', 2, new Peao(tabuleiro, Cor.Branca));
            colocarNovaPeca('g', 2, new Peao(tabuleiro, Cor.Branca));
            colocarNovaPeca('h', 2, new Peao(tabuleiro, Cor.Branca));
        }
    }
}
