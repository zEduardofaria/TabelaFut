using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabalhoES2
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            var clubes = new List<Time>();
            for (int i = 0; i < 20; i++)
            {
                clubes.Add(new Time($"Clube{i+1}"));
            }

            var campeonato = new Campeonato();

            var count = 0;

            while(campeonato.Rodadas.Count < 19)
            {
                var rodada = new Rodada();

                while(rodada.Partidas.Count < 10)
                {
                    foreach (var timeA in clubes)
                    {
                        foreach (var timeB in clubes)
                        {
                            if (!campeonato.JaOuveConfrontoEntreOsDoisTimes(timeA, timeB))
                            {
                                rodada.CriarPartida(timeA, timeB);
                            }
                        }
                    }
                }
                campeonato.Rodadas.Add(rodada);
            }
        }
    }

    public class Campeonato
    {
        public List<Rodada> Rodadas { get; set; }
        public Campeonato()
        {
            Rodadas = new List<Rodada>();
        }

        public bool JaOuveConfrontoEntreOsDoisTimes(Time timeA, Time timeB)
        {
            if (Rodadas.Count > 19)
                return true;

            foreach (var rodada in Rodadas)
	        {
                if (rodada.Partidas.Any(p => (p.TimeA == timeA && p.TimeB == timeB) || 
                                             (p.TimeA == timeB && p.TimeB == timeA)))
                {
                    return true;
                }
	        }

            return false;
        }

    }


    public class Rodada
    {
        public List<Partida> Partidas { get; set; }
        public Rodada()
        {
            Partidas = new List<Partida>();
        }   

        public void CriarPartida(Time timeA, Time timeB)
        {
            if (!TimeJaEstaJogandoNaRodada(timeA, timeB))
            {
                Partidas.Add(new Partida(timeA, timeB));
            }
        }

        private bool TimeJaEstaJogandoNaRodada(Time timeA, Time timeB)
        {
            if (Partidas.Count >= 10)
                return true;

            if (timeA == timeB)
                return true;

            if (Partidas.Any(p => p.TimeA == timeA || p.TimeA == timeB || p.TimeB == timeA || p.TimeB == timeB))
                return true;

            return false;
        }
    }

    public class Partida
    {
        public Time TimeA { get; set; }
        public Time TimeB { get; set; }

        public Partida(Time timeA, Time timeB)
        {
            TimeA = timeA;
            TimeB = timeB;
        }
    }

    public class Time
    {
        private string Nome { get; set; }
        public Time(string nome)
        {
            this.Nome = nome;
        }
    }

}
