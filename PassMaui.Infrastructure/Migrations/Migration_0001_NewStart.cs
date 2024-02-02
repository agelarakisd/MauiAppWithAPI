using Migr8;

namespace PassMaui.Infrastructure.Migrations;

[Migration(1,"a new start")]
public class Migration_0001_NewStart : ISqlMigration
{
    public string Sql => $@"

CREATE SCHEMA [MasterData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [MasterData].[Accounts](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Site] [nvarchar](100) NOT NULL,
    [Description] [nvarchar](100) NOT NULL,
    [Username] [nvarchar](100) NOT NULL,
    [Password] [nvarchar](100) NOT NULL,
CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY])
GO";
}