using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjustarLegendas
{
    public class ListLegenda : INotifyPropertyChanged
    {
        ObservableCollection<Legenda> _lstLegenda;
        public ObservableCollection<Legenda> lstLegenda
        {
            get { return _lstLegenda; }
            set
            {
                if (_lstLegenda == value)
                    return;
                _lstLegenda = value;
            }
        }
        public ListLegenda()
        {
            lstLegenda = new ObservableCollection<Legenda>();
            lstLegenda.CollectionChanged += lstLegenda_CollectionChanged;
            
        }

        void lstLegenda_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e != null)
            {
                if(e.OldItems != null)
                {
                    foreach (INotifyPropertyChanged item in e.OldItems)
                    {
                        item.PropertyChanged += item_PropertyChanged;
                    }
                }
            }
        }

        void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string property)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public void Save(string pathFinal)
        {
            System.IO.StreamWriter sr;
            if (File.Exists(pathFinal))
                sr = new System.IO.StreamWriter(pathFinal);
            else
                sr = new StreamWriter(File.Create(pathFinal));
            int id = 1;
            foreach (Legenda legenda in lstLegenda)            
            {
                sr.WriteLine(legenda.GetTextSave(id));
                id++;
            }
            sr.Close();
        }

        public void Load(string[] lines)
        {
            string textoLegenda = string.Empty;
            TimeSpan horaInicio = new TimeSpan();
            TimeSpan horaFim = new TimeSpan();
            int id=0;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].IsNumberInt())
                    id = Convert.ToInt32(lines[i]);
                else if (lines[i].Contains("-->"))
                {
                    horaInicio = Convert.ToDateTime(lines[i].Split('-')[0].Trim().Replace(",", ".")).TimeOfDay;
                    horaFim = Convert.ToDateTime(lines[i].Split('-')[2].Replace(">", "").Trim().Replace(",", ".")).TimeOfDay;
                }
                else
                {
                    while (i < lines.Length &&!lines[i].IsNumberInt() && !lines[i].Contains("-->"))
                    {
                        if(!string.IsNullOrEmpty(lines[i]))
                            textoLegenda += lines[i] + "\n";
                        i++;
                    }
                    i--;
                    if (id == 0)
                        id = lstLegenda.Count;
                    Legenda legenda = new Legenda(id, horaInicio, horaFim, textoLegenda.Substring(0,textoLegenda.Length-2));
                    lstLegenda.Add(legenda);
                    textoLegenda = "";
                    horaInicio = new TimeSpan();
                    horaFim = new TimeSpan();
                    id = 0;
                }
               
            }
        }
    }
}
