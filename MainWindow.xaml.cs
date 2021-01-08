using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ilerinesneProjeOdevi
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        Tasit t;
        Tasit seciliTasit;
        ObservableCollection<Tasit> Tasitlar = new ObservableCollection<Tasit>();
        public MainWindow()
        {
            InitializeComponent();
            LbTasitlar.ItemsSource = Tasitlar;
            aramaButonUret();
        }

        void aramaButonUret()
        {
            string harfler = "*ABCDEFGHIJKLMNOPRSTUWVXYZ";
            for (int i = 0; i < harfler.Length; i++)
            {
                Button b = new Button()
                {
                    Width = 20,
                    Height = 20,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(2),
                    Content = harfler[i],
                };
                b.Click += B_Click;
                WpButonlar.Children.Add(b);
            }
        }

        private void B_Click(object sender, RoutedEventArgs e)
        {
            Button gButton = (Button)sender;
            if (gButton.Content.ToString() == "*")
            {
                LbTasitlar.ItemsSource = Tasitlar;
            }
            else
            {
                List<Tasit> arananTasitlar = new List<Tasit>();
                foreach (Tasit t in Tasitlar)
                {
                    if (t.Marka.ToLower().StartsWith(gButton.Content.ToString().ToLower()))
                    {
                        arananTasitlar.Add(t);
                    }
                }
                LbTasitlar.ItemsSource = arananTasitlar;
            }
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtMarka.Text) == true)
            {
                MessageBox.Show("Marka alanı boş geçilemez.");
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtModel.Text) == true)
            {
                MessageBox.Show("Model alanı boş geçilemez.");
                return;
            }

            if (DpYıl.SelectedDate == null)
            {
                MessageBox.Show("Üretim Yılı alanı boş geçilemez.");
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtRenk.Text) == true)
            {
                MessageBox.Show("Renk alanı boş geçilemez.");
                return;
            }

            if (decimal.TryParse(TxtFiyat.Text, out decimal fiyat) == false)
            {
                MessageBox.Show("Fiyat alanı doğru biçimde değil.");
                return;
            }


            if (seciliTasit == null)
            {
                t = new Tasit();
                t.Marka = TxtMarka.Text;
                t.Model = TxtModel.Text;
                t.ÜretimYılı = DpYıl.SelectedDate.Value;
                t.Renk = TxtRenk.Text;
                t.Fiyat = decimal.Parse(TxtFiyat.Text);
                t.Yakıt = RbBenzin.IsChecked.Value;
                Tasitlar.Add(t);
            }

            else
            {
                seciliTasit.Marka = TxtMarka.Text;
                seciliTasit.Model = TxtModel.Text;
                seciliTasit.ÜretimYılı = DpYıl.SelectedDate.Value;
                seciliTasit.Renk = TxtRenk.Text;
                seciliTasit.Fiyat = decimal.Parse(TxtFiyat.Text);
                seciliTasit.Yakıt = RbBenzin.IsChecked.Value;
                LbTasitlar.Items.Refresh();
                LbTasitlar.SelectedItem = null;
            }


            TbYazi.Text = "Kaydedildi.";
            TxtMarka.Clear();
            TxtModel.Clear();
            DpYıl.SelectedDate = null;
            TxtRenk.Clear();
            TxtFiyat.Clear();
            RbBenzin.IsChecked = RbDizel.IsChecked = false;

        }

        private void LbTasitlar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            seciliTasit = (Tasit)LbTasitlar.SelectedItem;

            if (seciliTasit != null)
            {
                TxtMarka.Text = seciliTasit.Marka;
                TxtModel.Text = seciliTasit.Model;
                DpYıl.SelectedDate = seciliTasit.ÜretimYılı;
                TxtRenk.Text = seciliTasit.Renk;
                TxtFiyat.Text = seciliTasit.Fiyat.ToString();
                RbBenzin.IsChecked = seciliTasit.Yakıt;
                RbDizel.IsChecked = !seciliTasit.Yakıt;
            }
        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            Tasitlar.RemoveAt(LbTasitlar.SelectedIndex);
            LbTasitlar.Items.Refresh();
        }
    }
}
