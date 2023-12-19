using DATN_Client.Areas.Customer.Component;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class Voucher
    {
        [Inject] Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind
        HttpClient httpClient = new HttpClient();
        public Voucher_VM voucher_VM = new Voucher_VM();
        public Voucher_VM voucher_VM1 = new Voucher_VM();
        [Inject] NavigationManager navigationManager { get; set; }

        List<Voucher_VM> vouchers = new List<Voucher_VM>();
        public string Message { get; set; } = string.Empty;
		public string messagestart { get; set; }
		public string messageend { get; set; }
		protected override async Task OnInitializedAsync()
        {
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            vouchers = await httpClient.GetFromJsonAsync<List<Voucher_VM>>("https://localhost:7141/api/Voucher");
            await AutoChangeStatusVoucher();
        }
        public async IAsyncEnumerable<Voucher_VM> SelectOneVoucher()
        {
            foreach (var voucher in vouchers)
            {
                yield return voucher;
            }
        }
        public async Task AutoChangeStatusVoucher()
        {
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            await foreach (var voucher in SelectOneVoucher())
            {
                if (voucher.EndDate < DateTime.Now)
                {
                    voucher.Status = 0;
                    await httpClient.PutAsJsonAsync<Voucher_VM>("https://localhost:7141/api/Voucher/Put-Voucher", voucher);
                }
            }

        }

        public async Task AddVoucher()
        {
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }

            if (voucher_VM.Code == string.Empty)
            {
                _toastService.ShowError("Vui lòng nhập đầy đủ thông tin");
                return;
            }
			if (voucher_VM.StartDate < DateTime.Now)
			{
				messagestart = "Ngày bắt đầu phải lớn hơn ngày hiện tại";
            }
            else if (voucher_VM.StartDate > DateTime.Now && voucher_VM.EndDate < voucher_VM.StartDate)
            {
                messageend = "Ngày kết thúc phải lớn hơn ngày bắt đầu";
                messagestart = "";
            }
            else if (voucher_VM.EndDate < voucher_VM.StartDate)
            {
				messageend = "Ngày kết thúc phải lớn hơn ngày bắt đầu";
				messagestart = "";
            }
            else
            {
				voucher_VM.Id = Guid.NewGuid();

				await httpClient.PostAsJsonAsync<Voucher_VM>("https://localhost:7141/api/Voucher/Post-Voucher", voucher_VM);
				navigationManager.NavigateTo("/voucher-management", true);

			}


		}


        public async Task ChangeStatusVoucher(Voucher_VM voucher)
        {
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            voucher.Status = 0;
            await httpClient.PutAsJsonAsync<Voucher_VM>("https://localhost:7141/api/Voucher/Put-Voucher", voucher);
        }
        public async Task UpdateVoucher()
        {
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
			if (voucher_VM1.Code == string.Empty)
			{
				_toastService.ShowError("Vui lòng nhập đầy đủ thông tin");
				return;
			}

			if (voucher_VM1.StartDate < DateTime.Now)
			{
				messagestart = "Ngày bắt đầu phải lớn hơn ngày hiện tại";
			}
			else if (voucher_VM1.StartDate > DateTime.Now && voucher_VM.EndDate < voucher_VM.StartDate)
			{
				messageend = "Ngày kết thúc phải lớn hơn ngày bắt đầu";
				messagestart = "";
			}
			else if (voucher_VM1.EndDate < voucher_VM1.StartDate)
			{
				messageend = "Ngày kết thúc phải lớn hơn ngày bắt đầu";
				messagestart = "";
			}
            else
            {
				await httpClient.PutAsJsonAsync<Voucher_VM>("https://localhost:7141/api/Voucher/Put-Voucher", voucher_VM1);
				navigationManager.NavigateTo("/voucher-management", true);
			}
		
        }

        public async Task LoadFormVoucher(Voucher_VM GetValueFromList)
        {
            if (Login.Roleuser != "Admin")
            {
                navigationManager.NavigateTo("https://localhost:7075/Admin", true);
                return;
            }
            voucher_VM1.Id = GetValueFromList.Id;
            voucher_VM1.Name = GetValueFromList.Name;
            voucher_VM1.Code = GetValueFromList.Code;
            voucher_VM1.Percent = GetValueFromList.Percent;
            voucher_VM1.Quantity = GetValueFromList.Quantity;
            voucher_VM1.Discount_Conditions = GetValueFromList.Discount_Conditions;
            voucher_VM1.Maximum_Reduction = GetValueFromList.Maximum_Reduction;
            voucher_VM1.StartDate = GetValueFromList.StartDate;
            voucher_VM1.EndDate = GetValueFromList.EndDate;
            voucher_VM1.Status = GetValueFromList.Status;

        }
    }
}
