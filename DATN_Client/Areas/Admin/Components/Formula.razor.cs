using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using System.Net.Http;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class Formula
    {
        HttpClient _httpClient = new HttpClient();
        public Formula_VM formula_VM = new Formula_VM();
        [Inject] NavigationManager navigationManager { get; set; }
        List<Formula_VM> formula = new List<Formula_VM>();  
        public string Message { get; set; } = string.Empty;
		protected override async Task OnInitializedAsync()
		{
			formula = await _httpClient.GetFromJsonAsync<List<Formula_VM>>("https://localhost:7141/api/Formula/get_formula");
		}
		public async Task AddFormula()
		{
			formula_VM.Id = Guid.NewGuid();

			await _httpClient.PostAsJsonAsync<Formula_VM>("https://localhost:7141/api/Formula/PostFormula", formula_VM);
			navigationManager.NavigateTo("/formula-management", true);


		}
		public async Task UpdateFormula(Formula_VM formula)
		{
			await _httpClient.PutAsJsonAsync<Formula_VM>("https://localhost:7141/api/Formula/PutFormula", formula);
            navigationManager.NavigateTo("/formula-management", true);
        }
		public async void DeleteFormula(Guid Id)
		{
			await _httpClient.DeleteAsync("https://localhost:7141/api/Formula/DeleteFormula/" + Id);
			navigationManager.NavigateTo("/formula-management", true);
		}
		public async Task LoadForm(Formula_VM rvm)
		{
			formula_VM.Id = rvm.Id; 
			formula_VM.Coefficient = rvm.Coefficient;
			formula_VM.Status = rvm.Status;
		}


	}
}
