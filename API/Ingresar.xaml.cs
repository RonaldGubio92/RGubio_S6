using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace API
{
    

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ingresar : ContentPage
    {

        private const string URL = "http://192.168.1.58/clientes/post.php";
        //private ObservableCollection<Datos> post;
        private readonly HttpClient _client = new HttpClient();

        private Datos ItemSeleccionado;

        public Ingresar(Datos itemSeleccionado)
        {
            InitializeComponent();
            this.ItemSeleccionado = itemSeleccionado;
            txtNombre.Text = itemSeleccionado.nombre;
            txtApeliido.Text = itemSeleccionado.apellido;
            txtEdad.Text = itemSeleccionado.edad.ToString();
            txtCodigo.Text = itemSeleccionado.codigo.ToString();
        }

        private void btnInsertar_Clicked(object sender, EventArgs e)
        {
            WebClient cliente = new WebClient();
            try
            {
                var parametros = new System.Collections.Specialized.NameValueCollection();
                parametros.Add("codigo", txtCodigo.Text);
                parametros.Add("nombre", txtNombre.Text);
                parametros.Add("apellido", txtApeliido.Text);
                parametros.Add("edad", txtEdad.Text);
                cliente.UploadValues("http://192.168.1.58/clientes/post.php", "POST", parametros);
              //  DisplayAlert("Alerta", "Datos ingresados correctamente", "Cerrar");


            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta",ex.Message,"Cerrar");
            }
           
        }

        private async void btnRegresar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());        }

     

        private  async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            Datos post = ItemSeleccionado;
            post.codigo = Int32.Parse(txtCodigo.Text);
            post.nombre = txtNombre.Text;
            post.apellido = txtApeliido.Text;
            post.edad = Int32.Parse(txtEdad.Text);
            await _client.PutAsync($"{URL}?codigo={post.codigo}&nombre={post.nombre}&apellido={post.apellido}&edad={post.edad}", null);
        }

        private void btnNuevo_Clicked(object sender, EventArgs e)
        {
            txtCodigo.Text = "";
            txtApeliido.Text = "";
            txtNombre.Text = "";
            txtEdad.Text = "";
            txtCodigo.Focus();
        }
    }
}