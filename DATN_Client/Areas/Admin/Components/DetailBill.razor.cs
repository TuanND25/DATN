using Blazored.Toast.Configuration;
using Blazored.Toast;
using DATN_Client.Areas.Admin.Controllers;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using static System.Net.WebRequestMethods;
using DATN_Client.Areas.Customer.Component;
using System.Text.RegularExpressions;
using DATN_Shared.ViewModel.DiaChi;
using System;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class DetailBill
    {
        private HttpClient _client = new HttpClient();
        [Inject] Blazored.Toast.Services.IToastService _toastService { get; set; }
        private Bill_ShowModel _billModel = new Bill_ShowModel();
        private List<BillDetailShow> _lstBillDetail = new List<BillDetailShow>();
        private AddressShip addressShip = new AddressShip();
        private Bill_VM b = new Bill_VM();
        [Inject] private NavigationManager _navi { get; set; }

        [Inject] public IHttpContextAccessor _ihttpcontextaccessor { get; set; }

        public Bill_ShowModel _bill = new Bill_ShowModel();
        public Guid BillId { get; set; }
        public string TotalText { get; set; }
        public int PhiShip { get; set; }
        public string Note { get; set; }
        public bool ActiveTabFee { get; set; } = false;
        public string LyDoHuy { get; set; }
        public bool activetabHuy { get; set; } = false;
        public string TenNguoiHuy { get; set; } = string.Empty;
        public string nameNguoiNhan { get; set; } = string.Empty; 
        public bool checkPopupAddress { get; set; } = true;
		public string phoneNumberNguoinhan { get; set; } = "";
		public string NoteAddresShip { get; set; } = "";
		public string ShowDiaChi { get; set; } = "";
		public string _TinhTp { get; set; }
		public string _QuanHuyen { get; set; }
		public string _PhuongXa { get; set; }
		private bool isLoader = false;
		public string AddressDetail { get; set; } = "";
		private List<Province_VM> _lstTinhTp = new List<Province_VM>();
		private List<District_VM> _lstQuanHuyen = new List<District_VM>();
		private List<Ward_VM> _lstXaPhuong = new List<Ward_VM>();
		private List<Province_VM> _lstTinhTp_Data = new List<Province_VM>();
		private List<District_VM> _lstQuanHuyen_Data = new List<District_VM>();
		private List<Ward_VM> _lstXaPhuong_Data = new List<Ward_VM>();
		public string checkPhoneNumberNguoinhan { get; set; }
		protected override async Task OnInitializedAsync()
        {
            isLoader = true;
			await getDataBill();
            _lstBillDetail = await _client.GetFromJsonAsync<List<BillDetailShow>>("https://localhost:7141/api/BillItem/getbilldetail/" + BillId);
            if (!string.IsNullOrEmpty(_bill.CanelBy))
            {
                TenNguoiHuy = (await _client.GetFromJsonAsync<User_VM>($"https://localhost:7141/api/user/get_user_by_id/{_bill.CanelBy}") ?? new()).Name ?? string.Empty.ToString() ?? string.Empty;
            }
			_lstTinhTp_Data = await _client.GetFromJsonAsync<List<Province_VM>>("https://api.npoint.io/ac646cb54b295b9555be"); // api tỉnh tp
			_lstTinhTp = _lstTinhTp_Data;
			_lstQuanHuyen_Data = await _client.GetFromJsonAsync<List<District_VM>>("https://api.npoint.io/34608ea16bebc5cffd42");
			_lstXaPhuong_Data = await _client.GetFromJsonAsync<List<Ward_VM>>("https://api.npoint.io/dd278dc276e65c68cdf5");
			isLoader = false;
		}
        public async Task getDataBill()
        {
            if (Login.Roleuser != "Admin" && Login.Roleuser != "Staff")
            {
                _navi.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            BillId = BillManagementController._billId;




            List<Bill_ShowModel> _lstbill = await _client.GetFromJsonAsync<List<Bill_ShowModel>>("https://localhost:7141/api/Bill/get_alll_bill");

            _bill = _lstbill.FirstOrDefault(x => x.Id == BillId);
            TotalText = NumberToText(Convert.ToDouble(_bill.TotalAmount.ToString()));

            var _lstUser = await _client.GetFromJsonAsync<List<User_VM>>("https://localhost:7141/api/user/get-user");
            _bill.NameCreatBy = (await _client.GetFromJsonAsync<User_VM>($"https://localhost:7141/api/user/get_user_by_id/{_bill.CreateBy}")).Name.ToString();
            if (_bill.CanelBy != null)
            {
                TenNguoiHuy = (await _client.GetFromJsonAsync<User_VM>($"https://localhost:7141/api/user/get_user_by_id/{_bill.CanelBy}") ?? new()).Name.ToString() ?? string.Empty;

            }

            //int a = 1;
            //var CancelUser = await _client.GetFromJsonAsync<User_VM>("https://localhost:7141/api/user/get_user_by_id/" + _bill.CanelBy);
            //         _bill.NameCreatBy = CancelUser.Name;
        }

        public void ReturnBill()
        {
            JSRuntime.InvokeVoidAsync("history.back");
        }
        public void CheckFeeShip(ChangeEventArgs e)
        {
            if (Login.Roleuser != "Admin" && Login.Roleuser != "Staff")
            {
                _navi.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            if (int.TryParse(e.Value.ToString(), out int inputValue))
            {
                if (inputValue > 0)
                {
                    ActiveTabFee = true;
                }
                else
                {
                    ActiveTabFee = false;
                }
            }
        }
        public async void UpdateFeeship()
        {
            if (Login.Roleuser != "Admin" && Login.Roleuser != "Staff")
            {
                _navi.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            //CheckFee();
            if (PhiShip == 0 || string.IsNullOrEmpty(PhiShip.ToString()))
            {
                _toastService.ShowError("Vui lòng điền phí vận chuyển");
                return;
            }
            else if (PhiShip <= 0)
            {
                _toastService.ShowError("Phí vận chuyển phải lớn hơn 0");
                return;
            }
			if (string.IsNullOrEmpty(Note))
			{
				_toastService.ShowError("Vui lòng điền ghi chú");
				return;
			}
			ActiveTabFee = true;
            //update
            _bill.ShippingFee = PhiShip;
            _bill.Note = _bill.Note + "," + Note;
            _bill.ConfirmationDate = DateTime.Now;
            if (_bill.Type == 1)
            {
                _bill.Status = 3;
            }
            var reponse = await _client.PutAsJsonAsync("https://localhost:7141/api/Bill/Put-Bill", _bill);
            if (reponse.StatusCode.ToString() == "OK")
            {
                _toastService.ShowSuccess("Cập nhật phí vận chuyển thành công");
                await OnInitializedAsync();
            }
        }
        public void CheckFee()
        {
            if (Login.Roleuser != "Admin" && Login.Roleuser != "Staff")
            {
                _navi.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            if (PhiShip == 0 || string.IsNullOrEmpty(PhiShip.ToString()))
            {
                _toastService.ShowError("Vui lòng điền phí vận chuyển");
                return;
            }
			else if (PhiShip <= 0)
            {
                _toastService.ShowError("Phí vận chuyển phải lớn hơn 0");
                return;
            }
            ActiveTabFee = true;
        }
        public async void Bangiao()
        {
            if (Login.Roleuser != "Admin" && Login.Roleuser != "Staff")
            {
                _navi.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            if (_bill.Type == 1)
            {
                _bill.Status = 4;
            }
            else if (_bill.Type == 2)
            {
                _bill.Status = 2;
            }
            _bill.ConfirmationDate = DateTime.Now;
            var reponse = await _client.PutAsJsonAsync("https://localhost:7141/api/Bill/Put-Bill", _bill);
            if (reponse.StatusCode.ToString() == "OK")
            {
                _toastService.ShowSuccess("Bàn giao thành công");
                await OnInitializedAsync();
            }
        }
        public async void ThanhCong()
        {
            if (Login.Roleuser != "Admin" && Login.Roleuser != "Staff")
            {
                _navi.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            if (_bill.Type == 1)
            {
                _bill.Status = 5;
            }
            else if (_bill.Type == 2)
            {
                _bill.Status = 3;
            }
            _bill.CompletionDate = DateTime.Now;



            if ((_bill.UserId != null || _bill.UserId != default)&& _bill.Type==2)
            {

                var _lstfomula = await _client.GetFromJsonAsync<List<Formula_VM>>("https://localhost:7141/api/Formula/get_formula");
                var Fomula = _lstfomula.FirstOrDefault(x => x.Status == 1);

                var _lstPoint = await _client.GetFromJsonAsync<List<CustomerPoint_VM>>("https://localhost:7141/api/CustomerPoint/getAllCustomerPoint");

                CustomerPoint_VM PointUser = _lstPoint.FirstOrDefault(x => x.UserID == _bill.UserId);
                //Tạo bản ghi 
                HistoryConsumerPoint_VM htr = new HistoryConsumerPoint_VM();
                htr.Id = Guid.NewGuid();
                htr.FormulaId = Fomula.Id;
                htr.BillId = _bill.Id;
                htr.ConsumerPointId = PointUser.UserID;
                htr.Point = _bill.TotalAmount / Fomula.Coefficient ?? 0;
                htr.Status = 1;

                var reponse1 = await _client.PostAsJsonAsync(" https://localhost:7141/api/HistoryConsumerPoint/add-HistoryConsumerPoint", htr);

                PointUser.Point = (Convert.ToInt32(PointUser.Point) - htr.Point).ToString();
                var reponseUpdatePointUser = await _client.PutAsJsonAsync("https://localhost:7141/api/CustomerPoint/putCustomerPoint", PointUser);
            }




            var reponse = await _client.PutAsJsonAsync("https://localhost:7141/api/Bill/Put-Bill", _bill);

            if (reponse.StatusCode.ToString() == "OK")
            {
                _toastService.ShowSuccess("Đơn hàng đã được giao thành công");
            }
            else
            {
                _toastService.ShowSuccess("Cập nhật thất bại");
            }


        }
        public async void CancelBill()
        {
            if (Login.Roleuser != "Admin" && Login.Roleuser != "Staff")
            {
                _navi.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            if (string.IsNullOrEmpty(LyDoHuy))
            {
                _toastService.ShowError("Vui lòng điền lý do hủy đơn");
                return;
            }
            _bill.Note += "lý do hủy: " + LyDoHuy;
            _bill.Status = 0;
            _bill.CancelDate = DateTime.Now;
            _bill.ConfirmationDate = DateTime.Now;
            _bill.CanelBy = _ihttpcontextaccessor.HttpContext.Session.GetString("UserId");
            var reponse = await _client.PutAsJsonAsync("https://localhost:7141/api/Bill/Put-Bill", _bill);
            if (reponse.StatusCode.ToString() == "OK")
            {
                _toastService.ShowWarning("Hủy đơn hàng thành công");
                var _lstUser = await _client.GetFromJsonAsync<List<User_VM>>("https://localhost:7141/api/user/get-user");

                ////_bill.NameCreatBy = _lstUser.FirstOrDefault(x=>x.Id==_bill.CreateBy).UserName;

                //Guid a = Guid.Parse(_ihttpcontextaccessor.HttpContext.Session.GetString("UserId"));


                var lstRollBack = await _client.GetFromJsonAsync<List<BillItem_VM>>($"https://localhost:7141/api/BillItem/GetBillItemsByBillId_billitemdb/{_bill.Id}");
                foreach (var item in lstRollBack)
                {
                    try
                    {
                        var pi = await _client.GetFromJsonAsync<ProductItem_VM>($"https://localhost:7141/api/productitem/get_all_productitem_byID/{item.ProductItemsId}");
                        pi.AvaiableQuantity += item.Quantity;
                        var update = await _client.PutAsJsonAsync("https://localhost:7141/api/productitem/update_productitem", pi);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                var x = new Uri(_navi.Uri);
                _navi.NavigateTo(x.ToString(), true);


            }
            else
            {
                _toastService.ShowError("Hủy đơn hàng thất bại");
            }

        }

		public void checkNameNguoiNhan(ChangeEventArgs e)
		{
			nameNguoiNhan = "";
			if (!String.IsNullOrEmpty(e.Value.ToString()))
			{
				nameNguoiNhan = e.Value.ToString().Trim();
			}
			var regex = @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]";
			if (Regex.IsMatch(nameNguoiNhan, regex))
			{
				nameNguoiNhan = "kytudacbiet";
			}
			checkPopupAddress = checkShowPopupAddress();
		}

		public void CheckLyDoHuy(ChangeEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value.ToString()))
            {
                activetabHuy = true;
            }
            else
            {
                activetabHuy = false;
            }
        }

		public bool checkShowPopupAddress()
		{
			if (
			   String.IsNullOrEmpty(nameNguoiNhan) ||
			   String.IsNullOrEmpty(phoneNumberNguoinhan) ||
				//phoneNumberNguoinhan.Length != 10 ||
				String.IsNullOrEmpty(_TinhTp) ||
			   String.IsNullOrEmpty(_QuanHuyen) ||
				String.IsNullOrEmpty(_PhuongXa) ||
				String.IsNullOrEmpty(AddressDetail) ||
				checkPhoneNumberNguoinhan == "khong dung"
				)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public void checkNoteAddresShip(ChangeEventArgs e)
		{
			NoteAddresShip = "";
			if (!String.IsNullOrEmpty(e.Value.ToString()))
			{
				NoteAddresShip = e.Value.ToString().Trim();
			}
			checkPopupAddress = checkShowPopupAddress();
		}

		public void checkAddressDetail(ChangeEventArgs e)
		{
			AddressDetail = "";
			if (!String.IsNullOrEmpty(e.Value.ToString()))
			{
				AddressDetail = e.Value.ToString().Trim();
			}
			var regex = @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]";
			if (Regex.IsMatch(AddressDetail, regex))
			{
				AddressDetail = "kytudacbiet";
			}
			checkPopupAddress = checkShowPopupAddress();
		}

		public async void checkValidateAddress()
		{
			ShowDiaChi = "";
			List<String> listContentShow = new List<String>();
			if (
				String.IsNullOrEmpty(nameNguoiNhan) ||
				nameNguoiNhan == "kytudacbiet" ||
				 String.IsNullOrEmpty(phoneNumberNguoinhan) ||
				 //phoneNumberNguoinhan.Length != 10 ||
				 String.IsNullOrEmpty(_TinhTp) ||
				String.IsNullOrEmpty(_QuanHuyen) ||
				 String.IsNullOrEmpty(_PhuongXa) ||
				 String.IsNullOrEmpty(AddressDetail) ||
				  AddressDetail == "kytudacbiet" || checkPhoneNumberNguoinhan == "khong dung"
				 )
			{
				if (String.IsNullOrEmpty(nameNguoiNhan) || nameNguoiNhan == "kytudacbiet")
				{
					if (String.IsNullOrEmpty(nameNguoiNhan))
					{
						listContentShow.Add("Tên");
					}
					else if (nameNguoiNhan == "kytudacbiet")
					{
						listContentShow.Add("Tên có ký tự đặc biệt");
					}
				}
				if (String.IsNullOrEmpty(phoneNumberNguoinhan) || checkPhoneNumberNguoinhan == "khong dung")
				{
					listContentShow.Add("Số điện thoại");
				}
				if (String.IsNullOrEmpty(_TinhTp))
				{
					listContentShow.Add("Tỉnh thành");
				}
				if (String.IsNullOrEmpty(_QuanHuyen))
				{
					listContentShow.Add("Quận huyện");
				}
				if (String.IsNullOrEmpty(_PhuongXa))
				{
					listContentShow.Add("Phường Xã");
				}
				if (String.IsNullOrEmpty(AddressDetail) || AddressDetail == "kytudacbiet")
				{
					if (AddressDetail == "kytudacbiet")
					{
						listContentShow.Add("Địa chỉ có ký tự đặc biệt");
					}
					else if (String.IsNullOrEmpty(AddressDetail))
					{
						listContentShow.Add("Địa chỉ chi tiết");
					}
				}

				string result = string.Join(", ", listContentShow);
				string ContentShow = "Vui lòng kiểm tra lại: " + result;
				_toastService.ShowError(ContentShow);
				checkPopupAddress = true;
			}
			else
			{
                //List<string> _listDiaChi = new List<string>();
                //_listDiaChi.Clear();
                //_listDiaChi.Add(AddressDetail);
                //_listDiaChi.Add(_PhuongXa);
                //_listDiaChi.Add(_QuanHuyen);
                //_listDiaChi.Add(_TinhTp);
                //ShowDiaChi = string.Join(", ", _listDiaChi);
                var bill = await _client.GetFromJsonAsync<Bill_VM>($"https://localhost:7141/api/Bill/get_bill_by_id/{_bill.Id}");
                bill.Recipient = nameNguoiNhan;
                bill.NumberPhone = phoneNumberNguoinhan;
                bill.Province = _TinhTp;
                bill.District = _QuanHuyen;
                bill.WardName = _PhuongXa;
                bill.ToAddress = AddressDetail;
                bill.Note +=", "+ NoteAddresShip;
				var updateStatus = await _client.PutAsJsonAsync("https://localhost:7141/api/Bill/Put-Bill", bill);
                if (updateStatus.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var uri = new Uri(_navi.Uri);

					_navi.NavigateTo(uri.ToString(),true);
                }
				checkPopupAddress = false;
			}
		}

		public async Task ChonTinhTP(ChangeEventArgs e)
		{
			_TinhTp = e.Value.ToString();
			_lstQuanHuyen.Clear();
			_lstXaPhuong.Clear();

			_QuanHuyen = string.Empty;
			_PhuongXa = string.Empty;
			var chon = _lstTinhTp_Data.FirstOrDefault(x => x.Name == _TinhTp);
			if (chon != null)
			{
				_lstQuanHuyen = _lstQuanHuyen_Data.Where(x => x.ProvinceId == chon.Id).ToList();
			}
			checkPopupAddress = checkShowPopupAddress();
		}
		public async Task ChonQuanHuyen(ChangeEventArgs e)
		{
			_QuanHuyen = e.Value.ToString();
			_lstXaPhuong.Clear();
			_PhuongXa = string.Empty;
			var chon = _lstQuanHuyen.FirstOrDefault(x => x.Name == _QuanHuyen);
			if (chon != null)
			{
				_lstXaPhuong = _lstXaPhuong_Data.Where(x => x.DistrictId == chon.Id).ToList();
			}
			checkPopupAddress = checkShowPopupAddress();

		}
		public void ChonXaPhuong(ChangeEventArgs e)
		{
			_PhuongXa = e.Value.ToString();
			checkPopupAddress = checkShowPopupAddress();
		}

		public void checkPhoneNumberNguoiNhan(ChangeEventArgs e)
		{
			//phoneNumberNguoinhan = "";

			//var regexPhoneNumber = @"^(0[35789])[0-9]{9}$";
			var regexPhoneNumber = @"^(0|84)(2(0[3-9]|1[0-6|8|9]|2[0-2|5-9]|3[2-9]|4[0-9]|5[1|2|4-9]|6[0-3|9]|7[0-7]|8[0-9]|9[0-4|6|7|9])|3[2-9]|5[5|6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])([0-9]{7})$";

			if (!String.IsNullOrEmpty(e.Value.ToString()))
			{
				if (Regex.IsMatch(e.Value.ToString().Trim(), regexPhoneNumber))
				{
					phoneNumberNguoinhan = e.Value.ToString().Trim();
					if (phoneNumberNguoinhan.StartsWith("84"))
					{
						// Chuyển đổi số điện thoại bắt đầu bằng "84" thành "0"
						phoneNumberNguoinhan = "0" + phoneNumberNguoinhan.Substring(2);

					}
					checkPhoneNumberNguoinhan = "";
				}
				else
				{
					checkPhoneNumberNguoinhan = "khong dung";
				}
			}
			checkPopupAddress = checkShowPopupAddress();
		}
		private static string NumberToText(double inputNumber, bool suffix = true)
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