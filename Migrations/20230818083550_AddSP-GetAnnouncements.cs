using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowLearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddSPGetAnnouncements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetAnnouncements]
                    @PageIndex Int,@PageSize Int
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT [a].[AnnouncementId], [a].[CreateTime], [a].[HtmlText], [a].[Icon], [a].[SubTitle], [a].[Title], [a].[UpdateTime], [a].[UserId]
                    FROM [Announcements] AS [a]
                    ORDER BY [a].[UpdateTime] DESC
                    OFFSET @PageIndex ROWS FETCH NEXT @PageSize ROWS ONLY
                END";

            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
