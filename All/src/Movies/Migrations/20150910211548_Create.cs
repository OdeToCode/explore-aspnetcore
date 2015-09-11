using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace Movies.Migrations
{
    public partial class Create : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateSequence(
                name: "DefaultSequence",
                type: "bigint",
                startWith: 1L,
                incrementBy: 10);
            migration.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column(type: "int", nullable: false),
                    Length = table.Column(type: "int", nullable: false),
                    Release = table.Column(type: "datetime2", nullable: false),
                    Title = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                });
            migration.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column(type: "int", nullable: false),
                    Body = table.Column(type: "nvarchar(max)", nullable: true),
                    MovieId = table.Column(type: "int", nullable: true),
                    User = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Movie_MovieId",
                        columns: x => x.MovieId,
                        referencedTable: "Movie",
                        referencedColumn: "Id");
                });
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.DropSequence("DefaultSequence");
            migration.DropTable("Movie");
            migration.DropTable("Review");
        }
    }
}
