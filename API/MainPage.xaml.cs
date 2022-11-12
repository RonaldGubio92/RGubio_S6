using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace API
{
    public partial class MainPage : ContentPage
    {
        private const string URL = "http://192.168.1.58/clientes/post.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<Datos> _post;

      


        public MainPage()
        {
            InitializeComponent();
            GetData();
        }


        private async void GetData()
        {
            var content = await client.GetStringAsync(URL);

            List<Datos> posts = JsonConvert.DeserializeObject<List<Datos>>(content);

            _post = new ObservableCollection<Datos>(posts);

            MyListView.ItemsSource = _post;

        }
        private async void btnGet_Clicked(object sender, EventArgs e)
        {
            var itemSeleccionado = (Datos)MyListView.SelectedItem;
            await Navigation.PushAsync(new Ingresar(itemSeleccionado));
        }

       

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            Datos post = _post[0];
            await client.DeleteAsync($"{URL}?codigo={post.codigo}");
                _post.Remove(post);
        }
    }
}
