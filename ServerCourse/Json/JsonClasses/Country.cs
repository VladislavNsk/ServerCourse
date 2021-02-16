namespace Json.JsonClasses
{
    public class Country
    {
        public string Name { get; set; }

        public string Alpha2Code { get; set; }

        public string Alpha3Code { get; set; }

        public string Capital { get; set; }

        public string Region { get; set; }

        public string Subregion { get; set; }

        public double? Population { get; set; }

        public string Demonym { get; set; }

        public double? Area { get; set; }

        public double? Gini { get; set; }

        public string NativeName { get; set; }

        public string NumericCode { get; set; }

        public string Flag { get; set; }

        public string Cioc { get; set; }

        public string[] TopLevelDomain { get; set; }

        public string[] CallingCodes { get; set; }

        public string[] AltSpellings { get; set; }

        public double[] LatLng { get; set; }

        public string[] Timezones { get; set; }

        public string[] Borders { get; set; }

        public Currency[] Currencies { get; set; }

        public Language[] Languages { get; set; }

        public Translation Translations { get; set; }

        public RegionalBloc[] RegionalBlocs { get; set; }
    }
}