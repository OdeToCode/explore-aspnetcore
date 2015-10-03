using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Movies.Services;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace Movies.Migrations
{
    [DbContext(typeof(MovieDb))]
    partial class InitialCreate
    {
        public override string Id
        {
            get { return "20150928040934_InitialCreate"; }
        }

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta7-15540")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn);

            modelBuilder.Entity("Movies.Entities.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Length");

                    b.Property<DateTime>("Release");

                    b.Property<string>("Title")
                        .Required();

                    b.Key("Id");
                });

            modelBuilder.Entity("Movies.Entities.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<int?>("MovieId");

                    b.Property<string>("User");

                    b.Key("Id");
                });

            modelBuilder.Entity("Movies.Entities.Review", b =>
                {
                    b.Reference("Movies.Entities.Movie")
                        .InverseCollection()
                        .ForeignKey("MovieId");
                });
        }
    }
}
