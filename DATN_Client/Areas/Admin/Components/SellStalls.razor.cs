using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using DATN_Shared.ViewModel.DiaChi;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.NetworkInformation;


namespace DATN_Client.Areas.Admin.Components
{
    public partial class SellStalls
    {
        HttpClient _client = new HttpClient();
        [Inject] NavigationManager _navigation { get; set; }
        List<ProductItem_Show_VM> _lstPrI_show_VM = new List<ProductItem_Show_VM>();
        List<Image_Join_ProductItem> _lstImg_PI = new List<Image_Join_ProductItem>();
        List<Products_VM> _lstP = new List<Products_VM>();
        List<Products_VM> _lstP_1 = new List<Products_VM>();
        List<Bill_VM> _lstBill = new List<Bill_VM>();
        Bill_VM bill = new Bill_VM();
        List<Bill_VM> _lstBill_Vm = new List<Bill_VM>();
        List<Bill_VM> _lstBill_Vm_show = new List<Bill_VM>();
        List<User_VM> _lstUser = new List<User_VM>();
        List<User_VM> _lstUser_1 = new List<User_VM>();
        User_VM getuser = new User_VM();
        User_VM userKhachvanglai_bien = new User_VM();

        public int ActiveTabSearchUser { get; set; } = 1;
        public int paymentmethodid { get; set; } = 2;

        private List<Province_VM> _lstTinhTp = new List<Province_VM>();
        private List<District_VM> _lstQuanHuyen = new List<District_VM>();
        private List<Ward_VM> _lstXaPhuong = new List<Ward_VM>();
        private List<Province_VM> _lstTinhTp_Data = new List<Province_VM>();
        private List<District_VM> _lstQuanHuyen_Data = new List<District_VM>();
        private List<Ward_VM> _lstXaPhuong_Data = new List<Ward_VM>();


        private List<ProductItem_VM> _lstProductItem = new List<ProductItem_VM>();
        private List<Size_VM> _lstSizeAll = new List<Size_VM>();
        private List<Color_VM> _lstColorAll = new List<Color_VM>();
        private List<Size_VM> _lstSize = new List<Size_VM>();
        private List<Color_VM> _lstColor = new List<Color_VM>();


        private List<BillItem_VM> _lstBillItemOnBill = new List<BillItem_VM>();
        private List<BillItem_VM> _lstBillItemOnBillShow = new List<BillItem_VM>();
        private List<ProductItem_Show_VM> _lstProductItemShow = new List<ProductItem_Show_VM>();
        private List<BillItemShowSellStall> _lstBillItemShow = new List<BillItemShowSellStall>();



        public int? SoluongProductItem { get; set; } = 0;
        public int SoluongProductItemMua { get; set; } = 10;

        public Guid IdSize { get; set; }
        public Guid IdColor { get; set; }


        public Guid BillId { get; set; }
        public string _TinhTp { get; set; }
        public string _QuanHuyen { get; set; }
        public string _PhuongXa { get; set; }
        public bool activeTabThemThongTin { get; set; }
        public bool activeTabDungDiem { get; set; }
        public bool activeTienMat { get; set; } = false;
        public bool activeChuyenKhoan { get; set; } = false;
        public bool activeBtnAddProductToBill { get; set; } = false;
        public bool activePlus { get; set; } = false;
        public Guid activeSize { get; set; }
        public Guid activeColor { get; set; }




        protected override async Task OnInitializedAsync()
        {
            _lstPrI_show_VM = await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
            _lstImg_PI = await _client.GetFromJsonAsync<List<Image_Join_ProductItem>>("https://localhost:7141/api/Image/GetAllImage_PrductItem");
            _lstP = await _client.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");
            _lstP_1 = await _client.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");
            _lstUser = await _client.GetFromJsonAsync<List<User_VM>>("https://localhost:7141/api/user/get-user");
            _lstUser_1 = await _client.GetFromJsonAsync<List<User_VM>>("https://localhost:7141/api/user/get-user");
            _lstBillItemOnBill = await _client.GetFromJsonAsync<List<BillItem_VM>>("https://localhost:7141/api/BillItem/get_alll_bill_item");
            _lstProductItemShow = await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
            var userKhachvanglai1 = _lstUser_1.FirstOrDefault(c => c.UserName == "khachvanglai");
            var userKhachvanglai = _lstUser.FirstOrDefault(c => c.UserName == "khachvanglai");
            userKhachvanglai_bien = userKhachvanglai;
            getuser = userKhachvanglai;

            _lstUser_1.Remove(userKhachvanglai1);
            _lstUser.Remove(userKhachvanglai);

            _lstTinhTp_Data = await _client.GetFromJsonAsync<List<Province_VM>>("https://api.npoint.io/ac646cb54b295b9555be"); // api tỉnh tp
            _lstTinhTp = _lstTinhTp_Data;
            _lstQuanHuyen_Data = await _client.GetFromJsonAsync<List<District_VM>>("https://api.npoint.io/34608ea16bebc5cffd42");
            _lstXaPhuong_Data = await _client.GetFromJsonAsync<List<Ward_VM>>("https://api.npoint.io/dd278dc276e65c68cdf5");


        }
        public async Task addBill()
        {
            var codeToday = "B" + DateTime.Now.ToString().Substring(0, 10).Replace("/", "") + ".";
            var id = Guid.NewGuid();
            bill = new Bill_VM();
            bill.Id = id;
            bill.Status = 5;
            bill.Type = 1;//offline

            bill.UserId = getuser.Id;


            _lstBill = (await _client.GetFromJsonAsync<List<Bill_VM>>("https://localhost:7141/api/Bill/get_alll_bill")).Where(c => c.BillCode.StartsWith(codeToday)).ToList();
            if (_lstBill.Count == 0) bill.BillCode = codeToday + "1";
            else bill.BillCode = codeToday + _lstBill.Max(c => int.Parse(c.BillCode.Substring(10)) + 1).ToString();

            try
            {
                var a = await _client.PostAsJsonAsync("https://localhost:7141/api/Bill/Post-Bill", bill);
                if (a.StatusCode.ToString() == "OK")
                {
                    _lstBill_Vm_show.Add(bill);
                    BillId = id;
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
        private async Task getBillId(Guid id)
        {
            if (_lstBill_Vm_show.Count > 0)
            {
                var x = _lstBill_Vm_show.FirstOrDefault(x => x.Id == id);
                BillId = x.Id;
            }
            var _lstProductItem = (await _client.GetFromJsonAsync<List<BillItem_VM>>("https://localhost:7141/api/BillItem/getbilldetail/" + BillId)).ToList();

        }
        public void getPaymetMethod(int id)
        {
            paymentmethodid = id;
        }
        public void closeBill(Guid id)
        {
            var z = _lstBill_Vm_show.FirstOrDefault(x => x.Id == id);
            _lstBill_Vm_show.Remove(z);
            if (_lstBill_Vm_show.Count==0)
            {
                BillId = default;
                return;
            }
            if (_lstBill_Vm_show.Count > 0)
            {
                BillId = _lstBill_Vm_show[0].Id;
            }

        }
        public void SearchUser(ChangeEventArgs e)
        {
            string a = RemoveUnicode(e.Value.ToString().ToLower());
            if (e.Value == null || e.Value.ToString() == "" || e.Value.ToString() == string.Empty)
            {
                _lstUser = _lstUser_1;
                return;
            }
            if (_lstUser.Count == 0)
            {
                _lstUser = _lstUser_1;
            }


            _lstUser = _lstUser.Where(c => RemoveUnicode(c.Name.ToLower()).Contains(a) || c.PhoneNumber.Contains(e.Value.ToString())).ToList();

            var userKhachvanglai = _lstUser.FirstOrDefault(c => c.UserName == "khachvanglai");
            _lstUser.Remove(userKhachvanglai);
            ActiveTabSearchUser = 2;
        }

        public void SearchProduct(ChangeEventArgs e)
        {
            _lstP = _lstP_1;
            string productName = RemoveUnicode(e.Value.ToString().ToLower());

            if (e.Value == null || e.Value.ToString() == "" || e.Value.ToString() == string.Empty)
            {
                _lstP = _lstP_1;
                return;
            }
            //if (_lstUser.Count == 0)
            //{
            //    _lstP = _lstP_1;

            //}
            _lstP = _lstP.Where(c => RemoveUnicode(c.Name.ToLower()).Contains(productName) || RemoveUnicode(c.ProductCode.ToLower()).Contains(productName)).ToList();
            //Chưa xong còn search theo mã nữa
        }

        public void offTabSearchUser()
        {
            ActiveTabSearchUser = 1;
        }
        public void GetUserFormSearchUser(Guid id)
        {
            getuser = _lstUser_1.FirstOrDefault(x => x.Id == id);
            ActiveTabSearchUser = 1;
        }
        public void ClearInfoUser()
        {
            getuser = userKhachvanglai_bien;
        }
        public async Task ChonTinhTP(ChangeEventArgs e)
        {
            _TinhTp = e.Value.ToString();
            _lstQuanHuyen.Clear();
            _lstXaPhuong.Clear();

            _QuanHuyen = string.Empty;
            _PhuongXa = string.Empty;
            var chon = _lstTinhTp_Data.FirstOrDefault(x => x.Name == _TinhTp);
            _lstQuanHuyen = _lstQuanHuyen_Data.Where(x => x.ProvinceId == chon.Id).ToList();
        }
        public async Task ChonQuanHuyen(ChangeEventArgs e)
        {
            _QuanHuyen = e.Value.ToString();
            _lstXaPhuong.Clear();
            _PhuongXa = string.Empty;
            var chon = _lstQuanHuyen.FirstOrDefault(x => x.Name == _QuanHuyen);
            _lstXaPhuong = _lstXaPhuong_Data.Where(x => x.DistrictId == chon.Id).ToList();

        }
        public void ActiveTabTienMat()
        {
            activeTienMat = true;
        }
        public void ActiveTabChuyenKhoan()
        {
            activeChuyenKhoan = true;
        }
        public void CloseTabTienMat()
        {
            activeTienMat = false;
        }
        public void CloseTabChuyenKhoan()
        {
            activeChuyenKhoan = false;
        }

        public async Task getProductChooseSizeAndColor(Guid IdProduct)
        {
            activeSize = default;
            SoluongProductItemMua = 1;
            SoluongProductItem = 0;
            //Lấy list ProductItem của Product          
            _lstProductItem = (await _client.GetFromJsonAsync<List<ProductItem_VM>>($"https://localhost:7141/api/productitem/get_all_productitem_byProduct/{IdProduct}")).ToList();

            //Lấy list size All
            _lstSizeAll = (await _client.GetFromJsonAsync<List<Size_VM>>("https://localhost:7141/api/Size/get_size")).ToList();

            //Lấy các size hiện có của Product
            List<string> _lstSizeSample = new List<string> { "XS", "S", "M", "L", "XL", "2XL", "3XL", "4XL", "5XL" };
            var listSizeId = _lstProductItem.Select(c => c.SizeId).Distinct().ToList();

            _lstSize.Clear();
            _lstColor.Clear();
            foreach (var item in listSizeId)
            {
                _lstSize.Add(_lstSizeAll.FirstOrDefault(x => x.Id == item));
            }
            _lstSize = _lstSize.OrderBy(c => _lstSizeSample.IndexOf(c.Name)).ToList();
            //Lấy list size của Product


            //Lấy list số của Product
            //Lấy số lượng của Product Item

            CheckValidateAddProductToBill();
        }
        public async Task getSizeAndShowColor(Guid IdSize)
        {
            activeSize = default;
            activeSize = IdSize;
            activeColor = default;


            _lstColorAll = (await _client.GetFromJsonAsync<List<Color_VM>>("https://localhost:7141/api/Color/get_color")).ToList();
            var _listColorId = _lstProductItem.Select(c => c.ColorId).Distinct().ToList();

            _lstColor.Clear();
            foreach (var item in _listColorId)
            {
                _lstColor.Add(_lstColorAll.FirstOrDefault(x => x.Id == item));
            }
            CheckValidateAddProductToBill();
        }
        public async Task getColorAndgetQuantityProductItem(Guid IdColor)
        {
            activeColor = default;
            activeColor = IdColor;

            SoluongProductItem = _lstProductItem.FirstOrDefault(x => x.ColorId == IdColor && x.SizeId == activeSize).AvaiableQuantity;
            if (SoluongProductItem < SoluongProductItemMua)
            {
                activePlus = true;
            }
            else
            {
                activePlus = false;
            }
            CheckValidateAddProductToBill();
        }
        public async Task AddProductToBill()
        {

            //Thực hiện add productItem vào hóa đơn chờ
            CheckValidateAddProductToBill();
            //vừa phải add vào hóa đơn chi tiết thật và add vào hóa đơn chi tiết ảo 
            //add vào hòa đơn thật nếu thành công add vào hóa đơn ảo 
            //add vào hóa đơnt thật
            
            var ProductItem = _lstProductItem.FirstOrDefault(x => x.ColorId == activeColor && x.SizeId == activeSize);
            var ProductItemShow = _lstProductItemShow.FirstOrDefault(x => x.Id == ProductItem.Id);
            var Soluongtonkho = ProductItem.AvaiableQuantity;
            var price = ProductItem.PriceAfterReduction;
            var status = 1;
            var IdBillAdd = Guid.NewGuid();
            BillItem_VM billadd = new BillItem_VM();
            billadd.Id = IdBillAdd;
            billadd.BillId = BillId;
            billadd.ProductItemsId = ProductItem.Id;
            billadd.Quantity = SoluongProductItemMua;
            billadd.Price = ProductItem.PriceAfterReduction;
            billadd.Status = 1;
            var AddBillItemToDB = await _client.PostAsJsonAsync("https://localhost:7141/api/BillItem/Post-BillItem", billadd);
            if (AddBillItemToDB.StatusCode.ToString() == "OK")
            {
                var BillItemShow = _lstBillItemOnBill.FirstOrDefault(x => x.Id == IdBillAdd);
                BillItemShowSellStall BillItemShowSellStall = new BillItemShowSellStall();

                BillItemShowSellStall.Id = IdBillAdd;
                BillItemShowSellStall.ProductItemId = ProductItem.Id;
                BillItemShowSellStall.BillId = BillId;
                BillItemShowSellStall.ProductName = ProductItemShow.Name;
                BillItemShowSellStall.SizeName = ProductItemShow.SizeName;
                BillItemShowSellStall.ColorName = ProductItemShow.ColorName;
                BillItemShowSellStall.AvailableQuantity = ProductItemShow.AvaiableQuantity;
                BillItemShowSellStall.Quantity = billadd.Quantity;
                BillItemShowSellStall.Price = ProductItemShow.PriceAfterReduction;
                BillItemShowSellStall.Status = 1;

                _lstBillItemShow.Add(BillItemShowSellStall);
            }        
        }

        public void CheckSoluongProductItemMua(ChangeEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Value.ToString()))
            {
                SoluongProductItemMua = Convert.ToInt32(e.Value);

                //if (Convert.ToInt32(e.Value) < 0)
                //{
                //    SoluongProductItemMua = 1;               
                //    return;
                //}




                if (SoluongProductItem < SoluongProductItemMua)
                {
                    activePlus = true;
                }
                else
                {
                    activePlus = false;
                }
            }
            CheckValidateAddProductToBill();
        }





        public void PlusSoluongProduct()
        {
            if (SoluongProductItemMua < SoluongProductItem)
            {
                SoluongProductItemMua += 1;
            }

        }
        public void MinusSoluongProduct()
        {
            if (SoluongProductItemMua > 1)
            {
                SoluongProductItemMua -= 1;
            }

        }

        public class BillItemShowSellStall
        {
            public Guid Id { get; set; }
            public Guid BillId { get; set; }
            public Guid ProductItemId { get; set; }
            public string ProductName { get; set; }
            public string SizeName { get; set; }
            public string ColorName { get; set; }
            public int? AvailableQuantity { get; set; }
            public int Quantity { get; set; }
            public int? Price { get; set; }
            public int Status { get; set; }
        }

        public void CheckValidateAddProductToBill()
        {
            if (activeSize == default || activeColor == default || SoluongProductItem <= 0 || SoluongProductItemMua > SoluongProductItem || SoluongProductItemMua < 1 || BillId == default)
            {
                activeBtnAddProductToBill = false;
            }
            else
            {
                activeBtnAddProductToBill = true;
            }
        }
        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
    "đ",
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
    "í","ì","ỉ","ĩ","ị",
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
    "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
    "d",
    "e","e","e","e","e","e","e","e","e","e","e",
    "i","i","i","i","i",
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
    "u","u","u","u","u","u","u","u","u","u","u",
    "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
    }

}
