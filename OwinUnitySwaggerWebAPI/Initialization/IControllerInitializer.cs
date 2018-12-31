namespace OwinUnitySwaggerWebAPI.Initialization
{
    public interface IControllerInitializer
    {
        void OneTimeStartup();
        void OneTimeShutdown();
    }
}
