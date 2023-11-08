using DATN_Shared.ViewModel;
using Newtonsoft.Json;

namespace DATN_Client.SessionService
{
	public static class SessionServices
	{
		public static List<CartItems_VM> GetLstFromSession_LstCI(ISession session, string key)
		{
			// Lấy string Json từ Session
			var JsonData = session.GetString(key);
			if (JsonData == null) return new List<CartItems_VM>();

			// Chuyển đổi dữ liệu vừa lấy được từ sang dạng mong muốn
			var s = JsonConvert.DeserializeObject<List<CartItems_VM>>(JsonData);
			// Nếu null thì trả về 1 list rỗng
			return s;
		}
		public static bool SetLstFromSession_LstCI(ISession session, string key, List<CartItems_VM> values)
		{
			try
			{
				var JsonData = JsonConvert.SerializeObject(values);
				session.SetString(key, JsonData);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
			
		}
	}
}
