using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChameleonMVC.Models
{
    public class DisposalCreatorModel
    {
    }
    public class GenerateDisposal
    {
        public string CostCenter { get; set; }
        public string Lokasi { get; set; }
        public string BagianTeknik { get; set; }
        public string Alasan { get; set; }
        public string PIC { get; set; }
        public string Ruang { get; set; }
    }

    public class AssetAdd
    {
        public string AssetNumber { get; set; }
        public string NoDisposal { get; set; }
    }
}