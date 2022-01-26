namespace Constants.Controllers
{
    public static class RegionControllerConstant
    {
        // Routes
        public const string GET_ALL_REGIONS_ROUTE = "all";

        public const string ADD_REGION_ROUTE = "add/{regionName}";

        public const string UPDATE_REGION_ROUTE = "update/{oldRegion}-{newRegion}";

        public const string DELETE_REGION_ROUTE = "delete/{region}";


        // Error messages
        public const string INVALID_REGION_NAME_MESSAGE = "Invalid region name!";

        public const string REGION_DELETED_SUCCESSFULLY_MESSAGE = "{0} is successfully deleted!";
    }
}
