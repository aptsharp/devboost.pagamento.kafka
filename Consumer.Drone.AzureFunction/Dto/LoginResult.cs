namespace Consumer.Drone.AzureFunction.Dto
{
    public class LoginResult
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public bool Valid { get; set; }
    }
}
