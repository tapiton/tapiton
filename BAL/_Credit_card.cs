using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL
{
    public class _Credit_card
    {
        public int Credit_Card_ID { get; set; }
        public int Merchant_ID { get; set; }
        public string Cardholder_Name { get; set; }
        public string TransarmorToken { get; set; }
        public string  Expiry_Date { get; set; }
        public string Card_Type { get; set; }
        public DateTime  Added_On { get; set; }
    }
}
