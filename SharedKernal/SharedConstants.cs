namespace SharedKernal
{
    public static class SharedConstants
    {
        public static class Settings
        {
            // as we used it in many places so if we changes it we change in one place 
            public const string DatabaseConnection = "Database";
        }
        public static class CrossCutting
        {
            public const string ApiKeyHeader = "X-API-KEY";
            public const string CorrelationIdHeader = "X-Correlation-Id";
            public const string LogCorrelationId = "CorrelationId";
            public const string CROSPolicy = "App_Cros_Policy";
        }

    }
}
