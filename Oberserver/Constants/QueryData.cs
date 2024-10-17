namespace Oberserver.Constants
{
    public class QueryData
    {
        public const string InsertUsers = @"
insert into 
    dbo.Users
    (
        Name,
        LastName,
        Birthdate,
        Document,
        Login,
        Password,
        CreatedAt,
        UpdatedAt
    )
values
    (
        @Name,
        @LastName,
        @Birthdate,
        @Document,
        @Login,
        @Password,
        @CreatedAt,
        @UpdatedAt
    )";
    }
}
