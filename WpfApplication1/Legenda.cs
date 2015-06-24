using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjustarLegendas
{
    public class Legenda
    {
        public TimeSpan horaIncio { get; private set; }
        public TimeSpan horaFim { get; private set; }
        public string texto { get; set; }

        public int id { get; private set; }

        public Legenda(int ID, TimeSpan HoraIncio, TimeSpan HoraFim, string TextoLegenda)
        {
            id = ID;
            horaIncio = HoraIncio;
            horaFim = HoraFim;
            texto = TextoLegenda;
        }

        public void AlterarLegenda(string TextoLegenda)
        {
            texto = TextoLegenda;
        }

        public void AlterarHora(TimeSpan HoraInicio, TimeSpan HoraFim)
        {
            horaIncio = HoraInicio;
            horaFim = HoraFim;
        }
        public void AlterarHora(TimeSpan Hora, bool HoraInicio)
        {
            if (HoraInicio)
                horaIncio = Hora;
            else
                horaFim = horaFim;
        }

        public string GetTextSave(int id)
        {
            return id + "\n" + horaIncio.ToString("HH:mm:ss,fff") + " --> " + horaFim.ToString("HH:mm:ss,fff") + "\n" + texto;
        }

    }
}
