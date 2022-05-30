namespace PVK.Meetups.Web
{
    public class FeatureFlags
    {
        public FeatureFlags(ConfigurationManager configuration)
        {
            //TODO add logging
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var section = configuration.GetSection("Features");

            var boolValue = section.GetValue<bool?>("EnableLocalAccounts");
            if (boolValue != null)
                EnableLocalAccounts = boolValue.Value;

            boolValue = section.GetValue<bool?>("EnableWeakPasswords");
            if (boolValue != null)
                EnableWeakPasswords = boolValue.Value;

        }

        public bool EnableLocalAccounts { get; }
        public bool EnableWeakPasswords { get; }
    }
}
