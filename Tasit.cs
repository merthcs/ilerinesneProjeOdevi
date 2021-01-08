using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ilerinesneProjeOdevi
{
    class Tasit
    {
        public string Marka { get; set; }
        public string Model { get; set; }
        public DateTime ÜretimYılı { get; set; }
        public string Renk { get; set; }
        public decimal Fiyat { get; set; }
        public bool Yakıt { get; set; }
        public string MarkaModel { get { return $"{Marka}-{Model} / {ÜretimYılı.ToShortDateString()} / {Renk} / {Fiyat.ToString("₺ 0.00")} / {(Yakıt ? "Benzin" : "Dizel")}"; } }
    }
}
