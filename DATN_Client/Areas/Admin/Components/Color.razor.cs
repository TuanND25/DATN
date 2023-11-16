using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;
namespace DATN_Client.Areas.Admin.Components
{
	public partial class Color
	{
		HttpClient _httpClient = new HttpClient();
		public Color_VM color_VM = new Color_VM();
		[Inject] NavigationManager navigationManager { get; set; }
		List<Color_VM> color = new List<Color_VM>();
		public string Message { get; set; } = string.Empty;
		protected override async Task OnInitializedAsync()
		{
			color = await _httpClient.GetFromJsonAsync<List<Color_VM>>("https://localhost:7141/api/Color/get_color");
		}
		public async Task AddColor()
		{
			color_VM.Id = Guid.NewGuid();

			await _httpClient.PostAsJsonAsync<Color_VM>("https://localhost:7141/api/Color/PostColor", color_VM);
			navigationManager.NavigateTo("https://localhost:7075/Admin/Color", true);


		}
		public async Task UpdateColor(Color_VM color)
		{
			await _httpClient.PutAsJsonAsync<Color_VM>("https://localhost:7141/api/Color/PutColoe", color);
			navigationManager.NavigateTo("https://localhost:7075/Admin/Color", true);
		}
		public async void DeleteColor(Guid Id)
		{
			await _httpClient.DeleteAsync("https://localhost:7141/api/Color/DeleteColor/" + Id);
			navigationManager.NavigateTo("https://localhost:7075/Admin/Color", true);
		}
		public async Task LoadForm(Color_VM rvm)
		{
			color_VM.Id = rvm.Id;
			color_VM.Name = rvm.Name;
			color_VM.Status = rvm.Status;
		}

	}
}
