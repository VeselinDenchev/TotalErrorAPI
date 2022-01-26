namespace Constants.Controllers
{
    public static class OrderControllerConstant
    {
        // Routes
        public const string GET_ALL_ORDERS_ROUTE = "all";

        public const string GET_ORDERS_GROUPED_BY_REGION_ROUTE = "orders_grouped_by_region";
        
        public const string GET_ORDERS_GROUPED_BY_COUNTRY_ROUTE = "orders_grouped_by_country";
        
        public const string GET_ORDERS_GROUPED_BY_ORDER_DATE_ROUTE = "orders_grouped_by_order_date";

        public const string ADD_ORDER_ROUTE = "add";

        public const string UPDATE_ORDER_ROUTE = "update/{orderId}";

        public const string DELETE_ORDER_ROUTE = "delete/{orderId}";


        // Error messages
        public const string INVALID_ORDER_DATA_MESSAGE = "Invalid order data!";

        public const string SALE_IS_ALREADY_IN_THE_DATABASE_MESSAGE = "A sale is already in the database!";

        public const string ORDER_WITH_SUCH_ID_DOESNT_EXIST_MESSAGE = "Order with such id doesn't exist!";

        public const string ORDER_IS_SUCCSESSFULLY_DELETED_MESSAGE = "Order is successfully deleted!";
    }
}
