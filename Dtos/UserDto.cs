namespace serverAPI.Dtos{
    public record UserCreateDto : UserBaseDto{
        public string email {get; init;}
    }
    public record UserBaseDto {
        public string username {get; init;}
        public string password {get; init;}
    }
}