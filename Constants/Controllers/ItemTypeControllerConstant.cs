namespace Constants.Controllers
{
    public static class ItemTypeControllerConstant
    {
        // Routes
        public const string GET_ALL_ITEM_TYPES_ROUTE = "all";

        public const string ADD_ITEM_TYPE_ROUTE = "add/{itemType}";

        public const string UPDATE_ITEM_TYPE_ROUTE = "update/{oldItemType}-{newItemType}";

        public const string DELETE_ITEM_TYPE_ROUTE = "delete/{itemType}";


        // Error messages
        public const string INVALID_ITEM_TYPE_NAME_MESSAGE = "Invalid item type name!";

        public const string ITEM_TYPE_SUCCESSFULLY_DELETED_MESSAGE = "{0} is successfully deleted!";
    }
}
