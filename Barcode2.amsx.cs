using Spire.Barcode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace BarCode2
{
    /// <summary>
    /// Summary description for BarCode
    /// </summary>
    [WebService(Namespace = "http://www.tunion.com.br/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BarCode : System.Web.Services.WebService
    {
        [WebMethod]
        public String ScanByImage(Byte[] pImagem)
        {
            return this.GetBarCode(pImagem);
        }
        [WebMethod]
        public String ScanByHexa(String pImagem)
        {
            return this.GetBarCode(this.BinToByte(this.HexToBin(pImagem)));
        }
        [WebMethod]
        public String ScanByBin(String pImagem)
        {
            return this.GetBarCode(this.BinToByte(pImagem));
        }

        //Methods private
        private String GetBarCode(Byte[] pImagem)
        {
            using (var ms = new MemoryStream(pImagem, 0, pImagem.Length))
            {
                String[] barcodes = BarcodeScanner.Scan(((Bitmap)Image.FromStream(ms, true)));

                if (barcodes.Length > 0)
                {
                    return barcodes[0].ToString();
                }
            }
            return String.Empty;
        }
        private String HexToBin(String pString)
        {
            return String.Join(String.Empty, pString.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
        }
        private Byte[] BinToByte(String pString)
        {
            Int32 numOfBytes = pString.Length / 8;
            Byte[] bytes = new Byte[numOfBytes];

            for (int i = 0; i < numOfBytes; ++i)
            {
                bytes[i] = Convert.ToByte(pString.Substring(8 * i, 8), 2);
            }

            return bytes;
        }
    }
}
