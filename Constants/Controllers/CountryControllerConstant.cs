namespace Constants.Controllers
{
    public static class CountryControllerConstant
    {
        // Routes
        public const string GET_ALL_COUNTRIES_ROUTE = "all";

        public const string GET_COUNTRY_BY_NAME_ROUTE = "get_country_by_name/{countryName}";

        public const string ADD_COUNTRY_ROUTE = "add";

        public const string UPDATE_COUNTRY_ROUTE = "update/{countryName}";

        public const string DELETE_COUNTRY_ROUTE = "delete/{countryName}";


        // Error messages
        public const string INVALID_COUNTRY_DATA_MESSAGE = "Invalid country data!";

        public const string INVALID_COUNTRY_NAME_MESSAGE = "Invalid country name!";

        public const string COUNTRY_DELETED_SUCCESSFULLY_MESSAGE = "{0} is deleted successfully!";
    }
}
