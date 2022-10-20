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

        public enum Currencies
        {
            IDR,
            USD,
            JPY
        }

        public static List<string> UpdateValue(Currencies Currency, double Basenumber, double UsdToIdr, double UsdToJpy)
        {
            // order as in UI: IDR, USD, JPY
            var result = new List<string>() { "0.00", "0.00", "0.00" };
            double IdrValue = 0;
            double UsdValue = 0;
            double JpyValue = 0;

            if (Currency.Equals(Currencies.IDR))
            {
                IdrValue = Basenumber;
                UsdValue = Basenumber / UsdToIdr;
                JpyValue = UsdValue * UsdToJpy;
            }
            else if (Currency.Equals(Currencies.USD))
            {
                IdrValue = Basenumber * UsdToIdr;
                UsdValue = Basenumber;
                JpyValue = Basenumber * UsdToJpy;
            }
            else if (Currency.Equals(Currencies.JPY))
            {
                UsdValue = Basenumber / UsdToJpy;
                IdrValue = UsdValue * UsdToIdr;
                JpyValue = Basenumber;
            }

            result[0] = IdrValue.ToString("N");
            result[1] = UsdValue.ToString("N");
            result[2] = JpyValue.ToString("N");

            return result;
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