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

		// ��� ��������� SGTIN ������ ���� GTIN � ��������� ������
		public void SgtinTextChanged(object sender, EventArgs e)
		{
			if (SGTIN.Length == 27)
			{
				//�������� ����������, �� ��� �� �� ��������
				GTIN = SGTIN.Substring(0, 14);
				Serial = SGTIN.Substring(14, 13);
			}
		}

		// ��� ��������� ���� GTIN ������ ���� SGTIN
		public void GtinTextChanged(object sender, EventArgs e)
		{
			if (GTIN.Length > 14) GTIN = GTIN.Substring(0, 13);
			if (GTIN.Length == 14)
			{
				SGTIN = GTIN + Serial;
			}
		}

		// ��� ��������� ���� c�������� ������ ������ SGTIN
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