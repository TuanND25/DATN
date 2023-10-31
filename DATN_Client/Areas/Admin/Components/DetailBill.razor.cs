using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class DetailBill
    {
        HttpClient _client = new HttpClient();
        Bill_ShowModel _billModel = new Bill_ShowModel();
        List<BillDetailShow> _lstBillDetail = new List<BillDetailShow>();
        AddressShip addressShip = new AddressShip();
        Bill_VM b = new Bill_VM();
        [Inject]
        NavigationManager nav { get; set; }

        Bill bil = new Bill();
        public int? _tongtien { get; set; } = 0;
        public string texttongtien { get; set; }
        public int Phiship { get; set; }
        public string Note { get; set; }
        public string Suggest_number_1 { get; set; } = string.Empty;
        public string Suggest_number_2 { get; set; } = string.Empty;
        public bool displayButton { get; set; }
        public string CheckFee { get; set; }
        public string CheckNote { get; set; }


        protected override async Task OnInitializedAsync()
        {
            _billModel = BillManagement._billModel;
            _lstBillDetail = await _client.GetFromJsonAsync<List<BillDetailShow>>("https://localhost:7141/api/BillItem/getbilldetail/" + _billModel.Id);

            foreach (var item in _lstBillDetail)
            {
                _tongtien += item.Quantity * item.PriceAfter;
            }
            texttongtien = NumberToText(Convert.ToDouble(_tongtien));
            displayButton = false;
        }
        public void ReturnBill()
        {
            nav.NavigateTo("https://localhost:7075/Admin/BillManagement", true);
        }
        public async Task UpdateConfirmShipping()
        {
            if (Phiship == null && Note == null)
            {
                CheckFee = "Không được để trống số tiền";
                CheckNote = "Không được bỏ trống ghi chú";
            }
            else
            {
                Bill_VM b = await _client.GetFromJsonAsync<Bill_VM>($"https://localhost:7141/api/Bill/get_bill_by_id/{_billModel.Id}");
                b.ShippingFee = Phiship;
                b.Note = Note;
                b.Status = 2;
                var status = await _client.PutAsJsonAsync("https://localhost:7141/api/Bill/Put-Bill", b);
                if (status.IsSuccessStatusCode)
                {
                    nav.NavigateTo("https://localhost:7075/Admin/BillManagement/Details", true);
                }
            }
        }
        public async Task CancelOrder()
        {
            Bill_VM b = await _client.GetFromJsonAsync<Bill_VM>($"https://localhost:7141/api/Bill/get_bill_by_id/{_billModel.Id}");

            b.Status = 0;
            var status = await _client.PutAsJsonAsync("https://localhost:7141/api/Bill/Put-Bill", b);
            if (status.IsSuccessStatusCode)
            {
                nav.NavigateTo("https://localhost:7075/Admin/BillManagement/Details", true);
            }
        }
        private void HandleInput(ChangeEventArgs e)
        {
            var Phiship1 = e.Value.ToString();

            if (Phiship1 == null || Phiship1 == string.Empty)
            {
                HideButton();
                CheckFee = "Không được để trống số tiền";
            }
            else if (Convert.ToInt32(Phiship1) < 0)
            {
                CheckFee = "Vui lòng nhập số lớn hơn 0";
            }
            else if (Phiship1 != string.Empty)
            {
                CheckFee = null;
                Phiship = Convert.ToInt32(Phiship1);
                if (Phiship < 999)
                {
                    displayButton = true;
                    Suggest_number_1 = RoundToNearestPowerOfTen(Phiship, 100);
                    Suggest_number_2 = RoundToNearestPowerOfTen(Phiship, 1000);
                }
            }
            else
            {
                displayButton = false;
            }

            // Thực hiện các xử lý ngay khi giá trị thay đổi
        }
        private void CheckNoteNull(ChangeEventArgs e)
        {
            var Note = e.Value.ToString();
            if (Note.Trim() == null || Note.Trim() == string.Empty)
            {
                CheckNote = "Không được bỏ trống ghi chú";
            }
            else
            {
                CheckNote = null;
            }
        }
        private void HideButton()
        {
            displayButton = false;
        }
        private void SubmitFeeShip_1()
        {
            Phiship = Convert.ToInt32(Suggest_number_1);
        }
        private void SubmitFeeShip_2()
        {
            Phiship = Convert.ToInt32(Suggest_number_2);
        }
        private string RoundToNearestPowerOfTen(int number, int chiso)
        {
            int roundedNumber = number * chiso;
            var Suggest_number1 = roundedNumber.ToString();
            return Suggest_number1;
        }
        static string NumberToText(double inputNumber, bool suffix = true)
        {
            string[] unitNumbers = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] placeValues = new string[] { "", "nghìn", "triệu", "tỷ" };
            bool isNegative = false;

            // -12345678.3445435 => "-12345678"
            string sNumber = inputNumber.ToString("#");
            double number = Convert.ToDouble(sNumber);
            if (number < 0)
            {
                number = -number;
                sNumber = number.ToString();
                isNegative = true;
            }
            int ones, tens, hundreds;

            int positionDigit = sNumber.Length;   // last -> first

            string result = " ";


            if (positionDigit == 0)
                result = unitNumbers[0] + result;
            else
            {
                // 0:       ###
                // 1: nghìn ###,###
                // 2: triệu ###,###,###
                // 3: tỷ    ###,###,###,###
                int placeValue = 0;

                while (positionDigit > 0)
                {
                    // Check last 3 digits remain ### (hundreds tens ones)
                    tens = hundreds = -1;
                    ones = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                    positionDigit--;
                    if (positionDigit > 0)
                    {
                        tens = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                        if (positionDigit > 0)
                        {
                            hundreds = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                            positionDigit--;
                        }
                    }

                    if ((ones > 0) || (tens > 0) || (hundreds > 0) || (placeValue == 3))
                        result = placeValues[placeValue] + result;

                    placeValue++;
                    if (placeValue > 3) placeValue = 1;

                    if ((ones == 1) && (tens > 1))
                        result = "một " + result;
                    else
                    {
                        if ((ones == 5) && (tens > 0))
                            result = "lăm " + result;
                        else if (ones > 0)
                            result = unitNumbers[ones] + " " + result;
                    }
                    if (tens < 0)
                        break;
                    else
                    {
                        if ((tens == 0) && (ones > 0)) result = "lẻ " + result;
                        if (tens == 1) result = "mười " + result;
                        if (tens > 1) result = unitNumbers[tens] + " mươi " + result;
                    }
                    if (hundreds < 0) break;
                    else
                    {
                        if ((hundreds > 0) || (tens > 0) || (ones > 0))
                            result = unitNumbers[hundreds] + " trăm " + result;
                    }
                    result = " " + result;
                }
            }
            result = result.Trim();
            if (isNegative) result = "Âm " + result;
            return result + (suffix ? " đồng chẵn" : "");

        }
    }
}
