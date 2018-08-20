using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App05_Tarefa.Modelos;

namespace App05_Tarefa.Telas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Cadastro : ContentPage
	{
        private byte Prioridade { get; set; }

		public Cadastro ()
		{
			InitializeComponent ();
		}

        public void PrioridadeSelectAction(object sender, EventArgs args)
        {
            var Staks = SLPrioridades.Children;

            foreach(var Linha in Staks)
            {
                Label LblPrioridade = ((StackLayout)Linha).Children[1] as Label;
                LblPrioridade.TextColor = Color.Gray;
            }

            ((Label)((StackLayout)sender).Children[1]).TextColor = Color.Black;
            FileImageSource Source = ((Image)((StackLayout)sender).Children[0]).Source as FileImageSource;
            String Prioridade = Source.File.ToString().Replace("Resources/","").Replace(".png","");

            this.Prioridade = byte.Parse(Prioridade);
        }

        public void SalvarAction(object sender, EventArgs args)
        {
            bool ErroExiste = false;
            if (!(txtNome.Text.Trim().Length > 0))
            {
                ErroExiste = true;
                DisplayAlert("Erro", "Campo Nome não informado!", "OK");
            }

            if (!(Prioridade > 0))
            {
                ErroExiste = true;
                DisplayAlert("Erro", "Prioridade não informada!", "OK");
            }

            if (ErroExiste == false)
            {
                Tarefa tarefa = new Tarefa();
                tarefa.Nome = txtNome.Text.Trim();
                tarefa.Prioridade = Prioridade;

                new GerenciadorTarefa().Salvar(tarefa);

                App.Current.MainPage = new NavigationPage(new Inicio());
            }
               
        }

    }
}