using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AjustarLegendas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ListLegenda ObjDataContext = new ListLegenda();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = ObjDataContext;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.Multiselect = false;
                dlg.Filter = "All Files (*.*)|*.*";
                dlg.Title = "Seleccionar Ficheiro";
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {
                    txtDiretorio.Text = dlg.FileName.ToString();
                    ObjDataContext.Load(System.IO.File.ReadAllLines(txtDiretorio.Text));
                    txtPaginas.Text = "Existem " + lst.Items.Count + " linhas";
                    txtPf.Text = lst.Items.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPi.Text) || string.IsNullOrEmpty(txtPf.Text) || string.IsNullOrEmpty(txtSegundos.Text))
                MessageBox.Show("Necessário preencher página inicial e/ou final e/ou segundos");
            else if (Convert.ToInt32(txtPi.Text) > Convert.ToInt32(txtPf.Text))
                MessageBox.Show("O ponto inicial não pode ser maior que o ponto final");
            else if (Convert.ToInt32(txtPf.Text) > lst.Items.Count)
                MessageBox.Show("O ponto inicial não pode ser maior que o ponto final");
            else
            {
                for (int i = Convert.ToInt32(txtPi.Text) - 1; i < Convert.ToInt32(txtPf.Text); i++)
                {
                    if(lst.Items[i].ToString().Contains("-->"))
                    {
                        if((sender as Button).Content.ToString().Contains("+"))
                        {
                            string [] valor = lst.Items[i].ToString().Split('-');
                            lst.Items[i] = Convert.ToDateTime(valor[0].Trim().Replace(',', '.')).AddSeconds(Convert.ToDouble(txtSegundos.Text)).ToString("HH:mm:ss,fff") + "-->" + Convert.ToDateTime(valor[2].Substring(2).Trim().Replace(',', '.')).AddSeconds(Convert.ToDouble(txtSegundos.Text)).ToString("HH:mm:ss,fff");
                        }
                        else
                        {
                            string[] valor = lst.Items[i].ToString().Split('-');
                            TimeSpan sub = new TimeSpan(0,0,Convert.ToInt32(txtSegundos.Text));
                            lst.Items[i] = Convert.ToDateTime(valor[0].Trim().Replace(',', '.')).Subtract(sub).ToString("HH:mm:ss,fff") + "-->" + Convert.ToDateTime(valor[2].Substring(2).Trim().Replace(',', '.')).Subtract(sub).ToString("HH:mm:ss,fff");
                        }
                    }
                }
            }
        }

        private void lst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtPi.Text = ((sender as ListBox).SelectedIndex +1).ToString();
            texto.Text = ((sender as ListBox).SelectedItem as Legenda).texto;
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ObjDataContext.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Teste.txt");
        }

        private void txtPi_TextChanged(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!Char.IsNumber(c))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnGravarTexto_Click(object sender, RoutedEventArgs e)
        {
            if (lst.SelectedIndex != -1)
                (lst.SelectedItem as Legenda).texto = texto.Text;
        }
    }
}
