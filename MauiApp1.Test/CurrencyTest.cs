namespace MauiApp1.Test
{
    public class CurrencyTest
    {
        [Theory]
        [InlineData(2, "30,000.00", "2.00", "300.00")]
        public void UpdateValue_ValueUSD(double basenumber, string idr, string usd, string jpy)
        {
            double UsdtoIdr = 15000;
            double UsdtoJpy = 150;
            List<string> ActualList;

            ActualList = Currency.CurrencyService.UpdateValue(Currency.CurrencyService.Currencies.USD, basenumber, UsdtoIdr, UsdtoJpy);

            Assert.Equal(idr, ActualList[0]);
            Assert.Equal(usd, ActualList[1]);
            Assert.Equal(jpy, ActualList[2]);
        }

        [Theory]
        [InlineData(45000, "45,000.00", "3.00", "450.00")]
        public void UpdateValue_ValueIDR(double basenumber, string idr, string usd, string jpy)
        {
            double UsdtoIdr = 15000;
            double UsdtoJpy = 150;
            List<string> ActualList;

            ActualList = Currency.CurrencyService.UpdateValue(Currency.CurrencyService.Currencies.IDR, basenumber, UsdtoIdr, UsdtoJpy);

            Assert.Equal(idr, ActualList[0]);
            Assert.Equal(usd, ActualList[1]);
            Assert.Equal(jpy, ActualList[2]);
        }

        [Theory]
        [InlineData(6000, "600,000.00", "40.00", "6,000.00")]
        public void UpdateValue_ValueJPY(double basenumber, string idr, string usd, string jpy)
        {
            double UsdtoIdr = 15000;
            double UsdtoJpy = 150;
            List<string> ActualList;

            ActualList = Currency.CurrencyService.UpdateValue(Currency.CurrencyService.Currencies.JPY, basenumber, UsdtoIdr, UsdtoJpy);

            Assert.Equal(idr, ActualList[0]);
            Assert.Equal(usd, ActualList[1]);
            Assert.Equal(jpy, ActualList[2]);
        }
    }
}