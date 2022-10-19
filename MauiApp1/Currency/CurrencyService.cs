namespace MauiApp1.Currency
{
    public class CurrencyService
    {
        private static HttpClient httpClient;
        private static JsonSerializerOptions serializer;
        private static readonly string ApiKey = "F3p5mjqXn9wZ9LtR40pVHYF854wXxkqr";
        private static readonly string BaseUrl = "https://api.apilayer.com/exchangerates_data/latest?symbols=jpy%2C%20idr&base=USD";

        // TODO: always check the ApiKey when you just opened the project, before and after git operations.
        public static void InitCurrencyService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("apikey", ApiKey);
            serializer = new JsonSerializerOptions { WriteIndented = true };
        }

        public static async Task<CurrencyResponse> GetCurrency(bool update)
        {
            CurrencyResponse currencyContent = new();

            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet || !update)
                    currencyContent = Barrel.Current.Get<CurrencyResponse>(key: BaseUrl);
                if (currencyContent.success)
                    return currencyContent;

                HttpResponseMessage response = await httpClient.GetAsync(BaseUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsoncontent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Updated value" + jsoncontent);
                    currencyContent = JsonSerializer.Deserialize<CurrencyResponse>(jsoncontent, serializer);

                    Barrel.Current.Add(BaseUrl, jsoncontent, TimeSpan.FromDays(1));
                }
                else
                {
                    var jsoncontent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Updated value : " + jsoncontent);
                    currencyContent = Barrel.Current.Get<CurrencyResponse>(key: BaseUrl);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Cannot get currency: {ex.Message}");
                await Shell.Current.DisplayAlert("Error while getting value", ex.Message, "Ok");
            }

            return currencyContent;
        }
    }

    public class CurrencyResponse
    {
        public bool success { get; set; }
        public long timestamp { get; set; }
        public string _base { get; set; }
        public string date { get; set; }
        public Rates rates { get; set; }
    }

    public class Rates
    {
        public float JPY { get; set; }
        public float IDR { get; set; }
    }
}