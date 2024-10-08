using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebCryptoGetterRP.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		[BindProperty]
		public string GTIN { get; set; } = "04605310";

		[BindProperty]
		public string Serial { get; set; }

		[BindProperty]
		public string SGTIN { get; set; }

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}

		public void OnGet()
		{

		}

		// При изменении SGTIN меняет поля GTIN и серийного номера
		public void SgtinTextChanged(object sender, EventArgs e)
		{
			if (SGTIN.Length == 27)
			{
				//странная переменная, но без неё не работает
				GTIN = SGTIN.Substring(0, 14);
				Serial = SGTIN.Substring(14, 13);
			}
		}

		// При изменении поля GTIN меняет поле SGTIN
		public void GtinTextChanged(object sender, EventArgs e)
		{
			if (GTIN.Length > 14) GTIN = GTIN.Substring(0, 13);
			if (GTIN.Length == 14)
			{
				SGTIN = GTIN + Serial;
			}
		}

		// При изменении поля cерийного номера меняет SGTIN
		public void SerialTextChanged(object sender, EventArgs e)
		{
			if (Serial.Length > 13) Serial = Serial.Substring(0, 13);
			if (Serial.Length == 13)
			{
				SGTIN = GTIN + Serial;
			}
		}
		protected void SearchDMX(object sender, EventArgs e)
		{
		}
	}
}
