namespace Observer.Constants
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
    )

select scope_identity()";

        public const string SelectOneUsers = @"
select
    id,
    Name,
    LastName,
    Birthdate,
    Document,
    Login,
    Password,
    CreatedAt,
    UpdatedAt
from 
    dbo.Users
where
    id = @userId";

        public const string UpdateUsers = @"
update
    dbo.Users
set
    Name = @Name,
    Login = @Login,
    LastName = @LastName,
    Document = @Document,
    Password = @Password,
    Birthdate = @Birthdate,
    UpdatedAt = getdate()
where
    id = @Id";

        public const string DeleteUsers = @"
delete from
    dbo.Users
where
    Id = @userId;
";
    }
}
