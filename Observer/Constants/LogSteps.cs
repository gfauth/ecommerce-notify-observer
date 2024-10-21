namespace Observer.Constants
{
    internal static class LogSteps
    {
        public const string EXCEPTION = "EXCEPTION";

        // Users steps
        public const string GET_USER_BY_ID = "GET_USER_BY_ID";
        public const string CREATE_NEW_USER = "CREATE_NEW_USER";
        public const string EDIT_USER_BY_ID = "EDIT_USER_BY_ID";
        public const string DELETE_USER_BY_ID = "DELETE_USER_BY_ID";
        public const string USER_VALIDATION_ERROR = "USER_VALIDATION_ERROR";

        // Services steps
        public const string USER_SERVICE_CREATE_USER_PROCESSING = "USER_SERVICE_CREATE_USER_PROCESSING";
        public const string USER_SERVICE_RETRIEVE_USER_PROCESSING = "USER_SERVICE_RETRIEVE_USER_PROCESSING";
        public const string USER_SERVICE_DELETE_USER_PROCESSING = "USER_SERVICE_DELETE_USER_PROCESSING";
        public const string USER_SERVICE_UPDATE_USER_PROCESSING = "USER_SERVICE_UPDATE_USER_PROCESSING";

        // Database steps
        public const string USER_DATABASE_RETRIEVE_DATA = "USER_DATABASE_RETRIEVE_DATA";
        public const string USER_DATABASE_CREATE_DATA = "USER_DATABASE_CREATE_DATA";
        public const string USER_DATABASE_UPDATE_DATA = "USER_DATABASE_UPDATE_DATA";
        public const string USER_DATABASE_DELETE_DATA = "USER_DATABASE_DELETE_DATA";
    }
}
