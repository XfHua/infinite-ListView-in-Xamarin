using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App242
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private SmartCollection<NumerosSorteioDTO> smartCollection;

        private static ConcursoDTO _values = new ConcursoDTO();
        private static int OFFSET = 0;
        private const int LIMIT = 5;

        public MainPage()
        {
            InitializeComponent();

            firstLoad();

        }

        private void firstLoad()
        {


                FooterLoading.IsVisible = true;

                smartCollection = new SmartCollection<NumerosSorteioDTO>();

                _values.offset = OFFSET;
                _values.limit = LIMIT;
                //ConcursoDTO dto = ConcursoService.GetNumerosSorteConcursoLO(_values);

                //smartCollection.AddRange(dto.numeros);

                NumerosSorteioDTO obj1 = new NumerosSorteioDTO { numero = "11", valor = "11" };
                NumerosSorteioDTO obj2 = new NumerosSorteioDTO { numero = "22", valor = "22" };
                NumerosSorteioDTO obj3 = new NumerosSorteioDTO { numero = "33", valor = "33" };
                NumerosSorteioDTO obj4 = new NumerosSorteioDTO { numero = "44", valor = "44" };
                NumerosSorteioDTO obj5 = new NumerosSorteioDTO { numero = "55", valor = "55" };

                ConcursoDTO dto = new ConcursoDTO();

                dto.numeros.Add(obj1);
                dto.numeros.Add(obj2);
                dto.numeros.Add(obj3);
                dto.numeros.Add(obj4);
                dto.numeros.Add(obj5);

                smartCollection.AddRange(dto.numeros);

                this.BindingContext = smartCollection;
                this.MyList.ItemsSource = smartCollection;


                FooterLoading.IsVisible = false;
            


        }

        private void OnItemAppearing(object sender, ItemVisibilityEventArgs args)
        {
            try
            {
                FooterLoading.IsVisible = true;

                var _item = (NumerosSorteioDTO)args.Item;
                if (_item == smartCollection[smartCollection.Count - 1])
                {
                    _values.offset += 5;
                    //ConcursoDTO dto = ConcursoService.GetNumerosSorteConcursoLO(_values);
                    NumerosSorteioDTO obj1 = new NumerosSorteioDTO { numero = "1", valor = "1" };
                    NumerosSorteioDTO obj2 = new NumerosSorteioDTO { numero = "2", valor = "2" };
                    NumerosSorteioDTO obj3 = new NumerosSorteioDTO { numero = "3", valor = "3" };
                    NumerosSorteioDTO obj4 = new NumerosSorteioDTO { numero = "4", valor = "4" };
                    NumerosSorteioDTO obj5 = new NumerosSorteioDTO { numero = "5", valor = "5" };

                    ConcursoDTO dto = new ConcursoDTO();
                    dto.numeros.Add(obj1);
                    dto.numeros.Add(obj2);
                    dto.numeros.Add(obj3);
                    dto.numeros.Add(obj4);
                    dto.numeros.Add(obj5);

                    smartCollection.AddRange(dto.numeros);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro: " + e.Message);
            }
            finally
            {
                FooterLoading.IsVisible = false;
            }
        }
    }

    public class ConcursoDTO
    {
        public int offset { get; internal set; }
        public int limit { get; internal set; }
        public ObservableCollection<NumerosSorteioDTO> numeros { get; internal set; }

        public ConcursoDTO() {
            this.numeros = new ObservableCollection<NumerosSorteioDTO>();
        }
    }

    public class NumerosSorteioDTO
    {
        public string numero { get; set; }
        public string valor { get; set; }
    }

    public class SmartCollection<T> : ObservableCollection<T>
    {
        public SmartCollection()
            : base()
        {
        }

        public SmartCollection(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public SmartCollection(List<T> list)
            : base(list)
        {
        }

        public void AddRange(IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                Items.Add(item);
            }

            this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void Reset(IEnumerable<T> range)
        {
            this.Items.Clear();

            AddRange(range);
        }
    }
}
